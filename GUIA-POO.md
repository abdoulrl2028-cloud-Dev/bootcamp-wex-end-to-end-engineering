# Guia Completo - Módulo de Programação Orientada a Objetos (POO)

## 🎯 Visão Geral

O módulo **3-poo-csharp** é uma progressão completa pelos 4 pilares fundamentais da Programação Orientada a Objetos, seguido por um projeto prático integrador.

```
┌─────────────────────────────────────────────────────────┐
│                    3-POO-CSHARP                          │
├─────────────────────────────────────────────────────────┤
│                                                          │
│  ┌──────────────┐  ┌──────────────┐  ┌──────────────┐  │
│  │  ABSTRAÇÃO   │  │ENCAPSULAMENTO│  │   HERANÇA    │  │
│  │              │  │              │  │              │  │
│  │ - Interface  │  │ - Private    │  │ - Base       │  │
│  │ - Abstract   │  │ - Properties │  │ - Derived    │  │
│  │ - Contrato   │  │ - Validação  │  │ - Virtual    │  │
│  └──────────────┘  └──────────────┘  └──────────────┘  │
│                                                          │
│            ┌──────────────────────────────┐             │
│            │     POLIMORFISMO             │             │
│            │                              │             │
│            │  - Override                  │             │
│            │  - Sobrecarga                │             │
│            │  - Implementações Múltiplas  │             │
│            └──────────────────────────────┘             │
│                                                          │
│  ╔════════════════════════════════════════════╗         │
│  ║   PROJETO PRÁTICO: GERENCIADOR CELULAR    ║         │
│  ║   (Integra todos os 4 pilares)            ║         │
│  ╚════════════════════════════════════════════╝         │
└─────────────────────────────────────────────────────────┘
```

## 📂 Estrutura de Diretórios

```
3-poo-csharp/
│
├── 📄 README.md                    ← Documentação principal do módulo
│
├── 📁 abstracao/
│   ├── 📄 README.md               ← Conceitos de abstração
│   ├── 📄 Program.cs              ← Código completo com exemplos
│   └── 📄 abstracao.csproj        ← Arquivo de projeto
│
├── 📁 encapsulamento/
│   ├── 📄 README.md               ← Conceitos de encapsulamento
│   ├── 📄 Program.cs              ← Código completo com exemplos
│   └── 📄 encapsulamento.csproj   ← Arquivo de projeto
│
├── 📁 heranca/
│   ├── 📄 README.md               ← Conceitos de herança
│   ├── 📄 Program.cs              ← Código completo com exemplos
│   └── 📄 heranca.csproj          ← Arquivo de projeto
│
├── 📁 polimorfismo/
│   ├── 📄 README.md               ← Conceitos de polimorfismo
│   ├── 📄 Program.cs              ← Código completo com exemplos
│   └── 📄 polimorfismo.csproj     ← Arquivo de projeto
│
└── 📁 projeto-celular/
    ├── 📄 README.md               ← Documentação do projeto
    ├── 📄 Program.cs              ← Código completo (800+ linhas)
    └── 📄 projeto-celular.csproj  ← Arquivo de projeto
```

## 🚀 Como Usar Este Módulo

### Passo 1: Comece pela Abstração
```bash
cd /workspaces/bootcamp-wex-end-to-end-engineering/3-poo-csharp/abstracao
dotnet run
# Leia o README.md para entender conceitos
```

**O que você aprenderá:**
- Como criar interfaces
- Como usar classes abstratas
- Como definir contratos que subclasses devem seguir

---

### Passo 2: Estude o Encapsulamento
```bash
cd /workspaces/bootcamp-wex-end-to-end-engineering/3-poo-csharp/encapsulamento
dotnet run
# Leia o README.md para entender proteção de dados
```

**O que você aprenderá:**
- Modificadores de acesso (private, public, protected)
- Propriedades com backing fields
- Validação de dados
- Propriedades somente leitura

---

### Passo 3: Explore a Herança
```bash
cd /workspaces/bootcamp-wex-end-to-end-engineering/3-poo-csharp/heranca
dotnet run
# Leia o README.md para entender reutilização de código
```

