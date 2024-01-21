using Customer.Portal.Api.Automapper;
using Customer.Portal.Application.Contracts;
using Customer.Portal.Application.Services;
using Customer.Portal.DataAccess.Read.Contracts;
using Customer.Portal.DataAccess.Read.CustomerPortalRepo;
using Customer.Portal.Infrastructure.ConfigurationManager;
using Customer.Portal.Infrastructure.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Review.History.Infrastructure.Connection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAutoMapper(typeof(AutomapprConfig));
var key = builder.Configuration.GetValue<string>("ApiSettings:Secret");
builder.Services.AddAuthentication(x=>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(
    x=>

    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
            ValidateIssuer = false,
            ValidateAudience = false
        };

}); ;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description="Bearer [space] your token",
        Name="Authorization",
        In =ParameterLocation.Header,
        Scheme="Bearer"
    });
});

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
