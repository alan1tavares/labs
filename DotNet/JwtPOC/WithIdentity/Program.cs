
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WithDatabase;
using Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql("Host=localhost;Database=poc_identtity;Username=postgres;Password=1234"));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

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

app.MapPost("/user", (User user) =>
{

});

app.Run();
