using Core.Dto;
using Core.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;
public class PedidoItemRepository(AppDbContext context) : IPedidoItemRepository
{
    private DbSet<PedidoItem> EntitySet => context.PedidoItems;
    private IQueryable<PedidoItem> Queryable => context.PedidoItems.AsNoTracking();

    public async Task<PedidoItem?> SelectAsync(Guid id) => await Queryable.FirstOrDefaultAsync(x => x.Id == id);

    public async Task DeleteAsync(Guid id)
    {
        var current = Queryable
            .FirstOrDefault(x => x.Id == id)
            ?? throw new KeyNotFoundException(nameof(id));

        context.Entry(current.Item).State = EntityState.Unchanged;
        context.Entry(current.Pedido).State = EntityState.Unchanged;

        EntitySet.Remove(current);

        await context.SaveChangesAsync();
    }  

    public async Task<PedidoItem> UpdateAsync(PedidoItemDto dto)
    {
        var current = Queryable
            .FirstOrDefault(x => x.Id == dto.Id)
            ?? throw new KeyNotFoundException(nameof(dto.Id));

        context.Entry(current.Item).State = EntityState.Unchanged;
        context.Entry(current.Pedido).State = EntityState.Unchanged;

        current.DataAtualizacao = DateTime.Now.ToUniversalTime();
        current.Quantidade = dto.Quantidade;

        EntitySet.Update(current);
        await context.SaveChangesAsync();
        return current;
    }

    public async Task<bool> InsertManyAsync(PedidoItem[] entities, Guid pedidoId)
    {
        if (entities == null || entities.Length == 0)
            return false;

        var pedido = context.Pedidos
            .FirstOrDefault(x => x.Id == pedidoId) 
            ?? throw new KeyNotFoundException(nameof(pedidoId));

        foreach (var entity in entities)
        {
            var item = context.Items
                .FirstOrDefault(x => x.Id == entity.Item.Id) 
                ?? throw new KeyNotFoundException(nameof(entity.Item));

            entity.DataCriacao = DateTime.Now.ToUniversalTime();
            entity.DataAtualizacao = DateTime.Now.ToUniversalTime();
            entity.Pedido = pedido;
            entity.Item = item;
        }
        
        EntitySet.AddRange(entities);

        return await context.SaveChangesAsync() > 0;        
    }
}
