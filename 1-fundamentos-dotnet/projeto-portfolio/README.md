# Projeto: Portfolio (Minimal API)

Projeto exemplo usando ASP.NET Core Minimal API que expõe um endpoint JSON com informações do portfolio.

Como executar:

```bash
cd 1-fundamentos-dotnet/projeto-portfolio
dotnet run
```

Por padrão a aplicação vai escutar em uma porta (ex: 5041). Acesse:

- `http://localhost:5041/` — mensagem inicial
- `http://localhost:5041/portfolio` — JSON com dados de exemplo

Exemplo de resposta JSON:

```json
{
  "name": "Seu Nome",
  "bio": "Engenheiro(a) de software — Portfólio de exemplo",
  "skills": ["C#", ".NET", "ASP.NET Core", "SQL"],
  "projects": [
    { "title": "Projeto A", "description": "Descrição do projeto A", "url": "https://example.com/a" }
  ]
}
```
