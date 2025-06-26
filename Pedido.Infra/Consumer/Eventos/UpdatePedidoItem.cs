using Consumer.Model;
using Infrastructure.Repository;
using MassTransit;

namespace Consumer.Eventos;
public class UpdatePedidoItem(IPedidoItemRepository pedidoRepository) : IConsumer<UpdatePedidoItemDto>
{
    public async Task Consume(ConsumeContext<UpdatePedidoItemDto> context)
    {
        var dto = context.Message;

        var entity = await pedidoRepository.SelectAsync(dto.Id) ?? new();
        entity.Quantidade = dto.Quantidade;

        await pedidoRepository.UpdateAsync(entity);
    }
}
