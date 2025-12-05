using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var portfolio = new
{
    name = "Seu Nome",
    bio = "Engenheiro(a) de software — Portfólio de exemplo",
    skills = new[] { "C#", ".NET", "ASP.NET Core", "SQL" },
    projects = new[]
    {
        new { title = "Projeto A", description = "Descrição do projeto A", url = "https://example.com/a" },
        new { title = "Projeto B", description = "Descrição do projeto B", url = "https://example.com/b" }
    }
};

app.MapGet("/", () => Results.Text("API Portfolio - acesse /portfolio"));
app.MapGet("/portfolio", () => Results.Json(portfolio));

app.Run();
