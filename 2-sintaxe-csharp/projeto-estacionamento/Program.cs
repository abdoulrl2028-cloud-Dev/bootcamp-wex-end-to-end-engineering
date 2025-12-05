using System;
using System.Collections.Generic;

namespace ProjetoEstacionamento
{
    class Veiculo
    {
        public string? Placa { get; set; }
        public string? Marca { get; set; }
        public string? Modelo { get; set; }
        public string? Cor { get; set; }
        public DateTime DataEntrada { get; set; }

        public override string ToString()
        {
            return $"Placa: {Placa}, Marca: {Marca}, Modelo: {Modelo}, Cor: {Cor}, Entrada: {DataEntrada:dd/MM/yyyy HH:mm}";
        }
    }

    class EstacionamentoManager
    {
        private List<Veiculo> veiculos = new List<Veiculo>();
        private decimal precoHora = 5.00m;
        private int capacidadeMaxima = 10;

        public void AdicionarVeiculo(string placa, string marca, string modelo, string cor)
        {
            if (veiculos.Count >= capacidadeMaxima)
            {
                Console.WriteLine("❌ Estacionamento cheio!");
                return;
            }

            if (VeiculoExiste(placa))
            {
                Console.WriteLine("❌ Veículo já existe no estacionamento!");
                return;
            }

            Veiculo veiculo = new Veiculo
            {
                Placa = placa,
                Marca = marca,
                Modelo = modelo,
                Cor = cor,
                DataEntrada = DateTime.Now
            };

            veiculos.Add(veiculo);
            Console.WriteLine($"✅ Veículo {placa} adicionado com sucesso!");
        }

        public void RemoverVeiculo(string placa)
        {
            Veiculo? veiculo = veiculos.FirstOrDefault(v => v.Placa == placa);

            if (veiculo == null)
            {
                Console.WriteLine("❌ Veículo não encontrado!");
                return;
            }

            TimeSpan duracao = DateTime.Now - veiculo.DataEntrada;
            decimal valor = (decimal)Math.Ceiling(duracao.TotalHours) * precoHora;

            veiculos.Remove(veiculo);
            Console.WriteLine($"\n✅ Veículo {placa} removido!");
            Console.WriteLine($"Tempo de permanência: {duracao.Hours}h {duracao.Minutes}m");
            Console.WriteLine($"Valor a pagar: R$ {valor:F2}");
        }

        public void ListarVeiculos()
        {
            if (veiculos.Count == 0)
            {
                Console.WriteLine("📭 Nenhum veículo no estacionamento!");
                return;
            }

            Console.WriteLine("\n📋 Veículos no estacionamento:");
            Console.WriteLine("═══════════════════════════════════════════════════════════");
            for (int i = 0; i < veiculos.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {veiculos[i]}");
            }
            Console.WriteLine($"\nTotal: {veiculos.Count}/{capacidadeMaxima}");
            Console.WriteLine("═══════════════════════════════════════════════════════════\n");
        }

        public void BuscarVeiculo(string placa)
        {
            Veiculo? veiculo = veiculos.FirstOrDefault(v => v.Placa == placa);

            if (veiculo == null)
            {
                Console.WriteLine("❌ Veículo não encontrado!");
                return;
            }

            Console.WriteLine($"\n🔍 Veículo encontrado:");
            Console.WriteLine($"   {veiculo}");
            
            TimeSpan duracao = DateTime.Now - veiculo.DataEntrada;
            decimal valor = (decimal)Math.Ceiling(duracao.TotalHours) * precoHora;
            
            Console.WriteLine($"   Tempo estacionado: {duracao.Hours}h {duracao.Minutes}m");
            Console.WriteLine($"   Valor a pagar: R$ {valor:F2}\n");
        }

        public void MostrarEspacosDisponiveis()
        {
            int espacosLivres = capacidadeMaxima - veiculos.Count;
            Console.WriteLine($"\n🅿️ Espaços disponíveis: {espacosLivres}/{capacidadeMaxima}");
            Console.WriteLine($"   Ocupação: {(double)veiculos.Count / capacidadeMaxima * 100:F1}%\n");
        }

        private bool VeiculoExiste(string placa)
        {
            return veiculos.Any(v => v.Placa == placa);
        }
    }

    class Program
    {
        static void Main()
        {
            EstacionamentoManager estacionamento = new EstacionamentoManager();
            bool continuar = true;

            Console.WriteLine("╔═══════════════════════════════════════════╗");
            Console.WriteLine("║     🚗 SISTEMA DE ESTACIONAMENTO 🚗      ║");
            Console.WriteLine("╚═══════════════════════════════════════════╝\n");

            while (continuar)
            {
                Console.WriteLine("\n--- MENU PRINCIPAL ---");
                Console.WriteLine("1. Adicionar veículo");
                Console.WriteLine("2. Remover veículo");
                Console.WriteLine("3. Listar veículos");
                Console.WriteLine("4. Buscar veículo");
                Console.WriteLine("5. Ver espaços disponíveis");
                Console.WriteLine("0. Sair");
                Console.Write("\nEscolha uma opção: ");

                string? opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        AdicionarVeiculo(estacionamento);
                        break;
                    case "2":
                        RemoverVeiculo(estacionamento);
                        break;
                    case "3":
                        estacionamento.ListarVeiculos();
                        break;
                    case "4":
                        BuscarVeiculo(estacionamento);
                        break;
                    case "5":
                        estacionamento.MostrarEspacosDisponiveis();
                        break;
                    case "0":
                        Console.WriteLine("\n👋 Até logo!");
                        continuar = false;
                        break;
                    default:
                        Console.WriteLine("❌ Opção inválida!");
                        break;
                }
            }
        }

        static void AdicionarVeiculo(EstacionamentoManager estacionamento)
        {
            Console.Write("\nDigite a placa do veículo: ");
            string? placa = Console.ReadLine();

            if (string.IsNullOrEmpty(placa))
            {
                Console.WriteLine("❌ Placa inválida!");
                return;
            }

            Console.Write("Digite a marca: ");
            string? marca = Console.ReadLine();

            Console.Write("Digite o modelo: ");
            string? modelo = Console.ReadLine();

            Console.Write("Digite a cor: ");
            string? cor = Console.ReadLine();

            estacionamento.AdicionarVeiculo(placa, marca ?? "Desconhecida", modelo ?? "Desconhecido", cor ?? "Desconhecida");
        }

        static void RemoverVeiculo(EstacionamentoManager estacionamento)
        {
            Console.Write("\nDigite a placa do veículo a remover: ");
            string? placa = Console.ReadLine();

            if (string.IsNullOrEmpty(placa))
            {
                Console.WriteLine("❌ Placa inválida!");
                return;
            }

            estacionamento.RemoverVeiculo(placa);
        }

        static void BuscarVeiculo(EstacionamentoManager estacionamento)
        {
            Console.Write("\nDigite a placa do veículo a buscar: ");
            string? placa = Console.ReadLine();

            if (string.IsNullOrEmpty(placa))
            {
                Console.WriteLine("❌ Placa inválida!");
                return;
            }

            estacionamento.BuscarVeiculo(placa);
        }
    }
}
