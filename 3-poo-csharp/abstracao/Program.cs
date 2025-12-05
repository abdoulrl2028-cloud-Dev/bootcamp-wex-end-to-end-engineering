using System;

// Exemplo de Abstração: Classes abstratas e interfaces

// Interface que define o contrato para animais
public interface IAnimal
{
    void Fazer_Som();
    void Mover();
}

// Classe abstrata que implementa parte do contrato
public abstract class Animal : IAnimal
{
    public string Nome { get; set; }
    public int Idade { get; set; }

    public Animal(string nome, int idade)
    {
        Nome = nome;
        Idade = idade;
    }

    public abstract void Fazer_Som();

    public virtual void Mover()
    {
        Console.WriteLine($"{Nome} está se movimentando...");
    }

    public virtual void Dormir()
    {
        Console.WriteLine($"{Nome} está dormindo...");
    }
}

// Classe concreta que herda de Animal
public class Cachorro : Animal
{
    public Cachorro(string nome, int idade) : base(nome, idade) { }

    public override void Fazer_Som()
    {
        Console.WriteLine($"{Nome} faz: Au au au!");
    }

    public override void Mover()
    {
        Console.WriteLine($"{Nome} está correndo alegremente...");
    }

    public void Trazer_Bolinha()
    {
        Console.WriteLine($"{Nome} trouxe a bolinha!");
    }
}

// Outra classe concreta
public class Gato : Animal
{
    public Gato(string nome, int idade) : base(nome, idade) { }

    public override void Fazer_Som()
    {
        Console.WriteLine($"{Nome} faz: Miau miau!");
    }

    public override void Mover()
    {
        Console.WriteLine($"{Nome} está pulando silenciosamente...");
    }

    public void Arranhar_Sofá()
    {
        Console.WriteLine($"{Nome} está arranhando o sofá!");
    }
}

// Classe Program
public class Program
{
    public static void Main()
    {
        Console.WriteLine("=== DEMONSTRAÇÃO DE ABSTRAÇÃO ===\n");

        // Criando instâncias
        Animal cachorro = new Cachorro("Rex", 3);
        Animal gato = new Gato("Miau", 2);

        // Usando a abstração
        Console.WriteLine("--- Cachorro ---");
        cachorro.Fazer_Som();
        cachorro.Mover();
        cachorro.Dormir();
        ((Cachorro)cachorro).Trazer_Bolinha();

        Console.WriteLine("\n--- Gato ---");
        gato.Fazer_Som();
        gato.Mover();
        gato.Dormir();
        ((Gato)gato).Arranhar_Sofá();

        Console.WriteLine("\n--- Polimorfismo com Abstração ---");
        // Demonstrando polimorfismo
        IAnimal[] animais = { cachorro, gato };

        foreach (var animal in animais)
        {
            animal.Fazer_Som();
            animal.Mover();
        }
    }
}
