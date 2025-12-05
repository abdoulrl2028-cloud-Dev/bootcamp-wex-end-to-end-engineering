# Polimorfismo em C#

## Conceito

Polimorfismo significa **"muitas formas"** e permite que objetos de diferentes tipos respondam ao mesmo método de maneiras diferentes. Existem dois tipos principais:

### 1. Polimorfismo de Compilação (Sobrecarga)
- Métodos com o mesmo nome mas parâmetros diferentes
- Resolvido em tempo de compilação

### 2. Polimorfismo de Execução (Override)
- Subclasses sobrescrevem métodos da classe base
- Resolvido em tempo de execução

## Características

✅ **Flexibilidade**: Um método pode ter múltiplas implementações
✅ **Reutilização**: Código genérico que funciona com múltiplos tipos
✅ **Extensibilidade**: Fácil adicionar novos tipos
✅ **Simplicidade**: Interface uniforme para diferentes objetos

## Tipos de Polimorfismo

| Tipo | Descrição | Exemplo |
|------|-----------|---------|
| **Sobrecarga** | Mesmo nome, parâmetros diferentes | `Somar(2, 3)` e `Somar(2.5, 3.5)` |
| **Override** | Subclasse redefine método da base | `Quadrado.Calcular_Area()` |
| **Interface** | Diferentes implementações do contrato | `IForma` com `Quadrado`, `Circulo` |

## Exemplo Prático

No código `Program.cs`:

### Polimorfismo de Herança
```csharp
List<Forma> formas = new List<Forma>
{
    new Quadrado(5),
    new Circulo(3)
};

foreach (var forma in formas)
{
    forma.Calcular_Area();  // Cada classe implementa diferente!
}
```

### Polimorfismo de Método
```csharp
calc.Somar(5, 3);           // Soma dois ints
calc.Somar(5.5, 3.2);       // Soma dois doubles
calc.Somar(5.5, 3.2, 2.1);  // Soma três doubles
```

## Como Executar

```bash
dotnet run
```

## Saída Esperada

```
=== DEMONSTRAÇÃO DE POLIMORFISMO ===

--- POLIMORFISMO DE HERANÇA ---
Forma: Quadrado
Área: 25,00
Perímetro: 20,00

Forma: Retângulo
Área: 24,00
Perímetro: 20,00
Largura: 4, Altura: 6

Forma: Círculo
Área: 28,27
Perímetro: 18,85
Raio: 3

Forma: Triângulo
Área: 6,00
Perímetro: 12,00
Lados: 3, 4, 5

Área Total de Todas as Formas: 84,27

--- POLIMORFISMO DE MÉTODO (SOBRECARGA) ---
Somando dois doubles:
Resultado: 8,7

Somando três doubles:
Resultado: 10,8

Somando dois ints:
Resultado: 30

--- EXIBINDO DIFERENTES TIPOS ---
String: Olá Mundo
Inteiro: 42
Decimal: 3,14

--- USANDO INTERFACE ---
Tipo: Quadrado
Área: 25,00
Perímetro: 20,00

Tipo: Circulo
Área: 28,27
Perímetro: 18,85
```

## Vantagens do Polimorfismo

1. **Extensibilidade**: Adicionar novos tipos sem modificar código existente
2. **Manutenibilidade**: Código mais limpo e organizado
3. **Reutilização**: Uma única função funciona com múltiplos tipos
4. **Segurança**: Verificação de tipos em tempo de compilação (com override)
