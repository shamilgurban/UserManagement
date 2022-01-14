using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Sender.API.Mapper;
using Sender.API.Models.Users;
using Sender.API.Validators;

namespace Sender.API
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Sender.API", Version = "v1" });
            });

            services.AddMassTransit(config =>
            {
                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host("amqp://guest:guest@rabbitmq:5672");
                });
            });
            services.AddMassTransitHostedService();

            services.AddMediatR(typeof(Startup));

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new DomainProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddControllers().AddFluentValidation(fv =>
            {
                fv.ImplicitlyValidateChildProperties = true;
            });

            services.AddTransient<IValidator<UserCreateDto>, UserCreateValidator>();

            var serviceProvider = services.BuildServiceProvider();
            var logger = serviceProvider.GetService<ILogger<object>>();
            services.AddSingleton(typeof(ILogger), logger);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sender.API v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
