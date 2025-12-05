-- Transação básica
BEGIN TRANSACTION;

INSERT INTO Usuarios (Nome, Email, CPF)
VALUES ('Novo Usuario', 'novo@email.com', '111.222.333-00');

DECLARE @NovoUsuarioId INT = SCOPE_IDENTITY();

INSERT INTO Pedidos (UsuarioId, Total, Status)
VALUES (@NovoUsuarioId, 100.00, 'PENDENTE');

COMMIT;

-- Transação com ROLLBACK (erro)
BEGIN TRANSACTION;

UPDATE Usuarios SET Email = 'invalido' WHERE Id = 1;

-- Se algo der errado
IF @@ERROR <> 0
    ROLLBACK
ELSE
    COMMIT;

-- Transação com TRY-CATCH
BEGIN TRY
    BEGIN TRANSACTION;
    
    INSERT INTO Pedidos (UsuarioId, Total, Status)
    VALUES (999, 50.00, 'PENDENTE');  -- UsuarioId inválido vai gerar erro
    
    COMMIT;
END TRY
BEGIN CATCH
    ROLLBACK;
    SELECT ERROR_MESSAGE() as MensagemErro;
END CATCH;

-- Transação com ponto de salvamento
BEGIN TRANSACTION;

INSERT INTO Usuarios (Nome, Email, CPF)
VALUES ('Usuario 1', 'usuario1@email.com', '111.111.111-11');

SAVE TRANSACTION Ponto1;

INSERT INTO Usuarios (Nome, Email, CPF)
VALUES ('Usuario 2', 'usuario2@email.com', '222.222.222-22');

-- Reverter apenas até o ponto 1
ROLLBACK TRANSACTION Ponto1;

COMMIT;
