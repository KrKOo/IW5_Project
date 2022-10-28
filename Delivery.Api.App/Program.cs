using AutoMapper;
using AutoMapper.Internal;
using CookBook.Common.Extensions;
using Delivery.Api.BL.Installers;
using Delivery.Api.DAL.Common.Entities;
using Delivery.Api.DAL.EF.Extensions;
using Delivery.Api.DAL.EF.Installers;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

ConfigureControllers(builder.Services);
ConfigureOpenApiDocuments(builder.Services);
ConfigureDependencies(builder.Services, builder.Configuration);
ConfigureAutoMapper(builder.Services);

var app = builder.Build();

ValidateAutoMapperConfiguration(app.Services);
UseDevelopmentSettings(app);
UseSecurityFeatures(app);
UseRouting(app);
UseOpenApi(app);

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

void ConfigureControllers(IServiceCollection serviceCollection)
{
    serviceCollection.AddControllers()
        .AddNewtonsoftJson()
        .AddFluentValidation(options => options.RegisterValidatorsFromAssemblyContaining<ApiBLInstaller>())
        .AddDataAnnotationsLocalization();

    serviceCollection.AddCors(options =>
    {
        options.AddDefaultPolicy(options =>
            options.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());
    });
}

void ConfigureOpenApiDocuments(IServiceCollection serviceCollection)
{
    serviceCollection.AddEndpointsApiExplorer();
    serviceCollection.AddOpenApiDocument();
}

void UseDevelopmentSettings(WebApplication application)
{
    var environment = application.Services.GetRequiredService<IWebHostEnvironment>();

    if (environment.IsDevelopment())
    {
        application.UseDeveloperExceptionPage();
    }
}

void UseSecurityFeatures(IApplicationBuilder application)
{
    application.UseCors();
    application.UseHttpsRedirection();
}

void UseRouting(IApplicationBuilder application)
{
    application.UseRouting();
    application.UseAuthorization();
    application.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });
}

void UseOpenApi(IApplicationBuilder application)
{
    application.UseOpenApi();
    application.UseSwaggerUi3();
}




// Make the implicit Program class public so test projects can access it
public partial class Program
{
}

