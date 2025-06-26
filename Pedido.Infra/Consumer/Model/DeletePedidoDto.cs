using MassTransit;

namespace Consumer.Model;

[MessageUrn("delete-pedido-dto")]
[EntityName("delete-pedido-dto")]
public class DeletePedidoDto
{
    public Guid Id { get; set; }
}
