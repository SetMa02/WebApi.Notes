using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Notes.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAplication(
            this IServiceCollection service)
        {
            service.AddMediatR(Assembly.GetExecutingAssembly());
            return service;
        }
    }
}