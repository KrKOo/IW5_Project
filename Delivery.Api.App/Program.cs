using Delivery.Api.DAL.EF.Extensions;
using Delivery.Api.DAL.EF.Installers;

var builder = WebApplication.CreateBuilder(args);

ConfigureDependencies(builder.Services, builder.Configuration);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

void ConfigureDependencies(IServiceCollection serviceCollection, IConfiguration configuration)
{
    var connectionString = configuration.GetConnectionString("DefaultConnection")
        ?? throw new ArgumentException("The connection string is missing");
    serviceCollection.AddInstaller<ApiDALEFInstaller>(connectionString);
}
