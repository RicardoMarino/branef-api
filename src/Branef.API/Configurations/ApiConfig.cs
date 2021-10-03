using System.Text.Json.Serialization;
using Branef.Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Branef.API.Configurations
{
    public static class ApiConfig
    {
        public static IServiceCollection AdicionaApiConfiguration(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddControllers();
            services.AddControllers().AddJsonOptions(options =>
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
            
            services.AdicionaOpenApiConfiguration();
            services.AdicionaDependencyInjection(configuration);
            
            services.AddCors(options =>
            {
                options.AddPolicy("Development",
                    builder =>
                        builder
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                );

                options.AddPolicy("Production",
                    builder =>
                        builder
                            .WithMethods("GET", "POST", "PUT", "DELETE")
                            .WithOrigins("http://seudominio.com", "http://seudominio2.com")
                            .SetIsOriginAllowedToAllowWildcardSubdomains()
                            .AllowAnyHeader()
                );
            });
            return services;
        }

        public static IApplicationBuilder UsaApiConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpsRedirection();
            app.UsaOpenApiConfiguration();
            if (env.IsDevelopment())
            {
                app.UseCors("Development");
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseCors("Production");
                app.UseHsts();
            }
            
            
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            return app;
        }
    }
}