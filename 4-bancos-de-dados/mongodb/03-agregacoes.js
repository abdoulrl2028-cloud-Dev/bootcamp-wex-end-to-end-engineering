// MongoDB - Agregações Avançadas

// 1. AGREGAÇÃO - Relatório de vendas
db.pedidos.aggregate([
    { $match: { status: { $ne: "CANCELADO" } } },
    {
        $facet: {
            vendas: [
                {
                    $group: {
                        _id: null,
                        totalPedidos: { $sum: 1 },
                        totalVendas: { $sum: "$total" },
                        ticketMedio: { $avg: "$total" },
                        maiorPedido: { $max: "$total" },
                        menorPedido: { $min: "$total" }
                    }
                }
            ],
            porStatus: [
                {
                    $group: {
                        _id: "$status",
                        quantidade: { $sum: 1 },
                        valor: { $sum: "$total" }
                    }
                },
                { $sort: { quantidade: -1 } }
            ]
        }
    }
]);

// 2. AGREGAÇÃO - Top 10 clientes por valor gasto
db.pedidos.aggregate([
    { $match: { status: { $ne: "CANCELADO" } } },
    {
        $lookup: {
            from: "usuarios",
            localField: "usuarioId",
            foreignField: "_id",
            as: "usuario"
        }
    },
    { $unwind: "$usuario" },
    {
        $group: {
            _id: "$usuarioId",
            nomeUsuario: { $first: "$usuario.nome" },
            emailUsuario: { $first: "$usuario.email" },
            totalGasto: { $sum: "$total" },
            quantidadePedidos: { $sum: 1 },
            ticketMedio: { $avg: "$total" },
            ultimoPedido: { $max: "$dataPedido" }
        }
    },
    { $sort: { totalGasto: -1 } },
    { $limit: 10 }
]);

// 3. AGREGAÇÃO - Produtos mais vendidos com receita
db.pedidos.aggregate([
    { $match: { status: { $ne: "CANCELADO" } } },
    { $unwind: "$itens" },
    {
        $group: {
            _id: "$itens.produtoNome",
            totalQuantidade: { $sum: "$itens.quantidade" },
            receita: { $sum: "$itens.subtotal" },
            precoMedio: { $avg: "$itens.preco" },
            quantidadePedidos: { $sum: 1 }
        }
    },
    { $sort: { receita: -1 } },
    { $limit: 10 }
]);

// 4. AGREGAÇÃO - Análise de itens por pedido
db.pedidos.aggregate([
    {
        $addFields: {
            quantidadeItens: { $size: "$itens" },
            precoMedioPorItem: { $divide: ["$total", { $size: "$itens" }] }
        }
    },
    {
        $group: {
            _id: "$quantidadeItens",
            quantidadePedidos: { $sum: 1 },
            ticketMedio: { $avg: "$total" }
        }
    },
    { $sort: { _id: 1 } }
]);

// 5. AGREGAÇÃO - Pedidos por período (mês)
db.pedidos.aggregate([
    {
        $group: {
            _id: {
                ano: { $year: "$dataPedido" },
                mes: { $month: "$dataPedido" }
            },
            quantidadePedidos: { $sum: 1 },
            totalVendas: { $sum: "$total" },
            ticketMedio: { $avg: "$total" }
        }
    },
    {
        $sort: { "_id.ano": -1, "_id.mes": -1 }
    }
]);

// 6. AGREGAÇÃO - Performance de status
db.pedidos.aggregate([
    {
        $group: {
            _id: "$status",
            quantidade: { $sum: 1 },
            valor: { $sum: "$total" },
            percentualValor: null  // será calculado no estágio seguinte
        }
    },
    {
        $facet: {
            statusPedidos: [{ $sort: { valor: -1 } }],
            total: [{ $group: { _id: null, totalValor: { $sum: "$valor" } } }]
        }
    }
]);

