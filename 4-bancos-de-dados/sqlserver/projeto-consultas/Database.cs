using System.Data.SqlClient;

namespace ProjetoConsultas;

public class Database
{
    private readonly string _connectionString;

    public Database(string connectionString)
    {
        _connectionString = connectionString;
    }

    public SqlConnection GetConnection()
    {
        return new SqlConnection(_connectionString);
    }

    public void InitializeDatabase()
    {
        try
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                Console.WriteLine("✓ Conexão com banco de dados estabelecida com sucesso!");
                
                ExecuteScript(connection, CreateTablesScript());
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ Erro ao conectar ao banco de dados: {ex.Message}");
            throw;
        }
    }

    private void ExecuteScript(SqlConnection connection, string script)
    {
        try
        {
            using (var command = new SqlCommand(script, connection))
            {
                command.CommandTimeout = 300;
                command.ExecuteNonQuery();
                Console.WriteLine("✓ Tabelas criadas/atualizadas com sucesso!");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"! Tabelas podem já existir ou: {ex.Message}");
        }
    }

    private string CreateTablesScript()
    {
        return @"
            IF OBJECT_ID('ItensPedido', 'U') IS NOT NULL DROP TABLE ItensPedido;
            IF OBJECT_ID('Pedidos', 'U') IS NOT NULL DROP TABLE Pedidos;
            IF OBJECT_ID('Produtos', 'U') IS NOT NULL DROP TABLE Produtos;
            IF OBJECT_ID('Usuarios', 'U') IS NOT NULL DROP TABLE Usuarios;

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

            CREATE INDEX IDX_Usuarios_Email ON Usuarios(Email);
            CREATE INDEX IDX_Usuarios_Ativo ON Usuarios(Ativo);

            CREATE TABLE Produtos (
                Id INT PRIMARY KEY IDENTITY(1,1),
                Nome VARCHAR(200) NOT NULL,
                Descricao TEXT,
                Preco DECIMAL(10,2) NOT NULL,
                Estoque INT DEFAULT 0,
                Ativo BIT DEFAULT 1,
                DataCriacao DATETIME DEFAULT GETDATE()
            );

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

            CREATE INDEX IDX_Pedidos_UsuarioId ON Pedidos(UsuarioId);
            CREATE INDEX IDX_Pedidos_Status ON Pedidos(Status);

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

            CREATE INDEX IDX_ItensPedido_PedidoId ON ItensPedido(PedidoId);
            CREATE INDEX IDX_ItensPedido_ProdutoId ON ItensPedido(ProdutoId);
        ";
    }
}
