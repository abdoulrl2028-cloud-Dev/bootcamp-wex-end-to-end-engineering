# Como Rodar - Módulo 4: Bancos de Dados

## 📋 Índice
1. [SQL Server - Scripts SQL](#sql-server---scripts-sql)
2. [SQL Server - Projeto C#](#sql-server---projeto-c)
3. [MongoDB](#mongodb)

---

## SQL Server - Scripts SQL

### Pré-requisitos
- SQL Server 2019+ instalado
- SQL Server Management Studio (SSMS) ou Azure Data Studio
- Acesso com permissões de administrador

### 1️⃣ Criar o Banco de Dados

```sql
CREATE DATABASE BootcampDB;
USE BootcampDB;
```

### 2️⃣ Executar Scripts na Ordem

#### Passo 1: Criar Tabelas Básicas
Abra `4-bancos-de-dados/sqlserver/tabelas/01-usuarios.sql` e execute:
```bash
# No SSMS ou Azure Data Studio
# 1. Conecte ao SQL Server
# 2. Selecione o banco BootcampDB
# 3. Abra o arquivo 01-usuarios.sql
# 4. Pressione F5 ou clique em Execute
```

#### Passo 2: Criar Tabelas com Relacionamentos
Execute `02-pedidos.sql`:
```
Mesmo processo do arquivo anterior
```

#### Passo 3: Criar Tabelas de Itens e Produtos
Execute `03-itens-pedido.sql`:
```
Mesmo processo
```

### 3️⃣ Inserir Dados (Operações CRUD)

Execute nesta ordem:

1. **Inserir dados** - Execute `sqlserver/manipulacao/01-insert.sql`
   ```sql
   -- Adiciona usuários de exemplo
   -- Adiciona pedidos e produtos
   ```

2. **Consultar dados** - Execute `sqlserver/manipulacao/02-select.sql`
   ```sql
   -- Testa diferentes tipos de SELECT
   -- JOINs, agregações, CTEs
   ```

3. **Atualizar dados** - Execute `sqlserver/manipulacao/03-update.sql`
   ```sql
   -- Modifica dados existentes
   ```

4. **Deletar dados** - Execute `sqlserver/manipulacao/04-delete.sql`
   ```sql
   -- Remove dados com segurança
   ```

5. **Transações** - Execute `sqlserver/manipulacao/05-transacoes.sql`
   ```sql
   -- Testa operações atômicas
   ```

### Verificar Dados Inseridos

```sql
-- Ver todos os usuários
SELECT * FROM Usuarios;

-- Ver todos os pedidos
SELECT * FROM Pedidos;

-- Ver itens de pedidos
SELECT * FROM ItensPedido;

-- Ver produtos
SELECT * FROM Produtos;
```

---

## SQL Server - Projeto C#

### Pré-requisitos
- .NET 8.0 SDK instalado
- SQL Server rodando e acessível
- Visual Studio Code com extensões C#

### 1️⃣ Configurar Conexão com Banco de Dados

Abra o arquivo `projeto-consultas/Program.cs` e ajuste a string de conexão:

```csharp
// Linha ~1
const string connectionString = "Server=localhost;Database=BootcampDB;User Id=sa;Password=YourPassword123;";
```

**Opcões de configuração:**

| Campo | Valor | Descrição |
|-------|-------|-----------|
| Server | `localhost` | Se SQL Server está local |
| Server | `127.0.0.1` | Alternativa |
| Server | `.` | Ponto (apenas Windows) |
| Database | `BootcampDB` | Nome do banco criado |
| User Id | `sa` | Usuário admin do SQL Server |
| Password | `YourPassword123` | Senha do usuário |

### 2️⃣ Restaurar Dependências

Abra um terminal na pasta do projeto:

```bash
cd 4-bancos-de-dados/sqlserver/projeto-consultas

# Restaurar pacotes NuGet
dotnet restore
```

### 3️⃣ Compilar o Projeto

```bash
dotnet build
```

**Saída esperada:**
```
Build succeeded. 0 Warning(s)
```

### 4️⃣ Executar o Projeto

```bash
dotnet run
```

**Primeira execução:**
- O programa criará as tabelas automaticamente
- Mostrará: ✓ Conexão com banco de dados estabelecida com sucesso!
- Mostrará: ✓ Tabelas criadas/atualizadas com sucesso!

### 5️⃣ Usar o Menu Interativo

```
========== SISTEMA DE GERENCIAMENTO ==========
1. Gerenciar Usuários
2. Gerenciar Produtos
3. Gerenciar Pedidos
4. Relatórios
0. Sair
Escolha uma opção: 
```

#### Exemplo: Adicionar um Usuário

```
Escolha uma opção: 1
--- USUÁRIOS ---
1. Listar Usuários
2. Buscar por ID
3. Inserir Novo Usuário
4. Atualizar Usuário
5. Deletar Usuário
0. Voltar
Escolha: 3

Nome: João Silva
Email: joao@email.com
Telefone: 11999999999
CPF: 123.456.789-00

✓ Usuário 'João Silva' inserido com sucesso!
```

#### Exemplo: Ver Relatório de Vendas

```
Escolha uma opção: 4
--- RELATÓRIOS ---
1. Relatório de Vendas
2. Clientes com Mais Vendas
3. Produtos Mais Vendidos
4. Status de Pedidos
5. Relatório de Estoque
0. Voltar
Escolha: 1

=== RELATÓRIO DE VENDAS ===
Total de Pedidos: 5
Total de Vendas: R$ 1.250,00
Ticket Médio: R$ 250,00
Maior Pedido: R$ 500,00
Menor Pedido: R$ 100,00
```

### Estrutura do Projeto C#

```
projeto-consultas/
├── Program.cs              # Menu principal e entrada
├── Database.cs             # Conexão e inicialização
├── Models.cs               # Classes Usuario, Produto, Pedido
├── UsuarioService.cs       # CRUD de usuários
├── ProdutoService.cs       # CRUD de produtos
├── PedidoService.cs        # CRUD de pedidos
├── RelatorioService.cs     # Relatórios e análises
└── projeto-consultas.csproj # Configuração do projeto
```

### Troubleshooting - Erros Comuns

#### ❌ "Cannot open database 'BootcampDB'"
```
Solução: Crie o banco de dados primeiro
sqlserver/tabelas/01-usuarios.sql
```

#### ❌ "Login failed for user 'sa'"
```
Solução: Verifique senha no Program.cs
const string connectionString = "...Password=SuaSenha;";
```

#### ❌ "Connection timeout expired"
```
Solução: Certifique-se que SQL Server está rodando
Windows: Services > SQL Server (MSSQLSERVER)
Linux/Mac: docker ps (se usar container)
```

#### ❌ "Package not found"
```
Solução: Execute
dotnet restore
```

---

## MongoDB

### Pré-requisitos
- MongoDB Community Edition 5.0+
- MongoDB Compass (interface gráfica - opcional)
- mongosh (shell do MongoDB)

### 1️⃣ Iniciar MongoDB

#### No Windows
```bash
# Via PowerShell como Admin
mongod
```

#### No macOS
```bash
brew services start mongodb-community
```

#### No Linux (Docker - Recomendado)
```bash
docker run -d -p 27017:27017 --name mongodb mongo:latest
```

#### Verificar se está rodando
```bash
mongosh
```

Saída esperada:
```
Current Namespace: test>
```

### 2️⃣ Conectar ao MongoDB

```bash
# Abrir shell MongoDB
mongosh

# Ou com URI completa
mongosh "mongodb://localhost:27017"
```

### 3️⃣ Criar Banco e Coleções

```javascript
// Criar/usar banco de dados
use bootcamp_db

// Verificar banco selecionado
db

// Listar todos os bancos
show dbs

// Listar coleções
show collections
```

### 4️⃣ Executar Scripts de Exemplo

#### Operações com Usuários
```bash
# Copie o conteúdo de 01-usuarios.js
# Cole no shell MongoDB

mongosh < 4-bancos-de-dados/mongodb/01-usuarios.js
```

Ou cole diretamente no mongosh:
```javascript
// Cole o conteúdo de 01-usuarios.js aqui
db.usuarios.insertOne({
    nome: "João Silva",
    email: "joao@email.com",
    telefone: "11999999999",
    cpf: "123.456.789-00",
    ativo: true,
    dataCriacao: new Date()
});

// Ver resultado
db.usuarios.find().pretty();
```

#### Operações com Pedidos
```javascript
// Similarmente, execute 02-pedidos.js
mongosh < 4-bancos-de-dados/mongodb/02-pedidos.js
```

#### Agregações Avançadas
```javascript
// Veja exemplos em 03-agregacoes.js
mongosh < 4-bancos-de-dados/mongodb/03-agregacoes.js
```

#### Índices e Otimização
```javascript
// Execute 04-indices.js
mongosh < 4-bancos-de-dados/mongodb/04-indices.js
```

### 5️⃣ Comandos Úteis MongoDB

```javascript
// Conectar a um banco
use bootcamp_db

// Inserir um documento
db.usuarios.insertOne({
    nome: "Maria",
    email: "maria@email.com",
    ativo: true
});

// Buscar todos
db.usuarios.find();

// Buscar com filtro
db.usuarios.find({ ativo: true });

// Formatar melhor
db.usuarios.find().pretty();

// Contar documentos
db.usuarios.countDocuments();

// Atualizar
db.usuarios.updateOne(
    { email: "maria@email.com" },
    { $set: { nome: "Maria Silva" } }
);

// Deletar
db.usuarios.deleteOne({ email: "maria@email.com" });

// Ver índices
db.usuarios.getIndexes();

// Criar índice
db.usuarios.createIndex({ email: 1 });

// Explicar query (ver performance)
db.usuarios.find({ email: "maria@email.com" }).explain("executionStats");
```

### Usar MongoDB Compass (GUI)

1. Baixe em https://www.mongodb.com/products/compass
2. Instale
3. Abra Compass
4. Clique em "New Connection"
5. Use: `mongodb://localhost:27017`
6. Clique em "Connect"
7. Veja bancos e coleções na interface

### Troubleshooting - MongoDB

#### ❌ "Connection refused"
```
Solução: Verifique se MongoDB está rodando
mongosh
```

#### ❌ "No suitable servers found"
```
Solução: Inicie MongoDB
Windows: mongod
macOS: brew services start mongodb-community
Docker: docker run -d -p 27017:27017 mongo
```

#### ❌ "Command not found: mongosh"
```
Solução: Instale MongoDB Shell
npm install -g mongosh
ou
winget install mongodb.mongosh
```

---

## 📊 Resumo Rápido

### SQL Server Scripts (Terminal/SSMS)
```bash
# 1. Crie banco: BootcampDB
# 2. Execute: 01-usuarios.sql
# 3. Execute: 02-pedidos.sql
# 4. Execute: 03-itens-pedido.sql
# 5. Execute: 01-insert.sql até 05-transacoes.sql
```

### SQL Server + C# (Terminal)
```bash
cd 4-bancos-de-dados/sqlserver/projeto-consultas
dotnet restore
dotnet build
dotnet run
```

### MongoDB (Terminal)
```bash
mongosh
use bootcamp_db
# Cole scripts de 01-usuarios.js até 04-indices.js
```

---

## 🚀 Próximos Passos

1. ✅ Criar banco de dados
2. ✅ Executar scripts SQL
3. ✅ Rodar projeto C#
4. ✅ Explorar dados em MongoDB
5. ✅ Executar relatórios
6. 📚 Estudar conceitos de banco de dados
7. 🔧 Criar seus próprios projetos

---

**Dúvidas?** Consulte os READMEs específicos em cada pasta!
