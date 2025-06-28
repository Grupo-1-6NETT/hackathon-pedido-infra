using Core.Dto;
using Core.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;
public class PedidoRepository(AppDbContext context) : IPedidoRepository
{
    private DbSet<Pedido> EntitySet => context.Pedidos;
    private IQueryable<Pedido> Queryable => context.Pedidos.AsNoTracking();

    public async Task<Pedido?> SelectAsync(Guid id) => await Queryable.FirstOrDefaultAsync(x => x.Id == id);

    public async Task<Pedido> InsertAsync(Pedido entity, string clienteCpf)
    {
        var cliente = context.Clientes
            .FirstOrDefault(x => x.Cpf == clienteCpf) 
            ?? throw new KeyNotFoundException(nameof(clienteCpf));

        entity.Cliente = cliente;
        entity.DataCriacao = DateTime.Now.ToUniversalTime();
        entity.DataAtualizacao = DateTime.Now.ToUniversalTime();

        EntitySet.Add(entity);
        
        await context.SaveChangesAsync();
        
        return entity;
    }    

    public async Task<Pedido> UpdateAsync(PedidoDto dto)
    {
        var current = Queryable
            .Include(x => x.Cliente)
            .FirstOrDefault(x => x.Id == dto.Id)
            ?? throw new KeyNotFoundException(nameof(dto.Id));

        context.Entry(current.Cliente).State = EntityState.Unchanged;

        if(dto.Entrega != null)
            current.Entrega = dto.Entrega.Value;
        if(dto.Status != null)
            current.Status = dto.Status.Value;

        current.DataAtualizacao = DateTime.Now.ToUniversalTime();
        
        EntitySet.Update(current);
        await context.SaveChangesAsync();
        
        return current;
    }
}
