using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdemyRabbitMQ.Publisher;
using UdemyRabbitMQ.Publisher.udemyPublisher;
using UdemyRabbitMQ.Subscriber;
using UdemyRabbitMQ.Subscriber.udemysubs;

namespace KeyOnlineLog
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
            services.AddScoped<IPublisher, Programpublis>();
            

            services.AddSingleton(sp => new ConnectionFactory() { Uri = new Uri("amqps://mirmqflp:tu7ij7jBl6gd7Flbogzxf7C6gwsS4_dK@rat.rmq2.cloudamqp.com/mirmqflp"), DispatchConsumersAsync = true  });
            services.AddSingleton<RabbitMqClientService>();
            services.AddSingleton<RabbitMqPublisher>();
            services.AddSingleton<Subscriber>();

            //services.AddSingleton(sp=> new ConnectionFactory() {Uri = new Uri() });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "KeyOnlineLog", Version = "v1" });
            });
            services.AddHostedService<Subscriber>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "KeyOnlineLog v1"));
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
