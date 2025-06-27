using Core.Entities;

namespace Infrastructure.Repository;
public interface IPedidoItemRepository
{
    Task<PedidoItem?> SelectAsync(Guid id);
    Task<PedidoItem> InsertAsync(PedidoItem entity);
    Task<bool> InsertManyAsync(PedidoItem[] entities, Guid pedidoId);
    Task<PedidoItem> UpdateAsync(PedidoItem entity);
    Task<bool> DeleteAsync(Guid id);
}
