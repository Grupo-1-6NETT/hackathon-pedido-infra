using Consumer.Model;
using Infrastructure.Repository;
using MassTransit;

namespace Consumer.Eventos;
public class UpdatePedidoItem(IPedidoItemRepository pedidoRepository) : IConsumer<UpdatePedidoItemDto>
{
    public async Task Consume(ConsumeContext<UpdatePedidoItemDto> context)
    {
        var msg = context.Message;

        var dto = new Core.Dto.PedidoItemDto
        {
            Id = msg.Id,
            Quantidade = msg.Quantidade
        };

        await pedidoRepository.UpdateAsync(dto);
    }
}
