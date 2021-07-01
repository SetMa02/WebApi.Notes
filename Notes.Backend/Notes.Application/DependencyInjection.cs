using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using Notes.Application.Common.Behavior;


namespace Notes.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAplication(
            this IServiceCollection service)
        {
            service.AddMediatR(Assembly.GetExecutingAssembly());
            
            service
                .AddValidatorsFromAssemblies(new[] { Assembly.GetExecutingAssembly() });
            service.AddTransient(typeof(IPipelineBehavior<,>),
                typeof(ValidationBehavior<,>));
            
            return service;
        }
    }
}