using System.Data.SqlClient;
using ProjetoConsultas.Models;

namespace ProjetoConsultas.Services;

public class PedidoService
{
    private readonly Database _database;

    public PedidoService(Database database)
    {
        _database = database;
    }

    public void Inserir(Pedido pedido)
    {
        using (var connection = _database.GetConnection())
        {
            connection.Open();
            
            string query = @"
                INSERT INTO Pedidos (UsuarioId, Total, Status)
                VALUES (@UsuarioId, @Total, @Status)
            ";

            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@UsuarioId", pedido.UsuarioId);
                command.Parameters.AddWithValue("@Total", pedido.Total);
                command.Parameters.AddWithValue("@Status", pedido.Status);

                command.ExecuteNonQuery();
                Console.WriteLine($"✓ Pedido inserido com sucesso!");
            }
        }
    }

    public List<Pedido> ObterTodos()
    {
        var pedidos = new List<Pedido>();

        using (var connection = _database.GetConnection())
        {
            connection.Open();
            string query = @"
                SELECT p.Id, p.UsuarioId, p.DataPedido, p.DataEntrega, p.Total, p.Status, u.Nome
                FROM Pedidos p
                INNER JOIN Usuarios u ON p.UsuarioId = u.Id
                ORDER BY p.DataPedido DESC
            ";

            using (var command = new SqlCommand(query, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        pedidos.Add(MapearPedido(reader));
                    }
                }
            }
        }

        return pedidos;
    }

    public Pedido? ObterPorId(int id)
    {
        using (var connection = _database.GetConnection())
        {
            connection.Open();
            string query = @"
                SELECT p.Id, p.UsuarioId, p.DataPedido, p.DataEntrega, p.Total, p.Status, u.Nome
                FROM Pedidos p
                INNER JOIN Usuarios u ON p.UsuarioId = u.Id
                WHERE p.Id = @Id
            ";

            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                        return MapearPedido(reader);
                }
            }
        }

        return null;
    }

    public void AtualizarStatus(int id, string novoStatus)
    {
        var statusValidos = new[] { "PENDENTE", "ENVIADO", "ENTREGUE", "CANCELADO" };
        
        if (!statusValidos.Contains(novoStatus.ToUpper()))
        {
            Console.WriteLine("✗ Status inválido!");
            return;
        }

        using (var connection = _database.GetConnection())
        {
            connection.Open();
            
            string query = @"
                UPDATE Pedidos 
                SET Status = @Status,
                    DataEntrega = CASE WHEN @Status = 'ENTREGUE' THEN GETDATE() ELSE DataEntrega END
                WHERE Id = @Id
            ";

            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@Status", novoStatus.ToUpper());

                int linhasAfetadas = command.ExecuteNonQuery();
                if (linhasAfetadas > 0)
                    Console.WriteLine($"✓ Pedido {id} atualizado para '{novoStatus}'!");
                else
                    Console.WriteLine("✗ Pedido não encontrado!");
            }
        }
    }

    public void Deletar(int id)
    {
        using (var connection = _database.GetConnection())
        {
            connection.Open();
            
            // Primeiro deleta os itens do pedido
            string deleteItens = "DELETE FROM ItensPedido WHERE PedidoId = @Id";
            using (var command = new SqlCommand(deleteItens, connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }

            // Depois deleta o pedido
            string deletePedido = "DELETE FROM Pedidos WHERE Id = @Id";
            using (var command = new SqlCommand(deletePedido, connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                int linhasAfetadas = command.ExecuteNonQuery();
                
                if (linhasAfetadas > 0)
                    Console.WriteLine("✓ Pedido deletado com sucesso!");
                else
                    Console.WriteLine("✗ Pedido não encontrado!");
            }
        }
    }

    public List<Pedido> ObterPorUsuario(int usuarioId)
    {
        var pedidos = new List<Pedido>();

        using (var connection = _database.GetConnection())
        {
            connection.Open();
            string query = @"
                SELECT p.Id, p.UsuarioId, p.DataPedido, p.DataEntrega, p.Total, p.Status, u.Nome
                FROM Pedidos p
                INNER JOIN Usuarios u ON p.UsuarioId = u.Id
                WHERE p.UsuarioId = @UsuarioId
                ORDER BY p.DataPedido DESC
            ";

            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@UsuarioId", usuarioId);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        pedidos.Add(MapearPedido(reader));
                    }
                }
            }
        }

        return pedidos;
    }

    private Pedido MapearPedido(SqlDataReader reader)
    {
        return new Pedido
        {
            Id = reader.GetInt32(0),
            UsuarioId = reader.GetInt32(1),
            DataPedido = reader.GetDateTime(2),
            DataEntrega = reader.IsDBNull(3) ? null : reader.GetDateTime(3),
            Total = reader.GetDecimal(4),
            Status = reader.GetString(5),
            NomeUsuario = reader.GetString(6)
        };
    }
}
