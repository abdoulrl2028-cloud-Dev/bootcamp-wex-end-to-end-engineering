"""
Script para gerenciar buckets S3 na AWS.
"""

import boto3
import json
from botocore.exceptions import ClientError


class S3BucketManager:
    def __init__(self, region='us-east-1'):
        """Inicializa o gerenciador S3."""
        self.s3_client = boto3.client('s3', region_name=region)
        self.s3_resource = boto3.resource('s3', region_name=region)

    def create_bucket(self, bucket_name):
        """
        Cria um novo bucket S3.
        
        Args:
            bucket_name (str): Nome do bucket (deve ser único globalmente)
            
        Returns:
            bool: True se criado com sucesso
        """
        try:
            self.s3_client.create_bucket(Bucket=bucket_name)
            print(f"✓ Bucket criado: {bucket_name}")
            return True
        except ClientError as e:
            print(f"✗ Erro ao criar bucket: {e}")
            return False

    def list_buckets(self):
        """
        Lista todos os buckets.
        
        Returns:
            list: Lista de nomes de buckets
        """
        try:
            response = self.s3_client.list_buckets()
            buckets = [b['Name'] for b in response['Buckets']]
            return buckets
        except ClientError as e:
            print(f"✗ Erro ao listar buckets: {e}")
            return []

    def upload_file(self, file_path, bucket_name, object_key, 
                   content_type=None, metadata=None):
        """
        Faz upload de um arquivo para o bucket.
        
        Args:
            file_path (str): Caminho local do arquivo
            bucket_name (str): Nome do bucket
            object_key (str): Chave do objeto no S3
            content_type (str): Tipo MIME do arquivo
            metadata (dict): Metadados customizados
            
        Returns:
            bool: True se upload bem-sucedido
        """
        try:
            extra_args = {}
            if content_type:
                extra_args['ContentType'] = content_type
            if metadata:
                extra_args['Metadata'] = metadata
            
            self.s3_client.upload_file(file_path, bucket_name, object_key, 
                                       ExtraArgs=extra_args if extra_args else None)
            print(f"✓ Arquivo enviado: s3://{bucket_name}/{object_key}")
            return True
        except ClientError as e:
            print(f"✗ Erro ao fazer upload: {e}")
            return False

    def download_file(self, bucket_name, object_key, file_path):
        """
        Faz download de um arquivo do bucket.
        
        Args:
            bucket_name (str): Nome do bucket
            object_key (str): Chave do objeto
            file_path (str): Caminho local para salvar
            
        Returns:
            bool: True se download bem-sucedido
        """
        try:
            self.s3_client.download_file(bucket_name, object_key, file_path)
            print(f"✓ Arquivo baixado: {file_path}")
            return True
        except ClientError as e:
            print(f"✗ Erro ao fazer download: {e}")
            return False

    def list_objects(self, bucket_name, prefix=''):
        """
        Lista objetos em um bucket.
        
        Args:
            bucket_name (str): Nome do bucket
            prefix (str): Prefixo para filtrar
            
        Returns:
            list: Lista de objetos
        """
        try:
            response = self.s3_client.list_objects_v2(
                Bucket=bucket_name,
                Prefix=prefix
            )
            
            if 'Contents' not in response:
                return []
            
            objects = [{
                'Key': obj['Key'],
                'Size': obj['Size'],
                'LastModified': str(obj['LastModified']),
                'StorageClass': obj['StorageClass']
            } for obj in response['Contents']]
            
            return objects
        except ClientError as e:
            print(f"✗ Erro ao listar objetos: {e}")
            return []

    def get_object_metadata(self, bucket_name, object_key):
        """
        Obtém metadados de um objeto.
        
        Args:
            bucket_name (str): Nome do bucket
            object_key (str): Chave do objeto
            
        Returns:
            dict: Metadados do objeto
        """
        try:
            response = self.s3_client.head_object(
                Bucket=bucket_name,
                Key=object_key
            )
            return {
                'ContentType': response.get('ContentType'),
                'ContentLength': response.get('ContentLength'),
                'LastModified': str(response.get('LastModified')),
                'ETag': response.get('ETag'),
                'Metadata': response.get('Metadata', {})
            }
        except ClientError as e:
            print(f"✗ Erro ao obter metadados: {e}")
            return None

    def delete_object(self, bucket_name, object_key):
        """
        Deleta um objeto do bucket.
        
        Args:
            bucket_name (str): Nome do bucket
            object_key (str): Chave do objeto
            
        Returns:
            bool: True se deletado com sucesso
        """
        try:
            self.s3_client.delete_object(Bucket=bucket_name, Key=object_key)
            print(f"✓ Objeto deletado: s3://{bucket_name}/{object_key}")
            return True
        except ClientError as e:
            print(f"✗ Erro ao deletar objeto: {e}")
            return False

    def delete_bucket(self, bucket_name):
        """
        Deleta um bucket vazio.
        
        Args:
            bucket_name (str): Nome do bucket
            
        Returns:
            bool: True se deletado com sucesso
        """
        try:
            self.s3_client.delete_bucket(Bucket=bucket_name)
            print(f"✓ Bucket deletado: {bucket_name}")
            return True
        except ClientError as e:
            print(f"✗ Erro ao deletar bucket: {e}")
            return False

    def enable_versioning(self, bucket_name):
        """
        Habilita versionamento no bucket.
        
        Args:
            bucket_name (str): Nome do bucket
        """
        try:
            self.s3_client.put_bucket_versioning(
                Bucket=bucket_name,
                VersioningConfiguration={'Status': 'Enabled'}
            )
            print(f"✓ Versionamento habilitado: {bucket_name}")
        except ClientError as e:
            print(f"✗ Erro ao habilitar versionamento: {e}")

    def enable_encryption(self, bucket_name):
        """
        Habilita encriptação por padrão no bucket.
        
        Args:
            bucket_name (str): Nome do bucket
        """
        try:
            self.s3_client.put_bucket_encryption(
                Bucket=bucket_name,
                ServerSideEncryptionConfiguration={
                    'Rules': [{
                        'ApplyServerSideEncryptionByDefault': {
                            'SSEAlgorithm': 'AES256'
                        }
                    }]
                }
            )
            print(f"✓ Encriptação habilitada: {bucket_name}")
        except ClientError as e:
            print(f"✗ Erro ao habilitar encriptação: {e}")


# Exemplo de uso
if __name__ == '__main__':
    manager = S3BucketManager(region='us-east-1')
    
    # Listar buckets existentes
    print("=== Buckets Existentes ===")
    buckets = manager.list_buckets()
    for bucket in buckets:
        print(f"- {bucket}")
    
    # Criar um novo bucket (comentado por padrão)
    # print("\n=== Criando Bucket ===")
    # manager.create_bucket('meu-bucket-unico-12345')
    # manager.enable_encryption('meu-bucket-unico-12345')
    # manager.enable_versioning('meu-bucket-unico-12345')
