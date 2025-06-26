using Core.Enums;

namespace Core.Entities;
public class Pedido : BaseEntity
{
    public PedidoStatusEnum Status { get; set; }
    public PedidoEntregaEnum Entrega { get; set; }
    public Cliente Cliente { get; set; } = new();
}
