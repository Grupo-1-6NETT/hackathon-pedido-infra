using Core.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;
public class PedidoRepository(AppDbContext context) : IPedidoRepository
{
    private DbSet<Pedido> EntitySet => context.Pedidos;
    private IQueryable<Pedido> Queryable => context.Pedidos.AsNoTracking();

    public async Task<Pedido?> SelectAsync(Guid id) => await Queryable.FirstOrDefaultAsync(x => x.Id == id);

    public async Task<bool> DeleteAsync(Guid id)
    {
        var item = await EntitySet.SingleAsync(x => x.Id == id);
        EntitySet.Remove(item);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<Pedido> InsertAsync(Pedido entity, string clienteCpf)
    {
        var cliente = context.Clientes.FirstOrDefault(x => x.Cpf == clienteCpf) ?? throw new KeyNotFoundException(nameof(clienteCpf));

        entity.Cliente = cliente;
        entity.DataCriacao = DateTime.Now.ToUniversalTime();
        entity.DataAtualizacao = DateTime.Now.ToUniversalTime();

        await EntitySet.AddAsync(entity);
        await context.SaveChangesAsync();
        
        return entity;
    }    

    public async Task<Pedido> UpdateAsync(Pedido entity)
    {
        entity.DataAtualizacao = DateTime.Now.ToUniversalTime();
        EntitySet.Attach(entity);
        await context.SaveChangesAsync();
        return entity;
    }
}