// 7. AGREGAÇÃO - Usuários sem pedidos
db.usuarios.aggregate([
    {
        $lookup: {
            from: "pedidos",
            localField: "_id",
            foreignField: "usuarioId",
            as: "pedidos"
        }
    },
    {
        $match: { pedidos: { $size: 0 } }
    },
    {
        $project: { nome: 1, email: 1, ativo: 1 }
    }
]);

// 8. AGREGAÇÃO - Clientes inativos com histórico de compras
db.usuarios.aggregate([
    { $match: { ativo: false } },
    {
        $lookup: {
            from: "pedidos",
            localField: "_id",
            foreignField: "usuarioId",
            as: "pedidos"
        }
    },
    {
        $addFields: {
            quantidadePedidos: { $size: "$pedidos" },
            totalGasto: { $sum: "$pedidos.total" }
        }
    },
    {
        $match: { quantidadePedidos: { $gt: 0 } }
    },
    {
        $project: {
            nome: 1,
            email: 1,
            quantidadePedidos: 1,
            totalGasto: 1,
            dataCriacao: 1
        }
    },
    { $sort: { totalGasto: -1 } }
]);

// 9. AGREGAÇÃO - Distribuição de pedidos por dia da semana
db.pedidos.aggregate([
    {
        $group: {
            _id: { $dayOfWeek: "$dataPedido" },
            quantidade: { $sum: 1 },
            valor: { $sum: "$total" }
        }
    },
    {
        $project: {
            _id: 0,
            diaSemana: {
                $switch: {
                    branches: [
                        { case: { $eq: ["$_id", 1] }, then: "Domingo" },
                        { case: { $eq: ["$_id", 2] }, then: "Segunda" },
                        { case: { $eq: ["$_id", 3] }, then: "Terça" },
                        { case: { $eq: ["$_id", 4] }, then: "Quarta" },
                        { case: { $eq: ["$_id", 5] }, then: "Quinta" },
                        { case: { $eq: ["$_id", 6] }, then: "Sexta" },
                        { case: { $eq: ["$_id", 7] }, then: "Sábado" }
                    ],
                    default: "Desconhecido"
                }
            },
            quantidade: 1,
            valor: 1
        }
    }
]);

// 10. AGREGAÇÃO - Análise de tempo entre pedidos
db.pedidos.aggregate([
    { $sort: { dataPedido: 1 } },
    {
        $group: {
            _id: "$usuarioId",
            pedidos: { $push: "$dataPedido" }
        }
    },
    {
        $addFields: {
            quantidadePedidos: { $size: "$pedidos" }
        }
    },
    { $match: { quantidadePedidos: { $gt: 1 } } }
]);

// 11. AGREGAÇÃO - Resumo por múltiplas dimensões
db.pedidos.aggregate([
    {
        $facet: {
            resumoGeral: [
                {
                    $group: {
                        _id: null,
                        totalPedidos: { $sum: 1 },
                        totalReceita: { $sum: "$total" },
                        pedidoMaior: { $max: "$total" },
                        pedidoMenor: { $min: "$total" },
                        ticketMedio: { $avg: "$total" }
                    }
                }
            ],
            topProdutos: [
                { $unwind: "$itens" },
                {
                    $group: {
                        _id: "$itens.produtoNome",
                        vendas: { $sum: "$itens.quantidade" }
                    }
                },
                { $sort: { vendas: -1 } },
                { $limit: 5 }
            ],
            topClientes: [
                {
                    $group: {
                        _id: "$usuarioId",
                        gasto: { $sum: "$total" }
                    }
                },
                { $sort: { gasto: -1 } },
                { $limit: 5 }
            ]
        }
    }
]);

// 12. AGREGAÇÃO - Validação de dados (documentos com estrutura inconsistente)
db.pedidos.aggregate([
    {
        $match: {
            $or: [
                { itens: { $exists: false } },
                { itens: { $size: 0 } },
                { total: null }
            ]
        }
    },
    { $count: "problemas" }
]);
