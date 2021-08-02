using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventBus.Messages.Common;
using EventBus.Messages.Events;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SalaryManagement.Data;
using SalaryManagement.EventBusConsumer;
using SalaryManagement.Helpers;
using SalaryManagement.Interface;

namespace SalaryManagement
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

            services.AddControllers();
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 25));
            // services.AddDbContext<AppDbContext>(x => 
            //     x.UseMySql(_config.GetConnectionString("MysqlConnection"),serverVersion));

            services.AddDbContext<AppDbContext>(x =>
             x.UseSqlite(_config.GetConnectionString("DefaultConnection")));
            services.AddAutoMapper(typeof(MappingProfile));

            services.AddScoped<IGenericRepository,GenericRepository>();
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

            services.AddMassTransit(config =>
            {
                config.AddConsumer<GenerateSalaryEventConsumer>();
                config.AddConsumer<DeductSalaryEventConsumer>();
                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(_config["RabbitMq:DefaultConnection"]);
                    cfg.ReceiveEndpoint(EventBusConstants.generateSalaryQueue, c=> {
                        c.ConfigureConsumer<GenerateSalaryEventConsumer>(ctx);
                    });
                    cfg.ReceiveEndpoint(EventBusConstants.deductSalaryQueue, c => {
                        c.ConfigureConsumer<DeductSalaryEventConsumer>(ctx);
                    });
                    cfg.ConfigureEndpoints(ctx);

                });
                config.AddRequestClient<UserGetAttendanceEventRequest>();
                config.AddRequestClient<GetUserEventRequest>();
                config.AddRequestClient<GenerateSalarySlipRequest>();
                
            });
            services.AddMassTransitHostedService();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SalaryManagement", Version = "v1" });
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
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SalaryManagement v1"));
            }

            

            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseAuthentication();
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
