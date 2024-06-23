using G4.Infraestructure.Mapping;
using Microsoft.Extensions.DependencyInjection;

namespace G4.Infraestructure.DependencyInjection {
    public static class AutoMapperConfigure {
        public static IServiceCollection AddAutoMapperConfig(this IServiceCollection services) {

            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
            return services;
        }
    }
}
