using System;
using System.Collections.Generic;

// Projeto Prático: Sistema de Gerenciamento de Celulares
// Demonstra abstração, encapsulamento, herança e polimorfismo

// Interface para definir comportamento comum
public interface IDispositivoMovel
{
    void Ligar();
    void Desligar();
    void Carregar_Bateria();
    void Exibir_Status();
}

// Classe abstrata base
public abstract class DispositivoMovel : IDispositivoMovel
{
    protected string _marca;
    protected string _modelo;
    protected decimal _bateria;
    protected bool _ligado;
    protected string _so;

    public string Marca { get { return _marca; } }
    public string Modelo { get { return _modelo; } }
    public decimal Bateria { get { return _bateria; } }
    public bool Ligado { get { return _ligado; } }

    public DispositivoMovel(string marca, string modelo, string so)
    {
        _marca = marca;
        _modelo = modelo;
        _so = so;
        _bateria = 100;
        _ligado = false;
    }

    public virtual void Ligar()
    {
        if (_bateria > 5)
        {
            _ligado = true;
            _bateria -= 2;
            Console.WriteLine($"{_marca} {_modelo} ligado!");
        }
        else
        {
            Console.WriteLine("Bateria muito fraca! Não é possível ligar.");
        }
    }

    public virtual void Desligar()
    {
        _ligado = false;
        Console.WriteLine($"{_marca} {_modelo} desligado!");
    }

    public virtual void Carregar_Bateria()
    {
        _bateria = 100;
        Console.WriteLine($"Bateria de {_marca} {_modelo} carregada!");
    }

    public abstract void Exibir_Status();

    protected void Consumir_Bateria(decimal quantidade)
    {
        if (_ligado)
        {
            _bateria -= quantidade;
            if (_bateria < 0) _bateria = 0;
        }
    }
}

// Classe concreta: Smartphone
public class Smartphone : DispositivoMovel
{
    public int TamanhoTela { get; set; }
    public string Processador { get; set; }
    public int MemoriaRam { get; set; }
    public int Armazenamento { get; set; }
    private List<string> _aplicativos;

    public Smartphone(string marca, string modelo, string so, int tamanhoTela, 
                      string processador, int ram, int armazenamento)
        : base(marca, modelo, so)
    {
        TamanhoTela = tamanhoTela;
        Processador = processador;
        MemoriaRam = ram;
        Armazenamento = armazenamento;
        _aplicativos = new List<string> { "Câmera", "Galeria", "Contatos" };
    }

    public void Fazer_Chamada(string contato)
    {
        if (_ligado)
        {
            Console.WriteLine($"Chamando {contato}...");
            Consumir_Bateria(5);
        }
        else
        {
            Console.WriteLine("Dispositivo desligado!");
        }
    }

    public void Enviar_Mensagem(string contato, string mensagem)
    {
        if (_ligado)
        {
            Console.WriteLine($"SMS para {contato}: {mensagem}");
            Consumir_Bateria(1);
        }
    }

    public void Tirar_Foto()
    {
        if (_ligado)
        {
            Console.WriteLine("Foto capturada!");
            Consumir_Bateria(3);
        }
    }

    public void Instalar_App(string nomeApp)
    {
        if (_ligado && Armazenamento > 0)
        {
            _aplicativos.Add(nomeApp);
            Armazenamento -= 10;
            Console.WriteLine($"Aplicativo '{nomeApp}' instalado!");
            Consumir_Bateria(2);
        }
    }

    public void Listar_Aplicativos()
    {
        if (_ligado)
        {
            Console.WriteLine("Aplicativos instalados:");
            foreach (var app in _aplicativos)
            {
                Console.WriteLine($"  - {app}");
            }
        }
    }

    public override void Exibir_Status()
    {
        Console.WriteLine("\n=== STATUS DO SMARTPHONE ===");
        Console.WriteLine($"Marca: {_marca}");
        Console.WriteLine($"Modelo: {_modelo}");
        Console.WriteLine($"Sistema Operacional: {_so}");
        Console.WriteLine($"Bateria: {_bateria:F1}%");
        Console.WriteLine($"Status: {(_ligado ? "LIGADO" : "DESLIGADO")}");
        Console.WriteLine($"Tamanho Tela: {TamanhoTela}\"");
        Console.WriteLine($"Processador: {Processador}");
        Console.WriteLine($"RAM: {MemoriaRam}GB");
        Console.WriteLine($"Armazenamento: {Armazenamento}GB");
    }
}

