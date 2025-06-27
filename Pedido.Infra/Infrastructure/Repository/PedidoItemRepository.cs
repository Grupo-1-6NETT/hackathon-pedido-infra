using Core.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;
public class PedidoItemRepository(AppDbContext context) : IPedidoItemRepository
{
    private DbSet<PedidoItem> EntitySet => context.PedidoItems;
    private IQueryable<PedidoItem> Queryable => context.PedidoItems.AsNoTracking();

    public async Task<PedidoItem?> SelectAsync(Guid id) => await Queryable.FirstOrDefaultAsync(x => x.Id == id);

    public async Task<bool> DeleteAsync(Guid id)
    {
        var item = await EntitySet.SingleAsync(x => x.Id == id);
        EntitySet.Remove(item);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<PedidoItem> InsertAsync(PedidoItem entity)
    {
        entity.DataCriacao = DateTime.Now.ToUniversalTime();
        entity.DataAtualizacao = DateTime.Now.ToUniversalTime();
        EntitySet.Add(entity);
        await context.SaveChangesAsync();
        return entity;
    }    

    public async Task<PedidoItem> UpdateAsync(PedidoItem entity)
    {
        entity.DataAtualizacao = DateTime.Now.ToUniversalTime();

        EntitySet.Attach(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> InsertManyAsync(PedidoItem[] entities, Guid pedidoId)
    {
        if (entities == null || entities.Length == 0)
            return false;

        var pedido = await context.Pedidos.FirstOrDefaultAsync(x => x.Id == pedidoId) ?? throw new KeyNotFoundException(nameof(pedidoId));

        foreach (var entity in entities)
        {
            var item = await context.Items.FirstOrDefaultAsync(x => x.Id == entity.Item.Id) ?? throw new KeyNotFoundException(nameof(entity.Item));

            entity.DataCriacao = DateTime.Now.ToUniversalTime();
            entity.DataAtualizacao = DateTime.Now.ToUniversalTime();
            entity.Pedido = pedido;
            entity.Item = item;
        }
        
        await EntitySet.AddRangeAsync(entities);

        return await context.SaveChangesAsync() > 0;        
    }
}
