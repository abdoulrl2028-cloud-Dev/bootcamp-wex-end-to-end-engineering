# Manipulação de Dados SQL Server

## Objetivo
Aprender operações CRUD (Create, Read, Update, Delete) no SQL Server com exemplos práticos.

## Conteúdo

### 1. INSERT - Inserir dados
```sql
INSERT INTO Usuarios (Nome, Email, CPF) 
VALUES ('João Silva', 'joao@email.com', '123.456.789-00');
```

### 2. SELECT - Ler dados
```sql
SELECT * FROM Usuarios WHERE Ativo = 1;
```

### 3. UPDATE - Atualizar dados
```sql
UPDATE Usuarios 
SET Nome = 'João Pedro Silva' 
WHERE Id = 1;
```

### 4. DELETE - Deletar dados
```sql
DELETE FROM Usuarios WHERE Id = 1;
```

## Transações

Para garantir integridade dos dados:
```sql
BEGIN TRANSACTION;
-- Comandos SQL
COMMIT;
-- ou
ROLLBACK;
```

## Arquivos

- `01-insert.sql`: Inserção de dados
- `02-select.sql`: Consultas básicas e avançadas
- `03-update.sql`: Atualização de dados
- `04-delete.sql`: Exclusão de dados
- `05-transacoes.sql`: Operações com transações
