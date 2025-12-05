"""
Script para analisar e otimizar custos na AWS.
"""

import boto3
from datetime import datetime, timedelta
from botocore.exceptions import ClientError


class CostExplorer:
    def __init__(self, region='us-east-1'):
        """Inicializa o analisador de custos."""
        self.ce_client = boto3.client('ce', region_name=region)
        self.ec2_client = boto3.client('ec2', region_name=region)

    def get_daily_costs(self, days=30):
        """
        Obtém custos diários dos últimos N dias.
        
        Args:
            days (int): Número de dias para análise
            
        Returns:
            list: Custos por dia
        """
        try:
            start_date = (datetime.now() - timedelta(days=days)).strftime('%Y-%m-%d')
            end_date = datetime.now().strftime('%Y-%m-%d')
            
            response = self.ce_client.get_cost_and_usage(
                TimePeriod={
                    'Start': start_date,
                    'End': end_date
                },
                Granularity='DAILY',
                Metrics=['UnblendedCost'],
                GroupBy=[
                    {'Type': 'DIMENSION', 'Key': 'SERVICE'}
                ]
            )
            
            costs = []
            for result in response['ResultsByTime']:
                date = result['TimePeriod']['Start']
                for group in result['Groups']:
                    service = group['Keys'][0]
                    amount = float(group['Metrics']['UnblendedCost']['Amount'])
                    costs.append({
                        'date': date,
                        'service': service,
                        'cost': amount
                    })
            
            return costs
        except ClientError as e:
            print(f"✗ Erro ao obter custos: {e}")
            return []

    def get_service_costs(self, start_date, end_date):
        """
        Obtém custos por serviço em um período.
        
        Args:
            start_date (str): Data inicial (YYYY-MM-DD)
            end_date (str): Data final (YYYY-MM-DD)
            
        Returns:
            dict: Custos por serviço
        """
        try:
            response = self.ce_client.get_cost_and_usage(
                TimePeriod={
                    'Start': start_date,
                    'End': end_date
                },
                Granularity='MONTHLY',
                Metrics=['UnblendedCost'],
                GroupBy=[
                    {'Type': 'DIMENSION', 'Key': 'SERVICE'}
                ]
            )
            
            service_costs = {}
            for result in response['ResultsByTime']:
                for group in result['Groups']:
                    service = group['Keys'][0]
                    amount = float(group['Metrics']['UnblendedCost']['Amount'])
                    service_costs[service] = service_costs.get(service, 0) + amount
            
            return service_costs
        except ClientError as e:
            print(f"✗ Erro ao obter custos por serviço: {e}")
            return {}

    def get_ec2_costs(self, start_date, end_date):
        """
        Obtém custos detalhados de EC2.
        
        Args:
            start_date (str): Data inicial
            end_date (str): Data final
            
        Returns:
            dict: Custos de EC2
        """
        try:
            response = self.ce_client.get_cost_and_usage(
                TimePeriod={
                    'Start': start_date,
                    'End': end_date
                },
                Granularity='MONTHLY',
                Metrics=['UnblendedCost'],
                Filter={
                    'Dimensions': {
                        'Key': 'SERVICE',
                        'Values': ['Amazon Elastic Compute Cloud - Compute']
                    }
                },
                GroupBy=[
                    {'Type': 'DIMENSION', 'Key': 'INSTANCE_TYPE'},
                    {'Type': 'DIMENSION', 'Key': 'PURCHASE_TYPE'}
                ]
            )
            
            ec2_costs = {}
            for result in response['ResultsByTime']:
                for group in result['Groups']:
                    key = f"{group['Keys'][0]} ({group['Keys'][1]})"
                    amount = float(group['Metrics']['UnblendedCost']['Amount'])
                    ec2_costs[key] = ec2_costs.get(key, 0) + amount
            
            return ec2_costs
        except ClientError as e:
            print(f"✗ Erro ao obter custos de EC2: {e}")
            return {}

    def find_idle_instances(self):
        """
        Identifica instâncias EC2 ociosas (paradas).
        
        Returns:
            list: Lista de instâncias ociosas
        """
        try:
            response = self.ec2_client.describe_instances(
                Filters=[{'Name': 'instance-state-name', 'Values': ['stopped']}]
            )
            
            idle_instances = []
            for reservation in response['Reservations']:
                for instance in reservation['Instances']:
                    idle_instances.append({
                        'InstanceId': instance['InstanceId'],
                        'InstanceType': instance['InstanceType'],
                        'State': instance['State']['Name'],
                        'LaunchTime': str(instance['LaunchTime']),
                        'Tags': instance.get('Tags', [])
                    })
            
            return idle_instances
        except ClientError as e:
            print(f"✗ Erro ao buscar instâncias ociosas: {e}")
            return []

    def find_unused_volumes(self):
        """
        Identifica volumes EBS não conectados.
        
        Returns:
            list: Lista de volumes não utilizados
        """
        try:
            response = self.ec2_client.describe_volumes(
                Filters=[{'Name': 'status', 'Values': ['available']}]
            )
            
            unused_volumes = []
            for volume in response['Volumes']:
                unused_volumes.append({
                    'VolumeId': volume['VolumeId'],
                    'Size': volume['Size'],
                    'VolumeType': volume['VolumeType'],
                    'AvailabilityZone': volume['AvailabilityZone'],
                    'CreateTime': str(volume['CreateTime'])
                })
            
            return unused_volumes
        except ClientError as e:
            print(f"✗ Erro ao buscar volumes não utilizados: {e}")
            return []

    def get_recommendations(self):
        """
        Gera recomendações de otimização de custos.
        
        Returns:
            list: Lista de recomendações
        """
        recommendations = []
        
        # Verificar instâncias ociosas
        idle_instances = self.find_idle_instances()
        if idle_instances:
            recommendations.append({
                'tipo': 'EC2 Ocioso',
                'quantidade': len(idle_instances),
                'economia_potencial': f"${len(idle_instances) * 2.5}/mês",
                'acao': 'Considere terminar instâncias paradas não utilizadas'
            })
        
        # Verificar volumes não utilizados
        unused_volumes = self.find_unused_volumes()
        if unused_volumes:
            total_gb = sum(v['Size'] for v in unused_volumes)
            recommendations.append({
                'tipo': 'EBS Não Utilizado',
                'quantidade': len(unused_volumes),
                'economia_potencial': f"${total_gb * 0.1}/mês",
                'acao': 'Remova volumes EBS não conectados'
            })
        
        recommendations.append({
            'tipo': 'Instâncias Reservadas',
            'quantidade': 'N/A',
            'economia_potencial': 'Até 70%',
            'acao': 'Considere usar Instâncias Reservadas para cargas previsíveis'
        })
        
        recommendations.append({
            'tipo': 'Spot Instances',
            'quantidade': 'N/A',
            'economia_potencial': 'Até 90%',
            'acao': 'Use Spot Instances para cargas tolerantes a falhas'
        })
        
        return recommendations

    def generate_cost_report(self, filename='cost_report.txt'):
        """
        Gera um relatório de custos.
        
        Args:
            filename (str): Nome do arquivo de saída
        """
        start_date = (datetime.now() - timedelta(days=30)).strftime('%Y-%m-%d')
        end_date = datetime.now().strftime('%Y-%m-%d')
        
        with open(filename, 'w', encoding='utf-8') as f:
            f.write("=" * 60 + "\n")
            f.write("RELATÓRIO DE CUSTOS AWS\n")
            f.write(f"Período: {start_date} a {end_date}\n")
            f.write("=" * 60 + "\n\n")
            
            # Custos por serviço
            f.write("CUSTOS POR SERVIÇO\n")
            f.write("-" * 60 + "\n")
            service_costs = self.get_service_costs(start_date, end_date)
            total = sum(service_costs.values())
            
            for service, cost in sorted(service_costs.items(), key=lambda x: x[1], reverse=True):
                percentage = (cost / total * 100) if total > 0 else 0
                f.write(f"{service:<50} ${cost:>10.2f} ({percentage:>5.1f}%)\n")
            
            f.write("-" * 60 + "\n")
            f.write(f"{'TOTAL':<50} ${total:>10.2f}\n\n")
            
            # Recomendações
            f.write("RECOMENDAÇÕES DE OTIMIZAÇÃO\n")
            f.write("-" * 60 + "\n")
            recommendations = self.get_recommendations()
            
            for i, rec in enumerate(recommendations, 1):
                f.write(f"\n{i}. {rec['tipo']}\n")
                f.write(f"   Economia Potencial: {rec['economia_potencial']}\n")
                f.write(f"   Ação: {rec['acao']}\n")
            
            f.write("\n" + "=" * 60 + "\n")
        
        print(f"✓ Relatório gerado: {filename}")


# Exemplo de uso
if __name__ == '__main__':
    explorer = CostExplorer(region='us-east-1')
    
    print("=== Analisador de Custos AWS ===\n")
    
    # Obter custos diários
    print("Obtendo custos diários...")
    daily_costs = explorer.get_daily_costs(days=7)
    
    if daily_costs:
        total_cost = sum(c['cost'] for c in daily_costs)
        print(f"Custo total dos últimos 7 dias: ${total_cost:.2f}\n")
    
    # Gerar relatório
    print("Gerando relatório de custos...")
    explorer.generate_cost_report()
    
    # Mostrar recomendações
    print("\nRecomendações de Otimização:")
    recommendations = explorer.get_recommendations()
    for rec in recommendations:
        print(f"  • {rec['tipo']}: {rec['economia_potencial']}")
