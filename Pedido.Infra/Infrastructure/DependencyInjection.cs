using Core.Options;
using Infrastructure.Data;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureDi(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>((provider, options) =>
        {
            var connString = provider.GetRequiredService<IOptionsSnapshot<ConnectionStringOptions>>().Value.DefaultConnection;
            options.UseNpgsql(connString);
        });

        services.AddScoped<IPedidoRepository, PedidoRepository>();
        services.AddScoped<IPedidoItemRepository, PedidoItemRepository>();
        services.AddScoped<IClienteRepository, ClienteRepository>();

        return services;
    }
}
