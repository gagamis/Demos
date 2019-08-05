using AutoMapper;
using Core.Configurations;
using Core.Interfaces.Repository;
using Core.Interfaces.Repository.Products;
using Core.Interfaces.Repository.Users;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Products;
using Infrastructure.Repositories.Users;
using Infrastructure.Services.Interfaces;
using Infrastructure.Services.Mappings;
using Infrastructure.Services.Products;
using Infrastructure.Services.Tokens;
using Infrastructure.Services.Users;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using Shop.WebApi.CustomExceptionMiddleware;
using Shop.WebApi.Extensions;
using Shop.WebApi.Filters;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Linq;
using System.Net.Mime;

namespace Shop.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {        
            services.SetupAddDbContext(Configuration);

            services.SetupAddServices();
            services.SetupAddConfigurations(Configuration);

            services.AddCors();
            services.AddMvc(options =>
            {
                options.Filters.Add<ApiExceptionFilter>();               // filter for exception logging
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.SetupAddAuthorization();
            services.SetupAddSwagger();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Shop API V1");
                });

                app.UseDeveloperExceptionPage();
            }

            // custom Exception Middleware
            app.ConfigureCustomExceptionMiddleware();

            // Health check
            app.UseHealthChecks("/status", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions { ResponseWriter = async (context, report) =>
            {
                var result = JsonConvert.SerializeObject(
                    new
                    {
                        status = report.Status.ToString(),
                        errors = report.Entries.Select(e => new { key = e.Key, value = Enum.GetName(typeof(HealthStatus), e.Value.Status) })
                    });
                context.Response.ContentType = MediaTypeNames.Application.Json;
                await context.Response.WriteAsync(result);
            }
            });

            app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

            app.UseAuthentication();

            app.UseEndpointRouting();

            app.UseMvc();
        }
    }
}
