# Redes - VPC, Subnets e Load Balancers

Configuração de infraestrutura de rede na AWS.

## Tópicos

1. **VPC (Virtual Private Cloud)**
2. **Subnets (Públicas e Privadas)**
3. **Internet Gateway**
4. **Route Tables**
5. **Security Groups**
6. **Network ACLs**
7. **Elastic Load Balancer (ELB)**
8. **Application Load Balancer (ALB)**

## Arquivos

- `vpc_manager.py`: Gerenciar VPCs e subnets
- `load_balancer.py`: Configurar load balancers
- `nat_gateway.py`: Configurar NAT Gateway

## Exemplo de Arquitetura

```
VPC (10.0.0.0/16)
├── Subnet Pública (10.0.1.0/24)
│   ├── Internet Gateway
│   └── Instâncias EC2 com IP público
├── Subnet Privada (10.0.2.0/24)
│   ├── NAT Gateway
│   └── Instâncias EC2 sem IP público
└── Load Balancer
    ├── Porta 80
    └── Porta 443
```
