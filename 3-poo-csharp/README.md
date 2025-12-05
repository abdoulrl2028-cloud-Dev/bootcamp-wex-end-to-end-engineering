# 3 - Programação Orientada a Objetos (POO) em C#

Bem-vindo ao módulo de **Programação Orientada a Objetos**! Este módulo cobre os 4 pilares fundamentais da POO com exemplos práticos e um projeto integrador.

## 📚 Conteúdo

Este módulo está dividido em 5 seções:

### 1. **Abstração** (`abstracao/`)
Aprenda a esconder detalhes complexos e expor apenas o essencial.

- **Conceitos**: Classes abstratas, interfaces, contratos
- **Arquivo**: `Program.cs`
- **Executar**: `cd abstracao && dotnet run`

#### Principais Classes
- `IAnimal`: Interface definindo contrato
- `Animal`: Classe abstrata base
- `Cachorro`, `Gato`: Implementações concretas

---

### 2. **Encapsulamento** (`encapsulamento/`)
Aprenda a proteger dados com validações e controle de acesso.

- **Conceitos**: Modificadores de acesso, propriedades, validações
- **Arquivo**: `Program.cs`
- **Executar**: `cd encapsulamento && dotnet run`

#### Principais Classes
- `Pessoa`: Propriedades com validação
- `Produto`: Propriedades somente leitura e atualização controlada

---

### 3. **Herança** (`heranca/`)
Aprenda a reutilizar código através de hierarquias de classes.

- **Conceitos**: Herança simples, herança multinível, métodos virtuais
- **Arquivo**: `Program.cs`
- **Executar**: `cd heranca && dotnet run`

#### Principais Classes
- `Veiculo`: Classe base
- `Carro`, `Moto`, `Caminhao`: Classes derivadas especializadas

**Hierarquia:**
```
       Veiculo
         / | \
        /  |  \
    Carro Moto Caminhao
```

---

### 4. **Polimorfismo** (`polimorfismo/`)
Aprenda como diferentes objetos respondem ao mesmo método de formas diferentes.

- **Conceitos**: Override, sobrecarga de métodos, polimorfismo em tempo de execução
- **Arquivo**: `Program.cs`
- **Executar**: `cd polimorfismo && dotnet run`

#### Principais Classes
- `Forma`: Classe abstrata base
- `Quadrado`, `Retangulo`, `Circulo`, `Triangulo`: Implementações
- `Calculadora`: Demonstração de sobrecarga

---

### 5. **Projeto Prático - Celular** (`projeto-celular/`)
Projeto integrador que aplica TODOS os conceitos POO em um sistema real.

- **Conceitos**: Integração de todos os 4 pilares
- **Arquivo**: `Program.cs`
- **Executar**: `cd projeto-celular && dotnet run`

#### Estrutura do Projeto
```
IDispositivoMovel (interface)
        △
        │
DispositivoMovel (classe abstrata)
      / | \
     /  |  \
Smartphone Tablet Smartwatch

GerenciadorDispositivos (gerencia múltiplos dispositivos)
```

#### Recursos Principais
- **Abstração**: Classe abstrata `DispositivoMovel`
- **Encapsulamento**: Campos privados, propriedades públicas controladas
- **Herança**: Specialização em Smartphone, Tablet, Smartwatch
- **Polimorfismo**: Cada tipo implementa `Exibir_Status()` diferente

---

## 🚀 Como Executar Cada Seção

### Abstração
```bash
cd /workspaces/bootcamp-wex-end-to-end-engineering/3-poo-csharp/abstracao
dotnet run
```

### Encapsulamento
```bash
cd /workspaces/bootcamp-wex-end-to-end-engineering/3-poo-csharp/encapsulamento
dotnet run
```

### Herança
```bash
cd /workspaces/bootcamp-wex-end-to-end-engineering/3-poo-csharp/heranca
dotnet run
```

### Polimorfismo
```bash
cd /workspaces/bootcamp-wex-end-to-end-engineering/3-poo-csharp/polimorfismo
dotnet run
```

### Projeto Celular (Integrador)
```bash
cd /workspaces/bootcamp-wex-end-to-end-engineering/3-poo-csharp/projeto-celular
dotnet run
```

---

## 📋 Estrutura de Diretórios

```
3-poo-csharp/
├── abstracao/
│   ├── abstracao.csproj
│   ├── Program.cs
│   └── README.md
├── encapsulamento/
│   ├── encapsulamento.csproj
│   ├── Program.cs
│   └── README.md
├── heranca/
│   ├── heranca.csproj
│   ├── Program.cs
│   └── README.md
├── polimorfismo/
│   ├── polimorfismo.csproj
│   ├── Program.cs
│   └── README.md
├── projeto-celular/
│   ├── projeto-celular.csproj
│   ├── Program.cs
│   └── README.md
└── README.md (este arquivo)
```

---

## 🎯 Os 4 Pilares da POO

