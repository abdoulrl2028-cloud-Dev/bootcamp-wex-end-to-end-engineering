# Projeto Consultas SQL Server

## Objetivo
Projeto prático integrando C# com SQL Server para realizar operações de banco de dados.

## Funcionalidades

- Conexão com SQL Server
- CRUD de Usuários
- CRUD de Pedidos
- Relatórios e consultasavançadas
- ADO.NET e Entity Framework

## Estrutura

```
projeto-consultas/
├── projeto-consultas.csproj
├── Program.cs
├── Database.cs
├── Models/
│   ├── Usuario.cs
│   ├── Pedido.cs
│   └── Produto.cs
├── Services/
│   ├── UsuarioService.cs
│   ├── PedidoService.cs
│   └── RelatorioService.cs
└── Scripts/
    └── setup-database.sql
```

## Conexão com Banco de Dados

String de conexão:
```
Server=localhost;Database=BootcampDB;User Id=sa;Password=YourPassword;
```

## Como Executar

1. Configurar SQL Server
2. Criar banco de dados
3. Executar scripts SQL
4. Compilar e executar o projeto C#
