using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Branef.Negocio.Models;

namespace Branef.Negocio.Services.Interfaces
{
    public interface IClienteService
    {
        Task<IEnumerable<Cliente>> ObterTodos();
        Task<Cliente> ObterPorId(Guid id);
        Task<bool> Existe(Guid id);
        Task<bool> Adicionar(Cliente cliente);
        Task<bool> Atualizar(Cliente cliente);
        Task<bool> Remover(Guid id);
    }
}