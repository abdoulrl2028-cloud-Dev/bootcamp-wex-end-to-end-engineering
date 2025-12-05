using System;
using System.Collections.Generic;

// Exemplo de Polimorfismo: Mesmo nome, comportamentos diferentes

// Interface que define o contrato
public interface IForma
{
    double Calcular_Area();
    double Calcular_Perimetro();
    void Exibir_Informacoes();
}

// Classe base abstrata
public abstract class Forma : IForma
{
    public string Nome { get; set; }

    public abstract double Calcular_Area();
    public abstract double Calcular_Perimetro();

    public virtual void Exibir_Informacoes()
    {
        Console.WriteLine($"Forma: {Nome}");
        Console.WriteLine($"Área: {Calcular_Area():F2}");
        Console.WriteLine($"Perímetro: {Calcular_Perimetro():F2}");
    }
}

// Polimorfismo: Cada forma tem sua própria implementação
public class Quadrado : Forma
{
    public double Lado { get; set; }

    public Quadrado(double lado)
    {
        Lado = lado;
        Nome = "Quadrado";
    }

    public override double Calcular_Area()
    {
        return Lado * Lado;
    }

    public override double Calcular_Perimetro()
    {
        return Lado * 4;
    }
}

public class Retangulo : Forma
{
    public double Largura { get; set; }
    public double Altura { get; set; }

    public Retangulo(double largura, double altura)
    {
        Largura = largura;
        Altura = altura;
        Nome = "Retângulo";
    }

    public override double Calcular_Area()
    {
        return Largura * Altura;
    }

    public override double Calcular_Perimetro()
    {
        return 2 * (Largura + Altura);
    }

    public override void Exibir_Informacoes()
    {
        base.Exibir_Informacoes();
        Console.WriteLine($"Largura: {Largura}, Altura: {Altura}");
    }
}

public class Circulo : Forma
{
    public double Raio { get; set; }

    public Circulo(double raio)
    {
        Raio = raio;
        Nome = "Círculo";
    }

    public override double Calcular_Area()
    {
        return Math.PI * Raio * Raio;
    }

    public override double Calcular_Perimetro()
    {
        return 2 * Math.PI * Raio;
    }

    public override void Exibir_Informacoes()
    {
        base.Exibir_Informacoes();
        Console.WriteLine($"Raio: {Raio}");
    }
}

public class Triangulo : Forma
{
    public double LadoA { get; set; }
    public double LadoB { get; set; }
    public double LadoC { get; set; }
    public double Altura { get; set; }
    public double Base { get; set; }

    public Triangulo(double ladoA, double ladoB, double ladoC, double baseT, double altura)
    {
        LadoA = ladoA;
        LadoB = ladoB;
        LadoC = ladoC;
        Base = baseT;
        Altura = altura;
        Nome = "Triângulo";
    }

    public override double Calcular_Area()
    {
        return (Base * Altura) / 2;
    }

    public override double Calcular_Perimetro()
    {
        return LadoA + LadoB + LadoC;
    }

    public override void Exibir_Informacoes()
    {
        base.Exibir_Informacoes();
        Console.WriteLine($"Lados: {LadoA}, {LadoB}, {LadoC}");
    }
}

// Exemplo de Polimorfismo com Métodos Sobrecarregados
public class Calculadora
{
    // Método Somar sobrecarregado (Polimorfismo de Método)
    public double Somar(double a, double b)
    {
        return a + b;
    }

    public double Somar(double a, double b, double c)
    {
        return a + b + c;
    }

    public int Somar(int a, int b)
    {
        return a + b;
    }

    // Outro exemplo
    public void Exibir(string mensagem)
    {
        Console.WriteLine($"String: {mensagem}");
    }

    public void Exibir(int numero)
    {
        Console.WriteLine($"Inteiro: {numero}");
    }

    public void Exibir(double numero)
    {
        Console.WriteLine($"Decimal: {numero}");
    }
}

public class Program
{
    public static void Main()
    {
        Console.WriteLine("=== DEMONSTRAÇÃO DE POLIMORFISMO ===\n");

        // Exemplo 1: Polimorfismo de Herança (Override)
        Console.WriteLine("--- POLIMORFISMO DE HERANÇA ---");
        List<Forma> formas = new List<Forma>
        {
            new Quadrado(5),
            new Retangulo(4, 6),
            new Circulo(3),
            new Triangulo(3, 4, 5, 3, 4)
        };

        double areaTotal = 0;
        foreach (var forma in formas)
        {
            forma.Exibir_Informacoes();
            areaTotal += forma.Calcular_Area();
            Console.WriteLine();
        }

        Console.WriteLine($"Área Total de Todas as Formas: {areaTotal:F2}\n");

        // Exemplo 2: Polimorfismo de Método (Sobrecarga)
        Console.WriteLine("--- POLIMORFISMO DE MÉTODO (SOBRECARGA) ---");
        Calculadora calc = new Calculadora();

        Console.WriteLine("Somando dois doubles:");
        Console.WriteLine($"Resultado: {calc.Somar(5.5, 3.2)}");

        Console.WriteLine("\nSomando três doubles:");
        Console.WriteLine($"Resultado: {calc.Somar(5.5, 3.2, 2.1)}");

        Console.WriteLine("\nSomando dois ints:");
        Console.WriteLine($"Resultado: {calc.Somar(10, 20)}");

        Console.WriteLine("\n--- EXIBINDO DIFERENTES TIPOS ---");
        calc.Exibir("Olá Mundo");
        calc.Exibir(42);
        calc.Exibir(3.14);

        // Exemplo 3: Demonstração Prática
        Console.WriteLine("\n--- USANDO INTERFACE ---");
        IForma[] interfaceFormas = { 
            new Quadrado(5), 
            new Circulo(3) 
        };

        foreach (var forma in interfaceFormas)
        {
            Console.WriteLine($"Tipo: {forma.GetType().Name}");
            Console.WriteLine($"Área: {forma.Calcular_Area():F2}");
            Console.WriteLine($"Perímetro: {forma.Calcular_Perimetro():F2}\n");
        }
    }
}
