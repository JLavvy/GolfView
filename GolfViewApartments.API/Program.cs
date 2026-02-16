using GolfViewApartments.API.Data;
using GolfViewApartments.API.Repositories.Implementations;
using GolfViewApartments.API.Repositories.Interfaces;
using GolfViewApartments.API.Services.Implementations;
using GolfViewApartments.API.Services.Interfaces;
using GolfViewApartments.API.Services;
using GolfViewApartments.API.Models; // ADD THIS LINE
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using DotNetEnv;

// ==== DEBUG: Check .env file ====
var currentDir = Directory.GetCurrentDirectory();
var envPath = Path.Combine(currentDir, ".env");
Console.WriteLine("===========================================");
Console.WriteLine($"Current Directory: {currentDir}");
Console.WriteLine($".env path: {envPath}");
Console.WriteLine($".env exists: {File.Exists(envPath)}");

if (File.Exists(envPath))
{
    Console.WriteLine(".env content:");
    Console.WriteLine(File.ReadAllText(envPath));
}

// Load .env file
Env.Load();

// Check if variables are loaded
var jwtKey = Environment.GetEnvironmentVariable("JWT_KEY");
var jwtIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER");
var jwtAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE");

Console.WriteLine($"JWT_KEY: {(string.IsNullOrEmpty(jwtKey) ? "NOT LOADED" : "LOADED (" + jwtKey.Length + " chars)")}");
Console.WriteLine($"JWT_ISSUER: {jwtIssuer ?? "NOT LOADED"}");
Console.WriteLine($"JWT_AUDIENCE: {jwtAudience ?? "NOT LOADED"}");
Console.WriteLine("===========================================");

if (string.IsNullOrEmpty(jwtKey))
{
    throw new InvalidOperationException(
        ".env file not loaded properly! Make sure .env exists in: " + currentDir
    );
}

var builder = WebApplication.CreateBuilder(args);

// Map to configuration
builder.Configuration["Jwt:Key"] = jwtKey;
builder.Configuration["Jwt:Issuer"] = jwtIssuer;
builder.Configuration["Jwt:Audience"] = jwtAudience;
builder.Configuration["Jwt:ExpiryMinutes"] = Environment.GetEnvironmentVariable("JWT_EXPIRY_MINUTES");

// ---------------- SERVICES ----------------
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)
            )
        };
    });

builder.Services.AddAuthorization();

// DATABASE
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("BlazorClient", policy =>
    {
        policy
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
            .SetIsOriginAllowed(_ => true);
    });
});

// Repositories
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

// SERVICES
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IPasswordService, PasswordService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IPricingService, PricingService>();
builder.Services.AddScoped<IEmailService, EmailService>();



// BUILD THE APP FIRST
var app = builder.Build();

// ---------------- MIDDLEWARE ----------------

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Auto-migrate and seed database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    
    await context.Database.MigrateAsync();
    
    // Seed default admin user
    if (!context.Admins.Any(a => a.Email == "admin@golfview.com"))
    {
        var admin = new Admin
        {
            Email = "admin@golfview.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("$Admin123!"),
            Role = "Admin"
        };
        context.Admins.Add(admin);
        await context.SaveChangesAsync();
        Console.WriteLine("Default admin user created successfully.");
    }
    
    var seeder = new DataSeeder(context);
    await seeder.SeedAsync(); 
}

app.UseHttpsRedirection();
app.UseCors("BlazorClient");
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
