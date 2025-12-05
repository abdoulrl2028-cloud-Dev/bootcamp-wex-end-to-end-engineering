using System;

namespace ProjetoQA
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Projeto QA - Automação de Testes");
            var calc = new Calculadora();
            Console.WriteLine($"2 + 3 = {calc.Somar(2, 3)}");
            Console.WriteLine($"5 - 2 = {calc.Subtrair(5, 2)}");
        }
    }

    public class Calculadora
    {
        public int Somar(int a, int b) => a + b;
        public int Subtrair(int a, int b) => a - b;
    }
}
