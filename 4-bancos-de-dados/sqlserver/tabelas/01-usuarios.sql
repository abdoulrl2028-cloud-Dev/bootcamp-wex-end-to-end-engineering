-- Criar tabela de Usuários
CREATE TABLE Usuarios (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nome VARCHAR(100) NOT NULL,
    Email VARCHAR(100) UNIQUE NOT NULL,
    Telefone VARCHAR(15),
    CPF VARCHAR(14) UNIQUE,
    Ativo BIT DEFAULT 1,
    DataCriacao DATETIME DEFAULT GETDATE(),
    DataAtualizacao DATETIME DEFAULT GETDATE()
);

-- Índice para busca por email (otimização)
CREATE INDEX IDX_Usuarios_Email ON Usuarios(Email);

-- Índice para filtrar usuários ativos
CREATE INDEX IDX_Usuarios_Ativo ON Usuarios(Ativo);
