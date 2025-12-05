using ProjetoConsultas;
using ProjetoConsultas.Models;
using ProjetoConsultas.Services;

// Configuração da string de conexão
const string connectionString = "Server=localhost;Database=BootcampDB;User Id=sa;Password=YourPassword123;";

try
{
    // Inicializar banco de dados
    var database = new Database(connectionString);
    database.InitializeDatabase();

    // Inicializar serviços
    var usuarioService = new UsuarioService(database);
    var produtoService = new ProdutoService(database);
    var pedidoService = new PedidoService(database);
    var relatorioService = new RelatorioService(database);

    // Menu principal
    while (true)
    {
        Console.WriteLine("\n========== SISTEMA DE GERENCIAMENTO ==========");
        Console.WriteLine("1. Gerenciar Usuários");
        Console.WriteLine("2. Gerenciar Produtos");
        Console.WriteLine("3. Gerenciar Pedidos");
        Console.WriteLine("4. Relatórios");
        Console.WriteLine("0. Sair");
        Console.Write("Escolha uma opção: ");

        string opcao = Console.ReadLine() ?? "0";

        switch (opcao)
        {
            case "1":
                GerenciarUsuarios(usuarioService);
                break;
            case "2":
                GerenciarProdutos(produtoService);
                break;
            case "3":
                GerenciarPedidos(pedidoService, usuarioService);
                break;
            case "4":
                ExibirRelatorios(relatorioService);
                break;
            case "0":
                Console.WriteLine("Até logo!");
                return;
            default:
                Console.WriteLine("Opção inválida!");
                break;
        }
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Erro: {ex.Message}");
}

void GerenciarUsuarios(UsuarioService service)
{
    while (true)
    {
        Console.WriteLine("\n--- USUÁRIOS ---");
        Console.WriteLine("1. Listar Usuários");
        Console.WriteLine("2. Buscar por ID");
        Console.WriteLine("3. Inserir Novo Usuário");
        Console.WriteLine("4. Atualizar Usuário");
        Console.WriteLine("5. Deletar Usuário");
        Console.WriteLine("0. Voltar");
        Console.Write("Escolha: ");

        string opcao = Console.ReadLine() ?? "0";

        switch (opcao)
        {
            case "1":
                var usuarios = service.ObterTodos();
                if (usuarios.Count == 0)
                    Console.WriteLine("Nenhum usuário encontrado!");
                else
                    foreach (var u in usuarios)
                        Console.WriteLine(u);
                break;

            case "2":
                Console.Write("ID do usuário: ");
                if (int.TryParse(Console.ReadLine(), out int id))
                {
                    var usuario = service.ObterPorId(id);
                    Console.WriteLine(usuario ?? "Usuário não encontrado!");
                }
                break;

            case "3":
                var novoUsuario = new Usuario
                {
                    Nome = Ler("Nome"),
                    Email = Ler("Email"),
                    Telefone = Ler("Telefone"),
                    CPF = Ler("CPF")
                };
                service.Inserir(novoUsuario);
                break;

            case "4":
                Console.Write("ID do usuário: ");
                if (int.TryParse(Console.ReadLine(), out int idAtualizar))
                {
                    var usuarioAtualizar = service.ObterPorId(idAtualizar);
                    if (usuarioAtualizar != null)
                    {
                        usuarioAtualizar.Nome = Ler("Novo nome", usuarioAtualizar.Nome);
                        usuarioAtualizar.Email = Ler("Novo email", usuarioAtualizar.Email);
                        usuarioAtualizar.Telefone = Ler("Novo telefone", usuarioAtualizar.Telefone ?? "");
                        service.Atualizar(usuarioAtualizar);
                    }
                }
                break;

            case "5":
                Console.Write("ID do usuário a deletar: ");
                if (int.TryParse(Console.ReadLine(), out int idDeletar))
                    service.Deletar(idDeletar);
                break;

            case "0":
                return;
        }
    }
}

void GerenciarProdutos(ProdutoService service)
{
    while (true)
    {
        Console.WriteLine("\n--- PRODUTOS ---");
        Console.WriteLine("1. Listar Produtos");
        Console.WriteLine("2. Buscar Produto");
        Console.WriteLine("3. Inserir Produto");
        Console.WriteLine("4. Atualizar Produto");
        Console.WriteLine("5. Deletar Produto");
        Console.WriteLine("0. Voltar");
        Console.Write("Escolha: ");

        string opcao = Console.ReadLine() ?? "0";

        switch (opcao)
        {
            case "1":
                var produtos = service.ObterTodos();
                if (produtos.Count == 0)
                    Console.WriteLine("Nenhum produto encontrado!");
                else
                    foreach (var p in produtos)
                        Console.WriteLine(p);
                break;

            case "2":
                Console.Write("Nome do produto: ");
                string nomeBusca = Console.ReadLine() ?? "";
                var encontrados = service.BuscarPorNome(nomeBusca);
                if (encontrados.Count == 0)
                    Console.WriteLine("Produto não encontrado!");
                else
                    foreach (var p in encontrados)
                        Console.WriteLine(p);
                break;

            case "3":
                var novoProduto = new Produto
                {
                    Nome = Ler("Nome"),
                    Descricao = Ler("Descrição"),
                    Preco = decimal.Parse(Ler("Preço", "0")),
                    Estoque = int.Parse(Ler("Estoque", "0"))
                };
                service.Inserir(novoProduto);
                break;

            case "0":
                return;
        }
    }
}

void GerenciarPedidos(PedidoService service, UsuarioService usuarioService)
{
    while (true)
    {
        Console.WriteLine("\n--- PEDIDOS ---");
        Console.WriteLine("1. Listar Pedidos");
        Console.WriteLine("2. Pedidos de um Usuário");
        Console.WriteLine("3. Atualizar Status");
        Console.WriteLine("0. Voltar");
        Console.Write("Escolha: ");

        string opcao = Console.ReadLine() ?? "0";

        switch (opcao)
        {
            case "1":
                var pedidos = service.ObterTodos();
                if (pedidos.Count == 0)
                    Console.WriteLine("Nenhum pedido encontrado!");
                else
                    foreach (var p in pedidos)
                        Console.WriteLine(p);
                break;

            case "2":
                Console.Write("ID do usuário: ");
                if (int.TryParse(Console.ReadLine(), out int usuarioId))
                {
                    var pedidosUsuario = service.ObterPorUsuario(usuarioId);
                    if (pedidosUsuario.Count == 0)
                        Console.WriteLine("Nenhum pedido para este usuário!");
                    else
                        foreach (var p in pedidosUsuario)
                            Console.WriteLine(p);
                }
                break;

            case "3":
                Console.Write("ID do pedido: ");
                if (int.TryParse(Console.ReadLine(), out int pedidoId))
                {
                    Console.WriteLine("Novos status: PENDENTE, ENVIADO, ENTREGUE, CANCELADO");
                    string novoStatus = Console.ReadLine() ?? "";
                    service.AtualizarStatus(pedidoId, novoStatus);
                }
                break;

            case "0":
                return;
        }
    }
}

void ExibirRelatorios(RelatorioService service)
{
    while (true)
    {
        Console.WriteLine("\n--- RELATÓRIOS ---");
        Console.WriteLine("1. Relatório de Vendas");
        Console.WriteLine("2. Clientes com Mais Vendas");
        Console.WriteLine("3. Produtos Mais Vendidos");
        Console.WriteLine("4. Status de Pedidos");
        Console.WriteLine("5. Relatório de Estoque");
        Console.WriteLine("0. Voltar");
        Console.Write("Escolha: ");

        string opcao = Console.ReadLine() ?? "0";

        switch (opcao)
        {
            case "1":
                service.RelatorioVendas();
                break;
            case "2":
                service.RelatorioClientesPorVendas();
                break;
            case "3":
                service.RelatorioProdutosMaisVendidos();
                break;
            case "4":
                service.RelatorioStatusPedidos();
                break;
            case "5":
                service.RelatorioEstoque();
                break;
            case "0":
                return;
        }
    }
}

string Ler(string mensagem, string padrao = "")
{
    Console.Write($"{mensagem}: ");
    string entrada = Console.ReadLine() ?? "";
    return string.IsNullOrEmpty(entrada) ? padrao : entrada;
}
