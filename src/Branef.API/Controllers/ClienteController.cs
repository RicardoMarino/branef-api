using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Branef.API.Contratos;
using Branef.Negocio.Models;
using Branef.Negocio.Notificacoes.Interfaces;
using Branef.Negocio.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Branef.API.Controllers
{
    [Route("api/v1/clientes")]
    public class ClienteController : MainController
    {
        private readonly IClienteService _clienteService;
        private readonly IMapper _mapper;
        
        public ClienteController(IClienteService clienteService,
            IMapper mapper,
            INotificador notificador) : base(notificador)
        {
            _clienteService = clienteService;
            _mapper = mapper;
        }
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Cliente>>> ObterTodos() =>
            CustomResponse(HttpStatusCode.OK, 
                _mapper.Map<IEnumerable<ClienteContrato>>(await _clienteService.ObterTodos()));
        
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ClienteContrato>> ObterPorId(Guid id)
        {
            var clienteContract = _mapper.Map<ClienteContrato>(await _clienteService.ObterPorId(id));

            if (clienteContract == null)
            {
                NotificarErro("Registro não encontrado.");
                return CustomResponse(HttpStatusCode.NotFound);
            }
            return CustomResponse(HttpStatusCode.OK, clienteContract);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Adicionar(ClienteContrato contrato)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            
            if (await _clienteService.Adicionar(_mapper.Map<Cliente>(contrato)))
                return CustomResponse(HttpStatusCode.Created);
            
            return CustomResponse(HttpStatusCode.BadRequest);
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Atualizar(Guid id, [FromBody] ClienteContrato contrato)
        {
            if (id != contrato.Id)
            {
                NotificarErro("O id informado não é o mesmo passado na query");
                return CustomResponse(HttpStatusCode.BadRequest);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            if (!await _clienteService.Existe(id))
            {
               NotificarErro("Registro não encontrado");
               return CustomResponse(HttpStatusCode.NotFound);
            }

            if (await _clienteService.Atualizar(_mapper.Map<Cliente>(contrato))) 
                return CustomResponse(HttpStatusCode.NoContent);
            
            return CustomResponse(HttpStatusCode.BadRequest);
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Remover(Guid id)
        {
            if (!await _clienteService.Existe(id))
            {
               NotificarErro("Registro não encontrado");
               return CustomResponse(HttpStatusCode.NotFound);
            }

            if (await _clienteService.Remover(id)) return CustomResponse(HttpStatusCode.NoContent);
            return CustomResponse(HttpStatusCode.BadRequest);
        }
    }
}