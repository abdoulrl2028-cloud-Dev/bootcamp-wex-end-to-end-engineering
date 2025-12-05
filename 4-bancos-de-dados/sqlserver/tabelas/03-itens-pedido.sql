-- Criar tabela de Produtos
CREATE TABLE Produtos (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nome VARCHAR(200) NOT NULL,
    Descricao TEXT,
    Preco DECIMAL(10,2) NOT NULL,
    Estoque INT DEFAULT 0,
    Ativo BIT DEFAULT 1,
    DataCriacao DATETIME DEFAULT GETDATE()
);

-- Criar tabela de Itens do Pedido
CREATE TABLE ItensPedido (
    Id INT PRIMARY KEY IDENTITY(1,1),
    PedidoId INT NOT NULL,
    ProdutoId INT NOT NULL,
    Quantidade INT NOT NULL,
    PrecoUnitario DECIMAL(10,2) NOT NULL,
    Subtotal DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (PedidoId) REFERENCES Pedidos(Id),
    FOREIGN KEY (ProdutoId) REFERENCES Produtos(Id),
    CONSTRAINT CHK_Quantidade CHECK (Quantidade > 0)
);

-- Índices
CREATE INDEX IDX_ItensPedido_PedidoId ON ItensPedido(PedidoId);
CREATE INDEX IDX_ItensPedido_ProdutoId ON ItensPedido(ProdutoId);
