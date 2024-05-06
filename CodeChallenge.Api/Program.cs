using AutoMapper;
using CodeChallenge.Api.Middleware;
using CodeChallenge.Business.DTOs.Mapping;
using CodeChallenge.Business.Services;
using CodeChallenge.Business.Services.Interfaces;
using CodeChallenge.Data.Data;
using CodeChallenge.Data.Services;
using CodeChallenge.Data.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ProjectDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration["ConnectionString:CodeChallenge"], 
        b => b.MigrationsAssembly("CodeChallenge.Api")));

    builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy
            .WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});


builder.Services.AddSwaggerGen();
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfiles());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IBranchService, BranchService>();
builder.Services.AddScoped<IBranchRepository, BranchRepository>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ProjectDbContext>();
    context.Database.Migrate();
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.ConfigureExceptionHandler();
app.UseCors();
app.UseAuthorization();

app.MapControllers();

app.Run();

