using System;

// Demonstração de Operadores em C#

// Operadores Aritméticos
Console.WriteLine("=== OPERADORES ARITMÉTICOS ===");
int a = 10;
int b = 3;

Console.WriteLine($"Adição: {a} + {b} = {a + b}");
Console.WriteLine($"Subtração: {a} - {b} = {a - b}");
Console.WriteLine($"Multiplicação: {a} * {b} = {a * b}");
Console.WriteLine($"Divisão: {a} / {b} = {a / b}");
Console.WriteLine($"Módulo (Resto): {a} % {b} = {a % b}");
Console.WriteLine();

// Operadores de Incremento e Decremento
Console.WriteLine("=== OPERADORES DE INCREMENTO/DECREMENTO ===");
int contador = 5;
Console.WriteLine($"Valor inicial: {contador}");
Console.WriteLine($"Pré-incremento: {++contador}");
Console.WriteLine($"Pós-incremento: {contador++}");
Console.WriteLine($"Valor após pós-incremento: {contador}");

contador = 5;
Console.WriteLine($"Pré-decremento: {--contador}");
Console.WriteLine($"Pós-decremento: {contador--}");
Console.WriteLine($"Valor após pós-decremento: {contador}");
Console.WriteLine();

// Operadores de Comparação
Console.WriteLine("=== OPERADORES DE COMPARAÇÃO ===");
int x = 15;
int y = 10;

Console.WriteLine($"{x} == {y}: {x == y}");
Console.WriteLine($"{x} != {y}: {x != y}");
Console.WriteLine($"{x} > {y}: {x > y}");
Console.WriteLine($"{x} < {y}: {x < y}");
Console.WriteLine($"{x} >= {y}: {x >= y}");
Console.WriteLine($"{x} <= {y}: {x <= y}");
Console.WriteLine();

// Operadores Lógicos
Console.WriteLine("=== OPERADORES LÓGICOS ===");
bool condicao1 = true;
bool condicao2 = false;

Console.WriteLine($"true AND false: {condicao1 && condicao2}");
Console.WriteLine($"true OR false: {condicao1 || condicao2}");
Console.WriteLine($"NOT true: {!condicao1}");
Console.WriteLine($"NOT false: {!condicao2}");
Console.WriteLine();

// Operadores de Atribuição
Console.WriteLine("=== OPERADORES DE ATRIBUIÇÃO ===");
int valor = 20;
Console.WriteLine($"Valor inicial: {valor}");

valor += 5; // valor = valor + 5
Console.WriteLine($"Após += 5: {valor}");

valor -= 3; // valor = valor - 3
Console.WriteLine($"Após -= 3: {valor}");

valor *= 2; // valor = valor * 2
Console.WriteLine($"Após *= 2: {valor}");

valor /= 4; // valor = valor / 4
Console.WriteLine($"Após /= 4: {valor}");
Console.WriteLine();

// Operador Ternário
Console.WriteLine("=== OPERADOR TERNÁRIO ===");
int idade = 18;
string resultado = idade >= 18 ? "Maior de idade" : "Menor de idade";
Console.WriteLine($"Idade: {idade} - {resultado}");

idade = 15;
resultado = idade >= 18 ? "Maior de idade" : "Menor de idade";
Console.WriteLine($"Idade: {idade} - {resultado}");
Console.WriteLine();

// Operador NULL-COALESCING (??)
Console.WriteLine("=== OPERADOR NULL-COALESCING ===");
string? nome = null;
string nomeExibido = nome ?? "Nome não definido";
Console.WriteLine($"Nome: {nomeExibido}");

nome = "João";
nomeExibido = nome ?? "Nome não definido";
Console.WriteLine($"Nome: {nomeExibido}");
Console.WriteLine();

// Operador de Tipo (is, as)
Console.WriteLine("=== OPERADORES DE TIPO ===");
object obj = "Teste";

if (obj is string)
{
    Console.WriteLine($"obj é uma string: {obj}");
}

if (obj is int)
{
    Console.WriteLine($"obj é um inteiro");
}

// Conversão com 'as'
string? str = obj as string;
Console.WriteLine($"Conversão com 'as': {str}");

int? numero = obj as int?;
Console.WriteLine($"Conversão com 'as' para int: {numero}");
