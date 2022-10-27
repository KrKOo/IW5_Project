using AutoMapper;
using AutoMapper.Internal;
using CookBook.Common.Extensions;
using Delivery.Api.BL.Installers;
using Delivery.Api.DAL.Common.Entities;
using Delivery.Api.DAL.EF.Extensions;
using Delivery.Api.DAL.EF.Installers;

var builder = WebApplication.CreateBuilder(args);

ConfigureDependencies(builder.Services, builder.Configuration);
ConfigureAutoMapper(builder.Services);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

ValidateAutoMapperConfiguration(app.Services);

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

void ConfigureDependencies(IServiceCollection serviceCollection, IConfiguration configuration)
{
    var connectionString = configuration.GetConnectionString("DefaultConnection")
        ?? throw new ArgumentException("The connection string is missing");
    serviceCollection.AddInstaller<ApiDALEFInstaller>(connectionString);
    serviceCollection.AddInstaller<ApiBLInstaller>();
}

void ConfigureAutoMapper(IServiceCollection serviceCollection)
{
    serviceCollection.AddAutoMapper(configuration =>
    {
        configuration.Internal().MethodMappingEnabled = false;
    }, typeof(EntityBase), typeof(ApiBLInstaller));
}

void ValidateAutoMapperConfiguration(IServiceProvider serviceProvider)
{
    var mapper = serviceProvider.GetRequiredService<IMapper>();
    mapper.ConfigurationProvider.AssertConfigurationIsValid();
}

// Make the implicit Program class public so test projects can access it
public partial class Program
{
}

