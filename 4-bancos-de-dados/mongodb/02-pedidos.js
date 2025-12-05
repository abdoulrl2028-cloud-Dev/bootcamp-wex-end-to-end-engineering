// MongoDB - Operações com Pedidos

// 1. CRIAR coleção de pedidos
db.createCollection("pedidos");

// 2. INSERIR pedidos
db.pedidos.insertMany([
    {
        usuarioId: ObjectId("..."),  // Referência para usuário
        dataPedido: new Date(),
        dataEntrega: null,
        total: 150.00,
        status: "PENDENTE",
        itens: [
            {
                produtoNome: "Notebook",
                quantidade: 1,
                preco: 150.00,
                subtotal: 150.00
            }
        ]
    },
    {
        usuarioId: ObjectId("..."),
        dataPedido: new Date("2024-12-01"),
        dataEntrega: new Date("2024-12-05"),
        total: 450.00,
        status: "ENTREGUE",
        itens: [
            {
                produtoNome: "Mouse",
                quantidade: 2,
                preco: 50.00,
                subtotal: 100.00
            },
            {
                produtoNome: "Teclado",
                quantidade: 1,
                preco: 150.00,
                subtotal: 150.00
            },
            {
                produtoNome: "Monitor",
                quantidade: 1,
                preco: 200.00,
                subtotal: 200.00
            }
        ]
    }
]);

// 3. BUSCAR pedidos por status
db.pedidos.find({ status: "PENDENTE" });

// 4. BUSCAR pedidos de um usuário
db.pedidos.find({ usuarioId: ObjectId("...") });

// 5. BUSCAR pedidos em um período
db.pedidos.find({
    dataPedido: {
        $gte: new Date("2024-12-01"),
        $lt: new Date("2024-12-31")
    }
});

// 6. BUSCAR pedidos com valor acima de X
db.pedidos.find({ total: { $gte: 100 } });

// 7. BUSCAR pedidos com itens específicos
db.pedidos.find({
    "itens.produtoNome": "Notebook"
});

// 8. ATUALIZAR status de pedido
db.pedidos.updateOne(
    { _id: ObjectId("...") },
    {
        $set: {
            status: "ENVIADO",
            dataPedido: new Date()
        }
    }
);

// 9. ATUALIZAR com mudança condicional
db.pedidos.updateOne(
    { _id: ObjectId("..."), status: "PENDENTE" },
    {
        $set: {
            status: "CANCELADO"
        }
    }
);

// 10. ADICIONAR item ao pedido
db.pedidos.updateOne(
    { _id: ObjectId("...") },
    {
        $push: {
            itens: {
                produtoNome: "Cabo USB",
                quantidade: 3,
                preco: 15.00,
                subtotal: 45.00
            }
        },
        $inc: { total: 45.00 }
    }
);

// 11. REMOVER item do pedido
db.pedidos.updateOne(
    { _id: ObjectId("...") },
    {
        $pull: {
            itens: { produtoNome: "Cabo USB" }
        }
    }
);

// 12. ATUALIZAR múltiplos pedidos
db.pedidos.updateMany(
    {
        dataPedido: { $lt: new Date("2024-10-01") },
        status: "PENDENTE"
    },
    {
        $set: { status: "CANCELADO" }
    }
);

// 13. CONTAR pedidos por status
db.pedidos.countDocuments({ status: "ENTREGUE" });

// 14. LISTAR pedidos com ordenação
db.pedidos.find().sort({ dataPedido: -1 }).limit(10);

// 15. BUSCA COM AGGREGATION - Valor total por status
db.pedidos.aggregate([
    {
        $group: {
            _id: "$status",
            totalValor: { $sum: "$total" },
            quantidade: { $sum: 1 }
        }
    },
    { $sort: { totalValor: -1 } }
]);

// 16. AGGREGATION - Detalhes de pedidos com usuário (lookup)
db.pedidos.aggregate([
    {
        $lookup: {
            from: "usuarios",
            localField: "usuarioId",
            foreignField: "_id",
            as: "usuario"
        }
    },
    {
        $unwind: "$usuario"
    },
    {
        $project: {
            _id: 1,
            dataPedido: 1,
            total: 1,
            status: 1,
            "usuario.nome": 1,
            "usuario.email": 1
        }
    }
]);

// 17. AGGREGATION - Total gasto por usuário
db.pedidos.aggregate([
    { $match: { status: { $ne: "CANCELADO" } } },
    {
        $group: {
            _id: "$usuarioId",
            totalGasto: { $sum: "$total" },
            quantidadePedidos: { $sum: 1 },
            ticketMedio: { $avg: "$total" }
        }
    },
    { $sort: { totalGasto: -1 } }
]);

// 18. AGGREGATION - Produtos mais vendidos
db.pedidos.aggregate([
    { $unwind: "$itens" },
    {
        $group: {
            _id: "$itens.produtoNome",
            totalVendido: { $sum: "$itens.quantidade" },
            receita: { $sum: "$itens.subtotal" }
        }
    },
    { $sort: { totalVendido: -1 } }
]);

// 19. DELETAR pedido
db.pedidos.deleteOne({ _id: ObjectId("...") });

// 20. DELETAR múltiplos pedidos
db.pedidos.deleteMany({
    status: "CANCELADO",
    dataPedido: { $lt: new Date("2024-01-01") }
});

// 21. CRIAR índice em status
db.pedidos.createIndex({ status: 1 });

// 22. CRIAR índice composto
db.pedidos.createIndex({ usuarioId: 1, status: 1 });

// 23. CRIAR índice em data
db.pedidos.createIndex({ dataPedido: -1 });

// 24. LISTAR índices
db.pedidos.getIndexes();
