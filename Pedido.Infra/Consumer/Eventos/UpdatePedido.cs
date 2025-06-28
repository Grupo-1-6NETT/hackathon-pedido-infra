using Consumer.Model;
using Core.Enums;
using Infrastructure.Repository;
using MassTransit;

namespace Consumer.Eventos;
public class UpdatePedido(IPedidoRepository pedidoRepository) : IConsumer<UpdatePedidoDto>
{
    public async Task Consume(ConsumeContext<UpdatePedidoDto> context)
    {
        var msg = context.Message;

        var dto = new Core.Dto.PedidoDto { Id = msg.Id };

        if (!string.IsNullOrWhiteSpace(msg.Entrega))
            dto.Entrega = (PedidoEntregaEnum)Enum.Parse(typeof(PedidoEntregaEnum), msg.Entrega);

        if(!string.IsNullOrEmpty(msg.Status))
            dto.Status = (PedidoStatusEnum)Enum.Parse(typeof(PedidoStatusEnum), msg.Status);

        await pedidoRepository.UpdateAsync(dto);
    }
}
