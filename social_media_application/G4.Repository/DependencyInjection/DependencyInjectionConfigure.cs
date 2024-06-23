using G4.Infraestructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G4.Infraestructure.DependencyInjection {
    public static class DependencyInjectionConfigure {

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfigurationManager configuration) {

            var databaseConnectionString = configuration.GetConnectionString("DefaultConnection");

            if(databaseConnectionString == null)
                throw new Exception("ConnectionString 'DefaultConnection' not informed on appSettings.");

            services.AddDbContext<AppDataContext>(
                options => options.UseSqlServer(
                        databaseConnectionString, 
                        b => b.MigrationsAssembly("Posterr.WebApplication")
                    )
                );




            return services;
        }

    }
}
