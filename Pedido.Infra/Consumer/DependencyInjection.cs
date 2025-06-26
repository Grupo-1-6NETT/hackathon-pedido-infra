using Consumer.Eventos;
using Core;
using Infrastructure;
using MassTransit;

namespace Consumer;
public static class DependencyInjection
{
    public static IServiceCollection AddConsumerDI(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCoreDi(configuration)
            .AddInfrastructureDi();

        return services;
    }

    public static IServiceCollection ConfigureRabbitMQ(this IServiceCollection services, IConfiguration configuration)
    {
        var servidor = configuration.GetSection("RabbitMQ")["Hostname"] ?? string.Empty;
        var usuario = configuration.GetSection("RabbitMQ")["Username"] ?? string.Empty;
        var senha = configuration.GetSection("RabbitMQ")["Password"] ?? string.Empty;

        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(servidor, "/", h =>
                {
                    h.Username(usuario);
                    h.Password(senha);
                });

                cfg.ReceiveEndpoint(nameof(AddPedido).ToLower(), e => e.ConfigureConsumer<AddPedido>(context));
                cfg.ReceiveEndpoint(nameof(DeletePedido).ToLower(), e => e.ConfigureConsumer<DeletePedido>(context));
                cfg.ReceiveEndpoint(nameof(UpdatePedido).ToLower(), e => e.ConfigureConsumer<UpdatePedido>(context));

                cfg.ConfigureEndpoints(context);
            });

            x.AddConsumer<AddPedido>();
            x.AddConsumer<DeletePedido>();
            x.AddConsumer<UpdatePedido>();
        });

        return services;
    }
}
