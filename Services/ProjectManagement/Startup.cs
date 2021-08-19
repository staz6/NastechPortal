using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Data;
using ProjectManagement.Helper;
using ProjectManagement.Interface;
using Newtonsoft.Json;
using MassTransit;
using EventBus.Messages.Events;

namespace InventoryManagement
{
    public class Startup
    {
        private readonly IConfiguration _config;
        public Startup(IConfiguration config)
        {
            _config = config;

        }



        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers().AddNewtonsoftJson(options => {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if(environmentName=="Production")
            {
                var serverVersion = new MySqlServerVersion(new Version(8, 0, 25));
            services.AddDbContext<AppDbContext>(x =>
             x.UseMySql(_config.GetConnectionString("DefaultConnection"),serverVersion));
            }
            if(environmentName=="Development")
            {
                services.AddDbContext<AppDbContext>(x =>
             x.UseSqlite(_config.GetConnectionString("DefaultConnection")));
            }
             
            services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
            services.AddScoped<IProjectRepository,ProjectRepository>();
            // services.AddScoped<ISpecificRepository,SpecificRepository>();
            services.AddAutoMapper(typeof(MappingProfile));
            
            
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config
                            ["Token:Key"])),
                            ValidIssuer = _config["Token:Issuer"],
                            ValidateIssuer = true,
                            ValidateAudience = false
                        };
                    });




            //RabbitMQ/MassTransit
            services.AddMassTransit(config => {
                
               
                
               config.UsingRabbitMq((ctx , cfg) => {
                    cfg.Host(_config["RabbitMq:DefaultConnection"]);
                    
                    cfg.ConfigureEndpoints(ctx);
                    
                });
                config.AddRequestClient<GetUserByIdEventRequest>();
                
            });
            services.AddMassTransitHostedService();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProjectManagement", Version = "v1" });
            });
            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("*");
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment() || env.IsProduction())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "UserManagement v1"));
            }
            app.UseStatusCodePagesWithReExecute("/errors/{0}");


            app.UseRouting();

            app.UseCors("CorsPolicy");
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            using(var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                scope.ServiceProvider.GetService<AppDbContext>().MigrateDb();
            }
        }
    }
}
