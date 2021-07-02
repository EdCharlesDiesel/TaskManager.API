using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using TaskManager.API.Context;
using TaskManager.API.Repositories;
using TaskManager.API.Services;

namespace TaskManager.API
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
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOriginsHeadersAndMethods",
                    builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });
            services.AddTransient<IProjectService, ProjectRepository>();

            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc("TaskManagerAPISpecification", new OpenApiInfo()
                {
                    Title = "TaskManager API V1.0",
                    Version = "1.0",
                    Description = "API for Task Manager created with ASP.NET Core 5.0",
                    Contact = new OpenApiContact()
                    {
                        Email = "Mokhetkc@hotmail.com",
                        Name = "Khotso Mokhethi",
                        Url = new Uri("https://wwww.github.com/EdCharlesDiesel")
                    }
                });


                setupAction.OperationFilter<SecurityRequirementsOperationFilter>();


                var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

                var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);

                // setupAction.IncludeXmlComments(xmlCommentsFullPath);
            });


            var connectionString = Configuration["ConnectionStrings:TaskManagerDbContextDBConnectionString"];
            services.AddDbContext<TaskManagerDbContext>(o => o.UseSqlServer(connectionString));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseSwagger();

            app.UseSwaggerUI(setupAction =>
            {
                setupAction.SwaggerEndpoint("/swagger/TaskManagerAPISpecification/swagger.json", "TaskManager API V1.0");

                setupAction.RoutePrefix = "";

                setupAction.DefaultModelExpandDepth(2);
                setupAction.DefaultModelRendering(Swashbuckle.AspNetCore.SwaggerUI.ModelRendering.Model);
                setupAction.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);

                setupAction.EnableDeepLinking();
                setupAction.DisplayOperationId();
            });

            // Enable CORS
            app.UseCors("AllowAllOriginsHeadersAndMethods");

            app.UseAuthorization();

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
