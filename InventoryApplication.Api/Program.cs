using InventoryApplication.Api.Utils;
using InventoryApplication.Domain.Repository;
using InventoryApplication.Infrastructure.Repository;
using InventoryApplication.Infrastructure.Repository.Context;
using InventoryApplication.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<InventoryContext>(options =>
    options.UseSqlServer(Environment.GetEnvironmentVariable("INVENTORY_CONNECTION_STRING")));
builder.Services.AddScoped<IEquipmentTypeRepository, EquipmentTypeRepository>();
builder.Services.AddScoped<IEquipmentRepository, EquipmentRepository>();
builder.Services.AddScoped<IMaintenanceTaskRepository, MaintenanceTaskRepository>();
builder.Services.AddScoped<EquipmentService>();
builder.Services.AddScoped<MaintenanceTaskService>();

builder.Services.AddControllers(options => {
    options.Filters.Add<ValidateModelStateFilter>();
    options.Filters.Add<GlobalExceptionFilter>();
})
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        options.SerializerSettings.DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore;
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

var culture = new CultureInfo("en-US");
CultureInfo.DefaultThreadCurrentCulture = culture;
CultureInfo.DefaultThreadCurrentUICulture = culture;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<InventoryContext>();
        dbContext.Database.EnsureCreated();
    }
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
