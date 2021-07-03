using System;
using System.Text;
using EventBus.Messages.Common;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using UserManagment.Data;
using UserManagment.Entities;
using UserManagment.EventBusConsumer;
using UserManagment.Helper;
using UserManagment.Interface;


namespace UserManagment
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
            //DbContext / General
            services.AddDbContext<AppDbContext>(x =>
             x.UseMySql(_config.GetConnectionString("DefaultConnection"),serverVersion));
            services.AddAutoMapper(typeof(MappingProfile));

            services.AddScoped<IAccountRepository,AccountRepository>();
            services.AddScoped<ISeed,Seed>();
            //Identity
            services.AddScoped<ITokenService, TokenService>();
            services.AddIdentity<AppUser, IdentityRole>()
                    .AddEntityFrameworkStores<AppDbContext>()
                    .AddDefaultTokenProviders();

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
            //Identity

            //RabbitMQ/MassTransit
            services.AddMassTransit(config => {
                config.AddConsumer<AttendanceRecordConsumer>();
                config.AddConsumer<GetUserEventConsumer>();
               config.UsingRabbitMq((ctx , cfg) => {
                    cfg.Host("amqp://NastechPortal:adsiubfadsiasre44@rabbitmq.nastechltd.co");
                    cfg.ReceiveEndpoint(EventBusConstants.GetAttendaceRecordQueue, c=> {
                        c.ConfigureConsumer<AttendanceRecordConsumer>(ctx);
                    });
                    cfg.ConfigureEndpoints(ctx);
                    
                });
                
            });
            services.AddMassTransitHostedService();
            
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "UserManagment", Version = "v1" });
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
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "UserManagment v1"));
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
