"""
Script para limpeza automática de recursos subutilizados.
"""

import boto3
from botocore.exceptions import ClientError
from datetime import datetime, timedelta


class ResourceCleanup:
    def __init__(self, region='us-east-1'):
        """Inicializa o gerenciador de limpeza de recursos."""
        self.ec2_client = boto3.client('ec2', region_name=region)
        self.s3_client = boto3.client('s3', region_name=region)

    def find_and_delete_unused_volumes(self, days=30, dry_run=True):
        """
        Encontra e deleta volumes EBS não utilizados.
        
        Args:
            days (int): Dias sem modificação para considerá-lo não utilizado
            dry_run (bool): Se True, apenas simula a exclusão
            
        Returns:
            dict: Resumo da operação
        """
        try:
            response = self.ec2_client.describe_volumes(
                Filters=[{'Name': 'status', 'Values': ['available']}]
            )
            
            threshold_date = datetime.now(response['Volumes'][0]['CreateTime'].tzinfo) - timedelta(days=days)
            volumes_to_delete = []
            
            for volume in response['Volumes']:
                if volume['CreateTime'] < threshold_date:
                    volumes_to_delete.append({
                        'VolumeId': volume['VolumeId'],
                        'Size': volume['Size'],
                        'CreateTime': str(volume['CreateTime'])
                    })
            
            summary = {
                'total_found': len(volumes_to_delete),
                'total_gb': sum(v['Size'] for v in volumes_to_delete),
                'volumes': volumes_to_delete,
                'estimated_savings': sum(v['Size'] for v in volumes_to_delete) * 0.1  # $0.1 por GB
            }
            
            if not dry_run and volumes_to_delete:
                for volume in volumes_to_delete:
                    try:
                        self.ec2_client.delete_volume(VolumeId=volume['VolumeId'])
                        print(f"✓ Volume deletado: {volume['VolumeId']}")
                    except ClientError as e:
                        print(f"✗ Erro ao deletar {volume['VolumeId']}: {e}")
            
            return summary
        except ClientError as e:
            print(f"✗ Erro na operação: {e}")
            return {'error': str(e)}

    def find_unattached_elastic_ips(self, dry_run=True):
        """
        Encontra e libera Elastic IPs não associados.
        
        Args:
            dry_run (bool): Se True, apenas simula a exclusão
            
        Returns:
            dict: Resumo da operação
        """
        try:
            response = self.ec2_client.describe_addresses(
                Filters=[{'Name': 'instance-id', 'Values': ['']}]
            )
            
            unattached_eips = []
            for eip in response['Addresses']:
                if 'InstanceId' not in eip or not eip['InstanceId']:
                    unattached_eips.append({
                        'PublicIp': eip['PublicIp'],
                        'AllocationId': eip.get('AllocationId'),
                        'AssociationId': eip.get('AssociationId')
                    })
            
            summary = {
                'total_found': len(unattached_eips),
                'eips': unattached_eips,
                'estimated_savings': len(unattached_eips) * 0.005 * 730  # ~$3.5/mês por EIP não utilizado
            }
            
            if not dry_run and unattached_eips:
                for eip in unattached_eips:
                    try:
                        self.ec2_client.release_address(
                            AllocationId=eip['AllocationId']
                        )
                        print(f"✓ Elastic IP liberado: {eip['PublicIp']}")
                    except ClientError as e:
                        print(f"✗ Erro ao liberar {eip['PublicIp']}: {e}")
            
            return summary
        except ClientError as e:
            print(f"✗ Erro na operação: {e}")
            return {'error': str(e)}

    def find_old_snapshots(self, days=90, dry_run=True):
        """
        Encontra e deleta snapshots antigos.
        
        Args:
            days (int): Dias de retenção
            dry_run (bool): Se True, apenas simula a exclusão
            
        Returns:
            dict: Resumo da operação
        """
        try:
            response = self.ec2_client.describe_snapshots(OwnerIds=['self'])
            
            threshold_date = datetime.now(response['Snapshots'][0]['StartTime'].tzinfo) - timedelta(days=days)
            old_snapshots = []
            
            for snapshot in response['Snapshots']:
                if snapshot['StartTime'] < threshold_date:
                    old_snapshots.append({
                        'SnapshotId': snapshot['SnapshotId'],
                        'VolumeSize': snapshot['VolumeSize'],
                        'StartTime': str(snapshot['StartTime']),
                        'Description': snapshot.get('Description', 'N/A')
                    })
            
            summary = {
                'total_found': len(old_snapshots),
                'total_gb': sum(s['VolumeSize'] for s in old_snapshots),
                'snapshots': old_snapshots,
                'estimated_savings': sum(s['VolumeSize'] for s in old_snapshots) * 0.05  # $0.05 por GB
            }
            
            if not dry_run and old_snapshots:
                for snapshot in old_snapshots:
                    try:
                        self.ec2_client.delete_snapshot(
                            SnapshotId=snapshot['SnapshotId']
                        )
                        print(f"✓ Snapshot deletado: {snapshot['SnapshotId']}")
                    except ClientError as e:
                        print(f"✗ Erro ao deletar {snapshot['SnapshotId']}: {e}")
            
            return summary
        except ClientError as e:
            print(f"✗ Erro na operação: {e}")
            return {'error': str(e)}

    def find_empty_s3_buckets(self, dry_run=True):
        """
        Encontra e deleta buckets S3 vazios.
        
        Args:
            dry_run (bool): Se True, apenas simula a exclusão
            
        Returns:
            dict: Resumo da operação
        """
        try:
            response = self.s3_client.list_buckets()
            empty_buckets = []
            
            for bucket in response['Buckets']:
                try:
                    objects = self.s3_client.list_objects_v2(
                        Bucket=bucket['Name'],
                        MaxKeys=1
                    )
                    
                    if 'Contents' not in objects:
                        empty_buckets.append({
                            'BucketName': bucket['Name'],
                            'CreationDate': str(bucket['CreationDate'])
                        })
                except ClientError:
                    pass
            
            summary = {
                'total_found': len(empty_buckets),
                'buckets': empty_buckets
            }
            
            if not dry_run and empty_buckets:
                for bucket in empty_buckets:
                    try:
                        self.s3_client.delete_bucket(Bucket=bucket['BucketName'])
                        print(f"✓ Bucket S3 deletado: {bucket['BucketName']}")
                    except ClientError as e:
                        print(f"✗ Erro ao deletar {bucket['BucketName']}: {e}")
            
            return summary
        except ClientError as e:
            print(f"✗ Erro na operação: {e}")
            return {'error': str(e)}

    def generate_cleanup_report(self, filename='cleanup_report.txt'):
        """
        Gera um relatório completo de limpeza.
        
        Args:
            filename (str): Nome do arquivo de saída
        """
        print("Analisando recursos para limpeza...")
        
        volumes_summary = self.find_and_delete_unused_volumes(days=30, dry_run=True)
        eips_summary = self.find_unattached_elastic_ips(dry_run=True)
        snapshots_summary = self.find_old_snapshots(days=90, dry_run=True)
        buckets_summary = self.find_empty_s3_buckets(dry_run=True)
        
        with open(filename, 'w', encoding='utf-8') as f:
            f.write("=" * 60 + "\n")
            f.write("RELATÓRIO DE LIMPEZA DE RECURSOS AWS\n")
            f.write(f"Data: {datetime.now().strftime('%Y-%m-%d %H:%M:%S')}\n")
            f.write("=" * 60 + "\n\n")
            
            # Volumes não utilizados
            f.write("VOLUMES EBS NÃO UTILIZADOS\n")
            f.write("-" * 60 + "\n")
            f.write(f"Quantidade encontrada: {volumes_summary.get('total_found', 0)}\n")
            f.write(f"Tamanho total: {volumes_summary.get('total_gb', 0)} GB\n")
            f.write(f"Economia estimada: ${volumes_summary.get('estimated_savings', 0):.2f}/mês\n\n")
            
            # Elastic IPs não associados
            f.write("ELASTIC IPs NÃO ASSOCIADOS\n")
            f.write("-" * 60 + "\n")
            f.write(f"Quantidade encontrada: {eips_summary.get('total_found', 0)}\n")
            f.write(f"Economia estimada: ${eips_summary.get('estimated_savings', 0):.2f}/mês\n\n")
            
            # Snapshots antigos
            f.write("SNAPSHOTS ANTIGOS (>90 dias)\n")
            f.write("-" * 60 + "\n")
            f.write(f"Quantidade encontrada: {snapshots_summary.get('total_found', 0)}\n")
            f.write(f"Tamanho total: {snapshots_summary.get('total_gb', 0)} GB\n")
            f.write(f"Economia estimada: ${snapshots_summary.get('estimated_savings', 0):.2f}/mês\n\n")
            
            # Buckets S3 vazios
            f.write("BUCKETS S3 VAZIOS\n")
            f.write("-" * 60 + "\n")
            f.write(f"Quantidade encontrada: {buckets_summary.get('total_found', 0)}\n\n")
            
            # Total
            total_savings = (
                volumes_summary.get('estimated_savings', 0) +
                eips_summary.get('estimated_savings', 0) +
                snapshots_summary.get('estimated_savings', 0)
            )
            
            f.write("=" * 60 + "\n")
            f.write(f"ECONOMIA TOTAL ESTIMADA: ${total_savings:.2f}/mês\n")
            f.write("=" * 60 + "\n")
        
        print(f"✓ Relatório gerado: {filename}")


# Exemplo de uso
if __name__ == '__main__':
    cleanup = ResourceCleanup(region='us-east-1')
    
    print("=== Limpeza de Recursos AWS (Modo Simulação) ===\n")
    
    # Gerar relatório
    cleanup.generate_cleanup_report()
