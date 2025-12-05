"""
Script para gerenciar instâncias RDS na AWS.
"""

import boto3
from botocore.exceptions import ClientError


class RDSManager:
    def __init__(self, region='us-east-1'):
        """Inicializa o gerenciador RDS."""
        self.rds_client = boto3.client('rds', region_name=region)

    def create_db_instance(self, engine, db_instance_identifier, 
                          master_username='admin', master_user_password='',
                          db_instance_class='db.t3.micro', allocated_storage=20,
                          engine_version=None, multi_az=False, publicly_accessible=False):
        """
        Cria uma instância RDS.
        
        Args:
            engine (str): 'mysql', 'postgres', 'mariadb', 'oracle', 'sqlserver'
            db_instance_identifier (str): Identificador único da instância
            master_username (str): Usuário mestre
            master_user_password (str): Senha do usuário mestre
            db_instance_class (str): Tipo de instância
            allocated_storage (int): Espaço em GB
            engine_version (str): Versão do engine
            multi_az (bool): Habilitar Multi-AZ
            publicly_accessible (bool): Acessível publicamente
            
        Returns:
            dict: Informações da instância criada
        """
        try:
            params = {
                'DBInstanceIdentifier': db_instance_identifier,
                'DBInstanceClass': db_instance_class,
                'Engine': engine,
                'MasterUsername': master_username,
                'MasterUserPassword': master_user_password,
                'AllocatedStorage': allocated_storage,
                'MultiAZ': multi_az,
                'PubliclyAccessible': publicly_accessible,
                'StorageType': 'gp2'
            }
            
            if engine_version:
                params['EngineVersion'] = engine_version
            
            response = self.rds_client.create_db_instance(**params)
            db_instance = response['DBInstance']
            
            print(f"✓ Instância RDS criada: {db_instance_identifier}")
            print(f"  Engine: {engine}")
            print(f"  Status: {db_instance['DBInstanceStatus']}")
            
            return {
                'DBInstanceIdentifier': db_instance['DBInstanceIdentifier'],
                'Engine': db_instance['Engine'],
                'DBInstanceStatus': db_instance['DBInstanceStatus'],
                'Endpoint': db_instance.get('Endpoint', {}).get('Address', 'N/A')
            }
        except ClientError as e:
            print(f"✗ Erro ao criar instância RDS: {e}")
            return None

    def describe_db_instances(self, db_instance_identifier=None):
        """
        Lista instâncias RDS.
        
        Args:
            db_instance_identifier (str): Filtrar por identificador (opcional)
            
        Returns:
            list: Lista de instâncias
        """
        try:
            params = {}
            if db_instance_identifier:
                params['DBInstanceIdentifier'] = db_instance_identifier
            
            response = self.rds_client.describe_db_instances(**params)
            instances = []
            
            for instance in response['DBInstances']:
                instances.append({
                    'DBInstanceIdentifier': instance['DBInstanceIdentifier'],
                    'Engine': instance['Engine'],
                    'DBInstanceClass': instance['DBInstanceClass'],
                    'DBInstanceStatus': instance['DBInstanceStatus'],
                    'Endpoint': instance.get('Endpoint', {}).get('Address', 'N/A'),
                    'AllocatedStorage': instance['AllocatedStorage'],
                    'MultiAZ': instance['MultiAZ']
                })
            
            return instances
        except ClientError as e:
            print(f"✗ Erro ao descrever instâncias RDS: {e}")
            return []

    def create_db_snapshot(self, db_instance_identifier, db_snapshot_identifier):
        """
        Cria um snapshot de uma instância RDS.
        
        Args:
            db_instance_identifier (str): ID da instância
            db_snapshot_identifier (str): ID do snapshot
        """
        try:
            response = self.rds_client.create_db_snapshot(
                DBSnapshotIdentifier=db_snapshot_identifier,
                DBInstanceIdentifier=db_instance_identifier
            )
            print(f"✓ Snapshot criado: {db_snapshot_identifier}")
        except ClientError as e:
            print(f"✗ Erro ao criar snapshot: {e}")

    def create_read_replica(self, db_instance_identifier, read_replica_identifier,
                           availability_zone=None):
        """
        Cria uma read replica de uma instância RDS.
        
        Args:
            db_instance_identifier (str): ID da instância master
            read_replica_identifier (str): ID da read replica
            availability_zone (str): Zona de disponibilidade
        """
        try:
            params = {
                'DBInstanceIdentifier': read_replica_identifier,
                'SourceDBInstanceIdentifier': db_instance_identifier
            }
            if availability_zone:
                params['AvailabilityZone'] = availability_zone
            
            self.rds_client.create_db_instance_read_replica(**params)
            print(f"✓ Read replica criada: {read_replica_identifier}")
        except ClientError as e:
            print(f"✗ Erro ao criar read replica: {e}")

    def modify_db_instance(self, db_instance_identifier, allocated_storage=None,
                          db_instance_class=None, apply_immediately=False):
        """
        Modifica uma instância RDS.
        
        Args:
            db_instance_identifier (str): ID da instância
            allocated_storage (int): Novo tamanho em GB
            db_instance_class (str): Novo tipo de instância
            apply_immediately (bool): Aplicar imediatamente
        """
        try:
            params = {
                'DBInstanceIdentifier': db_instance_identifier,
                'ApplyImmediately': apply_immediately
            }
            
            if allocated_storage:
                params['AllocatedStorage'] = allocated_storage
            if db_instance_class:
                params['DBInstanceClass'] = db_instance_class
            
            self.rds_client.modify_db_instance(**params)
            print(f"✓ Instância modificada: {db_instance_identifier}")
        except ClientError as e:
            print(f"✗ Erro ao modificar instância: {e}")

    def reboot_db_instance(self, db_instance_identifier):
        """
        Reinicia uma instância RDS.
        
        Args:
            db_instance_identifier (str): ID da instância
        """
        try:
            self.rds_client.reboot_db_instance(
                DBInstanceIdentifier=db_instance_identifier
            )
            print(f"✓ Instância reiniciada: {db_instance_identifier}")
        except ClientError as e:
            print(f"✗ Erro ao reiniciar instância: {e}")

    def delete_db_instance(self, db_instance_identifier, skip_final_snapshot=True):
        """
        Deleta uma instância RDS.
        
        Args:
            db_instance_identifier (str): ID da instância
            skip_final_snapshot (bool): Pular snapshot final
        """
        try:
            self.rds_client.delete_db_instance(
                DBInstanceIdentifier=db_instance_identifier,
                SkipFinalSnapshot=skip_final_snapshot
            )
            print(f"✓ Instância deletada: {db_instance_identifier}")
        except ClientError as e:
            print(f"✗ Erro ao deletar instância: {e}")

    def describe_db_snapshots(self):
        """
        Lista todos os snapshots.
        
        Returns:
            list: Lista de snapshots
        """
        try:
            response = self.rds_client.describe_db_snapshots()
            snapshots = []
            
            for snapshot in response['DBSnapshots']:
                snapshots.append({
                    'DBSnapshotIdentifier': snapshot['DBSnapshotIdentifier'],
                    'DBInstanceIdentifier': snapshot['DBInstanceIdentifier'],
                    'SnapshotCreateTime': str(snapshot['SnapshotCreateTime']),
                    'AllocatedStorage': snapshot['AllocatedStorage']
                })
            
            return snapshots
        except ClientError as e:
            print(f"✗ Erro ao listar snapshots: {e}")
            return []


# Exemplo de uso
if __name__ == '__main__':
    manager = RDSManager(region='us-east-1')
    
    # Listar instâncias existentes
    print("=== Instâncias RDS ===")
    instances = manager.describe_db_instances()
    for instance in instances:
        print(f"- {instance['DBInstanceIdentifier']}: {instance['Engine']}")
    
    # Criar nova instância (comentado)
    # print("\n=== Criando Instância ===")
    # manager.create_db_instance(
    #     engine='postgres',
    #     db_instance_identifier='meu-postgres',
    #     master_username='postgres',
    #     master_user_password='SenhaSegura123!'
    # )
