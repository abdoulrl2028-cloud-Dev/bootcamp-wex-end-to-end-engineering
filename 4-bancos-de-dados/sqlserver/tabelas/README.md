# Tabelas SQL Server

## Objetivo
Aprender a criar e estruturar tabelas no SQL Server com relacionamentos, constraints e integridade referencial.

## Conteúdo

### 1. Criação Básica de Tabelas
```sql
CREATE TABLE Usuarios (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nome VARCHAR(100) NOT NULL,
    Email VARCHAR(100) UNIQUE NOT NULL,
    DataCriacao DATETIME DEFAULT GETDATE()
);
```

### 2. Relacionamentos
```sql
CREATE TABLE Pedidos (
    Id INT PRIMARY KEY IDENTITY(1,1),
    UsuarioId INT NOT NULL,
    DataPedido DATETIME DEFAULT GETDATE(),
    Total DECIMAL(10,2),
    FOREIGN KEY (UsuarioId) REFERENCES Usuarios(Id)
);
```

### 3. Constraints
- PRIMARY KEY: Chave primária única
- FOREIGN KEY: Referência a outra tabela
- UNIQUE: Valores únicos
- NOT NULL: Campo obrigatório
- DEFAULT: Valor padrão
- CHECK: Validação de condições

## Arquivos

- `01-usuarios.sql`: Tabela de usuários
- `02-pedidos.sql`: Tabela de pedidos com relacionamento
- `03-itens-pedido.sql`: Tabela de itens do pedido