**O que você aprenderá:**
- Como criar hierarquias de classes
- Métodos virtuais e sobrescrita
- Uso de `base` para chamar métodos da classe pai
- Especialização de comportamentos

---

### Passo 4: Domine o Polimorfismo
```bash
cd /workspaces/bootcamp-wex-end-to-end-engineering/3-poo-csharp/polimorfismo
dotnet run
# Leia o README.md para entender flexibilidade
```

**O que você aprenderá:**
- Polimorfismo de herança (override)
- Polimorfismo de método (sobrecarga)
- Interfaces como polimorfismo
- Como usar polimorfismo com coleções

---

### Passo 5: Implemente o Projeto Prático
```bash
cd /workspaces/bootcamp-wex-end-to-end-engineering/3-poo-csharp/projeto-celular
dotnet run
```

**Este projeto demonstra:**
- ✅ Abstração: Interface `IDispositivoMovel` e classe abstrata `DispositivoMovel`
- ✅ Encapsulamento: Campos privados com propriedades controladas
- ✅ Herança: `Smartphone`, `Tablet`, `Smartwatch` herdam de `DispositivoMovel`
- ✅ Polimorfismo: Cada classe implementa `Exibir_Status()` de forma única
- ✅ Gerenciamento: Classe `GerenciadorDispositivos` para orquestração

## 📊 Comparação dos Conceitos

| Conceito | Problema | Solução | Exemplo |
|----------|----------|---------|---------|
| **Abstração** | Como simplificar complexidade? | Esconder detalhes, expor interface | `public interface IAnimal` |
| **Encapsulamento** | Como proteger dados? | Usar private + propriedades | `public string Nome { get; set; }` |
| **Herança** | Como evitar duplicação? | Reutilizar através de hierarquia | `class Carro : Veiculo` |
| **Polimorfismo** | Como flexibilizar? | Múltiplas implementações | `override void Acelerar()` |

## 💻 Exemplos Rápidos

### Abstração
```csharp
public interface IAnimal
{
    void Fazer_Som();
}

public abstract class Animal : IAnimal
{
    public abstract void Fazer_Som();
}
```

### Encapsulamento
```csharp
private int _idade;

public int Idade
{
    get { return _idade; }
    set { if (value >= 0) _idade = value; }
}
```

### Herança
```csharp
public class Veiculo
{
    public virtual void Acelerar() { }
}

public class Carro : Veiculo
{
    public override void Acelerar() { /* implementação */ }
}
```

### Polimorfismo
```csharp
List<Forma> formas = new List<Forma> 
{ 
    new Quadrado(5), 
    new Circulo(3) 
};

foreach (var forma in formas)
{
    forma.Calcular_Area();  // Cada uma tem sua implementação!
}
```

## 🎓 Progressão Recomendada

```
Iniciante
    ↓
1. Leia README.md principal (este arquivo)
2. Execute abstracao/Program.cs
3. Estude abstracao/README.md
    ↓
Intermediário
    ↓
4. Execute encapsulamento/Program.cs
5. Estude encapsulamento/README.md
6. Execute heranca/Program.cs
7. Estude heranca/README.md
    ↓
Avançado
    ↓
8. Execute polimorfismo/Program.cs
9. Estude polimorfismo/README.md
10. Execute projeto-celular/Program.cs
11. Estude projeto-celular/README.md
12. Modifique e estenda o projeto
    ↓
Especialista
    ↓
13. Implemente os exercícios propostos
14. Crie seu próprio projeto POO
15. Explore Design Patterns
```

## 📋 Checklist de Aprendizado

### Abstração ✓
- [ ] Entendo o que é abstração
- [ ] Consigo criar interfaces
- [ ] Consigo criar classes abstratas
- [ ] Entendo a diferença entre interface e classe abstrata
- [ ] Executei o exemplo com sucesso

### Encapsulamento ✓
- [ ] Entendo modificadores de acesso
- [ ] Consigo criar propriedades
- [ ] Consigo fazer validação em propriedades
- [ ] Entendo o conceito de backing field
- [ ] Executei o exemplo com sucesso

### Herança ✓
- [ ] Entendo herança simples
- [ ] Consigo criar classes derivadas
- [ ] Entendo métodos virtuais
- [ ] Consigo fazer override
- [ ] Executei o exemplo com sucesso

