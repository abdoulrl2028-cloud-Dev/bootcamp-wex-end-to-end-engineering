# Projeto Celular - Sistema de Gerenciamento de Dispositivos Móveis

## Objetivo

Aplicar todos os conceitos de POO em C# em um projeto prático e integrado que simula um sistema de gerenciamento de dispositivos móveis.

## Conceitos Aplicados

### 1. **Abstração**
- Classe abstrata `DispositivoMovel` define comportamento comum
- Interface `IDispositivoMovel` define contrato
- Detalhes de implementação escondidos

### 2. **Encapsulamento**
- Campos privados protegidos (`_marca`, `_modelo`, `_bateria`)
- Propriedades públicas com controle de acesso
- Método `Consumir_Bateria()` privado

### 3. **Herança**
- `Smartphone`, `Tablet`, `Smartwatch` herdam de `DispositivoMovel`
- Reutilização de código comum
- Especialização em subclasses

### 4. **Polimorfismo**
- Cada classe implementa `Exibir_Status()` diferente
- Método `Ligar()` sobrescrito no Tablet
- Um array de `DispositivoMovel[]` funciona com todos os tipos

## Estrutura de Classes

```
                IDispositivoMovel
                      △
                      │
              DispositivoMovel (abstrata)
                    / | \
                   /  |  \
                  /   |   \
            Smartphone Tablet Smartwatch

GerenciadorDispositivos
```

## Recursos Principais

### DispositivoMovel (Classe Base Abstrata)
- Propriedades: `Marca`, `Modelo`, `Bateria`, `Ligado`
- Métodos: `Ligar()`, `Desligar()`, `Carregar_Bateria()`, `Exibir_Status()`

### Smartphone
- Características: Câmera, Aplicativos, Chamadas
- Métodos: `Fazer_Chamada()`, `Enviar_Mensagem()`, `Tirar_Foto()`, `Instalar_App()`

### Tablet
- Características: Tela Grande, Caneta Opcional, Taxa Atualização
- Métodos: `Assistir_Video()`, `Ler_Livro()`, `Usar_Caneta()`

### Smartwatch
- Características: GPS, Monitor Cardíaco, Pedômetro
- Métodos: `Rastrear_Passos()`, `Medir_Frequencia_Cardiaca()`, `Rastrear_Localizacao()`

### GerenciadorDispositivos
- Gerencia múltiplos dispositivos
- Métodos: `Ligar_Todos()`, `Desligar_Todos()`, `Exibir_Status_Todos()`, `Carregar_Todos()`

## Como Executar

```bash
cd /workspaces/bootcamp-wex-end-to-end-engineering/3-poo-csharp/projeto-celular
dotnet run
```

## Saída Esperada

```
╔════════════════════════════════════════════════════════╗
║   SISTEMA DE GERENCIAMENTO DE DISPOSITIVOS MÓVEIS      ║
╚════════════════════════════════════════════════════════╝

Dispositivo Apple iPhone 15 Pro adicionado!
Dispositivo Samsung Galaxy S24 adicionado!
Dispositivo Apple iPad Air adicionado!
Dispositivo Apple Watch Series 9 adicionado!

--- Ligando todos os dispositivos ---
Apple iPhone 15 Pro ligado!
Samsung Galaxy S24 ligado!
Apple iPad Air ligado!
Tablet em modo 120Hz
Apple Watch Series 9 ligado!

--- Usando iPhone ---
Chamando João...
SMS para Maria: Oi, tudo bem?
Foto capturada!
Aplicativo 'WhatsApp' instalado!
Aplicativos instalados:
  - Câmera
  - Galeria
  - Contatos
  - WhatsApp

--- Usando iPad ---
Reproduzindo vídeo...
Lendo livro digital...
Usando caneta digital para desenhar...

--- Usando Apple Watch ---
Passos registrados: 100
Frequência cardíaca: 75 bpm
Localizando...

╔════════════════════════════════════════════════════════╗
║        STATUS DE TODOS OS DISPOSITIVOS                 ║
╚════════════════════════════════════════════════════════╝

=== STATUS DO SMARTPHONE ===
Marca: Apple
Modelo: iPhone 15 Pro
Sistema Operacional: iOS 17
Bateria: 72.0%
Status: LIGADO
Tamanho Tela: 6"
Processador: A17 Pro
RAM: 8GB
Armazenamento: 256GB

=== STATUS DO SMARTPHONE ===
Marca: Samsung
Modelo: Galaxy S24
Sistema Operacional: Android 14
Bateria: 100.0%
Status: LIGADO
Tamanho Tela: 6"
Processador: Snapdragon 8 Gen 3
RAM: 12GB
Armazenamento: 512GB

=== STATUS DO TABLET ===
Marca: Apple
Modelo: iPad Air
Sistema Operacional: iPadOS 17
Bateria: 76.0%
Status: LIGADO
Tamanho Tela: 10.9"
Tem Caneta: Sim
Taxa Atualização: 120Hz

=== STATUS DO SMARTWATCH ===
Marca: Apple
Modelo: Watch Series 9
Sistema Operacional: watchOS 10
Bateria: 82.0%
Status: LIGADO
Tamanho Tela: 1.9"
Tem GPS: Sim
Monitor Cardíaco: Sim
Passos: 100

--- Carregando todos os dispositivos ---
Bateria de Apple iPhone 15 Pro carregada!
Bateria de Samsung Galaxy S24 carregada!
Bateria de Apple iPad Air carregada!
Bateria de Apple Watch Series 9 carregada!
```

## Aprendizados

✅ Uso de classes abstratas e interfaces
✅ Encapsulamento com validações
✅ Herança e especialização
✅ Polimorfismo em ação
✅ Gestão de coleções
✅ Código orientado a objetos profissional

## Extensões Possíveis

1. Adicionar sistema de eventos (quando bateria acaba)
2. Implementar serialização (salvar/carregar dispositivos)
3. Adicionar banco de dados
4. Criar interface gráfica
5. Implementar sistema de notificações
6. Adicionar mais tipos de dispositivos (Fone Bluetooth, Câmera Digital)
