# EC2 - Elastic Compute Cloud

Serviço de computação em nuvem que oferece instâncias redimensionáveis.

## Tópicos

1. **Criar e gerenciar instâncias EC2**
2. **Security Groups e regras de firewall**
3. **Pares de chaves (Key Pairs)**
4. **Elastic IPs**
5. **Auto Scaling Groups**
6. **AMIs (Amazon Machine Images)**

## Arquivos

- `create_instances.py`: Criar instâncias EC2
- `manage_instances.py`: Gerenciar ciclo de vida
- `security_groups.py`: Configurar grupos de segurança
- `auto_scaling.py`: Configurar auto scaling

## Exemplo Básico

```python
import boto3

ec2 = boto3.client('ec2')

# Criar uma instância
response = ec2.run_instances(
    ImageId='ami-0c55b159cbfafe1f0',
    MinCount=1,
    MaxCount=1,
    InstanceType='t2.micro'
)

print(f"Instância criada: {response['Instances'][0]['InstanceId']}")
```
