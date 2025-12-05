using System;

// Exemplo de Herança: Reutilização de código

// Classe base (superclasse)
public class Veiculo
{
    public string Marca { get; set; }
    public string Modelo { get; set; }
    public int Ano { get; set; }
    public decimal Velocidade { get; private set; }

    public Veiculo(string marca, string modelo, int ano)
    {
        Marca = marca;
        Modelo = modelo;
        Ano = ano;
        Velocidade = 0;
    }

    public virtual void Acelerar()
    {
        Velocidade += 10;
        Console.WriteLine($"{Marca} {Modelo} acelerou para {Velocidade} km/h");
    }

    public virtual void Frear()
    {
        if (Velocidade >= 10)
        {
            Velocidade -= 10;
            Console.WriteLine($"{Marca} {Modelo} freou para {Velocidade} km/h");
        }
        else
        {
            Velocidade = 0;
            Console.WriteLine($"{Marca} {Modelo} parou completamente");
        }
    }

    public virtual void Exibir_Informacoes()
    {
        Console.WriteLine($"Marca: {Marca}");
        Console.WriteLine($"Modelo: {Modelo}");
        Console.WriteLine($"Ano: {Ano}");
        Console.WriteLine($"Velocidade: {Velocidade} km/h");
    }
}

// Classe derivada (subclasse) 1
public class Carro : Veiculo
{
    public int NumPortas { get; set; }
    public string Combustivel { get; set; }

    public Carro(string marca, string modelo, int ano, int numPortas, string combustivel)
        : base(marca, modelo, ano)
    {
        NumPortas = numPortas;
        Combustivel = combustivel;
    }

    public override void Acelerar()
    {
        Velocidade += 15;
        Console.WriteLine($"Carro {Marca} {Modelo} acelerou para {Velocidade} km/h");
    }

    public void Abrir_Porta()
    {
        Console.WriteLine($"Carro abriu uma de suas {NumPortas} portas");
    }

    public override void Exibir_Informacoes()
    {
        base.Exibir_Informacoes();
        Console.WriteLine($"Número de Portas: {NumPortas}");
        Console.WriteLine($"Combustível: {Combustivel}");
    }
}

// Classe derivada (subclasse) 2
public class Moto : Veiculo
{
    public bool TemBau { get; set; }
    public int Cilindradas { get; set; }

    public Moto(string marca, string modelo, int ano, bool temBau, int cilindradas)
        : base(marca, modelo, ano)
    {
        TemBau = temBau;
        Cilindradas = cilindradas;
    }

    public override void Acelerar()
    {
        Velocidade += 20;
        Console.WriteLine($"Moto {Marca} {Modelo} acelerou para {Velocidade} km/h");
    }

    public void Dar_Grau()
    {
        if (Velocidade > 30)
        {
            Console.WriteLine($"Moto {Marca} está dando grau em alta velocidade!");
        }
    }

    public override void Exibir_Informacoes()
    {
        base.Exibir_Informacoes();
        Console.WriteLine($"Tem Baú: {(TemBau ? "Sim" : "Não")}");
        Console.WriteLine($"Cilindradas: {Cilindradas}cc");
    }
}

// Classe derivada (subclasse) 3
public class Caminhao : Veiculo
{
    public decimal CapacidadeCarga { get; set; }
    public int NumEixos { get; set; }

    public Caminhao(string marca, string modelo, int ano, decimal capacidadeCarga, int numEixos)
        : base(marca, modelo, ano)
    {
        CapacidadeCarga = capacidadeCarga;
        NumEixos = numEixos;
    }

    public override void Acelerar()
    {
        Velocidade += 8;
        Console.WriteLine($"Caminhão {Marca} {Modelo} acelerou para {Velocidade} km/h");
    }

    public override void Frear()
    {
        if (Velocidade >= 15)
        {
            Velocidade -= 15;
            Console.WriteLine($"Caminhão freou com força para {Velocidade} km/h");
        }
        else
        {
            Velocidade = 0;
            Console.WriteLine($"Caminhão parou");
        }
    }

    public void Carregar(decimal peso)
    {
        if (peso <= CapacidadeCarga)
        {
            Console.WriteLine($"Caminhão carregado com {peso} toneladas");
        }
        else
        {
            Console.WriteLine($"Peso excede capacidade de {CapacidadeCarga} toneladas");
        }
    }

    public override void Exibir_Informacoes()
    {
        base.Exibir_Informacoes();
        Console.WriteLine($"Capacidade de Carga: {CapacidadeCarga} toneladas");
        Console.WriteLine($"Número de Eixos: {NumEixos}");
    }
}

public class Program
{
    public static void Main()
    {
        Console.WriteLine("=== DEMONSTRAÇÃO DE HERANÇA ===\n");

        // Criando instâncias
        Carro carro = new Carro("Toyota", "Corolla", 2023, 4, "Gasolina");
        Moto moto = new Moto("Honda", "CB 500", 2022, true, 500);
        Caminhao caminhao = new Caminhao("Volvo", "FH16", 2021, 30, 3);

        // Demonstração Carro
        Console.WriteLine("--- CARRO ---");
        carro.Exibir_Informacoes();
        Console.WriteLine();
        carro.Acelerar();
        carro.Acelerar();
        carro.Abrir_Porta();
        carro.Frear();

        // Demonstração Moto
        Console.WriteLine("\n--- MOTO ---");
        moto.Exibir_Informacoes();
        Console.WriteLine();
        moto.Acelerar();
        moto.Acelerar();
        moto.Acelerar();
        moto.Dar_Grau();
        moto.Frear();

        // Demonstração Caminhão
        Console.WriteLine("\n--- CAMINHÃO ---");
        caminhao.Exibir_Informacoes();
        Console.WriteLine();
        caminhao.Acelerar();
        caminhao.Carregar(25);
        caminhao.Carregar(35);
        caminhao.Frear();

        // Demonstração de Polimorfismo
        Console.WriteLine("\n--- POLIMORFISMO COM HERANÇA ---");
        Veiculo[] veiculos = { carro, moto, caminhao };

        foreach (var veiculo in veiculos)
        {
            Console.WriteLine($"\nAcelerando {veiculo.Marca} {veiculo.Modelo}:");
            veiculo.Acelerar();
        }
    }
}
