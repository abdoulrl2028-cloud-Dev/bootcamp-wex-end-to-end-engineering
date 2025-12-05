"""
Script para operações com objetos S3.
"""

import boto3
import json
from botocore.exceptions import ClientError


class S3ObjectOperations:
    def __init__(self, region='us-east-1'):
        """Inicializa o gerenciador de objetos S3."""
        self.s3_client = boto3.client('s3', region_name=region)

    def put_object_with_content(self, bucket_name, object_key, content, 
                               content_type='text/plain'):
        """
        Cria um objeto diretamente com conteúdo.
        
        Args:
            bucket_name (str): Nome do bucket
            object_key (str): Chave do objeto
            content (str): Conteúdo do objeto
            content_type (str): Tipo MIME
        """
        try:
            self.s3_client.put_object(
                Bucket=bucket_name,
                Key=object_key,
                Body=content,
                ContentType=content_type
            )
            print(f"✓ Objeto criado: s3://{bucket_name}/{object_key}")
        except ClientError as e:
            print(f"✗ Erro ao criar objeto: {e}")

    def put_json_object(self, bucket_name, object_key, data):
        """
        Cria um objeto JSON no S3.
        
        Args:
            bucket_name (str): Nome do bucket
            object_key (str): Chave do objeto
            data (dict): Dados para serializar
        """
        try:
            json_content = json.dumps(data, indent=2)
            self.s3_client.put_object(
                Bucket=bucket_name,
                Key=object_key,
                Body=json_content,
                ContentType='application/json'
            )
            print(f"✓ Objeto JSON criado: s3://{bucket_name}/{object_key}")
        except ClientError as e:
            print(f"✗ Erro ao criar objeto JSON: {e}")

    def get_object_content(self, bucket_name, object_key):
        """
        Obtém o conteúdo de um objeto.
        
        Args:
            bucket_name (str): Nome do bucket
            object_key (str): Chave do objeto
            
        Returns:
            str: Conteúdo do objeto
        """
        try:
            response = self.s3_client.get_object(Bucket=bucket_name, Key=object_key)
            content = response['Body'].read().decode('utf-8')
            return content
        except ClientError as e:
            print(f"✗ Erro ao obter objeto: {e}")
            return None

    def get_json_object(self, bucket_name, object_key):
        """
        Obtém e deserializa um objeto JSON.
        
        Args:
            bucket_name (str): Nome do bucket
            object_key (str): Chave do objeto
            
        Returns:
            dict: Dados deserializados
        """
        try:
            content = self.get_object_content(bucket_name, object_key)
            if content:
                return json.loads(content)
            return None
        except json.JSONDecodeError as e:
            print(f"✗ Erro ao deserializar JSON: {e}")
            return None

    def copy_object(self, source_bucket, source_key, dest_bucket, dest_key):
        """
        Copia um objeto entre buckets ou dentro do mesmo bucket.
        
        Args:
            source_bucket (str): Bucket de origem
            source_key (str): Chave de origem
            dest_bucket (str): Bucket de destino
            dest_key (str): Chave de destino
        """
        try:
            copy_source = {'Bucket': source_bucket, 'Key': source_key}
            self.s3_client.copy_object(
                CopySource=copy_source,
                Bucket=dest_bucket,
                Key=dest_key
            )
            print(f"✓ Objeto copiado para: s3://{dest_bucket}/{dest_key}")
        except ClientError as e:
            print(f"✗ Erro ao copiar objeto: {e}")

    def generate_presigned_url(self, bucket_name, object_key, expiration=3600):
        """
        Gera uma URL pré-assinada para acesso temporário.
        
        Args:
            bucket_name (str): Nome do bucket
            object_key (str): Chave do objeto
            expiration (int): Tempo de expiração em segundos
            
        Returns:
            str: URL pré-assinada
        """
        try:
            url = self.s3_client.generate_presigned_url(
                'get_object',
                Params={'Bucket': bucket_name, 'Key': object_key},
                ExpiresIn=expiration
            )
            return url
        except ClientError as e:
            print(f"✗ Erro ao gerar URL pré-assinada: {e}")
            return None

    def batch_upload(self, bucket_name, file_list):
        """
        Faz upload em lote de múltiplos arquivos.
        
        Args:
            bucket_name (str): Nome do bucket
            file_list (list): Lista de tuplas (local_path, s3_key)
        """
        try:
            success = 0
            fail = 0
            
            for local_path, s3_key in file_list:
                try:
                    self.s3_client.upload_file(local_path, bucket_name, s3_key)
                    print(f"✓ {local_path} → s3://{bucket_name}/{s3_key}")
                    success += 1
                except Exception as e:
                    print(f"✗ Erro ao enviar {local_path}: {e}")
                    fail += 1
            
            print(f"\n✓ Resumo: {success} sucesso(s), {fail} falha(s)")
        except ClientError as e:
            print(f"✗ Erro no upload em lote: {e}")

    def batch_delete(self, bucket_name, keys_list):
        """
        Deleta múltiplos objetos.
        
        Args:
            bucket_name (str): Nome do bucket
            keys_list (list): Lista de chaves para deletar
        """
        try:
            delete_objects = [{'Key': key} for key in keys_list]
            self.s3_client.delete_objects(
                Bucket=bucket_name,
                Delete={'Objects': delete_objects}
            )
            print(f"✓ {len(keys_list)} objeto(s) deletado(s)")
        except ClientError as e:
            print(f"✗ Erro ao deletar objetos: {e}")


# Exemplo de uso
if __name__ == '__main__':
    ops = S3ObjectOperations(region='us-east-1')
    
    # Exemplo de criação de objeto JSON
    # ops.put_json_object('meu-bucket', 'dados.json', {
    #     'nome': 'Projeto AWS',
    #     'versao': '1.0',
    #     'tags': ['cloud', 'aws', 's3']
    # })
    
    # Exemplo de obtenção de objeto
    # content = ops.get_object_content('meu-bucket', 'dados.json')
    # print(f"Conteúdo: {content}")
    
    # Exemplo de URL pré-assinada
    # url = ops.generate_presigned_url('meu-bucket', 'dados.json', expiration=3600)
    # print(f"URL: {url}")
