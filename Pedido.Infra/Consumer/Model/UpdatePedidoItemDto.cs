using MassTransit;

namespace Consumer.Model;

[MessageUrn("update-pedidoitem-dto")]
[EntityName("update-pedidoitem-dto")]
public class UpdatePedidoItemDto
{
    public Guid Id { get; set; }
    public int Quantidade { get; set; }
}
