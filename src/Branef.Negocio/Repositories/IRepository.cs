using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Branef.Negocio.Models;

namespace Branef.Negocio.Repositories
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        Task Adicionar(TEntity entity);
        Task<TEntity> ObterPorId(Guid id);
        Task<IEnumerable<TEntity>> ObterTodos();
        Task Atualizar(TEntity entity);
        Task Remover(Guid id);
        Task<bool> Existe(Guid id);
    }
}