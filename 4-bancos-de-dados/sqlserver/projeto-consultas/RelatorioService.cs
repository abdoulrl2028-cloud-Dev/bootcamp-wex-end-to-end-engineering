using System.Data.SqlClient;
using ProjetoConsultas.Models;

namespace ProjetoConsultas.Services;

public class RelatorioService
{
    private readonly Database _database;

    public RelatorioService(Database database)
    {
        _database = database;
    }

    public void RelatorioVendas()
    {
        using (var connection = _database.GetConnection())
        {
            connection.Open();
            
            string query = @"
                SELECT 
                    COUNT(p.Id) as TotalPedidos,
                    SUM(p.Total) as TotalVendas,
                    AVG(p.Total) as TicketMedio,
                    MAX(p.Total) as MaiorPedido,
                    MIN(p.Total) as MenorPedido
                FROM Pedidos p
                WHERE p.Status != 'CANCELADO'
            ";

            using (var command = new SqlCommand(query, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Console.WriteLine("\n=== RELATÓRIO DE VENDAS ===");
                        Console.WriteLine($"Total de Pedidos: {reader.GetInt32(0)}");
                        Console.WriteLine($"Total de Vendas: R$ {reader.GetDecimal(1):F2}");
                        Console.WriteLine($"Ticket Médio: R$ {reader.GetDecimal(2):F2}");
                        Console.WriteLine($"Maior Pedido: R$ {reader.GetDecimal(3):F2}");
                        Console.WriteLine($"Menor Pedido: R$ {reader.GetDecimal(4):F2}");
                    }
                }
            }
        }
    }

    public void RelatorioClientesPorVendas()
    {
        using (var connection = _database.GetConnection())
        {
            connection.Open();
            
            string query = @"
                SELECT TOP 10
                    u.Nome,
                    u.Email,
                    COUNT(p.Id) as TotalPedidos,
                    SUM(p.Total) as TotalGasto,
                    AVG(p.Total) as TicketMedio
                FROM Usuarios u
                LEFT JOIN Pedidos p ON u.Id = p.UsuarioId
                GROUP BY u.Id, u.Nome, u.Email
                ORDER BY SUM(p.Total) DESC
            ";

            using (var command = new SqlCommand(query, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    Console.WriteLine("\n=== TOP 10 CLIENTES ===");
                    while (reader.Read())
                    {
                        Console.WriteLine($"\nNome: {reader.GetString(0)}");
                        Console.WriteLine($"Email: {reader.GetString(1)}");
                        Console.WriteLine($"Pedidos: {reader.GetInt32(2)}");
                        Console.WriteLine($"Total Gasto: R$ {reader.GetDecimal(3):F2}");
                        Console.WriteLine($"Ticket Médio: R$ {reader.GetDecimal(4):F2}");
                    }
                }
            }
        }
    }

    public void RelatorioProdutosMaisVendidos()
    {
        using (var connection = _database.GetConnection())
        {
            connection.Open();
            
            string query = @"
                SELECT TOP 10
                    pr.Nome,
                    SUM(ip.Quantidade) as TotalVendido,
                    SUM(ip.Subtotal) as ReceitaTotal,
                    COUNT(DISTINCT p.Id) as TotalPedidos
                FROM Produtos pr
                INNER JOIN ItensPedido ip ON pr.Id = ip.ProdutoId
                INNER JOIN Pedidos p ON ip.PedidoId = p.Id
                WHERE p.Status != 'CANCELADO'
                GROUP BY pr.Id, pr.Nome
                ORDER BY SUM(ip.Quantidade) DESC
            ";

            using (var command = new SqlCommand(query, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    Console.WriteLine("\n=== TOP 10 PRODUTOS MAIS VENDIDOS ===");
                    while (reader.Read())
                    {
                        Console.WriteLine($"\nProduto: {reader.GetString(0)}");
                        Console.WriteLine($"Quantidade Vendida: {reader.GetInt32(1)}");
                        Console.WriteLine($"Receita Total: R$ {reader.GetDecimal(2):F2}");
                        Console.WriteLine($"Número de Pedidos: {reader.GetInt32(3)}");
                    }
                }
            }
        }
    }

    public void RelatorioStatusPedidos()
    {
        using (var connection = _database.GetConnection())
        {
            connection.Open();
            
            string query = @"
                SELECT 
                    Status,
                    COUNT(*) as Quantidade,
                    SUM(Total) as TotalValor
                FROM Pedidos
                GROUP BY Status
                ORDER BY Quantidade DESC
            ";

            using (var command = new SqlCommand(query, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    Console.WriteLine("\n=== RELATÓRIO DE STATUS DE PEDIDOS ===");
                    while (reader.Read())
                    {
                        Console.WriteLine($"\nStatus: {reader.GetString(0)}");
                        Console.WriteLine($"Quantidade: {reader.GetInt32(1)}");
                        Console.WriteLine($"Valor Total: R$ {reader.GetDecimal(2):F2}");
                    }
                }
            }
        }
    }

    public void RelatorioEstoque()
    {
        using (var connection = _database.GetConnection())
        {
            connection.Open();
            
            string query = @"
                SELECT 
                    Nome,
                    Preco,
                    Estoque,
                    (Preco * Estoque) as ValorEstoque
                FROM Produtos
                WHERE Ativo = 1
                ORDER BY (Preco * Estoque) DESC
            ";

            using (var command = new SqlCommand(query, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    Console.WriteLine("\n=== RELATÓRIO DE ESTOQUE ===");
                    decimal totalEstoque = 0;
                    
                    while (reader.Read())
                    {
                        decimal valor = reader.GetDecimal(3);
                        totalEstoque += valor;
                        
                        Console.WriteLine($"\nProduto: {reader.GetString(0)}");
                        Console.WriteLine($"Preço: R$ {reader.GetDecimal(1):F2}");
                        Console.WriteLine($"Quantidade: {reader.GetInt32(2)}");
                        Console.WriteLine($"Valor Total: R$ {valor:F2}");
                    }
                    
                    Console.WriteLine($"\n--- Valor Total do Estoque: R$ {totalEstoque:F2} ---");
                }
            }
        }
    }
}
