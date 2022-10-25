using Challenge.Application.Services;
using Challenge.Application.Services.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Challenge.Application {
    public static class DependencyInjection {

        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration) {  
            services.AddScoped<IKafkaService, KafkaService>();
            return services;
        }

    }
}
