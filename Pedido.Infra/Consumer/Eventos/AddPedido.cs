using Consumer.Model;
using Core.Entities;
using Core.Enums;
using Infrastructure.Repository;
using MassTransit;

namespace Consumer.Eventos;
public class AddPedido(IPedidoRepository pedidoRepository, IClienteRepository clienteRepository) : IConsumer<AddPedidoDto>
{
    public async Task Consume(ConsumeContext<AddPedidoDto> context)
    {
        var dto = context.Message;

        var entity = new Pedido
        {
            Status = (PedidoStatusEnum)Enum.Parse(typeof(PedidoStatusEnum), dto.Status),
            Entrega = (PedidoEntregaEnum)Enum.Parse(typeof(PedidoEntregaEnum), dto.Entrega)
        };

        if (!string.IsNullOrEmpty(dto.ClienteCpf))        
            entity.Cliente = await clienteRepository.SelectByCpfAsync(dto.ClienteCpf.Trim()) ?? new();                    

        await pedidoRepository.InsertAsync(entity);
    }
}
