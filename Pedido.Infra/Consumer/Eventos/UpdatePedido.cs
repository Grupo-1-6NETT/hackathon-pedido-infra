using Consumer.Model;
using Core.Enums;
using Infrastructure.Repository;
using MassTransit;

namespace Consumer.Eventos;
public class UpdatePedido(IPedidoRepository pedidoRepository) : IConsumer<UpdatePedidoDto>
{
    public async Task Consume(ConsumeContext<UpdatePedidoDto> context)
    {
        var dto = context.Message;

        var entity = await pedidoRepository.SelectAsync(dto.Id) ?? new();

        if(!string.IsNullOrEmpty(dto.Status))
            entity.Status = (PedidoStatusEnum)Enum.Parse(typeof(PedidoStatusEnum), dto.Status);

        if(!string.IsNullOrEmpty(dto.Entrega))
            entity.Entrega = (PedidoEntregaEnum)Enum.Parse(typeof(PedidoEntregaEnum), dto.Entrega);

        await pedidoRepository.UpdateAsync(entity);
    }
}
