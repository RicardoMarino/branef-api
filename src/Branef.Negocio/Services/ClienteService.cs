using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Branef.Negocio.Models;
using Branef.Negocio.Models.Validator;
using Branef.Negocio.Notificacoes.Interfaces;
using Branef.Negocio.Repositories;
using Branef.Negocio.Services.Interfaces;

namespace Branef.Negocio.Services
{
    public class ClienteService : BaseService, IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository, INotificador notificador) : base(notificador)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<IEnumerable<Cliente>> ObterTodos() => await _clienteRepository.ObterTodos();

        public async Task<Cliente> ObterPorId(Guid id) => await _clienteRepository.ObterPorId(id);

        public async Task<bool> Existe(Guid id) => await _clienteRepository.Existe(id);

        public async Task<bool> Adicionar(Cliente cliente)
        {
            if (!ExecutarValidacao(new ClienteValidator(), cliente)) return false;
            await _clienteRepository.Adicionar(cliente);
            return true;
        }

        public async Task<bool> Atualizar(Cliente cliente)
        {
            if (!ExecutarValidacao(new ClienteValidator(), cliente)) return false;
            await _clienteRepository.Atualizar(cliente);
            return true;
        }

        public async Task<bool> Remover(Guid id)
        {
            await _clienteRepository.Remover(id);
            return true;
        }
    }
}