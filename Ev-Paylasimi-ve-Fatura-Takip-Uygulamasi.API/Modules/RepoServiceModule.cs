using Autofac;
using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.Repositories;
using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.Services;
using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.UnitOfWorks;
using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Repoitory;
using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Repoitory.Repositories;
using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Repoitory.UnitOfWorks;
using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Service.Mappings;
using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Service.Services;
using System.Reflection;
using Module = Autofac.Module;

namespace Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.API.Modules
{
    public class RepoServiceModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(GenericRepository<>))
                .As(typeof(IGenericRepository<>))
                .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(Service<>))
                .As(typeof(IService<>))
                .InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWorks>()
                .As<IUnitOfWorks>();

            builder.RegisterType<TokenHandler>().As<ITokenHandler>();

            var apiAssembly = Assembly.GetExecutingAssembly();
            var repoAssembly = Assembly.GetAssembly(typeof(AppDbContext));
            var serviceAssembly = Assembly.GetAssembly(typeof(MapProfile));

            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly)
                .Where(x => x.Name.EndsWith("Repository"))
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly)
                .Where(x => x.Name.EndsWith("Service"))
                .AsImplementedInterfaces();
        }
    }
}
