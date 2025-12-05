using System.Data.SqlClient;
using ProjetoConsultas.Models;

namespace ProjetoConsultas.Services;

public class ProdutoService
{
    private readonly Database _database;

    public ProdutoService(Database database)
    {
        _database = database;
    }

    public void Inserir(Produto produto)
    {
        using (var connection = _database.GetConnection())
        {
            connection.Open();
            
            string query = @"
                INSERT INTO Produtos (Nome, Descricao, Preco, Estoque, Ativo)
                VALUES (@Nome, @Descricao, @Preco, @Estoque, @Ativo)
            ";

            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Nome", produto.Nome);
                command.Parameters.AddWithValue("@Descricao", produto.Descricao ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Preco", produto.Preco);
                command.Parameters.AddWithValue("@Estoque", produto.Estoque);
                command.Parameters.AddWithValue("@Ativo", produto.Ativo);

                command.ExecuteNonQuery();
                Console.WriteLine($"✓ Produto '{produto.Nome}' inserido com sucesso!");
            }
        }
    }

    public List<Produto> ObterTodos()
    {
        var produtos = new List<Produto>();

        using (var connection = _database.GetConnection())
        {
            connection.Open();
            string query = "SELECT Id, Nome, Descricao, Preco, Estoque, Ativo, DataCriacao FROM Produtos WHERE Ativo = 1 ORDER BY Nome";

            using (var command = new SqlCommand(query, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        produtos.Add(new Produto
                        {
                            Id = reader.GetInt32(0),
                            Nome = reader.GetString(1),
                            Descricao = reader.IsDBNull(2) ? null : reader.GetString(2),
                            Preco = reader.GetDecimal(3),
                            Estoque = reader.GetInt32(4),
                            Ativo = reader.GetBoolean(5),
                            DataCriacao = reader.GetDateTime(6)
                        });
                    }
                }
            }
        }

        return produtos;
    }

    public Produto? ObterPorId(int id)
    {
        using (var connection = _database.GetConnection())
        {
            connection.Open();
            string query = "SELECT Id, Nome, Descricao, Preco, Estoque, Ativo, DataCriacao FROM Produtos WHERE Id = @Id";

            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Produto
                        {
                            Id = reader.GetInt32(0),
                            Nome = reader.GetString(1),
                            Descricao = reader.IsDBNull(2) ? null : reader.GetString(2),
                            Preco = reader.GetDecimal(3),
                            Estoque = reader.GetInt32(4),
                            Ativo = reader.GetBoolean(5),
                            DataCriacao = reader.GetDateTime(6)
                        };
                    }
                }
            }
        }

        return null;
    }

    public void Atualizar(Produto produto)
    {
        using (var connection = _database.GetConnection())
        {
            connection.Open();
            
            string query = @"
                UPDATE Produtos 
                SET Nome = @Nome, 
                    Descricao = @Descricao, 
                    Preco = @Preco, 
                    Estoque = @Estoque, 
                    Ativo = @Ativo
                WHERE Id = @Id
            ";

            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", produto.Id);
                command.Parameters.AddWithValue("@Nome", produto.Nome);
                command.Parameters.AddWithValue("@Descricao", produto.Descricao ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Preco", produto.Preco);
                command.Parameters.AddWithValue("@Estoque", produto.Estoque);
                command.Parameters.AddWithValue("@Ativo", produto.Ativo);

                int linhasAfetadas = command.ExecuteNonQuery();
                if (linhasAfetadas > 0)
                    Console.WriteLine($"✓ Produto atualizado com sucesso!");
                else
                    Console.WriteLine("✗ Produto não encontrado!");
            }
        }
    }

    public void Deletar(int id)
    {
        using (var connection = _database.GetConnection())
        {
            connection.Open();
            
            string query = "DELETE FROM Produtos WHERE Id = @Id";

            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);

                int linhasAfetadas = command.ExecuteNonQuery();
                if (linhasAfetadas > 0)
                    Console.WriteLine("✓ Produto deletado com sucesso!");
                else
                    Console.WriteLine("✗ Produto não encontrado!");
            }
        }
    }

    public List<Produto> BuscarPorNome(string nome)
    {
        var produtos = new List<Produto>();

        using (var connection = _database.GetConnection())
        {
            connection.Open();
            string query = "SELECT Id, Nome, Descricao, Preco, Estoque, Ativo, DataCriacao FROM Produtos WHERE Nome LIKE @Nome AND Ativo = 1";

            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Nome", $"%{nome}%");

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        produtos.Add(new Produto
                        {
                            Id = reader.GetInt32(0),
                            Nome = reader.GetString(1),
                            Descricao = reader.IsDBNull(2) ? null : reader.GetString(2),
                            Preco = reader.GetDecimal(3),
                            Estoque = reader.GetInt32(4),
                            Ativo = reader.GetBoolean(5),
                            DataCriacao = reader.GetDateTime(6)
                        });
                    }
                }
            }
        }

        return produtos;
    }
}
