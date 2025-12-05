# MongoDB

## Objetivo
Aprender a trabalhar com banco de dados NoSQL usando MongoDB com documentos JSON.

## Conteúdo

### 1. Inserção de Documentos
```javascript
db.usuarios.insertOne({
    nome: "João Silva",
    email: "joao@email.com",
    idade: 30,
    endereco: {
        rua: "Rua A",
        cidade: "São Paulo",
        pais: "Brasil"
    }
})
```

### 2. Consultas
```javascript
db.usuarios.find({ idade: { $gte: 18 } })
```

### 3. Atualização
```javascript
db.usuarios.updateOne(
    { _id: ObjectId("...") },
    { $set: { idade: 31 } }
)
```

### 4. Exclusão
```javascript
db.usuarios.deleteOne({ _id: ObjectId("...") })
```

## Características do MongoDB

- **Documentos**: Dados no formato JSON/BSON
- **Coleções**: Equivalente a tabelas em BD relacionais
- **Schema Flexível**: Não requer estrutura fixa
- **Índices**: Melhoram performance de queries
- **Agregação**: Consultas complexas com $lookup, $group, etc.

## Diferenças com SQL Server

| SQL Server | MongoDB |
|-----------|---------|
| Tabelas | Coleções |
| Linhas | Documentos |
| Colunas | Campos |
| JOINs | $lookup |
| GROUP BY | $group |
| Transações ACID | Multi-document transactions |

## Arquivos de Exemplo

- `01-usuarios.js`: Operações com usuários
- `02-pedidos.js`: Operações com pedidos
- `03-agregacoes.js`: Queries avançadas com agregação
- `04-indices.js`: Criação e otimização de índices
