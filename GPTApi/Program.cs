using Database;
using Domain.Repositories;
using Domain.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Repository.Mapper;
using Repository.Repositoriy;
using Repository.Repository;
using Services.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddScoped<IHistoryRepository, HistoryRepository>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddScoped<ITopicRepository, TopicRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IChatService, ChatService>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

builder.Services.AddScoped<ITokenGenerator>(provider =>
{
    var refreshTokenRepository = provider.GetRequiredService<IRefreshTokenRepository>();
    var configuration = provider.GetRequiredService<IConfiguration>();
    var issuer = configuration["Jwt:Issuer"];
    var audience = configuration["Jwt:Audience"];
    var secretKey = configuration["Jwt:SecretKey"];
    var accessExpiryInMinutes = int.Parse(configuration["Jwt:AccessExpireInMinutes"]);
    var refreshExpiryInMinutes = int.Parse(configuration["Jwt:RefreshExpireInMinutes"]);

    return new TokenGeneratorService(issuer, audience, secretKey, accessExpiryInMinutes, refreshExpiryInMinutes, refreshTokenRepository);
});

builder.Services.AddAutoMapper(typeof(MappingProfile));

//Connection string
builder.Services.AddDbContext<AppDbContext>(option =>
    option.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")));

// JWT auth
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "my-app",
            ValidAudience = "my-users",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("mE7P8JWwMdT7RzRgHfNv2AxcQ1tZqGKS123131313123123123123123123123123123123123123"))
        };
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});
// Add swagger services
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseCors("AllowAnyOrigin");

// Enable middleware to serve generated Swagger as a JSON endpoint.
app.UseSwagger();

// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
// specifying the Swagger JSON endpoint.
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});

app.UseHttpsRedirection();

app.UseRouting();

// Use authentication middleware
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
