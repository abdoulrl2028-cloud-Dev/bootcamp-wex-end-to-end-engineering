# Bootcamp Wex - End to End Engineering

Repositório completo com módulos de aprendizado em **.NET**, **C#**, **Docker**, **Kubernetes** e **AWS Cloud**.

## 📚 Estrutura do Bootcamp

### 1. Fundamentos .NET
- Conceitos básicos de .NET
- Tipos de dados
- Coleções
- Projeto Portfolio

[📖 Ir para Fundamentos .NET](./1-fundamentos-dotnet/)

### 2. Sintaxe C#
- Arrays e Listas
- Operadores
- Estruturas de Repetição
- Projeto: Estacionamento

[📖 Ir para Sintaxe C#](./2-sintaxe-csharp/)

### 3. Programação Orientada a Objetos (POO)
- Encapsulamento
- Herança
- Polimorfismo
- Abstração
- Projeto: Celular

[📖 Ir para POO C#](./3-poo-csharp/)

### 4. Bancos de Dados
- **SQL Server**: Manipulação e Consultas
- **MongoDB**: Documentos e Agregações
- Índices e Performance

[📖 Ir para Bancos de Dados](./4-bancos-de-dados/)

### 5. Qualidade de Software
- Automação de Testes
- Testes Manuais
- Fundamentos de QA
- Projeto QA: Calculadora

[📖 Ir para Qualidade de Software](./5-qualidade-software/)

### 6. Docker & Kubernetes
- Docker Basics
- Dockerfiles
- Docker Compose
- Kubernetes Deployments
- Redes e Volumes

[📖 Ir para Docker & Kubernetes](./6-docker-kubernetes/)

### 7. AWS Cloud ⭐ **NOVO**
Módulo completo de AWS com exemplos práticos:

#### 📍 **EC2 - Computação**
- Criar e gerenciar instâncias
- Security Groups
- Elastic IPs
- Auto Scaling

```python
from ec2.create_instances import EC2Manager

manager = EC2Manager()
instances = manager.list_instances()
```

#### 📍 **S3 - Armazenamento**
- Gerenciamento de buckets
- Upload/Download de objetos
- Versionamento
- Encriptação

```python
from s3.bucket_manager import S3BucketManager

s3 = S3BucketManager()
s3.create_bucket('meu-bucket')
```

#### 📍 **Redes - VPC**
- VPCs e Subnets
- Internet Gateway
- Route Tables
- Load Balancers

```python
from redes.vpc_manager import VPCManager

vpc = VPCManager()
vpc_id = vpc.create_vpc('10.0.0.0/16', 'minha-vpc')
```

#### 📍 **Banco de Dados**
- RDS (PostgreSQL, MySQL, etc)
- DynamoDB (NoSQL)
- Backups e Snapshots
- Read Replicas

```python
from banco_dados.rds_manager import RDSManager

rds = RDSManager()
rds.create_db_instance('postgres', 'meu-postgres')
```

#### 📍 **Projeto: Otimização de Custos** 💰
- Análise de custos em tempo real
- Identificação de recursos ociosos
- Automação de economia
- Geração de relatórios

```python
from projeto_custos.cost_explorer import CostExplorer

explorer = CostExplorer()
explorer.generate_cost_report()
```

[📖 Ir para AWS Cloud](./7-aws-cloud/)

## 🚀 Quick Start

### Pré-requisitos
- .NET SDK
- Python 3.8+
- Docker
- Git

### Instalação

```bash
# Clonar repositório
git clone https://github.com/abdoulrl2028-cloud-Dev/bootcamp-wex-end-to-end-engineering.git
cd bootcamp-wex-end-to-end-engineering

# Para módulos .NET
dotnet restore

# Para módulos Python/AWS
cd 7-aws-cloud
pip install -r requirements.txt
```

### Configurar AWS (Opcional)

```bash
aws configure
# Inserir suas credenciais AWS
```

## 📋 Arquivos Principais

```
bootcamp-wex-end-to-end-engineering/
├── 1-fundamentos-dotnet/        # Fundamentos .NET
├── 2-sintaxe-csharp/            # Sintaxe C#
├── 3-poo-csharp/                # POO em C#
├── 4-bancos-de-dados/           # SQL Server e MongoDB
├── 5-qualidade-software/        # Testes e QA
├── 6-docker-kubernetes/         # Docker e Kubernetes
├── 7-aws-cloud/                 # ⭐ AWS Cloud (NOVO)
│   ├── ec2/                     # EC2 Instances
│   ├── s3/                      # S3 Storage
│   ├── redes/                   # VPC e Networking
│   ├── banco-dados/             # RDS e DynamoDB
│   ├── projeto-custos/          # Cost Optimization
│   ├── requirements.txt         # Dependências Python
│   ├── SETUP.md                 # Guia de setup
│   └── README.md                # Documentação
├── GUIA-POO.md                  # Guia POO
├── README.md                    # Este arquivo
└── *.sln                        # Solução Visual Studio
```

## 🛠️ Tecnologias Utilizadas

### Backend
- **.NET 7+**
- **C# 11**
- **Entity Framework Core**

### Banco de Dados
- **SQL Server**
- **MongoDB**

### Cloud & DevOps
- **AWS** (EC2, S3, RDS, DynamoDB, VPC)
- **Docker**
- **Kubernetes**

### Qualidade
- **xUnit** / **NUnit**
- **pytest**

### Ferramentas
- **Git**
- **Visual Studio Code**
- **Visual Studio 2022**

## 📖 Documentação

Cada módulo contém um arquivo `README.md` com:
- Conceitos explicados
- Exemplos de código
- Exercícios práticos
- Links de referência

## 🎯 Objetivos do Bootcamp

✅ Dominar fundamentos de .NET e C#  
✅ Aprender Programação Orientada a Objetos  
✅ Trabalhar com Bancos de Dados Relacionais e NoSQL  
✅ Implementar testes e qualidade de código  
✅ Containerizar aplicações com Docker  
✅ Orquestrar com Kubernetes  
✅ Fazer Deploy na AWS Cloud  
✅ Otimizar custos e performance  

## 💡 Dicas

- Comece pelos fundamentos e progresse gradualmente
- Realize os projetos práticos para consolidar o aprendizado
- Revise os READMEs para entender conceitos
- Experimente com código - brinque com exemplos!
- Use o Git para versionamento

## 🤝 Contribuições

Este é um repositório educacional. Sugestões e melhorias são bem-vindas!

## 📞 Suporte

Para dúvidas:
1. Consulte a documentação em cada módulo
2. Verifique os arquivos README
3. Revise os exemplos de código
4. Consulte as referências oficiais

## 📄 Licença

MIT License - Veja LICENSE para detalhes

---

**Happy Coding!** 🚀

Desenvolvido com ❤️ para o Bootcamp Wex
