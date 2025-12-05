# S3 - Simple Storage Service

Serviço de armazenamento de objetos escalável e durável.

## Tópicos

1. **Criar e gerenciar buckets**
2. **Upload/Download de objetos**
3. **Configurar versionamento**
4. **Políticas de acesso (Bucket Policies)**
5. **CORS (Cross-Origin Resource Sharing)**
6. **Ciclo de vida de objetos**
7. **Server-side encryption**

## Arquivos

- `bucket_manager.py`: Gerenciar buckets
- `object_operations.py`: Operações com objetos
- `bucket_policies.py`: Configurar permissões
- `lifecycle_policies.py`: Políticas de ciclo de vida

## Exemplo Básico

```python
import boto3

s3 = boto3.client('s3')

# Criar um bucket
s3.create_bucket(Bucket='meu-bucket')

# Upload de arquivo
s3.upload_file('local.txt', 'meu-bucket', 'remote.txt')

# Download de arquivo
s3.download_file('meu-bucket', 'remote.txt', 'baixado.txt')
```
