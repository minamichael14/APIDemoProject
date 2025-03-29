using System.Text;
using APIDay1.Data;
using APIDay1.Middelwares;
using APIDay1.Repository;
using APIDay1.Services;
using APIDemoProject.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("JWT Bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Description = "Enter 'Bearer {your JWT token}' to authenticate"
    });
});

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("AllowAll", cfg =>
    {
        cfg.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });

    opt.AddPolicy("SpecifcAllow", cfg =>
    {
        cfg.WithOrigins("http://127.0.0.1:5500").AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);


var key = Encoding.UTF8.GetBytes(Constants.SecretKey);
builder.Services.AddAuthentication(opts =>
{
    opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opts =>
{
    opts.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidIssuer = Constants.JWTIssuer,
        ValidateIssuer = true,

        ValidAudience = Constants.JWTAudience,
        ValidateAudience = true,

        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuerSigningKey = true,

        ValidateLifetime = true
    };
});
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IAuthService, AuthService>();
var app = builder.Build();

app.UseMiddleware<GlobalErrorHandlerMiddleware>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();

}
app.UseCors("SpecifcAllow");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
