"""
Script para gerenciar Security Groups em EC2.
"""

import boto3
from botocore.exceptions import ClientError


class SecurityGroupManager:
    def __init__(self, region='us-east-1'):
        """Inicializa o gerenciador de Security Groups."""
        self.ec2_client = boto3.client('ec2', region_name=region)

    def create_security_group(self, group_name, description, vpc_id=None):
        """
        Cria um novo Security Group.
        
        Args:
            group_name (str): Nome do grupo
            description (str): Descrição do grupo
            vpc_id (str): ID da VPC (opcional)
            
        Returns:
            str: ID do grupo criado
        """
        try:
            params = {
                'GroupName': group_name,
                'Description': description
            }
            if vpc_id:
                params['VpcId'] = vpc_id
            
            response = self.ec2_client.create_security_group(**params)
            group_id = response['GroupId']
            print(f"✓ Security Group criado: {group_id}")
            return group_id
        except ClientError as e:
            print(f"✗ Erro ao criar Security Group: {e}")
            return None

    def authorize_ingress_rule(self, group_id, protocol, from_port, to_port, 
                              cidr_ip=None, source_group_id=None):
        """
        Adiciona uma regra de entrada (ingress) a um Security Group.
        
        Args:
            group_id (str): ID do grupo
            protocol (str): Protocolo (tcp, udp, icmp, -1 para todos)
            from_port (int): Porta inicial
            to_port (int): Porta final
            cidr_ip (str): CIDR IP range
            source_group_id (str): ID do grupo de segurança de origem
        """
        try:
            ip_permissions = [{
                'IpProtocol': protocol,
                'FromPort': from_port,
                'ToPort': to_port,
            }]
            
            if cidr_ip:
                ip_permissions[0]['IpRanges'] = [{'CidrIp': cidr_ip, 'Description': 'Acesso'}]
            
            if source_group_id:
                ip_permissions[0]['UserIdGroupPairs'] = [{'GroupId': source_group_id}]
            
            self.ec2_client.authorize_security_group_ingress(
                GroupId=group_id,
                IpPermissions=ip_permissions
            )
            print(f"✓ Regra de ingresso adicionada ao grupo {group_id}")
        except ClientError as e:
            print(f"✗ Erro ao adicionar regra: {e}")

    def authorize_egress_rule(self, group_id, protocol, from_port, to_port, cidr_ip='0.0.0.0/0'):
        """
        Adiciona uma regra de saída (egress) a um Security Group.
        
        Args:
            group_id (str): ID do grupo
            protocol (str): Protocolo
            from_port (int): Porta inicial
            to_port (int): Porta final
            cidr_ip (str): CIDR IP range
        """
        try:
            self.ec2_client.authorize_security_group_egress(
                GroupId=group_id,
                IpPermissions=[{
                    'IpProtocol': protocol,
                    'FromPort': from_port,
                    'ToPort': to_port,
                    'IpRanges': [{'CidrIp': cidr_ip}]
                }]
            )
            print(f"✓ Regra de egresso adicionada ao grupo {group_id}")
        except ClientError as e:
            print(f"✗ Erro ao adicionar regra de egresso: {e}")

    def revoke_ingress_rule(self, group_id, protocol, from_port, to_port, cidr_ip):
        """Remove uma regra de entrada."""
        try:
            self.ec2_client.revoke_security_group_ingress(
                GroupId=group_id,
                IpPermissions=[{
                    'IpProtocol': protocol,
                    'FromPort': from_port,
                    'ToPort': to_port,
                    'IpRanges': [{'CidrIp': cidr_ip}]
                }]
            )
            print(f"✓ Regra removida do grupo {group_id}")
        except ClientError as e:
            print(f"✗ Erro ao remover regra: {e}")

    def describe_security_groups(self):
        """Lista todos os Security Groups."""
        try:
            response = self.ec2_client.describe_security_groups()
            return response['SecurityGroups']
        except ClientError as e:
            print(f"✗ Erro ao listar Security Groups: {e}")
            return []

    def delete_security_group(self, group_id):
        """Deleta um Security Group."""
        try:
            self.ec2_client.delete_security_group(GroupId=group_id)
            print(f"✓ Security Group deletado: {group_id}")
        except ClientError as e:
            print(f"✗ Erro ao deletar Security Group: {e}")


# Exemplo de uso
if __name__ == '__main__':
    sg_manager = SecurityGroupManager(region='us-east-1')
    
    # Criar um Security Group
    group_id = sg_manager.create_security_group(
        'web-server',
        'Security group para servidor web'
    )
    
    if group_id:
        # Adicionar regras
        sg_manager.authorize_ingress_rule(group_id, 'tcp', 80, 80, '0.0.0.0/0')
        sg_manager.authorize_ingress_rule(group_id, 'tcp', 443, 443, '0.0.0.0/0')
        sg_manager.authorize_ingress_rule(group_id, 'tcp', 22, 22, '0.0.0.0/0')
        
        # Listar grupos
        print("\n=== Security Groups ===")
        groups = sg_manager.describe_security_groups()
        for group in groups[:3]:
            print(f"ID: {group['GroupId']}, Nome: {group['GroupName']}")
