using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AttendanceManagment.Data;
using AttendanceManagment.EventBusConsumer;
using AttendanceManagment.Helpers;
using AttendanceManagment.Interface;
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

namespace AttendanceManagment
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
            
            //General
            services.AddDbContext<AppDbContext>(x =>
             x.UseSqlite(_config.GetConnectionString("DefaultConnection")));
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

            //MassTransit
            services.AddMassTransit(config =>
            {
                
                config.AddConsumer<SetAttendanceConsumer>();
                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host("amqp://admin:admin@localhost:5672");
                    
                    cfg.ReceiveEndpoint(EventBusConstants.SetAttendanceRecordQueue, c=> {
                        c.ConfigureConsumer<SetAttendanceConsumer>(ctx);
                    });
                    // cfg.ReceiveEndpoint(EventBusConstants.UserGetAttendanceEvent,c =>{
                    //     c.Consumer<UserGetAttendanceConsumer>(ctx);
                    // });
                    cfg.ConfigureEndpoints(ctx);

                });
                config.AddRequestClient<GetUserEventRequest>();
                
            });
            services.AddMassTransitHostedService();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AttendanceManagment", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AttendanceManagment v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
