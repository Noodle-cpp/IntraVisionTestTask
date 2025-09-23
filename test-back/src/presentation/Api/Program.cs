using DependencyResolver;
using Scalar.AspNetCore;

const string _corsPolicy = "EnableAll";
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: _corsPolicy,
        corsPolicyBuilder =>
        {
            corsPolicyBuilder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin();
        });
});

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.RegisterDependencies(builder.Configuration);

var app = builder.Build();

app.UseCors(_corsPolicy);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapScalarApiReference(options =>
    {
        options.WithTitle("API")
            .WithTheme(ScalarTheme.DeepSpace)
            .WithDarkMode()
            .WithLayout(ScalarLayout.Classic);
    });
        
    
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();