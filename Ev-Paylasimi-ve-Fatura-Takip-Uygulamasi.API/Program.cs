var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// add jwt bearer - appsettings

// rate limiter

//  add output cache

// appdbcontext - appsettings

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// use authentication

app.UseAuthorization();

app.MapControllers();

app.Run();
