using Orders.BusinessLogic;
using Orders.BusinessLogic.HttpClients;
using Orders.DataAccess;
using Orders.DataAccess.Contexts;
using OrdersMicroservice.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDataAccess(builder.Configuration);
builder.Services.AddBusinessLogic(builder.Configuration);


builder.Services.AddHttpClient<UserMicroserviceClient>(c =>
{
    c.BaseAddress = new Uri($"http://{builder.Configuration["UsersMicroserviceName"]}:" +
        $"{builder.Configuration["UsersMicroservicePort"]}");
});

builder.Services.AddHttpClient<ProductMicroserviceClient>(c =>
{
    c.BaseAddress = new Uri($"http://{builder.Configuration["ProductsMicroserviceName"]}:" +
        $"{builder.Configuration["ProductsMicroservicePort"]}");
});

var app = builder.Build();
app.UseExceptionHandlingMiddleware();

await SeedingContext.ApplySeedingAsync(app.Services);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
