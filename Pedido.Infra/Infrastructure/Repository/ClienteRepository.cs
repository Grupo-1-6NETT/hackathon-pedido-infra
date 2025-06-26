using Core.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;
public class ClienteRepository(AppDbContext context) : IClienteRepository
{
    private DbSet<Cliente> EntitySet => context.Clientes;
    private IQueryable<Cliente> Queryable => context.Clientes.AsNoTracking();

    public async Task<Cliente?> SelectAsync(Guid id) => await Queryable.FirstOrDefaultAsync(x => x.Id == id);
    public async Task<Cliente?> SelectByCpfAsync(string cpf) => await Queryable.FirstOrDefaultAsync(x => x.Cpf == cpf);

    public async Task<bool> DeleteAsync(Guid id)
    {
        var item = await EntitySet.SingleAsync(x => x.Id == id);
        EntitySet.Remove(item);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<Cliente> InsertAsync(Cliente entity)
    {
        entity.DataCriacao = DateTime.Now.ToUniversalTime();
        entity.DataAtualizacao = DateTime.Now.ToUniversalTime();
        EntitySet.Add(entity);
        await context.SaveChangesAsync();
        return entity;
    }    

    public async Task<Cliente> UpdateAsync(Cliente entity)
    {
        entity.DataAtualizacao = DateTime.Now.ToUniversalTime();
        EntitySet.Attach(entity);
        await context.SaveChangesAsync();
        return entity;
    }
}
