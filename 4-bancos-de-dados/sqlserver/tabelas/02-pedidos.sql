-- Criar tabela de Pedidos
CREATE TABLE Pedidos (
    Id INT PRIMARY KEY IDENTITY(1,1),
    UsuarioId INT NOT NULL,
    DataPedido DATETIME DEFAULT GETDATE(),
    DataEntrega DATETIME,
    Total DECIMAL(10,2) NOT NULL,
    Status VARCHAR(20) DEFAULT 'PENDENTE',
    FOREIGN KEY (UsuarioId) REFERENCES Usuarios(Id),
    CONSTRAINT CHK_Status CHECK (Status IN ('PENDENTE', 'ENVIADO', 'ENTREGUE', 'CANCELADO'))
);

-- Índice para busca por usuário
CREATE INDEX IDX_Pedidos_UsuarioId ON Pedidos(UsuarioId);

-- Índice para filtrar por status
CREATE INDEX IDX_Pedidos_Status ON Pedidos(Status);