| Pilar | Descrição | Exemplo |
|-------|-----------|---------|
| **Abstração** | Esconder complexidade | `abstract class Animal` |
| **Encapsulamento** | Proteger dados | `private decimal _salario;` |
| **Herança** | Reutilizar código | `class Carro : Veiculo` |
| **Polimorfismo** | Múltiplas formas | `override void Calcular_Area()` |

---

## 💡 Conceitos Chave

### Classes Abstratas vs Interfaces

**Classe Abstrata**
```csharp
public abstract class Animal
{
    public abstract void Fazer_Som();
    public virtual void Dormir() { /* implementação */ }
}
```

**Interface**
```csharp
public interface IAnimal
{
    void Fazer_Som();
    void Mover();
}
```

### Modificadores de Acesso

| Modificador | Acesso |
|-----------|--------|
| `public` | Qualquer lugar |
| `private` | Apenas na classe |
| `protected` | Classe e subclasses |
| `internal` | Mesmo assembly |

### Herança vs Composição

**Herança** (Relação "é um")
```csharp
class Carro : Veiculo  // Carro É um Veiculo
```

**Composição** (Relação "tem um")
```csharp
class Carro
{
    public Motor motor;  // Carro TEM um Motor
}
```

---

## 📝 Exercícios Propostos

### 1. Abstracao
- [ ] Adicionar classe `Ave` e `Peixe` ao exemplo
- [ ] Implementar interface `IVoador`

### 2. Encapsulamento
- [ ] Adicionar validação de CPF na classe `Pessoa`
- [ ] Criar classe `Conta_Bancaria` com propriedades encapsuladas

### 3. Heranca
- [ ] Adicionar classe `Bicicleta` que herda de `Veiculo`
- [ ] Implementar classe `Onibus` com propriedade de capacidade

### 4. Polimorfismo
- [ ] Adicionar classe `Elipse` ao exemplo de formas
- [ ] Criar classe `Calculadora_Avancada` com mais operações sobrecarregadas

### 5. Projeto Celular
- [ ] Adicionar classe `FoneBluetooth` que herda de `DispositivoMovel`
- [ ] Implementar sistema de sincronização entre dispositivos
- [ ] Adicionar serialização (salvar/carregar estado)

---

## 🔗 Relacionamento Entre Conceitos

```
┌─────────────────────────────────────────┐
│    ABSTRAÇÃO (O quê?)                   │
│  Define interfaces e classes abstratas  │
│                                         │
│  ├─ ENCAPSULAMENTO (Como proteger?)    │
│  │  Controla acesso aos dados           │
│  │                                      │
│  ├─ HERANÇA (Como reutilizar?)         │
│  │  Cria hierarquias de classes        │
│  │                                      │
│  └─ POLIMORFISMO (Como flexibilizar?)  │
│     Permite múltiplas implementações    │
└─────────────────────────────────────────┘
```

---

## 📊 Resumo Comparativo

| Aspecto | Abstração | Encapsulamento | Herança | Polimorfismo |
|---------|-----------|----------------|---------|--------------|
| **Foco** | O quê? | Como proteger? | Como reutilizar? | Como flexibilizar? |
| **Problema** | Complexidade | Segurança de dados | Duplicação | Rigidez |
| **Solução** | Interfaces/abstratas | Propriedades | Herança | Override |
| **Exemplo** | `IAnimal` | `private int _idade` | `Carro : Veiculo` | `virtual void Acelerar()` |

---

## 🎓 Próximas Etapas

Após dominar estes conceitos, explore:

1. **Padrões de Projeto** (Design Patterns)
   - Strategy, Factory, Singleton, Observer

2. **SOLID Principles**
   - Single Responsibility, Open/Closed, Liskov Substitution, etc.

3. **Testes Unitários**
   - xUnit, Moq para testar classes POO

4. **Frameworks**
   - Entity Framework para persistência
   - ASP.NET Core para aplicações web

---

## 📚 Recursos Adicionais

### Documentação Oficial
- [Microsoft - Programação Orientada a Objetos em C#](https://docs.microsoft.com/pt-br/dotnet/csharp/fundamentals/object-oriented/)
- [Classes e Structs em C#](https://docs.microsoft.com/pt-br/dotnet/csharp/fundamentals/types/classes)

### Livros Recomendados
- "Clean Code" - Robert C. Martin
- "Design Patterns" - Gang of Four
- "Object-Oriented Programming in C#" - Svetlin Nakov

---

## ✅ Checklist de Aprendizado

- [ ] Entendo o conceito de abstração
- [ ] Consigo criar classes abstratas e interfaces
- [ ] Sei usar encapsulamento com propriedades
- [ ] Consigo criar hierarquias de herança
- [ ] Entendo polimorfismo e override
- [ ] Consigo combinar todos os 4 pilares
- [ ] Implementei o projeto do celular com sucesso
- [ ] Consigo resolver os exercícios propostos

---

## 🤝 Contribuições

Se encontrou erros ou quer sugerir melhorias, sinta-se livre para abrir uma issue ou pull request!

---

**Bom aprendizado! 🚀**
