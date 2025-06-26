using Consumer.Model;
using Core.Entities;
using Core.Enums;
using Infrastructure.Repository;
using MassTransit;

namespace Consumer.Eventos;
public class UpdatePedido(IPedidoRepository pedidoRepository, IClienteRepository clienteRepository) : IConsumer<UpdatePedidoDto>
{
    public async Task Consume(ConsumeContext<UpdatePedidoDto> context)
    {
        var dto = context.Message;

        var entity = new Pedido
        {
            Id = dto.Id,
            Status = (PedidoStatusEnum)Enum.Parse(typeof(PedidoStatusEnum), dto.Status),
            Entrega = (PedidoEntregaEnum)Enum.Parse(typeof(PedidoEntregaEnum), dto.Entrega)
        };

        if (!string.IsNullOrEmpty(dto.ClienteCpf))        
            entity.Cliente = await clienteRepository.SelectByCpfAsync(dto.ClienteCpf.Trim()) ?? new();                    

        await pedidoRepository.UpdateAsync(entity);
    }
}
