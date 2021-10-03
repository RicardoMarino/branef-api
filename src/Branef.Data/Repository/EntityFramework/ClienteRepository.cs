using Branef.Data.Context;
using Branef.Negocio.Repositories;
using Branef.Negocio.Models;

namespace Branef.Data.Repository.EntityFramework
{
    public class ClienteRepository : Repository<Cliente>, IClienteRepository
    {
        public ClienteRepository(MeuDbContext context) : base(context)
        {
        }
    }
}