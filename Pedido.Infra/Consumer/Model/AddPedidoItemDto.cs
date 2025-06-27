using MassTransit;

namespace Consumer.Model;

[MessageUrn("add-pedidoitem-dto")]
[EntityName("add-pedidoitem-dto")]
public class AddPedidoItemDto
{
    public Guid TransportId { get; set; }
    public Guid ItemId { get; set; }
    public Guid PedidoId { get; set; }
    public int Quantidade{ get; set; }
}
