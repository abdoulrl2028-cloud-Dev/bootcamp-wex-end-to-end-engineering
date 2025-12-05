# Anotações - Fundamentos .NET

Comandos úteis:

- Listar SDKs instalados:
  - `dotnet --list-sdks`
- Criar novo projeto console:
  - `dotnet new console -n NomeProjeto`
- Restaurar dependências:
  - `dotnet restore`
- Rodar projeto:
  - `dotnet run`
- Publicar aplicativo:
  - `dotnet publish -c Release -o ./publish`

Conceitos básicos:

- `dotnet` é a CLI que gerencia projetos, builds e execução.
- Projetos usam um arquivo `.csproj` para definir framework e dependências.
- `using` importa namespaces; `implicit usings` facilita em .NET 6+
- `Nullable` melhora segurança de referências nulas.

Recomendações:

- Use `dotnet format` e `dotnet test` quando aplicável.
- Prefira `record` para tipos imutáveis simples.
