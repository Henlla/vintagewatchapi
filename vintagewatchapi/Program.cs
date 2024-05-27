using Microsoft.EntityFrameworkCore;
using vintagewatchDAO;
using vintagewatchResponsitory.IResponsitory;
using vintagewatchResponsitory.Responsitory;
using vintagewatchService.IService;
using vintagewatchService.Service;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options => {

    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin();

    });
});

builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("connectString")));

// Product
builder.Services.AddScoped<ProductDAO, ProductDAO>();
builder.Services.AddScoped<IProductResponsitory, ProductResponsitory>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.ConfigureSwaggerGen(setup =>
{
    setup.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Weather Forecasts",
        Version = "v2"
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
