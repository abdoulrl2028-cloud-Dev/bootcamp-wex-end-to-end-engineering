using System;

// Demonstração de Estruturas de Repetição em C#

// FOR - Repetição com contador
Console.WriteLine("=== ESTRUTURA FOR ===");
Console.WriteLine("Contando de 1 a 5:");
for (int i = 1; i <= 5; i++)
{
    Console.WriteLine($"Número: {i}");
}
Console.WriteLine();

// FOR - Tabuada do 7
Console.WriteLine("Tabuada do 7:");
for (int i = 1; i <= 10; i++)
{
    Console.WriteLine($"7 x {i} = {7 * i}");
}
Console.WriteLine();

// WHILE - Repetição com condição
Console.WriteLine("=== ESTRUTURA WHILE ===");
int contador = 1;
Console.WriteLine("Contando com WHILE de 1 a 5:");
while (contador <= 5)
{
    Console.WriteLine($"Valor: {contador}");
    contador++;
}
Console.WriteLine();

// WHILE - Entrada do usuário
Console.WriteLine("WHILE com entrada do usuário:");
string? entrada = "";
int tentativas = 0;
while (entrada != "sair" && tentativas < 3)
{
    Console.Write("Digite 'sair' para encerrar: ");
    entrada = Console.ReadLine();
    tentativas++;
}
Console.WriteLine();

// DO-WHILE - Repetição que executa pelo menos uma vez
Console.WriteLine("=== ESTRUTURA DO-WHILE ===");
int numero = 1;
Console.WriteLine("Contando com DO-WHILE de 1 a 5:");
do
{
    Console.WriteLine($"Valor: {numero}");
    numero++;
} while (numero <= 5);
Console.WriteLine();

// DO-WHILE - Menu
Console.WriteLine("DO-WHILE com menu:");
int opcao = 0;
int iteracoes = 0;
do
{
    Console.WriteLine("\n--- Menu ---");
    Console.WriteLine("1. Opção 1");
    Console.WriteLine("2. Opção 2");
    Console.WriteLine("3. Sair");
    
    // Simulando entrada
    if (iteracoes == 0)
        opcao = 1;
    else if (iteracoes == 1)
        opcao = 2;
    else
        opcao = 3;
    
    Console.WriteLine($"Você escolheu: {opcao}");
    iteracoes++;
} while (opcao != 3 && iteracoes < 3);
Console.WriteLine();

// FOREACH - Iteração sobre coleções
Console.WriteLine("=== ESTRUTURA FOREACH ===");
int[] numeros = { 10, 20, 30, 40, 50 };
Console.WriteLine("Array de números:");
foreach (int num in numeros)
{
    Console.WriteLine($"Número: {num}");
}
Console.WriteLine();

// FOREACH - Com strings
string[] frutas = { "Maçã", "Banana", "Laranja", "Morango" };
Console.WriteLine("Lista de frutas:");
foreach (string fruta in frutas)
{
    Console.WriteLine($"- {fruta}");
}
Console.WriteLine();

// FOR com BREAK
Console.WriteLine("=== FOR com BREAK ===");
Console.WriteLine("Contando até encontrar 5:");
for (int i = 1; i <= 10; i++)
{
    if (i == 5)
    {
        Console.WriteLine($"Encontrei o número {i}! Parando...");
        break;
    }
    Console.WriteLine($"Número: {i}");
}
Console.WriteLine();

// FOR com CONTINUE
Console.WriteLine("=== FOR com CONTINUE ===");
Console.WriteLine("Números de 1 a 10, pulando pares:");
for (int i = 1; i <= 10; i++)
{
    if (i % 2 == 0)
    {
        continue;
    }
    Console.WriteLine($"Número: {i}");
}
Console.WriteLine();

// FOR aninhado - Matriz
Console.WriteLine("=== FOR ANINHADO ===");
Console.WriteLine("Tabuleiro 3x3:");
for (int linha = 1; linha <= 3; linha++)
{
    for (int coluna = 1; coluna <= 3; coluna++)
    {
        Console.Write($"[{linha},{coluna}] ");
    }
    Console.WriteLine();
}
Console.WriteLine();

// Calculando soma com FOR
Console.WriteLine("=== Aplicações Práticas ===");
Console.WriteLine("Soma dos números de 1 a 100:");
int soma = 0;
for (int i = 1; i <= 100; i++)
{
    soma += i;
}
Console.WriteLine($"Soma: {soma}");
Console.WriteLine();

// Fatorial com FOR
Console.WriteLine("Fatorial de 5:");
int n = 5;
int fatorial = 1;
for (int i = 1; i <= n; i++)
{
    fatorial *= i;
}
Console.WriteLine($"5! = {fatorial}");
Console.WriteLine();

// Multiplicação de matrizes (FOR aninhado)
Console.WriteLine("Multiplicação com FOR aninhado:");
int[,] matriz = new int[3, 3];
for (int i = 0; i < 3; i++)
{
    for (int j = 0; j < 3; j++)
    {
        matriz[i, j] = (i + 1) * (j + 1);
        Console.Write($"{matriz[i, j]:D2} ");
    }
    Console.WriteLine();
}
