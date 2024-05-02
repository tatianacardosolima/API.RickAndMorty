using API.RickAndMorty.Commands;
using API.RickAndMorty.Filters;
using API.RickAndMorty.Handlers;
using API.RickAndMorty.Interfaces.IServices;
using API.RickAndMorty.Services;
using MediatR;
using Microsoft.OpenApi.Models;
using System.Data;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = @"Consumir os dados de personagens e listar todos os que atendam os seguintes critérios: 
           1) Status = unknown, 2)Species = alien 
           e 3) Apareceram em mais de 1 episódio.  
           O parâmetros Status e Species deixamos texto livre para simples demonstração.", Version = "v1", Description = "Código para demonstração" });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);  

});

builder.Services.AddSingleton<ICacheService, CacheService>();

builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.AddScoped<IRequestHandler<FilterCharectersRequest, DefaultResponse>, GetCharactersHandler>();

builder.Services.AddMvc(config =>
{
    config.Filters.Add(typeof(ExceptionFilter));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();






