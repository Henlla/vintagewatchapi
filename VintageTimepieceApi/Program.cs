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

// Set up CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("Cors",
        builder => builder.WithOrigins("http://localhost:5173")
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod());
});


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




// Setup JWT
builder.Services.AddAuthentication(options =>
{
    // Set up jwt
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    //Set up google cookie
    //options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    //options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    //options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;

})
.AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
{
    options.AuthorizationEndpoint = GoogleDefaults.AuthorizationEndpoint;
    options.ClientId = builder.Configuration["Google:clientId"];
    options.ClientSecret = builder.Configuration["Google:clientSecret"];
    //options.CallbackPath = builder.Configuration["Google:RedirectUri"];
    //options.SaveTokens = true;
    options.Scope.Add("profile");
    options.Scope.Add("email");
})
.AddJwtBearer(options =>
{
    options.Authority = "https://accounts.google.com";
    options.Audience = builder.Configuration["Google:clientId"];
    options.SaveToken = true;

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,

        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:JwtSecret"])),

        ValidIssuer = builder.Configuration["Jwt:JwtIssuer"],
        ValidAudience = builder.Configuration["Jwt:JwtAudience"],
    };
})
.AddCookie();


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

// Jwt
builder.Services.AddScoped<IJwtConfigRepository, JwtConfigRepository>();
builder.Services.AddScoped<IJwtConfigService, JwtConfigService>();


// Authenticate
builder.Services.AddScoped<IAuthenticateRepository, AuthenticateRepository>();
builder.Services.AddScoped<IAuthenticateService, AuthenticateService>();
builder.Services.AddScoped<IHashPasswordRepository, HashPasswordRepository>();


// Role
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IRoleService, RoleService>();


// Category
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();


// Timepiece
builder.Services.AddScoped<ITimepieceRepository, TimepieceRepository>();
builder.Services.AddScoped<ITimepiecesService, TimepiecesService>();



var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseCors("Cors");

app.UseCookiePolicy();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
