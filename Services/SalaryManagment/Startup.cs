using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventBus.Messages.Common;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using SalaryManagment.Data;
using SalaryManagment.EventBusConsumer;
using SalaryManagment.Helpers;
using SalaryManagment.Interface;

namespace SalaryManagment
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
            services.AddDbContext<AppDbContext>(x => x.UseSqlite(_config.GetConnectionString("DefaultConnection")));
            services.AddScoped<IGenericRepository,GenericRepository>();
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddMassTransit(config =>
            {
                config.AddConsumer<GenerateSalaryEventConsumer>();
                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host("amqp://admin:admin@localhost:5672");
                    cfg.ReceiveEndpoint(EventBusConstants.generateSalaryQueue, c=> {
                        c.ConfigureConsumer<GenerateSalaryEventConsumer>(ctx);
                    });
                    cfg.ConfigureEndpoints(ctx);

                });
                //config.AddRequestClient<UserGetAttendanceEventRequest>();
                
            });
            services.AddMassTransitHostedService();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SalaryManagment", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SalaryManagment v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
