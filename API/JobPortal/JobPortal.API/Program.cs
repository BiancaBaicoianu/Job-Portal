using JobPortal.API.Data;
using JobPortal.API.Repositories.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using JobPortal.API.Repositories;
using JobPortal.API.Repositories.UnitOfWork;
using JobPortal.API.Services;
using JobPortal.API.Helpers;
using JobPortal.API.Helpers.Extensions;
using JobPortal.API.Helpers.Seeder;
using Microsoft.OpenApi.Models;
using JobPortal.API.Helpers.MiddleWare;
using JobPortal.API.Helpers.JwtUtils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// legam contextul de baza de date cu aplicatia noastra (PortalContext) 
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<PortalContext>(options =>
    options.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                },
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});
builder.Services.AddRepositories();
builder.Services.AddServices();
builder.Services.AddSeeders();

//transient => se creeze de fiecare cand se face un request
//scoped => se creeaza o singura data pe request
//singleton => se creeaza o singura data pe aplicatie

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IAuthenticationService, AuthenticationService>();

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//builder.Services.AddScoped<IJwtUtils, JwtUtils>();
builder.Services.AddEndpointsApiExplorer();

// Authentication scheme

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        var Key = Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]);
        // options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            ValidAudience = builder.Configuration["JWT:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Key)
        };
    });
/*
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var Key = Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]);
        // options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JWT:Key").Value)),
            ValidateAudience = true,
            ValidateLifetime = true,
           
        };
    });
/*
builder.Services.AddCors((setup) =>
{
    setup.AddPolicy("default", (options) =>
    {
        options.AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader();
    });
});
*/
var app = builder.Build();

// Configure the HTTP request pipeline.
// cand se face request, trecem prin fiecare middleware !conteaza ordinea
// pentru response => ordine inversa
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("default");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
//app.UseMiddleware<JwtMiddleware>();
app.MapControllers();

app.Run();


void SeedData(IHost app) // se ruleaza la inceput cand nu ai nimic in tabela
{
    var scopeFactory = app.Services.GetService<IServiceScopeFactory>();

    using (var scope = scopeFactory.CreateScope())
    {
        var seeder = scope.ServiceProvider.GetService<UserSeeder>(); // luam serviciile inregistrate
        seeder.SeedInitialUsers();
    }
}