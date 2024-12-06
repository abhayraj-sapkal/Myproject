using Myproject;
using MyProject;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var _configuration = builder.Configuration;

// Register custom services
builder.Services.RegisterRepos(_configuration);

// Add services to the container
builder.Services.AddControllers();

// Configure Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Add JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _configuration["JwtSettings:Issuer"],
            ValidAudience = _configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]))
        };
    });

// Build the app
var app = builder.Build();

// Configure the HTTP request pipeline


// Add Authentication and Authorization middleware
app.UseAuthentication(); // Added middleware to validate tokens
app.UseAuthorization();

app.MapControllers();

app.Run();
