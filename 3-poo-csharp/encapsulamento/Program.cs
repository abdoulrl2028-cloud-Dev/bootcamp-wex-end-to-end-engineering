using System;

// Exemplo de Encapsulamento: Controle de acesso a dados

public class Pessoa
{
    // Campos privados (encapsulados)
    private string _nome;
    private int _idade;
    private decimal _salario;

    // Propriedades públicas com validação
    public string Nome
    {
        get { return _nome; }
        set 
        { 
            if (!string.IsNullOrWhiteSpace(value))
                _nome = value;
            else
                Console.WriteLine("Nome não pode ser vazio!");
        }
    }

    public int Idade
    {
        get { return _idade; }
        set 
        { 
            if (value >= 0 && value <= 150)
                _idade = value;
            else
                Console.WriteLine("Idade deve estar entre 0 e 150!");
        }
    }

    public decimal Salario
    {
        get { return _salario; }
        set 
        { 
            if (value >= 0)
                _salario = value;
            else
                Console.WriteLine("Salário não pode ser negativo!");
        }
    }

    // Construtor
    public Pessoa(string nome, int idade, decimal salario)
    {
        Nome = nome;
        Idade = idade;
        Salario = salario;
    }

    // Método público
    public void Exibir_Informacoes()
    {
        Console.WriteLine($"Nome: {Nome}");
        Console.WriteLine($"Idade: {Idade} anos");
        Console.WriteLine($"Salário: R$ {Salario:F2}");
    }

    // Método privado (não pode ser acessado de fora da classe)
    private void Processar_Bonus()
    {
        _salario += _salario * 0.10m; // 10% de bônus
    }

    public void Aplicar_Aumento(decimal percentual)
    {
        if (percentual > 0)
        {
            _salario += _salario * (percentual / 100);
            Console.WriteLine($"Aumento de {percentual}% aplicado!");
            Processar_Bonus();
        }
        else
        {
            Console.WriteLine("Percentual deve ser maior que zero!");
        }
    }
}

// Classe com propriedade auto-implementada
public class Produto
{
    public string Codigo { get; private set; }
    public string Descricao { get; set; }
    public decimal Preco { get; private set; }

    public Produto(string codigo, string descricao, decimal preco)
    {
        Codigo = codigo;
        Descricao = descricao;

        if (preco > 0)
            Preco = preco;
        else
        {
            Console.WriteLine("Preço deve ser maior que zero!");
            Preco = 0;
        }
    }

    public void Atualizar_Preco(decimal novoPreco)
    {
        if (novoPreco > 0)
        {
            Console.WriteLine($"Preço atualizado de R$ {Preco:F2} para R$ {novoPreco:F2}");
            Preco = novoPreco;
        }
        else
        {
            Console.WriteLine("Preço inválido!");
        }
    }

    public void Exibir_Informacoes()
    {
        Console.WriteLine($"Código: {Codigo}");
        Console.WriteLine($"Descrição: {Descricao}");
        Console.WriteLine($"Preço: R$ {Preco:F2}");
    }
}

public class Program
{
    public static void Main()
    {
        Console.WriteLine("=== DEMONSTRAÇÃO DE ENCAPSULAMENTO ===\n");

        // Exemplo 1: Pessoa com validação
        Console.WriteLine("--- Criando Pessoa ---");
        Pessoa pessoa = new Pessoa("João Silva", 30, 3000);
        pessoa.Exibir_Informacoes();

        Console.WriteLine("\n--- Tentando atribuições inválidas ---");
        pessoa.Nome = "";  // Será recusado
        pessoa.Idade = 200;  // Será recusado
        pessoa.Salario = -500;  // Será recusado

        Console.WriteLine("\n--- Aplicando aumento válido ---");
        pessoa.Aplicar_Aumento(15);
        pessoa.Exibir_Informacoes();

        // Exemplo 2: Produto
        Console.WriteLine("\n--- Criando Produto ---");
        Produto produto = new Produto("001", "Notebook", 2500);
        produto.Exibir_Informacoes();

        Console.WriteLine("\n--- Tentando atualizar preço ---");
        produto.Atualizar_Preco(2800);
        produto.Exibir_Informacoes();

        Console.WriteLine("\n--- Tentando preço inválido ---");
        produto.Atualizar_Preco(-100);

        // Demonstração: Campos privados não são acessíveis
        // pessoa._nome = "Teste";  // ERRO: _nome é privado!
        // produto.Codigo = "002";  // ERRO: Codigo é somente leitura!
    }
}
