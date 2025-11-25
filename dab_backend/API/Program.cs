using DAL.Entities;
using DAL.Repositories;
using Service.Services;
using API.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();


// Configure DbContexts for both databases
builder.Services.AddDbContext<DABDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection") ?? 
                      "Host=localhost;Database=dab;Username=postgres;Password=123456"));

builder.Services.AddDbContext<IAMDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("IAMConnection") ?? 
                      "Host=localhost;Database=IAM_base;Username=postgres;Password=123456"));

// Register Unit of Work (unified for both databases)
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Register Services
builder.Services.AddScoped<IAgenceService, AgenceService>();
builder.Services.AddScoped<IDabService, DabService>();
builder.Services.AddScoped<IUtilisateurService, UtilisateurService>();
builder.Services.AddScoped<IComposantService, ComposantService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<ICaisseService, CaisseService>();
builder.Services.AddScoped<IImprimanteService, ImprimanteService>();
builder.Services.AddScoped<ILecteurcodeService, LecteurcodeService>();
builder.Services.AddScoped<IMenuService, MenuService>();
builder.Services.AddScoped<IProfileService, ProfileService>();

var app = builder.Build();

// Initialize databases on startup
await app.InitializeDatabasesAsync();

// Configure the HTTP request pipeline.


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
