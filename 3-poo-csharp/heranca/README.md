# Herança em C#

## Conceito

Herança é um dos pilares da POO que permite que uma classe **herde propriedades e métodos** de outra classe. A classe que herda é chamada de **subclasse (derivada)** e a classe de origem é chamada de **superclasse (base)**.

## Características

✅ **Reutilização de Código**: Evita duplicação
✅ **Hierarquia**: Organiza classes em uma estrutura hierárquica
✅ **Especialização**: Subclasses podem estender ou modificar comportamentos
✅ **Polimorfismo**: Permite usar subclasses como se fossem da classe base

## Tipos de Herança

| Tipo | Descrição |
|------|-----------|
| **Herança Simples** | Uma classe herda de apenas uma classe base |
| **Herança Multinível** | A -> B -> C |
| **Herança Hierárquica** | Múltiplas classes herdam da mesma base |

## Modificador virtual e override

```csharp
public virtual void Acelerar()  // Pode ser sobrescrito
{
    Velocidade += 10;
}

public override void Acelerar()  // Sobrescreve o método da classe base
{
    Velocidade += 15;
}
```

## Exemplo Prático

No código `Program.cs`:

- **Veiculo**: Classe base com propriedades comuns
- **Carro, Moto, Caminhao**: Classes derivadas que especializam Veiculo
- **Métodos virtuais**: Acelerar, Frear e Exibir_Informacoes podem ser sobrescritos

## Como Executar

```bash
dotnet run
```

## Hierarquia

```
       Veiculo
         / | \
        /  |  \
    Carro Moto Caminhao
```

## Saída Esperada

```
=== DEMONSTRAÇÃO DE HERANÇA ===

--- CARRO ---
Marca: Toyota
Modelo: Corolla
Ano: 2023
Velocidade: 0 km/h
Número de Portas: 4
Combustível: Gasolina

Carro Toyota Corolla acelerou para 15 km/h
Carro Toyota Corolla acelerou para 30 km/h
Carro abriu uma de suas 4 portas
Carro Toyota Corolla freou para 15 km/h

--- MOTO ---
Marca: Honda
Modelo: CB 500
Ano: 2022
Velocidade: 0 km/h
Tem Baú: Sim
Cilindradas: 500cc

Moto Honda CB 500 acelerou para 20 km/h
Moto Honda CB 500 acelerou para 40 km/h
Moto Honda CB 500 acelerou para 60 km/h
Moto Honda CB 500 está dando grau em alta velocidade!
Moto Honda CB 500 freou para 50 km/h

--- CAMINHÃO ---
Marca: Volvo
Modelo: FH16
Ano: 2021
Velocidade: 0 km/h
Capacidade de Carga: 30 toneladas
Número de Eixos: 3

Caminhão Volvo FH16 acelerou para 8 km/h
Caminhão carregado com 25 toneladas
Peso excede capacidade de 30 toneladas
Caminhão freou com força para -7 km/h

--- POLIMORFISMO COM HERANÇA ---

Acelerando Toyota Corolla:
Carro Toyota Corolla acelerou para 15 km/h

Acelerando Honda CB 500:
Moto Honda CB 500 acelerou para 20 km/h

Acelerando Volvo FH16:
Caminhão Volvo FH16 acelerou para 8 km/h
```
