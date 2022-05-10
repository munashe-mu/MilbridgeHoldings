using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MilbridgeHoldings.Models.Data;
using MilbridgeHoldings.Models.Data.Repositories;
using System.Data.Entity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options => options.AddPolicy("CorsPolicy",
builder =>
{
    builder.AllowAnyHeader()
    .AllowAnyMethod()
    .SetIsOriginAllowed((host) => true)
    .AllowCredentials();
}));

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Connection")));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<JobRepository>();
builder.Services.AddTransient<MachinesRepository>();
builder.Services.AddTransient<WorkCentreRepository>();
builder.Services.AddTransient<DepartmentRepository>();

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

app.UseCors("CorsPolicy");
using var serviceScope = app.Services.GetService<IServiceScopeFactory>().CreateScope();
var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
context.Database.EnsureCreated();

app.Run();
