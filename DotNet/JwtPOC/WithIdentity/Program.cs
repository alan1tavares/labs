using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Services;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WithDatabase;

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


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql("Host=localhost;Database=poc_identtity;Username=postgres;Password=1234"));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredLength = 3;
    options.Password.RequireUppercase = false;
    options.Password.RequireDigit = false;
    options.Password.RequiredUniqueChars = 0;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
});

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
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

app.MapPost("/role", async (RoleManager<IdentityRole> roleManager, string name) =>
{
    IdentityResult result = await roleManager.CreateAsync(new IdentityRole(name));
    if (result.Succeeded)
        return Results.Ok($"Role {name} criada");
    return Results.Problem(result.Errors.ToArray().ToString());

});

app.MapPost("/user", async (IServiceProvider serviceProvider, User aUser) =>
{
    var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    var user = new ApplicationUser
    {
        UserName = aUser.Username,
        Email = aUser.Email
    };

    var result = await userManager.CreateAsync(user, aUser.Password);

    if (result.Succeeded)
        return Results.Ok($"Usuário {user.Email} criado com sucesso");
    return Results.Problem(result.ToString());
});

app.MapPost("/role-to-user", async (IServiceProvider serviceProvider, string role, string email) =>
{
    var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    var user = await userManager.FindByEmailAsync(email);

    if (user != null)
    {
        var result = await userManager.AddToRoleAsync(user, role);

        if (result.Succeeded)
            return Results.Ok($"Operação realizada");
        return Results.Problem(result.ToString());
    }
    return Results.NotFound($"Usuário {email} não encontrado");
});

app.MapPost("/login", async (IServiceProvider serviceProvider, string email, string password) =>
{
    var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    var user = await userManager.FindByEmailAsync(email);

    if (user == null)
    {
        return Results.NotFound(new { message = "Usuário não encontrado" });
    }

    var isValidPassword = await userManager.CheckPasswordAsync(user, password);

    if (isValidPassword)
    {
        var roles = await userManager.GetRolesAsync(user);
        var token = TokenService.GenerateToken(new User()
        {
            Email = email,
            Username = user.UserName,
            Roles = roles
        });


        return Results.Ok(new
        {
            username = user.UserName,
            token = token
        });
    }

    return Results.Problem(":(");

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

app.MapGet("/ping", [AllowAnonymous] () =>
{
    return Results.Ok("pong");
});

app.Run();
