namespace ProjetoConsultas.Models;

public class Usuario
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Telefone { get; set; }
    public string? CPF { get; set; }
    public bool Ativo { get; set; } = true;
    public DateTime DataCriacao { get; set; }
    public DateTime DataAtualizacao { get; set; }

    public override string ToString()
    {
        return $"ID: {Id} | Nome: {Nome} | Email: {Email} | Ativo: {(Ativo ? "Sim" : "Não")}";
    }
}

public class Produto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    public decimal Preco { get; set; }
    public int Estoque { get; set; }
    public bool Ativo { get; set; } = true;
    public DateTime DataCriacao { get; set; }

    public override string ToString()
    {
        return $"ID: {Id} | Nome: {Nome} | Preço: R$ {Preco:F2} | Estoque: {Estoque}";
    }
}

public class Pedido
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public DateTime DataPedido { get; set; }
    public DateTime? DataEntrega { get; set; }
    public decimal Total { get; set; }
    public string Status { get; set; } = "PENDENTE";
    public string? NomeUsuario { get; set; }

    public override string ToString()
    {
        return $"ID: {Pedido} | Usuario: {NomeUsuario} | Total: R$ {Total:F2} | Status: {Status}";
    }
}

public class ItensPedido
{
    public int Id { get; set; }
    public int PedidoId { get; set; }
    public int ProdutoId { get; set; }
    public int Quantidade { get; set; }
    public decimal PrecoUnitario { get; set; }
    public decimal Subtotal { get; set; }
    public string? NomeProduto { get; set; }

    public override string ToString()
    {
        return $"ID: {Id} | Produto: {NomeProduto} | Qtd: {Quantidade} | Subtotal: R$ {Subtotal:F2}";
    }
}
