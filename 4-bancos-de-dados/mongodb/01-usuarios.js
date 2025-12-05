// MongoDB - Operações com Usuários

// 1. INSERIR um usuário
db.usuarios.insertOne({
    nome: "João Silva",
    email: "joao@email.com",
    telefone: "11999999999",
    cpf: "123.456.789-00",
    ativo: true,
    dataCriacao: new Date(),
    dataAtualizacao: new Date()
});

// 2. INSERIR múltiplos usuários
db.usuarios.insertMany([
    {
        nome: "Maria Santos",
        email: "maria@email.com",
        telefone: "11988888888",
        cpf: "987.654.321-00",
        ativo: true,
        dataCriacao: new Date()
    },
    {
        nome: "Pedro Oliveira",
        email: "pedro@email.com",
        telefone: "11977777777",
        cpf: "456.789.123-00",
        ativo: true,
        dataCriacao: new Date()
    },
    {
        nome: "Ana Costa",
        email: "ana@email.com",
        telefone: "11966666666",
        cpf: "789.123.456-00",
        ativo: false,
        dataCriacao: new Date()
    }
]);

// 3. BUSCAR todos os usuários
db.usuarios.find();

// 4. BUSCAR usuários com filtro
db.usuarios.find({ ativo: true });

// 5. BUSCAR por email (com projeção)
db.usuarios.findOne(
    { email: "joao@email.com" },
    { nome: 1, email: 1, ativo: 1, _id: 0 }
);

// 6. BUSCAR com múltiplos critérios (AND)
db.usuarios.find({
    ativo: true,
    nome: { $regex: "Silva" }
});

// 7. BUSCAR com condições (OR)
db.usuarios.find({
    $or: [
        { email: "joao@email.com" },
        { email: "maria@email.com" }
    ]
});

// 8. BUSCAR com operadores de comparação
db.usuarios.find({
    dataCriacao: {
        $gte: new Date("2024-01-01"),
        $lt: new Date("2025-01-01")
    }
});

// 9. ATUALIZAR um usuário
db.usuarios.updateOne(
    { email: "joao@email.com" },
    {
        $set: {
            telefone: "11999999999",
            dataAtualizacao: new Date()
        }
    }
);

// 10. ATUALIZAR múltiplos usuários
db.usuarios.updateMany(
    { ativo: false },
    {
        $set: {
            ativo: true,
            dataAtualizacao: new Date()
        }
    }
);

// 11. SUBSTITUIR um documento
db.usuarios.replaceOne(
    { email: "ana@email.com" },
    {
        nome: "Ana Silva Costa",
        email: "ana.silva@email.com",
        telefone: "11966666666",
        cpf: "789.123.456-00",
        ativo: true,
        dataCriacao: new Date(),
        dataAtualizacao: new Date()
    }
);

// 12. DELETAR um usuário
db.usuarios.deleteOne({ email: "ana@email.com" });

// 13. DELETAR múltiplos usuários
db.usuarios.deleteMany({ ativo: false });

// 14. CONTAR documentos
db.usuarios.countDocuments({ ativo: true });

// 15. CONTAR com filtro
db.usuarios.countDocuments({
    dataCriacao: { $gte: new Date("2024-01-01") }
});

// 16. FIND com SORT (ordenação)
db.usuarios.find().sort({ nome: 1 });  // 1 = ascendente, -1 = descendente

// 17. FIND com LIMIT e SKIP
db.usuarios.find().limit(10).skip(0);  // Página 1

// 18. FIND com agregação simples
db.usuarios.find().pretty();

// 19. DISTINCT (valores únicos)
db.usuarios.distinct("ativo");

// 20. CRIAR índice em email
db.usuarios.createIndex({ email: 1 });

// 21. CRIAR índice composto
db.usuarios.createIndex({ ativo: 1, dataCriacao: -1 });

// 22. LISTAR índices
db.usuarios.getIndexes();

// 23. DELETAR índice
db.usuarios.dropIndex({ email: 1 });

// 24. BUSCA CASE-INSENSITIVE
db.usuarios.find({
    nome: { $regex: "silva", $options: "i" }
});

// 25. BULK OPERATIONS
db.usuarios.bulkWrite([
    {
        insertOne: {
            document: {
                nome: "Carlos Mendes",
                email: "carlos@email.com",
                ativo: true
            }
        }
    },
    {
        updateOne: {
            filter: { email: "joao@email.com" },
            update: { $set: { telefone: "11999999999" } }
        }
    },
    {
        deleteOne: {
            filter: { email: "ana@email.com" }
        }
    }
]);
