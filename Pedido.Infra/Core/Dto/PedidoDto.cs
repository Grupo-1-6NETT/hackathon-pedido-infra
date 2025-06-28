using Core.Enums;

namespace Core.Dto;
public class PedidoDto
{
    public Guid Id { get; set; }
    public PedidoStatusEnum? Status { get; set; }
    public PedidoEntregaEnum? Entrega { get; set; }    
}