### Polimorfismo ✓
- [ ] Entendo polimorfismo de herança
- [ ] Entendo polimorfismo de método (sobrecarga)
- [ ] Consigo usar polimorfismo com coleções
- [ ] Entendo late binding
- [ ] Executei o exemplo com sucesso

### Projeto Prático ✓
- [ ] Entendo a arquitetura do projeto
- [ ] Consigo identificar abstração no projeto
- [ ] Consigo identificar encapsulamento no projeto
- [ ] Consigo identificar herança no projeto
- [ ] Consigo identificar polimorfismo no projeto
- [ ] Executei o projeto com sucesso
- [ ] Estendi o projeto com novas funcionalidades

## 🔍 Análise Detalhada do Projeto Celular

### Estrutura de Classes

```
                ┌─────────────────┐
                │IDispositivoMovel│
                │   (interface)   │
                └────────┬────────┘
                         │
                         │ implements
                         ↓
        ┌────────────────────────────────┐
        │   DispositivoMovel (abstrata)  │
        │  - Marca, Modelo, Bateria      │
        │  - Ligar(), Desligar()         │
        │  - Exibir_Status() [virtual]   │
        └────────────┬───────────────────┘
                     │ herda
                ┌────┼────┬─────────┐
                ↓    ↓    ↓         ↓
            ┌────────┐ ┌─────┐ ┌──────────┐
            │Smartphone│Tablet  │Smartwatch
            └────────┘ └─────┘ └──────────┘
```

### Recursos Demonstrados

#### Abstração
```csharp
public interface IDispositivoMovel { ... }
public abstract class DispositivoMovel { ... }
```

#### Encapsulamento
```csharp
private string _marca;
private bool _ligado;
private decimal _bateria;

public string Marca { get { return _marca; } }
public bool Ligado { get { return _ligado; } }
```

#### Herança
```csharp
public class Smartphone : DispositivoMovel { }
public class Tablet : DispositivoMovel { }
public class Smartwatch : DispositivoMovel { }
```

#### Polimorfismo
```csharp
public override void Exibir_Status() { ... }  // Cada classe diferente
DispositivoMovel[] dispositivos = { ... };     // Array pode conter qualquer tipo
```

## 🎯 Objetivos de Aprendizado

Após completar este módulo, você será capaz de:

✅ **Criar abstrações** usando interfaces e classes abstratas
✅ **Encapsular dados** protegendo-os com propriedades e validações
✅ **Reutilizar código** através de hierarquias de herança
✅ **Implementar polimorfismo** com override e sobrecarga
✅ **Combinar todos os 4 pilares** em aplicações reais
✅ **Escrever código orientado a objetos** profissional e manutenível

## 🚀 Próximos Passos

1. **Padrões de Projeto**: Aprenda Strategy, Factory, Singleton, Observer
2. **Princípios SOLID**: Single Responsibility, Open/Closed, Liskov Substitution, etc.
3. **Testes Unitários**: xUnit, Moq para testar suas classes
4. **Frameworks**: Entity Framework, ASP.NET Core
5. **Arquitetura**: Clean Architecture, Onion Architecture

## 📞 Dúvidas Frequentes

**P: Qual é a diferença entre classe abstrata e interface?**
R: Uma classe abstrata pode ter implementação; uma interface é apenas um contrato. Use classe abstrata quando há código comum; use interface para definir um comportamento que qualquer classe pode implementar.

**P: Por que encapsular se posso deixar tudo público?**
R: Encapsulamento permite validação, segurança e flexibilidade futura. Se você precisar mudar como um campo é armazenado, com encapsulamento é fácil; sem, quebra tudo.

**P: Quando usar herança vs composição?**
R: Use herança para relações "é um" (Carro é um Veiculo). Use composição para relações "tem um" (Carro tem um Motor).

**P: Como sei quando algo é polimórfico?**
R: Quando diferentes tipos respondem ao mesmo método de formas diferentes. Se você escreve `foreach (var item in lista) item.Fazer_Algo()` e cada item faz algo diferente, é polimorfismo!

---

**Comece pelo passo 1 e progrida regularmente. Boa sorte! 🚀**
