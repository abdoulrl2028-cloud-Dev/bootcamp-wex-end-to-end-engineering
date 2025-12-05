# Banco de Dados - RDS e DynamoDB

Serviços gerenciados de banco de dados na AWS.

## Tópicos

1. **RDS (Relational Database Service)**
   - MySQL, PostgreSQL, MariaDB, Oracle, SQL Server
   - Multi-AZ, Read Replicas
   - Backups e restore

2. **DynamoDB**
   - Banco de dados NoSQL
   - Escalabilidade automática
   - Índices globais secundários

3. **Gerenciamento de Conexões**
4. **Monitoramento e Alertas**

## Arquivos

- `rds_manager.py`: Gerenciar instâncias RDS
- `dynamodb_manager.py`: Operações DynamoDB
- `database_backup.py`: Backup e restore

## Exemplo de Uso

```python
# RDS
db_manager = RDSManager()
db_manager.create_db_instance('mysql', 'db-instance-1')

# DynamoDB
dynamo = DynamoDBManager()
dynamo.create_table('usuarios', 'user_id')
dynamo.put_item('usuarios', {'user_id': '123', 'nome': 'João'})
```
