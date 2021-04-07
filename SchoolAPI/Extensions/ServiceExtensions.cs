using System;
using System.Collections.Generic;
using System.Text;
using Contracts;
using LoggerService;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace SchoolAPI.Extensions
{
    public static class ServiceExtensions
    {
        

        public static void ConfigureLoggerService(this IServiceCollection services) =>
            services.AddScoped<ILoggingManager, LoggerManager>();

    }
}
