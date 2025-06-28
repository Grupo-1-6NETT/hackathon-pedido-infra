using Consumer.Model;
using Core.Entities;
using Infrastructure.Repository;
using MassTransit;

namespace Consumer.Eventos;
public class AddPedidoItem(IPedidoItemRepository repository) : IConsumer<AddPedidoItemDto>
{
    public Task Consume(ConsumeContext<AddPedidoItemDto> context)
    {
        var dto = context.Message;

        var entities = new PedidoItem[] 
        {
            new()
            {
                Item = new Item { Id = dto.ItemId},                
                Quantidade = dto.Quantidade
            }
        };

        repository.InsertManyAsync(entities, dto.PedidoId);

        return Task.CompletedTask;
    }
}
