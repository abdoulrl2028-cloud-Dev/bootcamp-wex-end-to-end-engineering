-- UPDATE simples
UPDATE Usuarios 
SET Telefone = '11999999999'
WHERE Id = 1;

-- UPDATE múltiplas colunas
UPDATE Usuarios 
SET 
    Telefone = '11988888888',
    DataAtualizacao = GETDATE()
WHERE Nome = 'João Silva';

-- UPDATE com condição
UPDATE Pedidos 
SET Status = 'ENVIADO',
    DataAtualizacao = GETDATE()
WHERE DataPedido < DATEADD(DAY, -3, GETDATE()) AND Status = 'PENDENTE';

-- UPDATE com cálculo
UPDATE ItensPedido 
SET Subtotal = Quantidade * PrecoUnitario;

-- UPDATE FROM (usando JOIN)
UPDATE Pedidos 
SET Total = (
    SELECT SUM(Subtotal) 
    FROM ItensPedido 
    WHERE PedidoId = Pedidos.Id
)
WHERE Status = 'PENDENTE';

-- UPDATE com CASE
UPDATE Usuarios
SET Ativo = CASE 
    WHEN DataCriacao < DATEADD(DAY, -365, GETDATE()) THEN 0
    ELSE 1
END;

-- UPDATE com TOP
UPDATE TOP (10) Pedidos 
SET Status = 'PROCESSANDO'
WHERE Status = 'PENDENTE';
