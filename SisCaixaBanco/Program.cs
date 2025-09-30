using Microsoft.EntityFrameworkCore;
using SisCaixaBanco.AutoMapper;
using SisCaixaBanco.Data;
using SisCaixaBanco.Models;
using SisCaixaBanco.Repositories;
using SisCaixaBanco.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// DbContext
builder.Services.AddDbContext<CaixaBancoDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositórios
builder.Services.AddScoped(typeof(Repository<>), typeof(Repository<>));
builder.Services.AddScoped<IRepository<Transferencia>, Repository<Transferencia>>();
builder.Services.AddScoped<IRepository<ContaLog>, Repository<ContaLog>>();
builder.Services.AddScoped<IContaRepository, ContaRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Services
builder.Services.AddScoped<IContaService, ContaService>();
builder.Services.AddScoped<ITransferenciaService, TransferenciaService>();


//GlobalResources
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.AddAutoMapper(cfg => cfg.AddProfile<ContaProfile>());
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<ContaLogProfile>());
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<TransferenciaProfile>());

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
