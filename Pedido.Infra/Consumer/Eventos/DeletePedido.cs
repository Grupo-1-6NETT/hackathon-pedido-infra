using Consumer.Model;
using Infrastructure.Repository;
using MassTransit;

namespace Consumer.Eventos;
public class DeletePedido(IPedidoRepository pedidoRepository) : IConsumer<DeletePedidoDto>
{
    public Task Consume(ConsumeContext<DeletePedidoDto> context)
    {
        pedidoRepository.DeleteAsync(context.Message.Id);

        return Task.CompletedTask;
    }
}
