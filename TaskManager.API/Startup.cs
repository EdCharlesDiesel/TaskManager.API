using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.IO;
using System.Reflection;
using TaskManager.API.Identity;
using TaskManager.API.Repositories;
using TaskManager.API.Services;
using Microsoft.IdentityModel.Tokens;
using TaskManager.Identity;
using TaskManager.Services;

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
                options.AddPolicy("TaskManagerPolicy",
                    builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200"));
            });
            services.AddTransient<IProjectService, ProjectRepository>();

            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc("TaskManagerAPISpecification", new OpenApiInfo()
                {
                    Title = "Task Manager API V1.0",
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
            services.AddDbContext<ApplicationDbContext>(o => o.UseSqlServer(connectionString));

            services.AddTransient<IRoleStore<ApplicationRole>, ApplicationRoleStore>();
            services.AddTransient<UserManager<ApplicationUser>, ApplicationUserManager>();
            services.AddTransient<SignInManager<ApplicationUser>, ApplicationSignInManager>();
            services.AddTransient<RoleManager<ApplicationRole>, ApplicationRoleManager>();
            services.AddTransient<IUserStore<ApplicationUser>, ApplicationUserStore>();
            services.AddTransient<IUsersService, UsersService>();            


           services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddUserStore<ApplicationUserStore>()
                .AddUserManager<ApplicationUserManager>()
                .AddRoleManager<ApplicationRoleManager>()
                .AddSignInManager<ApplicationSignInManager>()
                .AddRoleStore<ApplicationRoleStore>()
                .AddDefaultTokenProviders();

            services.AddScoped<ApplicationRoleStore>();
            services.AddScoped<ApplicationUserStore>();

            //Configure JWT Authentication
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = System.Text.Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                //x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                //x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddCookie()
            .AddJwtBearer(x => {
                x.RequireHttpsMetadata = false;
                x.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddAntiforgery(options => {
                options.Cookie.Name = "XSRF-Cookie-TOKEN";
                options.HeaderName = "X-XSRF-TOKEN";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
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
            app.UseCors("TaskManagerPolicy");

            app.UseAuthorization();

            app.UseAuthentication();
            IServiceScopeFactory serviceScopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();

            using (IServiceScope scope = serviceScopeFactory.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                //Create Admin Role
                if (!(await roleManager.RoleExistsAsync("Admin")))
                {
                    var role = new ApplicationRole();
                    role.Name = "Admin";
                    await roleManager.CreateAsync(role);
                }

                //Create Admin User
                if ((await userManager.FindByNameAsync("admin")) == null)
                {
                    var user = new ApplicationUser();
                    user.UserName = "admin";
                    user.Email = "admin@gmail.com";
                    var userPassword = "Admin123#";
                    var chkUser = await userManager.CreateAsync(user, userPassword);
                    if (chkUser.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, "Admin");
                    }
                }
                if (!(await userManager.IsInRoleAsync(await userManager.FindByNameAsync("admin"), "Admin")))
                {
                    await userManager.AddToRoleAsync(await userManager.FindByNameAsync("admin"), "Admin");
                }

                //Create Employee Role
                if (!(await roleManager.RoleExistsAsync("Employee")))
                {
                    var role = new ApplicationRole();
                    role.Name = "Employee";
                    await roleManager.CreateAsync(role);
                }
            }

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
