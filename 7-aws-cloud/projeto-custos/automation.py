"""
Script para automação de economia de custos.
"""

import boto3
import schedule
import time
from datetime import datetime
from botocore.exceptions import ClientError


class CostAutomation:
    def __init__(self, region='us-east-1'):
        """Inicializa o automador de custos."""
        self.ec2_client = boto3.client('ec2', region_name=region)
        self.cloudwatch = boto3.client('cloudwatch', region_name=region)

    def stop_idle_instances(self, cpu_threshold=5, duration_hours=2):
        """
        Para instâncias com CPU baixa.
        
        Args:
            cpu_threshold (float): Percentual de CPU considerado ocioso
            duration_hours (int): Duração da métrica em horas
        """
        try:
            response = self.ec2_client.describe_instances(
                Filters=[{'Name': 'instance-state-name', 'Values': ['running']}]
            )
            
            instances_to_stop = []
            
            for reservation in response['Reservations']:
                for instance in reservation['Instances']:
                    instance_id = instance['InstanceId']
                    
                    # Obter métrica de CPU
                    metrics = self.cloudwatch.get_metric_statistics(
                        Namespace='AWS/EC2',
                        MetricName='CPUUtilization',
                        Dimensions=[
                            {'Name': 'InstanceId', 'Value': instance_id}
                        ],
                        StartTime=datetime.now() - timedelta(hours=duration_hours),
                        EndTime=datetime.now(),
                        Period=300,
                        Statistics=['Average']
                    )
                    
                    if metrics['Datapoints']:
                        avg_cpu = sum(d['Average'] for d in metrics['Datapoints']) / len(metrics['Datapoints'])
                        
                        if avg_cpu < cpu_threshold:
                            instances_to_stop.append({
                                'InstanceId': instance_id,
                                'AvgCPU': avg_cpu,
                                'InstanceType': instance['InstanceType']
                            })
            
            # Para instâncias ociosas
            if instances_to_stop:
                instance_ids = [i['InstanceId'] for i in instances_to_stop]
                self.ec2_client.stop_instances(InstanceIds=instance_ids)
                print(f"✓ {len(instance_ids)} instância(s) parada(s)")
                
                for instance in instances_to_stop:
                    print(f"  - {instance['InstanceId']}: CPU médio {instance['AvgCPU']:.2f}%")
            else:
                print("ℹ Nenhuma instância ociosa encontrada")
            
            return instances_to_stop
        except ClientError as e:
            print(f"✗ Erro ao parar instâncias: {e}")
            return []

    def delete_old_snapshots(self, retention_days=30):
        """
        Deleta snapshots mais antigos que o período de retenção.
        
        Args:
            retention_days (int): Dias de retenção
        """
        try:
            from datetime import timedelta
            
            response = self.ec2_client.describe_snapshots(OwnerIds=['self'])
            threshold_date = datetime.now(response['Snapshots'][0]['StartTime'].tzinfo) - timedelta(days=retention_days)
            
            snapshots_to_delete = []
            
            for snapshot in response['Snapshots']:
                if snapshot['StartTime'] < threshold_date:
                    snapshots_to_delete.append(snapshot['SnapshotId'])
            
            deleted_count = 0
            for snapshot_id in snapshots_to_delete:
                try:
                    self.ec2_client.delete_snapshot(SnapshotId=snapshot_id)
                    deleted_count += 1
                except ClientError as e:
                    print(f"✗ Erro ao deletar snapshot {snapshot_id}: {e}")
            
            if deleted_count > 0:
                print(f"✓ {deleted_count} snapshot(s) deletado(s)")
            else:
                print("ℹ Nenhum snapshot antigo encontrado")
            
            return snapshots_to_delete
        except ClientError as e:
            print(f"✗ Erro ao listar snapshots: {e}")
            return []

    def release_unused_elastic_ips(self):
        """Libera Elastic IPs não associados."""
        try:
            response = self.ec2_client.describe_addresses(
                Filters=[{'Name': 'instance-id', 'Values': ['']}]
            )
            
            eips_to_release = []
            
            for eip in response['Addresses']:
                if 'InstanceId' not in eip or not eip['InstanceId']:
                    eips_to_release.append({
                        'PublicIp': eip['PublicIp'],
                        'AllocationId': eip.get('AllocationId')
                    })
            
            released_count = 0
            for eip in eips_to_release:
                try:
                    self.ec2_client.release_address(
                        AllocationId=eip['AllocationId']
                    )
                    released_count += 1
                except ClientError as e:
                    print(f"✗ Erro ao liberar {eip['PublicIp']}: {e}")
            
            if released_count > 0:
                print(f"✓ {released_count} Elastic IP(s) liberado(s)")
            else:
                print("ℹ Nenhum Elastic IP não utilizado encontrado")
            
            return eips_to_release
        except ClientError as e:
            print(f"✗ Erro ao listar Elastic IPs: {e}")
            return []

    def schedule_automation(self):
        """Agenda tarefas de automação."""
        print("🔄 Agendador de Automação Iniciado\n")
        
        # Agendar limpeza diária
        schedule.every().day.at("02:00").do(self.delete_old_snapshots, retention_days=30)
        
        # Agendar liberação de Elastic IPs diariamente
        schedule.every().day.at("03:00").do(self.release_unused_elastic_ips)
        
        # Agendar verificação de instâncias ociosas a cada hora
        schedule.every().hour.do(self.stop_idle_instances, cpu_threshold=5)
        
        print("Tarefas agendadas:")
        print("  • 02:00 - Deletar snapshots antigos")
        print("  • 03:00 - Liberar Elastic IPs não utilizados")
        print("  • A cada hora - Verificar instâncias ociosas\n")
        
        # Executar scheduler em loop
        while True:
            schedule.run_pending()
            time.sleep(60)


# Exemplo de uso
if __name__ == '__main__':
    from datetime import timedelta
    
    automation = CostAutomation(region='us-east-1')
    
    print("=== Automação de Custos AWS ===\n")
    
    # Executar uma vez
    print("Executando verificações iniciais...\n")
    
    print("1. Verificando instâncias ociosas:")
    automation.stop_idle_instances(cpu_threshold=5, duration_hours=2)
    
    print("\n2. Liberando Elastic IPs não utilizados:")
    automation.release_unused_elastic_ips()
    
    print("\n3. Deletando snapshots antigos (>30 dias):")
    automation.delete_old_snapshots(retention_days=30)
    
    # Para ativar o agendador, descomente:
    # print("\n4. Iniciando agendador...")
    # automation.schedule_automation()
