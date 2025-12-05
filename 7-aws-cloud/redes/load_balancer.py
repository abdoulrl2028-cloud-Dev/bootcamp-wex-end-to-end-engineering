"""
Script para gerenciar Load Balancers na AWS.
"""

import boto3
from botocore.exceptions import ClientError


class LoadBalancerManager:
    def __init__(self, region='us-east-1'):
        """Inicializa o gerenciador de Load Balancers."""
        self.elb_client = boto3.client('elbv2', region_name=region)

    def create_alb(self, name, subnets, security_groups=None, scheme='internet-facing'):
        """
        Cria um Application Load Balancer.
        
        Args:
            name (str): Nome do ALB
            subnets (list): Lista de IDs de subnets
            security_groups (list): Lista de IDs de security groups
            scheme (str): 'internet-facing' ou 'internal'
            
        Returns:
            str: ARN do ALB criado
        """
        try:
            params = {
                'Name': name,
                'Subnets': subnets,
                'Scheme': scheme,
                'Type': 'application'
            }
            if security_groups:
                params['SecurityGroups'] = security_groups
            
            response = self.elb_client.create_load_balancer(**params)
            alb_arn = response['LoadBalancers'][0]['LoadBalancerArn']
            alb_dns = response['LoadBalancers'][0]['DNSName']
            
            print(f"✓ ALB criado: {name}")
            print(f"  ARN: {alb_arn}")
            print(f"  DNS: {alb_dns}")
            
            return alb_arn
        except ClientError as e:
            print(f"✗ Erro ao criar ALB: {e}")
            return None

    def create_target_group(self, name, protocol='HTTP', port=80, vpc_id=None):
        """
        Cria um Target Group.
        
        Args:
            name (str): Nome do target group
            protocol (str): Protocolo (HTTP, HTTPS, TCP, TLS)
            port (int): Porta
            vpc_id (str): ID da VPC
            
        Returns:
            str: ARN do target group
        """
        try:
            params = {
                'Name': name,
                'Protocol': protocol,
                'Port': port,
            }
            if vpc_id:
                params['VpcId'] = vpc_id
            
            response = self.elb_client.create_target_group(**params)
            tg_arn = response['TargetGroups'][0]['TargetGroupArn']
            
            print(f"✓ Target Group criado: {name}")
            print(f"  ARN: {tg_arn}")
            
            return tg_arn
        except ClientError as e:
            print(f"✗ Erro ao criar Target Group: {e}")
            return None

    def register_targets(self, target_group_arn, instance_ids, port=80):
        """
        Registra instâncias em um Target Group.
        
        Args:
            target_group_arn (str): ARN do target group
            instance_ids (list): Lista de IDs de instâncias
            port (int): Porta
        """
        try:
            targets = [{'Id': instance_id, 'Port': port} for instance_id in instance_ids]
            
            self.elb_client.register_targets(
                TargetGroupArn=target_group_arn,
                Targets=targets
            )
            print(f"✓ {len(instance_ids)} instância(s) registrada(s)")
        except ClientError as e:
            print(f"✗ Erro ao registrar alvos: {e}")

    def create_listener(self, alb_arn, target_group_arn, protocol='HTTP', port=80):
        """
        Cria um listener para o ALB.
        
        Args:
            alb_arn (str): ARN do ALB
            target_group_arn (str): ARN do target group
            protocol (str): Protocolo
            port (int): Porta
        """
        try:
            response = self.elb_client.create_listener(
                LoadBalancerArn=alb_arn,
                Protocol=protocol,
                Port=port,
                DefaultActions=[{
                    'Type': 'forward',
                    'TargetGroupArn': target_group_arn
                }]
            )
            print(f"✓ Listener criado: {protocol}:{port}")
        except ClientError as e:
            print(f"✗ Erro ao criar listener: {e}")

    def describe_load_balancers(self):
        """
        Lista todos os load balancers.
        
        Returns:
            list: Lista de load balancers
        """
        try:
            response = self.elb_client.describe_load_balancers()
            albs = []
            for alb in response['LoadBalancers']:
                albs.append({
                    'Name': alb['LoadBalancerName'],
                    'DNSName': alb['DNSName'],
                    'State': alb['State']['Code'],
                    'CreatedTime': str(alb['CreatedTime'])
                })
            return albs
        except ClientError as e:
            print(f"✗ Erro ao descrever ALBs: {e}")
            return []

    def describe_target_groups(self):
        """
        Lista todos os target groups.
        
        Returns:
            list: Lista de target groups
        """
        try:
            response = self.elb_client.describe_target_groups()
            tgs = []
            for tg in response['TargetGroups']:
                tgs.append({
                    'Name': tg['TargetGroupName'],
                    'Protocol': tg['Protocol'],
                    'Port': tg['Port'],
                    'HealthCheckEnabled': tg['HealthCheckEnabled']
                })
            return tgs
        except ClientError as e:
            print(f"✗ Erro ao descrever Target Groups: {e}")
            return []

    def configure_health_check(self, target_group_arn, protocol='HTTP', 
                              path='/', interval=30, timeout=5, healthy_threshold=2):
        """
        Configura health check para um Target Group.
        
        Args:
            target_group_arn (str): ARN do target group
            protocol (str): Protocolo
            path (str): Caminho para checar
            interval (int): Intervalo em segundos
            timeout (int): Timeout em segundos
            healthy_threshold (int): Contagem de sucesso
        """
        try:
            self.elb_client.modify_target_group(
                TargetGroupArn=target_group_arn,
                HealthCheckProtocol=protocol,
                HealthCheckPath=path,
                HealthCheckIntervalSeconds=interval,
                HealthCheckTimeoutSeconds=timeout,
                HealthyThresholdCount=healthy_threshold
            )
            print(f"✓ Health check configurado")
        except ClientError as e:
            print(f"✗ Erro ao configurar health check: {e}")

    def delete_load_balancer(self, alb_arn):
        """
        Deleta um load balancer.
        
        Args:
            alb_arn (str): ARN do ALB
        """
        try:
            self.elb_client.delete_load_balancer(LoadBalancerArn=alb_arn)
            print(f"✓ Load balancer deletado")
        except ClientError as e:
            print(f"✗ Erro ao deletar load balancer: {e}")


# Exemplo de uso
if __name__ == '__main__':
    manager = LoadBalancerManager(region='us-east-1')
    
    # Listar ALBs existentes
    print("=== Application Load Balancers ===")
    albs = manager.describe_load_balancers()
    for alb in albs:
        print(f"- {alb['Name']}: {alb['DNSName']}")
    
    # Exemplo de criação (comentado)
    # vpc_id = 'vpc-xxxxx'
    # subnet_ids = ['subnet-xxxxx', 'subnet-yyyyy']
    # sg_ids = ['sg-xxxxx']
    # 
    # alb_arn = manager.create_alb('meu-alb', subnet_ids, sg_ids)
    # tg_arn = manager.create_target_group('meu-tg', vpc_id=vpc_id)
    # 
    # if alb_arn and tg_arn:
    #     manager.create_listener(alb_arn, tg_arn)
    #     manager.configure_health_check(tg_arn)
