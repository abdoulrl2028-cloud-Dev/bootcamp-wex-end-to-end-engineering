# Encapsulamento em C#

## Conceito

Encapsulamento é um dos pilares da POO que permite **proteger os dados de uma classe** controlando como são acessados e modificados. Utiliza os modificadores de acesso:

- **private**: Acessível apenas dentro da classe
- **public**: Acessível de qualquer lugar
- **protected**: Acessível na classe e suas subclasses
- **internal**: Acessível no mesmo assembly

## Benefícios

✅ **Proteção de Dados**: Controla o acesso aos atributos
✅ **Validação**: Garante que dados estejam sempre em estado válido
✅ **Flexibilidade**: Pode alterar implementação sem quebrar código externo
✅ **Segurança**: Impede modificações diretas de dados críticos

## Características

| Modificador | Acesso |
|-----------|--------|
| **private** | Apenas dentro da classe |
| **public** | Qualquer lugar |
| **protected** | Classe + subclasses |
| **internal** | Mesmo assembly |

## Exemplo Prático

No código `Program.cs`:

- **Pessoa**: Usar propriedades com validação
- **Produto**: Propriedades somente leitura (get privado)
- **Validações**: Todas as mudanças são validadas

## Como Executar

```bash
dotnet run
```

## Padrões Comuns

### Propriedade com Backing Field
```csharp
private string _nome;

public string Nome
{
    get { return _nome; }
    set { _nome = value; }
}
```

### Propriedade Auto-implementada
```csharp
public string Descricao { get; set; }
```

### Propriedade Somente Leitura
```csharp
public string Codigo { get; private set; }
```

## Saída Esperada

```
=== DEMONSTRAÇÃO DE ENCAPSULAMENTO ===

--- Criando Pessoa ---
Nome: João Silva
Idade: 30 anos
Salário: R$ 3000,00

--- Tentando atribuições inválidas ---
Nome não pode ser vazio!
Idade deve estar entre 0 e 150!
Salário não pode ser negativo!

--- Aplicando aumento válido ---
Aumento de 15% aplicado!
Nome: João Silva
Idade: 30 anos
Salário: R$ 3465,00

--- Criando Produto ---
Código: 001
Descrição: Notebook
Preço: R$ 2500,00

--- Tentando atualizar preço ---
Preço atualizado de R$ 2500,00 para R$ 2800,00
Código: 001
Descrição: Notebook
Preço: R$ 2800,00

--- Tentando preço inválido ---
Preço inválido!
```
