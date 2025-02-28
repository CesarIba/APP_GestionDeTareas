using BackGestionTareas.Context;
using BackGestionTareas.Infrastructure;
using BackGestionTareas.Models;
using BackGestionTareas.Models.Validators;
using BackGestionTareas.Repositories;
using BackGestionTareas.Services;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ITareaService, TareaService>();
builder.Services.AddScoped<ITareaRepository, TareaRepository>();
builder.Services.AddValidatorsFromAssemblyContaining<TareaDtoValidator>();
//builder.Services.AddScoped<IValidator<TareaDto>, TareaDtoValidator>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        builder => builder.WithOrigins("http://localhost:4200") // Se ajusta según la URL del frontend
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BackGestionTareas", Version = "v1" });
});
builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));


var app = builder.Build();

// Middleware de manejo de errores
app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BackGestionTareas v1"));
}

app.UseCors("AllowFrontend");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
