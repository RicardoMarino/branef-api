using Branef.Data.Repository.EntityFramework;
using Branef.Negocio.Notificacoes;
using Branef.Negocio.Notificacoes.Interfaces;
using Branef.Negocio.Repositories;
using Branef.Negocio.Services;
using Branef.Negocio.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace Branef.API.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AdicionaDependencyInjection(this IServiceCollection services,
            IConfiguration configuration)
        {
            var usaEntityFramework = bool.Parse(configuration.GetSection("UsaEntityFramework").Value);
            
            if (usaEntityFramework) services.AddScoped<IClienteRepository, ClienteRepository>();
            else services.AddScoped<IClienteRepository, Data.Repository.ADO.ClienteRepository>();
            
            services.AddScoped<IClienteService, ClienteService>();

            services.AddScoped<INotificador, Notificador>();
            
            return services;
        }
    }
}