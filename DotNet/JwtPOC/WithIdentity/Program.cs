
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WithDatabase;
using Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql("Host=localhost;Database=poc_identtity;Username=postgres;Password=1234"));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

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


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

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

app.MapPost("role-to-user", async (IServiceProvider serviceProvider, string role, string email) =>
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

app.Run();
