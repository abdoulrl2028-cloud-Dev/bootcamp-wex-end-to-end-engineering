-- DELETE simples
DELETE FROM Usuarios 
WHERE Id = 1;

-- DELETE com condição
DELETE FROM Usuarios 
WHERE Ativo = 0 AND DataCriacao < DATEADD(YEAR, -1, GETDATE());

-- DELETE com JOIN
DELETE FROM Pedidos 
WHERE UsuarioId IN (
    SELECT Id FROM Usuarios WHERE Ativo = 0
);

-- DELETE com TOP
DELETE TOP (5) FROM ItensPedido 
WHERE PedidoId IN (SELECT Id FROM Pedidos WHERE Status = 'CANCELADO');

-- DELETE em cascata (cuidado!)
-- Primeiro deleta itens, depois o pedido
BEGIN TRANSACTION;
DELETE FROM ItensPedido WHERE PedidoId = 1;
DELETE FROM Pedidos WHERE Id = 1;
COMMIT;

-- TRUNCATE - Remove todos os dados (mais rápido)
-- TRUNCATE TABLE Usuarios;  -- Cuidado: não pode ser revertido com rollback
