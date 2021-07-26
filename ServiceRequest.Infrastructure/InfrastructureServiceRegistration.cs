using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceRequest.Application.Contracts.Infrastructure;
using ServiceRequest.Application.Models;
using ServiceRequest.Infrastructure.Email;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceRequest.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            services.AddTransient<IEmailService, EmailService>();
            return services;

        }
    }
}
