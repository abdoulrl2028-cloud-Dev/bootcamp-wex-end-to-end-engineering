"""
Script para gerenciar VPC e subnets na AWS.
"""

import boto3
from botocore.exceptions import ClientError


class VPCManager:
    def __init__(self, region='us-east-1'):
        """Inicializa o gerenciador VPC."""
        self.ec2_client = boto3.client('ec2', region_name=region)
        self.ec2_resource = boto3.resource('ec2', region_name=region)

    def create_vpc(self, cidr_block, tag_name=None):
        """
        Cria uma nova VPC.
        
        Args:
            cidr_block (str): Bloco CIDR (ex: 10.0.0.0/16)
            tag_name (str): Nome da VPC
            
        Returns:
            str: ID da VPC criada
        """
        try:
            response = self.ec2_client.create_vpc(CidrBlock=cidr_block)
            vpc_id = response['Vpc']['VpcId']
            
            if tag_name:
                self.ec2_client.create_tags(
                    Resources=[vpc_id],
                    Tags=[{'Key': 'Name', 'Value': tag_name}]
                )
                print(f"✓ VPC criada: {vpc_id} ({tag_name})")
            else:
                print(f"✓ VPC criada: {vpc_id}")
            
            return vpc_id
        except ClientError as e:
            print(f"✗ Erro ao criar VPC: {e}")
            return None

    def create_subnet(self, vpc_id, cidr_block, availability_zone=None, tag_name=None):
        """
        Cria uma subnet.
        
        Args:
            vpc_id (str): ID da VPC
            cidr_block (str): Bloco CIDR da subnet
            availability_zone (str): Zona de disponibilidade
            tag_name (str): Nome da subnet
            
        Returns:
            str: ID da subnet criada
        """
        try:
            params = {
                'VpcId': vpc_id,
                'CidrBlock': cidr_block
            }
            if availability_zone:
                params['AvailabilityZone'] = availability_zone
            
            response = self.ec2_client.create_subnet(**params)
            subnet_id = response['Subnet']['SubnetId']
            
            if tag_name:
                self.ec2_client.create_tags(
                    Resources=[subnet_id],
                    Tags=[{'Key': 'Name', 'Value': tag_name}]
                )
                print(f"✓ Subnet criada: {subnet_id} ({tag_name})")
            else:
                print(f"✓ Subnet criada: {subnet_id}")
            
            return subnet_id
        except ClientError as e:
            print(f"✗ Erro ao criar subnet: {e}")
            return None

    def create_internet_gateway(self, vpc_id, tag_name=None):
        """
        Cria um Internet Gateway.
        
        Args:
            vpc_id (str): ID da VPC
            tag_name (str): Nome do gateway
            
        Returns:
            str: ID do gateway criado
        """
        try:
            response = self.ec2_client.create_internet_gateway()
            igw_id = response['InternetGateway']['InternetGatewayId']
            
            self.ec2_client.attach_internet_gateway(
                InternetGatewayId=igw_id,
                VpcId=vpc_id
            )
            
            if tag_name:
                self.ec2_client.create_tags(
                    Resources=[igw_id],
                    Tags=[{'Key': 'Name', 'Value': tag_name}]
                )
                print(f"✓ Internet Gateway criado: {igw_id} ({tag_name})")
            else:
                print(f"✓ Internet Gateway criado: {igw_id}")
            
            return igw_id
        except ClientError as e:
            print(f"✗ Erro ao criar Internet Gateway: {e}")
            return None

    def create_route_table(self, vpc_id, tag_name=None):
        """
        Cria uma Route Table.
        
        Args:
            vpc_id (str): ID da VPC
            tag_name (str): Nome da route table
            
        Returns:
            str: ID da route table criada
        """
        try:
            response = self.ec2_client.create_route_table(VpcId=vpc_id)
            rt_id = response['RouteTable']['RouteTableId']
            
            if tag_name:
                self.ec2_client.create_tags(
                    Resources=[rt_id],
                    Tags=[{'Key': 'Name', 'Value': tag_name}]
                )
                print(f"✓ Route Table criada: {rt_id} ({tag_name})")
            else:
                print(f"✓ Route Table criada: {rt_id}")
            
            return rt_id
        except ClientError as e:
            print(f"✗ Erro ao criar Route Table: {e}")
            return None

    def add_route(self, route_table_id, destination_cidr, gateway_id=None):
        """
        Adiciona uma rota à Route Table.
        
        Args:
            route_table_id (str): ID da route table
            destination_cidr (str): CIDR de destino (ex: 0.0.0.0/0)
            gateway_id (str): ID do gateway (Internet Gateway ou NAT)
        """
        try:
            params = {
                'RouteTableId': route_table_id,
                'DestinationCidrBlock': destination_cidr,
            }
            if gateway_id:
                params['GatewayId'] = gateway_id
            
            self.ec2_client.create_route(**params)
            print(f"✓ Rota adicionada: {destination_cidr} → {gateway_id}")
        except ClientError as e:
            print(f"✗ Erro ao adicionar rota: {e}")

    def associate_route_table(self, route_table_id, subnet_id):
        """
        Associa uma Route Table a uma subnet.
        
        Args:
            route_table_id (str): ID da route table
            subnet_id (str): ID da subnet
        """
        try:
            self.ec2_client.associate_route_table(
                RouteTableId=route_table_id,
                SubnetId=subnet_id
            )
            print(f"✓ Route Table associada à subnet")
        except ClientError as e:
            print(f"✗ Erro ao associar Route Table: {e}")

    def enable_public_ip_on_subnet(self, subnet_id):
        """
        Habilita atribuição automática de IP público na subnet.
        
        Args:
            subnet_id (str): ID da subnet
        """
        try:
            self.ec2_client.modify_subnet_attribute(
                SubnetId=subnet_id,
                MapPublicIpOnLaunch={'Value': True}
            )
            print(f"✓ IP público automático habilitado na subnet")
        except ClientError as e:
            print(f"✗ Erro ao habilitar IP público: {e}")

    def list_vpcs(self):
        """Lista todas as VPCs."""
        try:
            response = self.ec2_client.describe_vpcs()
            vpcs = []
            for vpc in response['Vpcs']:
                vpcs.append({
                    'VpcId': vpc['VpcId'],
                    'CidrBlock': vpc['CidrBlock'],
                    'State': vpc['State'],
                    'IsDefault': vpc['IsDefault']
                })
            return vpcs
        except ClientError as e:
            print(f"✗ Erro ao listar VPCs: {e}")
            return []

    def list_subnets(self, vpc_id=None):
        """
        Lista subnets de uma VPC.
        
        Args:
            vpc_id (str): ID da VPC (opcional)
            
        Returns:
            list: Lista de subnets
        """
        try:
            params = {}
            if vpc_id:
                params['Filters'] = [{'Name': 'vpc-id', 'Values': [vpc_id]}]
            
            response = self.ec2_client.describe_subnets(**params)
            subnets = []
            for subnet in response['Subnets']:
                subnets.append({
                    'SubnetId': subnet['SubnetId'],
                    'VpcId': subnet['VpcId'],
                    'CidrBlock': subnet['CidrBlock'],
                    'AvailabilityZone': subnet['AvailabilityZone']
                })
            return subnets
        except ClientError as e:
            print(f"✗ Erro ao listar subnets: {e}")
            return []


