using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using umg_database.Common.Interfaces;
using umg_database.Sources.Jwt.Models;
using umg_database.Sources.Session.Entities;
using umg_database.Sources.Supplier.Entities;
using umg_database.Sources.Supplier.Models;
using umg_safety_standars.Common.Interfaces;
using umg_safety_standars.Context;
using umg_safety_standars.Source.Jwt.Models;
using umg_safety_standars.Source.Jwt.Services;
using umg_safety_standars.Source.Session.Models;
using umg_safety_standars.Source.Session.Repositories;
using umg_safety_standars.Source.Session.Services;
using umg_safety_standars.Source.Supplier.Services;
using umg_safety_standars.Sources.Supplier.Repositories;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<SafetyStandarContext>(
    db => db.UseNpgsql(
        builder.Configuration.GetConnectionString("UmgAuditoriaDb")
        )
    );

builder.Configuration.AddEnvironmentVariables();

//Configuration 

#region Repositories

builder.Services.AddScoped<IGetRepository<SupplierIdFilterModel, SupplierEntity>,SupplierRepository>();
builder.Services.AddScoped<IPostRepository<SupplierRegistryModel, SupplierEntity>, SupplierRepository>();
builder.Services.AddScoped<IGetRepository<SupplierLoginFilterModel, SupplierEntity>, SupplierRepository>();
builder.Services.AddScoped<IPostRepository<SessionEntity, SessionEntity>, SessionRepository>();
builder.Services.AddScoped<IPutRepository<SessionEntity>, SessionRepository>();

#endregion

#region Services

builder.Services.AddScoped<IJwtService<JwtAuthServiceRequestModel, JwtModel>, JwtAuthService>();
builder.Services.AddScoped<IPostService<LoginServiceRequestModel, LoginServiceResponseModel>, LoginService>();
builder.Services.AddScoped<IPostService<SupplierRegistryModel, SupplierEntity>, SupplierRegistryService>();

#endregion

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDataProtection();

builder.Services.AddCors(corsOptions =>
{
    corsOptions.AddPolicy("DefaultPolicy", application =>
    {
        application.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("DefaultPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();

[ExcludeFromCodeCoverage]
public partial class Program { }