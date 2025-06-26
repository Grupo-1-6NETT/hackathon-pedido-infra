using Consumer.Model;
using Core.Entities;
using Infrastructure.Repository;
using MassTransit;

namespace Consumer.Eventos;
public class AddPedidoItem(IPedidoItemRepository pedidoRepository) : IConsumer<AddPedidoItemDto>
{
    public Task Consume(ConsumeContext<AddPedidoItemDto> context)
    {
        var dto = context.Message;

        var entity = new PedidoItem
        {
            Item = new Item { Id = dto.ItemId},
            Pedido = new Pedido { Id = dto.PedidoId },
            Quantidade = dto.Quantidade
        };

        pedidoRepository.InsertAsync(entity);

        return Task.CompletedTask;
    }
}
