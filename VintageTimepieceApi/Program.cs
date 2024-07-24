using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using VintageTimepieceModel.Models;
using VintageTimePieceRepository.IRepository;
using VintageTimePieceRepository.Repository;
using VintageTimePieceRepository.Util;
using VintageTimepieceService.IService;
using VintageTimepieceService.Service;


var builder = WebApplication.CreateBuilder(args);

// ----------------------------Set up CORS----------------------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("Cors",
        builder => builder.WithOrigins("http://localhost:5173")
        .AllowCredentials()
        .AllowAnyHeader()
        .AllowAnyMethod());
});

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




// ----------------------------------------- Setup JWT -----------------------------------------
builder.Services.AddAuthentication(options =>
{
    // Set up jwt
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
.AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
{
    options.AuthorizationEndpoint = GoogleDefaults.AuthorizationEndpoint;
    options.ClientId = builder.Configuration["Google:clientId"];
    options.ClientSecret = builder.Configuration["Google:clientSecret"];
    options.CallbackPath = builder.Configuration["Google:RedirectUri"];
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
        ValidIssuer = builder.Configuration["Jwt:JwtIssuer"],
        ValidAudience = builder.Configuration["Jwt:JwtAudience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:JwtSecret"])),

    };
})
.AddCookie();


builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.MinimumSameSitePolicy = SameSiteMode.Lax;
    options.Secure = CookieSecurePolicy.Always;
});


// ----------------------------------------- Setup DB ------------------------------------------------
builder.Services.AddDbContext<VintagedbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("connectString"));
    options.UseLazyLoadingProxies();
});


// -------------------------------------------- Setup DI --------------------------------------------
builder.Services.AddSingleton(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddSingleton<IHelper, Helper>();
builder.Services.AddSingleton<IVNPayService, VNPayService>();
builder.Services.AddHttpContextAccessor();

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



// Timepiece Image
builder.Services.AddScoped<IImageRepository, ImageRepository>();
builder.Services.AddScoped<IImageService, ImageService>();



// Rating
builder.Services.AddScoped<IRatingRepository, RatingRepository>();
builder.Services.AddScoped<IRatingService, RatingService>();



// Brand
builder.Services.AddScoped<IBrandRepository, BrandRepository>();
builder.Services.AddScoped<IBrandService, BrandService>();



// Evaluate
builder.Services.AddScoped<IEvaluationRepository, EvaluationRepository>();
builder.Services.AddScoped<IEvaluationService, EvaluationService>();



// Timepiece Evaluate
builder.Services.AddScoped<ITimepieceEvaluateRepository, TimepieceEvaluateRepository>();
builder.Services.AddScoped<ITimepieceEvaluateService, TimepieceEvaluateService>();


// Order
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();


// Order Detail
builder.Services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
builder.Services.AddScoped<IOrderDetailService, OrderDetailService>();

// Transaction
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<ITransactionService, TransactionService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors("Cors");

app.UseTokenMiddleware();

app.UseCookiePolicy();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
