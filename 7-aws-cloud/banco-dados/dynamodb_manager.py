"""
Script para operações com DynamoDB.
"""

import boto3
from botocore.exceptions import ClientError
import json


class DynamoDBManager:
    def __init__(self, region='us-east-1'):
        """Inicializa o gerenciador DynamoDB."""
        self.dynamodb = boto3.resource('dynamodb', region_name=region)
        self.client = boto3.client('dynamodb', region_name=region)

    def create_table(self, table_name, partition_key, sort_key=None,
                    read_capacity=5, write_capacity=5):
        """
        Cria uma tabela DynamoDB.
        
        Args:
            table_name (str): Nome da tabela
            partition_key (str): Chave de partição
            sort_key (str): Chave de ordenação (opcional)
            read_capacity (int): Unidades de leitura
            write_capacity (int): Unidades de escrita
            
        Returns:
            str: Nome da tabela criada
        """
        try:
            key_schema = [
                {'AttributeName': partition_key, 'KeyType': 'HASH'}
            ]
            attribute_definitions = [
                {'AttributeName': partition_key, 'AttributeType': 'S'}
            ]
            
            if sort_key:
                key_schema.append({'AttributeName': sort_key, 'KeyType': 'RANGE'})
                attribute_definitions.append(
                    {'AttributeName': sort_key, 'AttributeType': 'S'}
                )
            
            table = self.dynamodb.create_table(
                TableName=table_name,
                KeySchema=key_schema,
                AttributeDefinitions=attribute_definitions,
                BillingMode='PROVISIONED',
                ProvisionedThroughput={
                    'ReadCapacityUnits': read_capacity,
                    'WriteCapacityUnits': write_capacity
                }
            )
            
            table.meta.client.get_waiter('table_exists').wait(TableName=table_name)
            print(f"✓ Tabela criada: {table_name}")
            return table_name
        except ClientError as e:
            print(f"✗ Erro ao criar tabela: {e}")
            return None

    def list_tables(self):
        """
        Lista todas as tabelas.
        
        Returns:
            list: Lista de nomes de tabelas
        """
        try:
            response = self.client.list_tables()
            return response['TableNames']
        except ClientError as e:
            print(f"✗ Erro ao listar tabelas: {e}")
            return []

    def put_item(self, table_name, item):
        """
        Insere um item na tabela.
        
        Args:
            table_name (str): Nome da tabela
            item (dict): Item a inserir
        """
        try:
            table = self.dynamodb.Table(table_name)
            table.put_item(Item=item)
            print(f"✓ Item inserido na tabela {table_name}")
        except ClientError as e:
            print(f"✗ Erro ao inserir item: {e}")

    def get_item(self, table_name, key):
        """
        Obtém um item da tabela.
        
        Args:
            table_name (str): Nome da tabela
            key (dict): Chave do item
            
        Returns:
            dict: Item encontrado
        """
        try:
            table = self.dynamodb.Table(table_name)
            response = table.get_item(Key=key)
            return response.get('Item', None)
        except ClientError as e:
            print(f"✗ Erro ao obter item: {e}")
            return None

    def scan(self, table_name, filter_expression=None):
        """
        Escaneia todos os itens da tabela.
        
        Args:
            table_name (str): Nome da tabela
            filter_expression: Expressão de filtro (opcional)
            
        Returns:
            list: Lista de itens
        """
        try:
            table = self.dynamodb.Table(table_name)
            params = {}
            if filter_expression:
                params['FilterExpression'] = filter_expression
            
            response = table.scan(**params)
            return response.get('Items', [])
        except ClientError as e:
            print(f"✗ Erro ao escanear tabela: {e}")
            return []

    def update_item(self, table_name, key, update_expression, attribute_values):
        """
        Atualiza um item.
        
        Args:
            table_name (str): Nome da tabela
            key (dict): Chave do item
            update_expression (str): Expressão de atualização
            attribute_values (dict): Valores dos atributos
        """
        try:
            table = self.dynamodb.Table(table_name)
            table.update_item(
                Key=key,
                UpdateExpression=update_expression,
                ExpressionAttributeValues=attribute_values
            )
            print(f"✓ Item atualizado na tabela {table_name}")
        except ClientError as e:
            print(f"✗ Erro ao atualizar item: {e}")

    def delete_item(self, table_name, key):
        """
        Deleta um item.
        
        Args:
            table_name (str): Nome da tabela
            key (dict): Chave do item
        """
        try:
            table = self.dynamodb.Table(table_name)
            table.delete_item(Key=key)
            print(f"✓ Item deletado da tabela {table_name}")
        except ClientError as e:
            print(f"✗ Erro ao deletar item: {e}")

    def query(self, table_name, partition_key_value, partition_key_name):
        """
        Consulta itens com a mesma chave de partição.
        
        Args:
            table_name (str): Nome da tabela
            partition_key_value (str): Valor da chave de partição
            partition_key_name (str): Nome da chave de partição
            
        Returns:
            list: Items encontrados
        """
        try:
            table = self.dynamodb.Table(table_name)
            response = table.query(
                KeyConditionExpression=f'{partition_key_name} = :pk',
                ExpressionAttributeValues={':pk': partition_key_value}
            )
            return response.get('Items', [])
        except ClientError as e:
            print(f"✗ Erro ao consultar tabela: {e}")
            return []

    def delete_table(self, table_name):
        """
        Deleta uma tabela.
        
        Args:
            table_name (str): Nome da tabela
        """
        try:
            table = self.dynamodb.Table(table_name)
            table.delete()
            print(f"✓ Tabela deletada: {table_name}")
        except ClientError as e:
            print(f"✗ Erro ao deletar tabela: {e}")

    def enable_billing_mode(self, table_name):
        """
        Muda para modo de cobrança PAY_PER_REQUEST.
        
        Args:
            table_name (str): Nome da tabela
        """
        try:
            self.client.update_table(
                TableName=table_name,
                BillingMode='PAY_PER_REQUEST'
            )
            print(f"✓ Modo de cobrança alterado para PAY_PER_REQUEST")
        except ClientError as e:
            print(f"✗ Erro ao alterar modo de cobrança: {e}")


# Exemplo de uso
if __name__ == '__main__':
    manager = DynamoDBManager(region='us-east-1')
    
    # Listar tabelas existentes
    print("=== Tabelas DynamoDB ===")
    tables = manager.list_tables()
    for table in tables:
        print(f"- {table}")
    
    # Criar uma nova tabela
    # print("\n=== Criando Tabela ===")
    # manager.create_table('usuarios', 'user_id')
    # 
    # # Inserir um item
    # manager.put_item('usuarios', {
    #     'user_id': 'user123',
    #     'nome': 'João Silva',
    #     'email': 'joao@example.com',
    #     'data_criacao': '2025-01-01'
    # })
    # 
    # # Consultar um item
    # item = manager.get_item('usuarios', {'user_id': 'user123'})
    # print(f"Item encontrado: {json.dumps(item, indent=2)}")
