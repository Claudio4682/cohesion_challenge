using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceRequest.Application.Contracts.Persistence;
using ServiceRequest.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ServiceRequest.Persistance
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistanceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ServiceRequestDbContext>(options => 
                options.UseSqlServer(configuration.GetConnectionString("ServiceRequestConnectionString")));

            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));

            return services;
        }
    }
}
