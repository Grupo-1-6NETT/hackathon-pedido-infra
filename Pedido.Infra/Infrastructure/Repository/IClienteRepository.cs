using Core.Entities;

namespace Infrastructure.Repository;
public interface IClienteRepository
{
    Task<Cliente?> SelectAsync(Guid id);
    Task<Cliente> InsertAsync(Cliente entity);
    Task<Cliente> UpdateAsync(Cliente entity);
    Task<bool> DeleteAsync(Guid id);
    Task<Cliente?> SelectByCpfAsync(string cpf);
}
