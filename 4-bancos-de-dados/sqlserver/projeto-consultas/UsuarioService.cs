using System.Data.SqlClient;
using ProjetoConsultas.Models;

namespace ProjetoConsultas.Services;

public class UsuarioService
{
    private readonly Database _database;

    public UsuarioService(Database database)
    {
        _database = database;
    }

    // CREATE
    public void Inserir(Usuario usuario)
    {
        using (var connection = _database.GetConnection())
        {
            connection.Open();
            
            string query = @"
                INSERT INTO Usuarios (Nome, Email, Telefone, CPF, Ativo)
                VALUES (@Nome, @Email, @Telefone, @CPF, @Ativo)
            ";

            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Nome", usuario.Nome);
                command.Parameters.AddWithValue("@Email", usuario.Email);
                command.Parameters.AddWithValue("@Telefone", usuario.Telefone ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@CPF", usuario.CPF ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Ativo", usuario.Ativo);

                command.ExecuteNonQuery();
                Console.WriteLine($"✓ Usuário '{usuario.Nome}' inserido com sucesso!");
            }
        }
    }

    // READ - Obter todos
    public List<Usuario> ObterTodos()
    {
        var usuarios = new List<Usuario>();

        using (var connection = _database.GetConnection())
        {
            connection.Open();
            string query = "SELECT Id, Nome, Email, Telefone, CPF, Ativo, DataCriacao, DataAtualizacao FROM Usuarios ORDER BY Nome";

            using (var command = new SqlCommand(query, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        usuarios.Add(MapearUsuario(reader));
                    }
                }
            }
        }

        return usuarios;
    }

    // READ - Obter por ID
    public Usuario? ObterPorId(int id)
    {
        using (var connection = _database.GetConnection())
        {
            connection.Open();
            string query = "SELECT Id, Nome, Email, Telefone, CPF, Ativo, DataCriacao, DataAtualizacao FROM Usuarios WHERE Id = @Id";

            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                        return MapearUsuario(reader);
                }
            }
        }

        return null;
    }

    // UPDATE
    public void Atualizar(Usuario usuario)
    {
        using (var connection = _database.GetConnection())
        {
            connection.Open();
            
            string query = @"
                UPDATE Usuarios 
                SET Nome = @Nome, 
                    Email = @Email, 
                    Telefone = @Telefone, 
                    CPF = @CPF, 
                    Ativo = @Ativo,
                    DataAtualizacao = GETDATE()
                WHERE Id = @Id
            ";

            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", usuario.Id);
                command.Parameters.AddWithValue("@Nome", usuario.Nome);
                command.Parameters.AddWithValue("@Email", usuario.Email);
                command.Parameters.AddWithValue("@Telefone", usuario.Telefone ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@CPF", usuario.CPF ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Ativo", usuario.Ativo);

                int linhasAfetadas = command.ExecuteNonQuery();
                if (linhasAfetadas > 0)
                    Console.WriteLine($"✓ Usuário atualizado com sucesso!");
                else
                    Console.WriteLine("✗ Usuário não encontrado!");
            }
        }
    }

    // DELETE
    public void Deletar(int id)
    {
        using (var connection = _database.GetConnection())
        {
            connection.Open();
            
            string query = "DELETE FROM Usuarios WHERE Id = @Id";

            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);

                int linhasAfetadas = command.ExecuteNonQuery();
                if (linhasAfetadas > 0)
                    Console.WriteLine("✓ Usuário deletado com sucesso!");
                else
                    Console.WriteLine("✗ Usuário não encontrado!");
            }
        }
    }

    // Buscar por Email
    public Usuario? BuscarPorEmail(string email)
    {
        using (var connection = _database.GetConnection())
        {
            connection.Open();
            string query = "SELECT Id, Nome, Email, Telefone, CPF, Ativo, DataCriacao, DataAtualizacao FROM Usuarios WHERE Email = @Email";

            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Email", email);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                        return MapearUsuario(reader);
                }
            }
        }

        return null;
    }

    // Contar usuários ativos
    public int ContarAtivos()
    {
        using (var connection = _database.GetConnection())
        {
            connection.Open();
            string query = "SELECT COUNT(*) FROM Usuarios WHERE Ativo = 1";

            using (var command = new SqlCommand(query, connection))
            {
                return (int)command.ExecuteScalar();
            }
        }
    }

    private Usuario MapearUsuario(SqlDataReader reader)
    {
        return new Usuario
        {
            Id = reader.GetInt32(0),
            Nome = reader.GetString(1),
            Email = reader.GetString(2),
            Telefone = reader.IsDBNull(3) ? null : reader.GetString(3),
            CPF = reader.IsDBNull(4) ? null : reader.GetString(4),
            Ativo = reader.GetBoolean(5),
            DataCriacao = reader.GetDateTime(6),
            DataAtualizacao = reader.GetDateTime(7)
        };
    }
}
