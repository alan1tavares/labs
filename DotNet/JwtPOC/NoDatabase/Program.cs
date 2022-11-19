using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Services;
using PocJwt;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Description = "JWT Authorization header using the Bearer scheme.",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(jwtOptions =>
{
    jwtOptions.SaveToken = true;
    jwtOptions.RequireHttpsMetadata = false;
    jwtOptions.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        IssuerSigningKey = new SymmetricSecurityKey(SecretKey.GetWithEncoding()),
    };

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/ping", () =>
{
    return "pong";
})
.WithName("ping");

app.MapGet("/secreteroute", () =>
{
    return "You find me";
}).RequireAuthorization();

app.MapPost("/login", (User aUser) =>
{
    var user = UserRepository.Get(aUser.Username, aUser.Password);

    if (user == null)
    {
        return Results.NotFound(new { message = "Usuário ou senha inválidos" });
    }

    var token = TokenService.GenerateToken(user);
    return Results.Ok(
        new
        {
            userName = user.Username,
            token = token
        });
});

app.MapGet("/is_manager", [Authorize(Roles = "manager")] () =>
{
    return Results.Ok("True");
});

app.MapGet("/is_admin", [Authorize(Roles = "admin")] () => 
{
    return Results.Ok("True");
});

app.MapGet("/you_have_access", [Authorize(Roles = "admin,manager")] () => 
{
    return Results.Ok("True");
});

app.Run();