// Classe concreta: Tablet
public class Tablet : DispositivoMovel
{
    public decimal TamanhoTela { get; set; }
    public bool TemCaneta { get; set; }
    public int RefreshRate { get; set; }

    public Tablet(string marca, string modelo, string so, decimal tamanhoTela, 
                  bool temCaneta, int refreshRate)
        : base(marca, modelo, so)
    {
        TamanhoTela = tamanhoTela;
        TemCaneta = temCaneta;
        RefreshRate = refreshRate;
    }

    public void Assistir_Video()
    {
        if (_ligado)
        {
            Console.WriteLine("Reproduzindo vídeo...");
            Consumir_Bateria(8);
        }
    }

    public void Ler_Livro()
    {
        if (_ligado)
        {
            Console.WriteLine("Lendo livro digital...");
            Consumir_Bateria(2);
        }
    }

    public void Usar_Caneta()
    {
        if (_ligado && TemCaneta)
        {
            Console.WriteLine("Usando caneta digital para desenhar...");
            Consumir_Bateria(4);
        }
    }

    public override void Ligar()
    {
        base.Ligar();
        if (_ligado)
        {
            Console.WriteLine($"Tablet em modo {RefreshRate}Hz");
        }
    }

    public override void Exibir_Status()
    {
        Console.WriteLine("\n=== STATUS DO TABLET ===");
        Console.WriteLine($"Marca: {_marca}");
        Console.WriteLine($"Modelo: {_modelo}");
        Console.WriteLine($"Sistema Operacional: {_so}");
        Console.WriteLine($"Bateria: {_bateria:F1}%");
        Console.WriteLine($"Status: {(_ligado ? "LIGADO" : "DESLIGADO")}");
        Console.WriteLine($"Tamanho Tela: {TamanhoTela}\"");
        Console.WriteLine($"Tem Caneta: {(TemCaneta ? "Sim" : "Não")}");
        Console.WriteLine($"Taxa Atualização: {RefreshRate}Hz");
    }
}

// Classe concreta: Smartwatch
public class Smartwatch : DispositivoMovel
{
    public decimal TamanhoTela { get; set; }
    public bool TemGPS { get; set; }
    public bool TemMonitorCardiaco { get; set; }
    private int _passos;

    public Smartwatch(string marca, string modelo, string so, decimal tamanhoTela, 
                      bool temGPS, bool temMonitorCardiaco)
        : base(marca, modelo, so)
    {
        TamanhoTela = tamanhoTela;
        TemGPS = temGPS;
        TemMonitorCardiaco = temMonitorCardiaco;
        _passos = 0;
    }

    public void Rastrear_Passos()
    {
        if (_ligado)
        {
            _passos += 100;
            Console.WriteLine($"Passos registrados: {_passos}");
            Consumir_Bateria(1);
        }
    }

    public void Medir_Frequencia_Cardiaca()
    {
        if (_ligado && TemMonitorCardiaco)
        {
            int bpm = new Random().Next(60, 100);
            Console.WriteLine($"Frequência cardíaca: {bpm} bpm");
            Consumir_Bateria(2);
        }
    }

    public void Rastrear_Localizacao()
    {
        if (_ligado && TemGPS)
        {
            Console.WriteLine("Localizando...");
            Consumir_Bateria(5);
        }
    }

    public override void Exibir_Status()
    {
        Console.WriteLine("\n=== STATUS DO SMARTWATCH ===");
        Console.WriteLine($"Marca: {_marca}");
        Console.WriteLine($"Modelo: {_modelo}");
        Console.WriteLine($"Sistema Operacional: {_so}");
        Console.WriteLine($"Bateria: {_bateria:F1}%");
        Console.WriteLine($"Status: {(_ligado ? "LIGADO" : "DESLIGADO")}");
        Console.WriteLine($"Tamanho Tela: {TamanhoTela}\"");
        Console.WriteLine($"Tem GPS: {(TemGPS ? "Sim" : "Não")}");
        Console.WriteLine($"Monitor Cardíaco: {(TemMonitorCardiaco ? "Sim" : "Não")}");
        Console.WriteLine($"Passos: {_passos}");
    }
}

