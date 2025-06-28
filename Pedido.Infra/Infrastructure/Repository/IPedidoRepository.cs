﻿using Core.Dto;
using Core.Entities;

namespace Infrastructure.Repository;
public interface IPedidoRepository
{
    Task<Pedido?> SelectAsync(Guid id);
    Task<Pedido> InsertAsync(Pedido entity, string clienteCpf);
    Task<Pedido> UpdateAsync(PedidoDto dto);
}
