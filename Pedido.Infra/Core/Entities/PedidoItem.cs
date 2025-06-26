namespace Core.Entities;
public class PedidoItem : BaseEntity
{
    public Item Item { get; set; } = new();
    public Pedido Pedido { get; set; } = new();
    public int Quantidade { get; set; }
}
