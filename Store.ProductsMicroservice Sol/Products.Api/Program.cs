using eCommerce.API.Middlewares;
using Microsoft.EntityFrameworkCore;
using Products.Api.Extensions;
using Products.BusinessLogic;
using Products.DataAccess;
using Products.DataAccess.Contexts;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddServices(builder.Configuration);


builder.Services.AddBusinessLogic();
builder.Services.AddDataAccess();



var app = builder.Build();

app.UseExceptionHandlingMiddleware();

using var scope = app.Services.CreateScope();
var Services = scope.ServiceProvider;
var context = Services.GetRequiredService<AppDbContext>();
var loggerFactory = Services.GetRequiredService<ILoggerFactory>();
try
{
    await context.Database.MigrateAsync();
}
catch (Exception ex)
{
    var logger = loggerFactory.CreateLogger<Program>();
    logger.LogError(ex, "Error in Updating Database");
}



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
