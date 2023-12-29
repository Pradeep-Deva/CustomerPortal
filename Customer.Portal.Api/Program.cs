using Customer.Portal.Application.Contracts;
using Customer.Portal.Application.Services;
using Customer.Portal.DataAccess.Read.Contracts;
using Customer.Portal.DataAccess.Read.CustomerPortalRepo;
using Customer.Portal.Infrastructure.ConfigurationManager;
using Customer.Portal.Infrastructure.Contracts;
using Microsoft.Extensions.Configuration;
using Review.History.Infrastructure.Connection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IOrdersProcessor, OrdersProcessor>();
builder.Services.AddScoped<ICustomerPortalReadRepository, CustomerPortalReadRepository>();
builder.Services.AddScoped<IConnectionFactory, ConnectionFactory>();

builder.Services.Configure<ConnectionString> (builder.Configuration.GetSection("ConnectionStrings"));

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
