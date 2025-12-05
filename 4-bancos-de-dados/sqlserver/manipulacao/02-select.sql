-- SELECT básico
SELECT * FROM Usuarios;

-- SELECT com colunas específicas
SELECT Id, Nome, Email FROM Usuarios;

-- SELECT com WHERE
SELECT * FROM Usuarios WHERE Nome LIKE '%Silva%';

-- SELECT com condições múltiplas
SELECT * FROM Usuarios 
WHERE Ativo = 1 AND Nome LIKE '%Silva%';

-- SELECT com ORDER BY
SELECT * FROM Usuarios 
ORDER BY Nome ASC;

-- SELECT com TOP
SELECT TOP 5 * FROM Usuarios 
ORDER BY DataCriacao DESC;

-- SELECT com COUNT (agregação)
SELECT COUNT(*) as TotalUsuarios FROM Usuarios;

-- SELECT com GROUP BY
SELECT COUNT(*) as Total, Ativo 
FROM Usuarios 
GROUP BY Ativo;

-- JOIN - Listar pedidos com dados do usuário
SELECT 
    p.Id as PedidoId,
    p.DataPedido,
    p.Total,
    p.Status,
    u.Nome as NomeUsuario,
    u.Email
FROM Pedidos p
INNER JOIN Usuarios u ON p.UsuarioId = u.Id;

-- LEFT JOIN - Usuários e seus pedidos (mesmo sem pedidos)
SELECT 
    u.Nome,
    u.Email,
    COUNT(p.Id) as TotalPedidos,
    SUM(ISNULL(p.Total, 0)) as TotalGasto
FROM Usuarios u
LEFT JOIN Pedidos p ON u.Id = p.UsuarioId
GROUP BY u.Id, u.Nome, u.Email;

-- INNER JOIN múltiplo - Itens do pedido com detalhes
SELECT 
    p.Id as PedidoId,
    u.Nome as NomeUsuario,
    pr.Nome as NomeProduto,
    ip.Quantidade,
    ip.PrecoUnitario,
    ip.Subtotal
FROM ItensPedido ip
INNER JOIN Pedidos p ON ip.PedidoId = p.Id
INNER JOIN Usuarios u ON p.UsuarioId = u.Id
INNER JOIN Produtos pr ON ip.ProdutoId = pr.Id;

-- Subquery
SELECT * FROM Usuarios 
WHERE Id IN (SELECT DISTINCT UsuarioId FROM Pedidos);

-- CTE (Common Table Expression)
WITH UsuariosPedidos AS (
    SELECT 
        u.Id,
        u.Nome,
        COUNT(p.Id) as TotalPedidos,
        SUM(ISNULL(p.Total, 0)) as TotalGasto
    FROM Usuarios u
    LEFT JOIN Pedidos p ON u.Id = p.UsuarioId
    GROUP BY u.Id, u.Nome
)
SELECT * FROM UsuariosPedidos 
WHERE TotalPedidos > 0
ORDER BY TotalGasto DESC;

-- CASE (Lógica condicional)
SELECT 
    Nome,
    Email,
    CASE 
        WHEN Ativo = 1 THEN 'Ativo'
        ELSE 'Inativo'
    END as Status
FROM Usuarios;
