using TarefasAPI.Repositories;
using TarefasAPI.Services;
using TarefasAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

// Adicionar banco de dados InMemory
builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("TarefasDb"));

// Injeção de dependências
builder.Services.AddScoped<ITarefaRepository, TarefaRepository>();
builder.Services.AddScoped<ITarefaService, TarefaService>();

// Adicionar controladores e Swagger
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    }); 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API de Tarefas",
        Version = "v1",
        Description = "API para gerenciamento de tarefas.",
        Contact = new OpenApiContact
        {
            Name = "GRUPO SADA",
            Email = "suporte@gruposada.com"
        }
    });
    c.UseInlineDefinitionsForEnums();

    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API de Tarefas v1");
        c.DocumentTitle = "Documentação da API de Tarefas";
    });
}

app.UseMiddleware<TarefasAPI.Middlewares.ErrorHandlingMiddleware>();

app.UseAuthorization();
app.MapControllers();
app.Run();
