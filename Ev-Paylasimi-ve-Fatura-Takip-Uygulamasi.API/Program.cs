using Autofac;
using Autofac.Extensions.DependencyInjection;
using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.API.Filters;
using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.API.Middlewares;
using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.API.Modules;
using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Repoitory;
using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Service.Mappings;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);



// add jwt bearer - appsettings

// rate limiter

//  add output cache



builder.Services.AddControllers();

builder.Services.AddOpenApi();
builder.Services.AddScoped(typeof(NotFoundFilter<>));
builder.Services.AddAutoMapper(typeof(MapProfile));
builder.Services.AddDbContext<AppDbContext>(x =>
{
       x.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"), option =>
    {
        option.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
    });

});

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
containerBuilder.RegisterModule(new RepoServiceModule()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


app.UseCustomException();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
