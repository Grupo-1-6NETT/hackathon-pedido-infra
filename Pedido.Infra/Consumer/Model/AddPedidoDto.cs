using MassTransit;

namespace Consumer.Model;

[MessageUrn("add-pedido-dto")]
[EntityName("add-pedido-dto")]
public class AddPedidoDto
{
    public string Status { get; set; }
    public string Entrega { get; set; }
    public string ClienteCpf { get; set; }
}
