# Abstração em C#

## Conceito

Abstração é um dos pilares da POO que permite **esconder detalhes complexos** e mostrar apenas os recursos essenciais de um objeto. Usamos:

- **Classes Abstratas**: Servem como base para outras classes, podem ter métodos abstratos (sem implementação) e métodos concretos.
- **Interfaces**: Definem um contrato que as classes devem seguir.

## Características

✅ **Abstração de Dados**: Oculta os detalhes internos
✅ **Reutilização de Código**: Classes abstratas compartilham comportamentos comuns
✅ **Flexibilidade**: Permite diferentes implementações da mesma interface

## Exemplo Prático

No código `Program.cs`:

- **IAnimal**: Interface que define o contrato (Fazer_Som, Mover)
- **Animal**: Classe abstrata com implementação parcial
- **Cachorro e Gato**: Implementações concretas que estendem Animal

## Como Executar

```bash
dotnet run
```

## Conceitos Chave

| Conceito | Descrição |
|----------|-----------|
| **Classe Abstrata** | Não pode ser instanciada diretamente |
| **Método Abstrato** | Sem implementação, deve ser sobrescrito |
| **Interface** | Define o que a classe deve fazer |
| **Herança** | Classes herdam de classes abstratas |
| **Contrato** | Interface é um contrato que deve ser seguido |

## Saída Esperada

```
=== DEMONSTRAÇÃO DE ABSTRAÇÃO ===

--- Cachorro ---
Rex faz: Au au au!
Rex está correndo alegremente...
Rex está dormindo...
Rex trouxe a bolinha!

--- Gato ---
Miau faz: Miau miau!
Miau está pulando silenciosamente...
Miau está dormindo...
Miau está arranhando o sofá!

--- Polimorfismo com Abstração ---
Rex faz: Au au au!
Rex está correndo alegremente...
Miau faz: Miau miau!
Miau está pulando silenciosamente...
```
