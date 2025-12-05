# 2 - Sintaxe C#

Módulo completo sobre sintaxe fundamental de C#, com práticas e projetos integrados.

## 📚 Estrutura do Módulo

```
2-sintaxe-csharp/
│
├── operadores/
│   ├── operadores.csproj
│   ├── Program.cs
│   └── README.md
│
├── estruturas-repeticao/
│   ├── estruturas-repeticao.csproj
│   ├── Program.cs
│   └── README.md
│
├── arrays-listas/
│   ├── arrays-listas.csproj
│   ├── Program.cs
│   └── README.md
│
└── projeto-estacionamento/
    ├── projeto-estacionamento.csproj
    ├── Program.cs
    └── README.md
```

## 🎯 Objetivos de Aprendizado

### 1️⃣ **Operadores** (`operadores/`)
Compreender e dominar todos os tipos de operadores em C#:

- ✅ Operadores Aritméticos: `+`, `-`, `*`, `/`, `%`
- ✅ Operadores de Incremento/Decremento: `++`, `--`
- ✅ Operadores de Comparação: `==`, `!=`, `>`, `<`, `>=`, `<=`
- ✅ Operadores Lógicos: `&&`, `||`, `!`
- ✅ Operadores de Atribuição: `=`, `+=`, `-=`, `*=`, `/=`
- ✅ Operador Ternário: `condicao ? verdadeiro : falso`
- ✅ Operador NULL-COALESCING: `??`
- ✅ Operadores de Tipo: `is`, `as`

**Como executar:**
```bash
cd operadores
dotnet run
```

---

### 2️⃣ **Estruturas de Repetição** (`estruturas-repeticao/`)
Aprender todos os tipos de loops e sua aplicação prática:

- ✅ **FOR**: Repetição com contador
- ✅ **WHILE**: Repetição com condição
- ✅ **DO-WHILE**: Executa pelo menos uma vez
- ✅ **FOREACH**: Iteração sobre coleções
- ✅ **BREAK**: Encerra o loop
- ✅ **CONTINUE**: Pula para próxima iteração
- ✅ **Loops aninhados**: Matrizes e estruturas complexas

**Como executar:**
```bash
cd estruturas-repeticao
dotnet run
```

**Exemplos práticos:**
- Tabuadas
- Menus interativos
- Cálculo de soma
- Fatorial
- Matrizes

---

### 3️⃣ **Arrays e Listas** (`arrays-listas/`)
Trabalhar com coleções de dados:

- ✅ **Arrays**: Tamanho fixo
- ✅ **Arrays Multidimensionais**: Matrizes 2D
- ✅ **Jagged Arrays**: Arrays de arrays
- ✅ **List<T>**: Coleções dinâmicas
- ✅ **Operações Comuns**: Add, Insert, Remove
- ✅ **Pesquisa**: Contains, IndexOf
- ✅ **Ordenação**: Sort, Reverse
- ✅ **LINQ**: Where, Select, FirstOrDefault, Any, Count

**Como executar:**
```bash
cd arrays-listas
dotnet run
```

**Métodos úteis:**
- `Average()`: Média de valores
- `Max()`: Valor máximo
- `Min()`: Valor mínimo
- `Where()`: Filtragem
- `Select()`: Transformação

---

### 4️⃣ **Projeto Estacionamento** (`projeto-estacionamento/`)
Aplicação completa integrando todos os conceitos:

🚗 **Sistema de Gerenciamento de Estacionamento**

**Funcionalidades:**
- ✅ Adicionar veículos com informações completas
- ✅ Remover veículos com cálculo de tarifa
- ✅ Listar todos os veículos
- ✅ Buscar veículos específicos
- ✅ Controle de capacidade (máx. 10 veículos)
- ✅ Cálculo automático de tarifa (R$ 5,00/hora)
- ✅ Interface interativa com menu

**Conceitos Aplicados:**
- Classes e Objetos
- Properties (get/set)
- Coleções (List<T>)
- Estruturas de Repetição
- Operadores e Operações Lógicas
- LINQ (FirstOrDefault, Any)
- DateTime (Data e Hora)
- Métodos com Parâmetros

**Como executar:**
```bash
cd projeto-estacionamento
dotnet run
```

**Menu:**
```
1. Adicionar veículo
2. Remover veículo
3. Listar veículos
4. Buscar veículo
5. Ver espaços disponíveis
0. Sair
```

---

## 🚀 Guia de Uso Rápido

### Executar todos os projetos

```bash
# 1. Operadores
cd 2-sintaxe-csharp/operadores && dotnet run

# 2. Estruturas de Repetição
cd ../estruturas-repeticao && dotnet run

# 3. Arrays e Listas
cd ../arrays-listas && dotnet run

# 4. Projeto Estacionamento
cd ../projeto-estacionamento && dotnet run
```

### Criar novo projeto similar

```bash
# Criar novo projeto .NET
dotnet new console -n seu-projeto
cd seu-projeto

# Restaurar dependências
dotnet restore

# Executar
dotnet run

# Build para release
dotnet publish -c Release
```

---

## 📖 Resumo de Conceitos

| Conceito | Descrição | Exemplo |
|----------|-----------|---------|
| **Operadores** | Realizam operações | `a + b`, `a > b` |
| **FOR** | Loop com contador | `for(int i=0; i<10; i++)` |
| **WHILE** | Loop com condição | `while(x < 10) { x++; }` |
| **FOREACH** | Loop sobre coleção | `foreach(var item in lista)` |
| **Array** | Coleção de tamanho fixo | `int[] nums = new int[5];` |
| **List<T>** | Coleção dinâmica | `List<int> nums = new();` |
| **LINQ** | Consulta sobre dados | `nums.Where(n => n > 5)` |
| **Classe** | Tipo de referência | `class Pessoa { ... }` |
| **Property** | Acessador encapsulado | `public string Nome { get; set; }` |

---

## 🎓 Próximos Passos

Após completar este módulo, estude:

1. **Programação Orientada a Objetos (POO)**
   - Herança
   - Polimorfismo
   - Encapsulamento
   - Interfaces

2. **Tratamento de Erros**
   - Try/Catch/Finally
   - Custom Exceptions

3. **ASP.NET Core**
   - Web APIs
   - Minimal APIs
   - Entity Framework

4. **Banco de Dados**
   - SQL Server
   - EF Core ORM

---

## 💡 Dicas Importantes

✅ **Pratique regularmente** - Todos os dias um pouco
✅ **Modifique os exemplos** - Não apenas copie, experimente
✅ **Crie seus próprios projetos** - Use o estacionamento como referência
✅ **Entenda os conceitos** - Não decora, entende!
✅ **Revise regularmente** - Reforce o aprendizado

---

## 📚 Recursos Adicionais

- [Documentação C#](https://docs.microsoft.com/en-us/dotnet/csharp/)
- [.NET SDK](https://dotnet.microsoft.com/download)
- [Microsoft Learn - C#](https://learn.microsoft.com/en-us/dotnet/csharp/)
- [LINQ Tutorial](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/)

---

## 👨‍🏫 Autor

Desenvolvido como parte do **Bootcamp WEX - End to End Engineering** (DIO)

---

**Bom aprendizado! 🚀**
