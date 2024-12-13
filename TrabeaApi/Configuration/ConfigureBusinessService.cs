using TrabeaApi.Services;
using TrabeaBusiness.Interfaces;
using TrabeaBusiness.Repositories;

namespace TrabeaApi.Configuration;

public static class ConfigureBusinessService
{
    public static IServiceCollection AddBusinessService(this IServiceCollection services)
    {
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IPartTimeEmployeeRepository, PartTimeEmployeeRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IWorkScheduleRepository, WorkScheduleRepository>();
        services.AddScoped<IWorkShiftRepository, WorkShiftRepository>();

        services.AddScoped<AuthService>();
        services.AddScoped<EmployeeService>();
        services.AddScoped<PartTimeEmployeeService>();
        services.AddScoped<RoleService>();
        services.AddScoped<UserService>();
        services.AddScoped<WorkScheduleService>();
        return services;
    }
}
