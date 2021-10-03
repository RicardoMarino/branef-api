using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Branef.API.Configurations
{
    public static class SwaggerConfig
    {
        public static IServiceCollection AdicionaOpenApiConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "Branef API", 
                    Version = "v1",
                    Description = "API para validação de conhecimento da empresa Branef.",
                    Contact = new OpenApiContact() { Name = "Ricardo Marino da Silva Oliveira", Email = "ricardomarino@live.com" },                    
                });
            });
            return services;
        }

        public static IApplicationBuilder UsaOpenApiConfiguration(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Branef.API v1"));
            return app;
        }
    }
}