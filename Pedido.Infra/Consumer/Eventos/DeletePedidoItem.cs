using Consumer.Model;
using Infrastructure.Repository;
using MassTransit;

namespace Consumer.Eventos;
public class DeletePedidoItem(IPedidoItemRepository pedidoRepository) : IConsumer<DeletePedidoItemDto>
{
    public Task Consume(ConsumeContext<DeletePedidoItemDto> context)
    {
        pedidoRepository.DeleteAsync(context.Message.Id);

        return Task.CompletedTask;
    }
}
