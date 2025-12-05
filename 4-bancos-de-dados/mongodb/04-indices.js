// MongoDB - Índices e Otimização

// 1. CRIAR índice simples em campo único
db.usuarios.createIndex({ email: 1 });

// 2. CRIAR índice composto
db.usuarios.createIndex({ ativo: 1, dataCriacao: -1 });

// 3. CRIAR índice de texto para busca
db.usuarios.createIndex({ nome: "text" });

// 4. CRIAR índice único (garante unicidade)
db.usuarios.createIndex({ cpf: 1 }, { unique: true });

// 5. CRIAR índice sparse (ignora documentos sem o campo)
db.usuarios.createIndex({ telefone: 1 }, { sparse: true });

// 6. CRIAR índice com TTL (Time To Live)
db.sessoes.createIndex({ dataCriacao: 1 }, { expireAfterSeconds: 3600 });

// 7. LISTAR todos os índices de uma coleção
db.usuarios.getIndexes();

// 8. LISTAR informações detalhadas de índices
db.usuarios.getIndexStats();

// 9. DELETAR um índice específico
db.usuarios.dropIndex({ email: 1 });

// 10. DELETAR todos os índices (exceto _id)
db.usuarios.dropIndexes();

// 11. VER plano de execução de uma query
db.usuarios.find({ email: "joao@email.com" }).explain("executionStats");

// 12. ANALISAR performance de agregação
db.pedidos.aggregate([
    { $match: { status: "PENDENTE" } },
    { $sort: { dataPedido: -1 } }
]).explain("executionStats");

// 13. ÍNDICE GEOESPACIAL - Buscar por localização
db.lojas.createIndex({ localizacao: "2dsphere" });

// Busca próxima
db.lojas.find({
    localizacao: {
        $near: {
            $geometry: {
                type: "Point",
                coordinates: [-73.99279, 40.71455]
            },
            $maxDistance: 5000
        }
    }
});

// 14. ÍNDICE WILDCARD - Buscar em múltiplos campos
db.produtos.createIndex({ "atributos.$**": 1 });

// 15. CRIAR índice com opções
db.pedidos.createIndex(
    { usuarioId: 1, status: 1 },
    { 
        name: "idx_usuario_status",
        background: true,
        partialFilterExpression: { status: { $ne: "CANCELADO" } }
    }
);

// 16. REBUILT ÍNDICES
db.usuarios.reIndex();

// 17. ESTATÍSTICAS DE COLEÇÃO
db.usuarios.stats();

// 18. TAMANHO DA COLEÇÃO
db.usuarios.totalSize();

// 19. CONTAR documentos com filter
db.pedidos.countDocuments({ status: "PENDENTE" });

// 20. AGREGAÇÃO - Analise de cardinalidade de índice
db.usuarios.aggregate([
    { $group: { _id: "$email" } },
    { $count: "total" }
]);

// 21. HINTS - Forçar uso de um índice
db.usuarios.find({ email: "joao@email.com", ativo: true })
    .hint({ email: 1 });

// 22. COVERED QUERY - Query que usa apenas dados do índice
db.usuarios.find(
    { email: "joao@email.com" },
    { _id: 0, email: 1, ativo: 1 }
)
.hint({ email: 1, ativo: 1 });

// 23. COMPOUND INDEX - Exemplo de ordem importa
// Para { ativo: 1, dataCriacao: -1 }
// Eficiente para: find({ ativo: true }) e find({ ativo: true, dataCriacao: ... })
db.usuarios.createIndex({ ativo: 1, dataCriacao: -1 });

// 24. ESR - Equality, Sort, Range
// Ordem ideal: { ativo: 1, dataCriacao: -1, idade: 1 }
// Equality: ativo = true
// Sort: dataCriacao DESC
// Range: idade > 18
db.usuarios.createIndex({ ativo: 1, dataCriacao: -1, idade: 1 });

// 25. BOAS PRÁTICAS - Índices para agregação
db.pedidos.createIndex({ status: 1 });
db.pedidos.createIndex({ dataPedido: -1 });
db.pedidos.createIndex({ usuarioId: 1 });

// Agregação otimizada com esses índices
db.pedidos.aggregate([
    { $match: { status: "ENTREGUE" } },  // Usa índice de status
    { $sort: { dataPedido: -1 } },      // Usa índice de dataPedido
    { $limit: 10 }
]);

// 26. REMOVER ÍNDICES NÃO UTILIZADOS
// Verificar stats primeiro
db.usuarios.aggregate([
    { $indexStats: {} }
]);

// 27. CRIAR ÍNDICE COM COLLATION
db.usuarios.createIndex(
    { nome: 1 },
    { collation: { locale: "pt", strength: 2 } }  // Case-insensitive
);

// 28. QUERY COM COLLATION
db.usuarios.find({ nome: "João" }).collation({ locale: "pt", strength: 2 });

// 29. BENCHMARK - Sem índice vs Com índice
// Sem índice: Full collection scan
var inicio = Date.now();
db.usuarios.find({ email: "joao@email.com" }).count();
var semIndice = Date.now() - inicio;

// Com índice
db.usuarios.createIndex({ email: 1 });
inicio = Date.now();
db.usuarios.find({ email: "joao@email.com" }).count();
var comIndice = Date.now() - inicio;

print("Sem índice: " + semIndice + "ms");
print("Com índice: " + comIndice + "ms");

// 30. LIMPAR CACHE E TESTAR NOVAMENTE
db.getCollection("usuarios").getPlanCache().clear();
