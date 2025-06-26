using MassTransit;

namespace Consumer.Model;

[MessageUrn("delete-pedidoitem-dto")]
[EntityName("delete-pedidoitem-dto")]
public class DeletePedidoItemDto
{
    public Guid Id { get; set; }
}