# Exemplo de uso
if __name__ == '__main__':
    manager = VPCManager(region='us-east-1')
    
    # Criar VPC
    print("=== Criando Infraestrutura de Rede ===")
    vpc_id = manager.create_vpc('10.0.0.0/16', 'minha-vpc')
    
    if vpc_id:
        # Criar subnets
        subnet_pub_id = manager.create_subnet(vpc_id, '10.0.1.0/24', tag_name='subnet-publica')
        subnet_priv_id = manager.create_subnet(vpc_id, '10.0.2.0/24', tag_name='subnet-privada')
        
        # Criar Internet Gateway
        igw_id = manager.create_internet_gateway(vpc_id, 'meu-igw')
        
        # Criar Route Table pública
        rt_pub_id = manager.create_route_table(vpc_id, 'rt-publica')
        
        if igw_id and rt_pub_id:
            # Adicionar rota padrão para Internet
            manager.add_route(rt_pub_id, '0.0.0.0/0', igw_id)
            
            # Associar à subnet pública
            manager.associate_route_table(rt_pub_id, subnet_pub_id)
            
            # Habilitar IP público automático
            manager.enable_public_ip_on_subnet(subnet_pub_id)
    
    # Listar VPCs
    print("\n=== VPCs Criadas ===")
    vpcs = manager.list_vpcs()
    for vpc in vpcs:
        print(f"- {vpc['VpcId']}: {vpc['CidrBlock']}")
