using AutoMapper;
using Consumer.API.Consumers;
using Consumer.API.DataAccess;
using Consumer.API.DataAccess.Users;
using Consumer.API.Mapper;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Consumer.API
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Consumer.API", Version = "v1" });
            });

            services.AddMassTransit(config =>
            {
                config.AddConsumer<UserConsumer>();

                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host("amqp://guest:guest@rabbitmq:5672");

                    cfg.ReceiveEndpoint("user-queue", c =>
                    {
                        c.ConfigureConsumer<UserConsumer>(ctx);
                    });
                });
            });

            services.AddMassTransitHostedService();

            services.AddMediatR(typeof(Startup));

            services.AddDbContext<DataContext>(options => options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new DomainProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddScoped<IUserRepository, UserRepository>();

            services.AddLogging();

            var serviceProvider = services.BuildServiceProvider();
            var logger = serviceProvider.GetService<ILogger<object>>();
            services.AddSingleton(typeof(ILogger), logger);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataContext dataContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Consumer.API v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            dataContext.Database.Migrate();
        }
    }
}
