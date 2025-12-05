using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        var lista = new List<string> { "maçã", "banana", "laranja" };
        var dict = new Dictionary<string, int> { { "um", 1 }, { "dois", 2 } };

        Console.WriteLine("Lista:");
        foreach (var item in lista)
        {
            Console.WriteLine($"- {item}");
        }

        Console.WriteLine("Dicionário:");
        foreach (var kv in dict)
        {
            Console.WriteLine($"- {kv.Key} = {kv.Value}");
        }

        // Linq simples (se disponível)
        var filtradas = lista.FindAll(s => s.StartsWith("b"));
        Console.WriteLine("Filtradas (começam com 'b'):");
        foreach (var f in filtradas) Console.WriteLine($"- {f}");
    }
}
