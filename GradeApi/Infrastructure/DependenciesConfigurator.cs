namespace GradeApi.Infrastructure
{
    using GradeApi.Application.Services;
    using GradeApi.Domain.Services;
    using GradeApi.Persistence.Repositories;
    using Microsoft.Extensions.DependencyInjection;

    public static class DependenciesConfigurator
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services)
        {
            AddApplicationServices(services);
            AddDomainServices(services);
            AddRepositoryServices(services);

            services.AddAutoMapper(typeof(Program));

            return services;
        }

        private static void AddApplicationServices(IServiceCollection services)
        {
            services.AddScoped<IStudentApplicationService, StudentApplicationService>();
        }

        private static void AddDomainServices(IServiceCollection services)
        {
            services.AddScoped<IStudentDomainService, StudentDomainService>();
        }

        private static void AddRepositoryServices(IServiceCollection services)
        {
            services.AddScoped<IStudentRepository, StudentRepository>();
        }
    }
}
