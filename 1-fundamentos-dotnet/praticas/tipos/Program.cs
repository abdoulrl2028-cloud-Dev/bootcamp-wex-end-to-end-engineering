using System;

class Program
{
    static void Main()
    {
        int inteiro = 42;
        double real = 3.1415;
        string texto = "exemplo";
        bool ativo = true;

        Console.WriteLine($"int: {inteiro}");
        Console.WriteLine($"double: {real}");
        Console.WriteLine($"string: {texto}");
        Console.WriteLine($"bool: {ativo}");

        // Conversões
        int convertido = (int)real;
        Console.WriteLine($"Conversão double -> int: {convertido}");

        // Interpolação e formatação
        Console.WriteLine($"Valor formatado: {real:F2}");
    }
}
