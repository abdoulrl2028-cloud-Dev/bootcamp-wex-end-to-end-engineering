"""
Script para criar e gerenciar instâncias EC2 na AWS.
"""

import boto3
import json
from botocore.exceptions import ClientError


class EC2Manager:
    def __init__(self, region='us-east-1'):
        """
        Inicializa o gerenciador EC2.
        
        Args:
            region (str): Região AWS
        """
        self.ec2_client = boto3.client('ec2', region_name=region)
        self.ec2_resource = boto3.resource('ec2', region_name=region)

    def create_instance(self, image_id, instance_type='t2.micro', 
                       key_name=None, security_groups=None, 
                       tag_name=None, user_data=None):
        """
        Cria uma nova instância EC2.
        
        Args:
            image_id (str): ID da AMI
            instance_type (str): Tipo de instância
            key_name (str): Nome do par de chaves
            security_groups (list): Lista de grupos de segurança
            tag_name (str): Nome da tag para a instância
            user_data (str): Script de inicialização
            
        Returns:
            str: ID da instância criada
        """
        try:
            params = {
                'ImageId': image_id,
                'MinCount': 1,
                'MaxCount': 1,
                'InstanceType': instance_type,
            }
            
            if key_name:
                params['KeyName'] = key_name
            
            if security_groups:
                params['SecurityGroups'] = security_groups
            
            if user_data:
                params['UserData'] = user_data
            
            response = self.ec2_client.run_instances(**params)
            instance_id = response['Instances'][0]['InstanceId']
            
            # Adicionar tag à instância
            if tag_name:
                self.ec2_client.create_tags(
                    Resources=[instance_id],
                    Tags=[{'Key': 'Name', 'Value': tag_name}]
                )
                print(f"✓ Tag adicionada: {tag_name}")
            
            print(f"✓ Instância criada: {instance_id}")
            return instance_id
            
        except ClientError as e:
            print(f"✗ Erro ao criar instância: {e}")
            return None

    def list_instances(self):
        """
        Lista todas as instâncias EC2.
        
        Returns:
            list: Lista de instâncias
        """
        try:
            response = self.ec2_client.describe_instances()
            instances = []
            
            for reservation in response['Reservations']:
                for instance in reservation['Instances']:
                    instances.append({
                        'InstanceId': instance['InstanceId'],
                        'InstanceType': instance['InstanceType'],
                        'State': instance['State']['Name'],
                        'PublicIpAddress': instance.get('PublicIpAddress', 'N/A'),
                        'LaunchTime': str(instance['LaunchTime'])
                    })
            
            return instances
            
        except ClientError as e:
            print(f"✗ Erro ao listar instâncias: {e}")
            return []

    def get_instance_details(self, instance_id):
        """
        Obtém detalhes de uma instância.
        
        Args:
            instance_id (str): ID da instância
            
        Returns:
            dict: Detalhes da instância
        """
        try:
            instance = self.ec2_resource.Instance(instance_id)
            return {
                'InstanceId': instance.id,
                'InstanceType': instance.instance_type,
                'State': instance.state['Name'],
                'PublicIpAddress': instance.public_ip_address,
                'PrivateIpAddress': instance.private_ip_address,
                'LaunchTime': str(instance.launch_time),
                'SecurityGroups': instance.security_groups,
                'Tags': instance.tags or []
            }
        except ClientError as e:
            print(f"✗ Erro ao obter detalhes: {e}")
            return None

    def start_instance(self, instance_id):
        """Inicia uma instância parada."""
        try:
            self.ec2_client.start_instances(InstanceIds=[instance_id])
            print(f"✓ Instância iniciada: {instance_id}")
        except ClientError as e:
            print(f"✗ Erro ao iniciar instância: {e}")

    def stop_instance(self, instance_id):
        """Para uma instância em execução."""
        try:
            self.ec2_client.stop_instances(InstanceIds=[instance_id])
            print(f"✓ Instância parada: {instance_id}")
        except ClientError as e:
            print(f"✗ Erro ao parar instância: {e}")

    def terminate_instance(self, instance_id):
        """Encerra uma instância."""
        try:
            self.ec2_client.terminate_instances(InstanceIds=[instance_id])
            print(f"✓ Instância encerrada: {instance_id}")
        except ClientError as e:
            print(f"✗ Erro ao encerrar instância: {e}")

    def allocate_elastic_ip(self):
        """
        Aloca um Elastic IP.
        
        Returns:
            str: Endereço IP alocado
        """
        try:
            response = self.ec2_client.allocate_address(Domain='vpc')
            allocation_id = response['AllocationId']
            public_ip = response['PublicIp']
            print(f"✓ Elastic IP alocado: {public_ip}")
            return allocation_id
        except ClientError as e:
            print(f"✗ Erro ao alocar Elastic IP: {e}")
            return None

    def associate_elastic_ip(self, allocation_id, instance_id):
        """
        Associa um Elastic IP a uma instância.
        
        Args:
            allocation_id (str): ID da alocação
            instance_id (str): ID da instância
        """
        try:
            self.ec2_client.associate_address(
                InstanceId=instance_id,
                AllocationId=allocation_id
            )
            print(f"✓ Elastic IP associado à instância {instance_id}")
        except ClientError as e:
            print(f"✗ Erro ao associar Elastic IP: {e}")


# Exemplo de uso
if __name__ == '__main__':
    manager = EC2Manager(region='us-east-1')
    
    # Listar instâncias existentes
    print("=== Listando Instâncias ===")
    instances = manager.list_instances()
    for instance in instances:
        print(json.dumps(instance, indent=2))
    
    # Criar uma nova instância (comentado por padrão)
    # print("\n=== Criando Instância ===")
    # instance_id = manager.create_instance(
    #     image_id='ami-0c55b159cbfafe1f0',
    #     instance_type='t2.micro',
    #     tag_name='minha-instancia'
    # )
