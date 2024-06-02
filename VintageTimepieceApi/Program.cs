using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using VintageTimepieceModel;
using VintageTimePieceRepository.IRepository;
using VintageTimePieceRepository.Repository;
using VintageTimepieceService.IService;
using VintageTimepieceService.Service;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Set up CORS

builder.Services.AddCors(options =>
{
    options.AddPolicy("EnableCors", builder =>
    {
        builder.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

// Setup JWT
builder.Services.AddAuthentication(options =>
{
    // Set up jwt
    //options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    //options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

    //Set up google cookie
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;

}).AddCookie()
.AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
{
    options.ClientId = builder.Configuration["Google:clientId"];
    options.ClientSecret = builder.Configuration["Google:clientSecret"];
    //options.CallbackPath = new PathString(builder.Configuration["Google:clientCallBack"]);
})
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.Authority = "https://accounts.google.com";
    options.Audience = builder.Configuration["Google:clientId"];

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,

        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:JwtSecret"])),

        ValidIssuer = builder.Configuration["Jwt:JwtIssuer"],
        ValidAudience = builder.Configuration["Jwt:JwtAudience"],
    };
});


builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.MinimumSameSitePolicy = SameSiteMode.Lax;
    options.Secure = CookieSecurePolicy.Always;
});


// Setup DB
builder.Services.AddDbContext<VintagedbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("connectString"));
    options.UseLazyLoadingProxies();
});


// Setup DI
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

// Authenticate
builder.Services.AddScoped<IAuthenticateRepository, AuthenticateRepository>();
builder.Services.AddScoped<IAuthenticateService, AuthenticateService>();
builder.Services.AddScoped<IHashPasswordRepository, HassPasswordRepository>();



var app = builder.Build();




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCookiePolicy();

app.UseAuthentication();

app.UseAuthorization();

app.UseCors("EnableCors");

app.MapControllers();

app.Run();
