using Consumer.Model;
using Core.Entities;
using Core.Enums;
using Infrastructure.Repository;
using MassTransit;

namespace Consumer.Eventos;
public class AddPedido(IPedidoRepository pedidoRepository, IPedidoItemRepository pedidoItemRepository) : IConsumer<AddPedidoDto>
{
    public async Task Consume(ConsumeContext<AddPedidoDto> context)
    {
        var dto = context.Message;

        var entity = new Pedido
        {            
            Entrega = (PedidoEntregaEnum)Enum.Parse(typeof(PedidoEntregaEnum), dto.Entrega),
            Status = PedidoStatusEnum.Pendente,
        };                    

        var pedido = await pedidoRepository.InsertAsync(entity, dto.ClienteCpf);

        if (dto.Items != null && dto.Items.Length > 0)
        {
            var pedidoItems = dto.Items.Select(i => new PedidoItem
            {
                Item = new Item { Id = i.ItemId},                
                Quantidade = i.Quantidade,
            }).ToArray();

            await pedidoItemRepository.InsertManyAsync(pedidoItems, pedido.Id);
        }
    }
}
