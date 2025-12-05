using System;
using System.Collections.Generic;
using System.Linq;

// Demonstração de Arrays e Listas em C#

// ARRAYS - Tamanho fixo
Console.WriteLine("=== ARRAYS ===");
Console.WriteLine("Criando um array com 5 inteiros:");
int[] numeros = new int[5];
numeros[0] = 10;
numeros[1] = 20;
numeros[2] = 30;
numeros[3] = 40;
numeros[4] = 50;

Console.WriteLine("Exibindo array:");
for (int i = 0; i < numeros.Length; i++)
{
    Console.WriteLine($"numeros[{i}] = {numeros[i]}");
}
Console.WriteLine();

// ARRAYS - Inicialização direta
Console.WriteLine("Array com inicialização direta:");
string[] nomes = { "Ana", "Bruno", "Carlos", "Diana" };
foreach (string nome in nomes)
{
    Console.WriteLine($"- {nome}");
}
Console.WriteLine();

// ARRAYS - Tipos de dados
Console.WriteLine("=== ARRAYS DE DIFERENTES TIPOS ===");
double[] notas = { 7.5, 8.0, 9.5, 6.0 };
Console.WriteLine("Notas dos alunos:");
foreach (double nota in notas)
{
    Console.WriteLine($"Nota: {nota:F1}");
}

double media = notas.Average();
double maiorNota = notas.Max();
double menorNota = notas.Min();
Console.WriteLine($"Média: {media:F2}");
Console.WriteLine($"Maior nota: {maiorNota}");
Console.WriteLine($"Menor nota: {menorNota}");
Console.WriteLine();

// ARRAYS - Multidimensionais
Console.WriteLine("=== ARRAYS MULTIDIMENSIONAIS ===");
int[,] matriz = new int[3, 3];
int valor = 1;

Console.WriteLine("Preenchendo matriz 3x3:");
for (int i = 0; i < 3; i++)
{
    for (int j = 0; j < 3; j++)
    {
        matriz[i, j] = valor;
        valor++;
    }
}

Console.WriteLine("Exibindo matriz:");
for (int i = 0; i < 3; i++)
{
    for (int j = 0; j < 3; j++)
    {
        Console.Write($"{matriz[i, j]:D2} ");
    }
    Console.WriteLine();
}
Console.WriteLine();

// ARRAYS - Jagged Arrays
Console.WriteLine("=== JAGGED ARRAYS ===");
int[][] matrizJagged = new int[3][];
matrizJagged[0] = new int[2];  // Primeira linha com 2 elementos
matrizJagged[1] = new int[3];  // Segunda linha com 3 elementos
matrizJagged[2] = new int[4];  // Terceira linha com 4 elementos

Console.WriteLine("Jagged Array criado com linhas de tamanhos diferentes");
for (int i = 0; i < matrizJagged.Length; i++)
{
    Console.WriteLine($"Linha {i}: {matrizJagged[i].Length} elementos");
}
Console.WriteLine();

// LISTAS - Tamanho dinâmico
Console.WriteLine("=== LISTAS (List<T>) ===");
List<string> frutas = new List<string>();
frutas.Add("Maçã");
frutas.Add("Banana");
frutas.Add("Laranja");
frutas.Add("Morango");

Console.WriteLine("Lista de frutas:");
foreach (string fruta in frutas)
{
    Console.WriteLine($"- {fruta}");
}
Console.WriteLine($"Total de frutas: {frutas.Count}");
Console.WriteLine();

// LISTAS - Operações comuns
Console.WriteLine("=== OPERAÇÕES COM LISTAS ===");
frutas.Insert(1, "Abacaxi");  // Insere na posição 1
Console.WriteLine("Após inserir 'Abacaxi' na posição 1:");
foreach (string fruta in frutas)
{
    Console.WriteLine($"- {fruta}");
}

frutas.Remove("Banana");  // Remove o primeiro "Banana"
Console.WriteLine("\nApós remover 'Banana':");
foreach (string fruta in frutas)
{
    Console.WriteLine($"- {fruta}");
}

frutas.RemoveAt(0);  // Remove na posição 0
Console.WriteLine("\nApós remover elemento na posição 0:");
foreach (string fruta in frutas)
{
    Console.WriteLine($"- {fruta}");
}
Console.WriteLine();

// LISTAS - Pesquisa
Console.WriteLine("=== PESQUISA EM LISTAS ===");
List<int> numeros2 = new List<int> { 5, 15, 25, 35, 45, 55 };

Console.WriteLine($"Lista: {string.Join(", ", numeros2)}");
Console.WriteLine($"Contém 25? {numeros2.Contains(25)}");
Console.WriteLine($"Contém 100? {numeros2.Contains(100)}");
Console.WriteLine($"Índice de 35: {numeros2.IndexOf(35)}");
Console.WriteLine($"Índice de 100: {numeros2.IndexOf(100)}");
Console.WriteLine();

// LISTAS - Ordenação
Console.WriteLine("=== ORDENAÇÃO ===");
List<int> valores = new List<int> { 45, 12, 78, 23, 56, 89, 1 };
Console.WriteLine($"Lista original: {string.Join(", ", valores)}");

valores.Sort();
Console.WriteLine($"Após Sort(): {string.Join(", ", valores)}");

valores.Reverse();
Console.WriteLine($"Após Reverse(): {string.Join(", ", valores)}");
Console.WriteLine();

// LISTAS - Inicialização com valores
Console.WriteLine("=== LISTAS COM INICIALIZAÇÃO ===");
List<string> linguagens = new List<string> { "C#", "Java", "Python", "JavaScript" };
Console.WriteLine("Linguagens de programação:");
foreach (string ling in linguagens)
{
    Console.WriteLine($"- {ling}");
}
Console.WriteLine();

// LISTAS - LINQ (Language Integrated Query)
Console.WriteLine("=== LINQ COM LISTAS ===");
List<int> numeros3 = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

// WHERE - Filtragem
var pares = numeros3.Where(n => n % 2 == 0).ToList();
Console.WriteLine($"Números pares: {string.Join(", ", pares)}");

// SELECT - Transformação
var dobrados = numeros3.Select(n => n * 2).ToList();
Console.WriteLine($"Números dobrados: {string.Join(", ", dobrados)}");

// FirstOrDefault - Primeiro elemento ou padrão
int primeiro = numeros3.FirstOrDefault(n => n > 5);
Console.WriteLine($"Primeiro número maior que 5: {primeiro}");

// Any - Verifica se existe
bool temMaiorQue8 = numeros3.Any(n => n > 8);
Console.WriteLine($"Tem algum número maior que 8? {temMaiorQue8}");

// Count com condição
int quantosPares = numeros3.Count(n => n % 2 == 0);
Console.WriteLine($"Quantos números pares tem? {quantosPares}");
Console.WriteLine();

// LISTAS - Limpeza
Console.WriteLine("=== LIMPANDO LISTAS ===");
List<int> temp = new List<int> { 1, 2, 3, 4, 5 };
Console.WriteLine($"Lista antes: {string.Join(", ", temp)}");
temp.Clear();
Console.WriteLine($"Lista após Clear(): {string.Join(", ", temp)} (Vazia)");
