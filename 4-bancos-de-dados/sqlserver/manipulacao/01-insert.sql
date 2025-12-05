-- INSERT - Inserir múltiplos usuários
INSERT INTO Usuarios (Nome, Email, Telefone, CPF)
VALUES 
    ('João Silva', 'joao@email.com', '11999999999', '123.456.789-00'),
    ('Maria Santos', 'maria@email.com', '11988888888', '987.654.321-00'),
    ('Pedro Oliveira', 'pedro@email.com', '11977777777', '456.789.123-00'),
    ('Ana Costa', 'ana@email.com', '11966666666', '789.123.456-00');

-- INSERT com valores padrão
INSERT INTO Usuarios (Nome, Email, CPF)
VALUES ('Carlos Mendes', 'carlos@email.com', '321.654.987-00');

-- Verificar IDs inseridos
SELECT * FROM Usuarios;

-- INSERT INTO ... SELECT (copiar dados)
INSERT INTO Usuarios (Nome, Email, CPF, Ativo)
SELECT Nome, Email, CPF, Ativo FROM Usuarios WHERE Id > 0;
