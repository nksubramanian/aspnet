using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication.Controllers;

namespace WebApplication
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApplication", Version = "v1" });
            });
            //services.AddScoped<ObjectOne>();
            //services.AddScoped<ObjectTwo>();


            //services.AddSingleton<ObjectOne>();
            //services.AddSingleton<ObjectTwo>();

            services.AddTransient<ObjectOne>();
            services.AddTransient<ObjectTwo>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            

            app.Use(async (context, next) =>
            {
                if (context.Request.Path.Value.Contains("middleware"))
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (var header in context.Request.Headers)
                    {
                        sb.AppendLine($"Header -> {header.Key}:{header.Value}");
                    }
                    var reader = new StreamReader(context.Request.Body, Encoding.UTF8);
                    var body = await reader.ReadToEndAsync().ConfigureAwait(false);
                    sb.AppendLine($"body -> {body}");
                    await context.Response.WriteAsync(sb.ToString());
                }
                else 
                {
                    await next();
                }
            });


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApplication v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            app.Run(async (context) =>
            {
                await context.Response.WriteAsync($"Hello there");
            });
        }
    }
}


// this is to check