// Classe para gerenciar vários dispositivos
public class GerenciadorDispositivos
{
    private List<DispositivoMovel> _dispositivos = new List<DispositivoMovel>();

    public void Adicionar_Dispositivo(DispositivoMovel dispositivo)
    {
        _dispositivos.Add(dispositivo);
        Console.WriteLine($"Dispositivo {dispositivo.Marca} {dispositivo.Modelo} adicionado!");
    }

    public void Ligar_Todos()
    {
        Console.WriteLine("\n--- Ligando todos os dispositivos ---");
        foreach (var device in _dispositivos)
        {
            device.Ligar();
        }
    }

    public void Desligar_Todos()
    {
        Console.WriteLine("\n--- Desligando todos os dispositivos ---");
        foreach (var device in _dispositivos)
        {
            device.Desligar();
        }
    }

    public void Exibir_Status_Todos()
    {
        Console.WriteLine("\n╔════════════════════════════════════════════════════════╗");
        Console.WriteLine("║        STATUS DE TODOS OS DISPOSITIVOS                  ║");
        Console.WriteLine("╚════════════════════════════════════════════════════════╝");
        foreach (var device in _dispositivos)
        {
            device.Exibir_Status();
        }
    }

    public void Carregar_Todos()
    {
        Console.WriteLine("\n--- Carregando todos os dispositivos ---");
        foreach (var device in _dispositivos)
        {
            device.Carregar_Bateria();
        }
    }
}

// Programa Principal
public class Program
{
    public static void Main()
    {
        Console.WriteLine("╔════════════════════════════════════════════════════════╗");
        Console.WriteLine("║   SISTEMA DE GERENCIAMENTO DE DISPOSITIVOS MÓVEIS      ║");
        Console.WriteLine("╚════════════════════════════════════════════════════════╝\n");

        // Criar gerenciador
        GerenciadorDispositivos gerenciador = new GerenciadorDispositivos();

        // Criar dispositivos
        Smartphone iphone = new Smartphone("Apple", "iPhone 15 Pro", "iOS 17", 6, "A17 Pro", 8, 256);
        Smartphone android = new Smartphone("Samsung", "Galaxy S24", "Android 14", 6, "Snapdragon 8 Gen 3", 12, 512);
        Tablet ipad = new Tablet("Apple", "iPad Air", "iPadOS 17", 10.9m, true, 120);
        Smartwatch reloj = new Smartwatch("Apple", "Watch Series 9", "watchOS 10", 1.9m, true, true);

        // Adicionar ao gerenciador
        gerenciador.Adicionar_Dispositivo(iphone);
        gerenciador.Adicionar_Dispositivo(android);
        gerenciador.Adicionar_Dispositivo(ipad);
        gerenciador.Adicionar_Dispositivo(reloj);

        // Ligar todos
        gerenciador.Ligar_Todos();

        // Usar smartphone
        Console.WriteLine("\n--- Usando iPhone ---");
        iphone.Fazer_Chamada("João");
        iphone.Enviar_Mensagem("Maria", "Oi, tudo bem?");
        iphone.Tirar_Foto();
        iphone.Instalar_App("WhatsApp");
        iphone.Listar_Aplicativos();

        // Usar tablet
        Console.WriteLine("\n--- Usando iPad ---");
        ipad.Assistir_Video();
        ipad.Ler_Livro();
        ipad.Usar_Caneta();

        // Usar smartwatch
        Console.WriteLine("\n--- Usando Apple Watch ---");
        reloj.Rastrear_Passos();
        reloj.Medir_Frequencia_Cardiaca();
        reloj.Rastrear_Localizacao();

        // Exibir status de todos
        gerenciador.Exibir_Status_Todos();

        // Carregar todos
        gerenciador.Carregar_Todos();

        // Status final
        gerenciador.Exibir_Status_Todos();
    }
}
