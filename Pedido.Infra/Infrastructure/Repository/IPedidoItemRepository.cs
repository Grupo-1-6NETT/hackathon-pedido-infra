using Core.Dto;
using Core.Entities;

namespace Infrastructure.Repository;
public interface IPedidoItemRepository
{
    Task<PedidoItem?> SelectAsync(Guid id);
    Task<bool> InsertManyAsync(PedidoItem[] entities, Guid pedidoId);
    Task<PedidoItem> UpdateAsync(PedidoItemDto dto);
    Task DeleteAsync(Guid id);
}
