# 🚗 Projeto Estacionamento

Sistema completo de gerenciamento de estacionamento desenvolvido em C#, integrando todos os conceitos de sintaxe aprendidos.

## Características

✅ **Adicionar Veículos** - Registra novos veículos com placa, marca, modelo e cor
✅ **Remover Veículos** - Remove veículos e calcula o valor a pagar
✅ **Listar Veículos** - Mostra todos os veículos estacionados
✅ **Buscar Veículos** - Encontra um veículo específico
✅ **Controle de Capacidade** - Limite de 10 veículos
✅ **Cálculo de Tarifa** - R$ 5,00 por hora (arredonda pra cima)

## Conceitos Utilizados

### 1. **Classes e Objetos**
```csharp
class Veiculo
{
    public string? Placa { get; set; }
    public string? Marca { get; set; }
    // ...
}
```

### 2. **Coleções (List<T>)**
```csharp
List<Veiculo> veiculos = new List<Veiculo>();
```

### 3. **Estruturas de Repetição**
```csharp
while (continuar) { ... }
for (int i = 0; i < veiculos.Count; i++) { ... }
```

### 4. **Operadores**
```csharp
if (veiculos.Count >= capacidadeMaxima) { ... }
decimal valor = (decimal)Math.Ceiling(duracao.TotalHours) * precoHora;
```

### 5. **LINQ**
```csharp
Veiculo? veiculo = veiculos.FirstOrDefault(v => v.Placa == placa);
bool existe = veiculos.Any(v => v.Placa == placa);
```

### 6. **Métodos**
```csharp
public void AdicionarVeiculo(string placa, string marca, string modelo, string cor)
public void RemoverVeiculo(string placa)
```

### 7. **Properties (get/set)**
```csharp
public string? Placa { get; set; }
```

## Como Executar

```bash
dotnet run
```

## Menu Principal

```
--- MENU PRINCIPAL ---
1. Adicionar veículo
2. Remover veículo
3. Listar veículos
4. Buscar veículo
5. Ver espaços disponíveis
0. Sair
```

## Exemplos de Uso

### Adicionar Veículo
```
Escolha uma opção: 1
Digite a placa do veículo: ABC-1234
Digite a marca: Toyota
Digite o modelo: Corolla
Digite a cor: Prata
✅ Veículo ABC-1234 adicionado com sucesso!
```

### Remover Veículo
```
Escolha uma opção: 2
Digite a placa do veículo a remover: ABC-1234
✅ Veículo ABC-1234 removido!
Tempo de permanência: 1h 30m
Valor a pagar: R$ 10.00
```

### Listar Veículos
```
📋 Veículos no estacionamento:
═══════════════════════════════════════════════════════════
1. Placa: ABC-1234, Marca: Toyota, Modelo: Corolla, Cor: Prata, Entrada: 05/12/2025 14:30
2. Placa: XYZ-5678, Marca: Honda, Modelo: Civic, Cor: Branca, Entrada: 05/12/2025 14:35

Total: 2/10
═══════════════════════════════════════════════════════════
```

## Estrutura do Código

```
Program.cs
├── Classe Veiculo
│   ├── Properties: Placa, Marca, Modelo, Cor, DataEntrada
│   └── ToString()
├── Classe EstacionamentoManager
│   ├── AdicionarVeiculo()
│   ├── RemoverVeiculo()
│   ├── ListarVeiculos()
│   ├── BuscarVeiculo()
│   ├── MostrarEspacosDisponiveis()
│   └── VeiculoExiste()
└── Class Program
    ├── Main()
    ├── AdicionarVeiculo()
    ├── RemoverVeiculo()
    └── BuscarVeiculo()
```

## Melhorias Futuras

- 💾 Persistência de dados em arquivo
- 🔐 Autenticação de funcionários
- 📊 Relatórios de faturamento
- 🌐 Interface web com ASP.NET Core
- 📱 Aplicativo mobile
- 🗄️ Banco de dados SQL Server

## Autor

Desenvolvido como parte do Bootcamp WEX - End to End Engineering (DIO)
