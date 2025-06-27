using MassTransit;

namespace Consumer.Model;

[MessageUrn("add-pedido-dto")]
[EntityName("add-pedido-dto")]
public class AddPedidoDto
{
    public Guid TransportId { get; set; }
    public string Entrega { get; set; }
    public string ClienteCpf { get; set; }
    public PedidoItemDto[] Items { get; set; }
}

[MessageUrn("pedido-item-dto")]
[EntityName("pedido-item-dto")]
public class PedidoItemDto
{
    public Guid ItemId { get; set; }
    public int Quantidade { get; set; }
}
