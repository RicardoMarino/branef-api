using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Branef.Data.Context;
using Branef.Negocio.Repositories;
using Branef.Negocio.Models;
using Microsoft.EntityFrameworkCore;

namespace Branef.Data.Repository.EntityFramework
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        protected readonly MeuDbContext _context;
        protected readonly DbSet<TEntity> DbSet;

        protected Repository(MeuDbContext context)
        {
            _context = context;
            DbSet = _context.Set<TEntity>();
        }

        public virtual async Task<TEntity> ObterPorId(Guid id) => await DbSet.FindAsync(id);

        public virtual async Task<IEnumerable<TEntity>> ObterTodos() => await DbSet.ToListAsync();


        public virtual async Task Adicionar(TEntity entity)
        {
            DbSet.Add(entity);
            await SaveChanges();
        }

        public virtual async Task Atualizar(TEntity entity)
        {
            DbSet.Update(entity);
            await SaveChanges();
        }

        public virtual async Task Remover(Guid id)
        {
            DbSet.Remove(new TEntity { Id = id });
            await SaveChanges();
        }

        public virtual async Task<bool> Existe(Guid id) => await DbSet.Where(x => x.Id == id).AnyAsync();
        public async Task<int> SaveChanges() => await _context.SaveChangesAsync();

        public void Dispose() => _context?.Dispose();
    }
}