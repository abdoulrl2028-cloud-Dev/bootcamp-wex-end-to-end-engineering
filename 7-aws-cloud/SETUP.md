# AWS Cloud - Guia de Configuração

## 1. Instalação de Dependências

```bash
cd 7-aws-cloud
pip install -r requirements.txt
```

## 2. Configurar Credenciais AWS

### Opção A: Usando AWS CLI

```bash
aws configure
# Inserir:
# AWS Access Key ID
# AWS Secret Access Key
# Default region: us-east-1
# Default output format: json
```

### Opção B: Variáveis de Ambiente

```bash
export AWS_ACCESS_KEY_ID="sua-chave-aqui"
export AWS_SECRET_ACCESS_KEY="sua-secreta-aqui"
export AWS_DEFAULT_REGION="us-east-1"
```

### Opção C: Arquivo .env

Criar arquivo `.env` na raiz do projeto:

```env
AWS_ACCESS_KEY_ID=sua-chave-aqui
AWS_SECRET_ACCESS_KEY=sua-secreta-aqui
AWS_DEFAULT_REGION=us-east-1
```

## 3. Estrutura de Módulos

### EC2 - Computação

```python
from ec2.create_instances import EC2Manager

manager = EC2Manager()
instances = manager.list_instances()
```

### S3 - Armazenamento

```python
from s3.bucket_manager import S3BucketManager

s3 = S3BucketManager()
buckets = s3.list_buckets()
```

### Redes - VPC

```python
from redes.vpc_manager import VPCManager

vpc = VPCManager()
vpc_id = vpc.create_vpc('10.0.0.0/16', 'minha-vpc')
```

### Banco de Dados

```python
from banco_dados.rds_manager import RDSManager
from banco_dados.dynamodb_manager import DynamoDBManager

rds = RDSManager()
dynamo = DynamoDBManager()
```

### Projeto - Custos

```python
from projeto_custos.cost_explorer import CostExplorer
from projeto_custos.resource_cleanup import ResourceCleanup
from projeto_custos.automation import CostAutomation

explorer = CostExplorer()
cleanup = ResourceCleanup()
automation = CostAutomation()
```

## 4. Exemplos de Uso

### EC2 - Criar Instância

```bash
cd ec2
python create_instances.py
```

### S3 - Gerenciar Buckets

```bash
cd s3
python bucket_manager.py
```

### Análise de Custos

```bash
cd projeto_custos
python cost_explorer.py
python reports.py
```

## 5. Referências

- [AWS SDK Boto3](https://boto3.amazonaws.com/)
- [Documentação AWS](https://docs.aws.amazon.com/)
- [AWS CLI](https://aws.amazon.com/cli/)
- [AWS Free Tier](https://aws.amazon.com/free/)

## 6. Dicas de Segurança

⚠️ **NUNCA** commit credenciais no repositório
⚠️ Use IAM Roles em produção
⚠️ Habilite MFA na conta AWS
⚠️ Use variáveis de ambiente para secrets
⚠️ Revise regularmente as políticas de segurança

## 7. Troubleshooting

### Erro: "No AWS credentials found"

Verifique se as credenciais estão configuradas:

```bash
aws sts get-caller-identity
```

### Erro: "Access Denied"

Verifique as permissões do IAM:

```bash
aws iam list-attached-user-policies
```

### Erro de Conexão

Verifique a região:

```bash
echo $AWS_DEFAULT_REGION
```
