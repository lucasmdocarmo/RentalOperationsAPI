using LocalizaLab.Operacoes.Application.Command;
using LocalizaLab.Operacoes.Application.Command.Contratos;
using LocalizaLab.Operacoes.Application.Command.Reservas;
using LocalizaLab.Operacoes.Application.Queries.Base;
using LocalizaLab.Operacoes.Application.Queries.Query;
using LocalizaLab.Operacoes.Application.Queries.Reservas;
using LocalizaLab.Operacoes.Domain.Command.Handlers;
using LocalizaLab.Operacoes.Domain.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LocalizaLab.Operacoes.API.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{api-version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [ApiController]
    [Authorize]
    public class ReservasController : ControllerBase
    {
        private readonly ICommandHandler<SimularReservaCommand> _command;
        private readonly ICommandHandler<CadastrarReservaCommand> _commandReserva;
        private readonly ICommandHandler<DeletarReservaCommand> _commandDeletar;
        private readonly IQueryHandler<ConsultarReservaQuery> _queryReserva;

        public ReservasController(ICommandHandler<SimularReservaCommand> command,
            ICommandHandler<CadastrarReservaCommand> commandReserva, IQueryHandler<ConsultarReservaQuery> queryReserva, ICommandHandler<DeletarReservaCommand> commandDeletar)
        {
            _command = command;
            _commandReserva = commandReserva;
            _queryReserva = queryReserva;
            _commandDeletar = commandDeletar;
        }

        [HttpPost]
        [Route("Simular")]
        [Authorize(Roles = "Cliente")]
        public async Task<IActionResult> SimularAsync([FromBody]SimularReservaCommand command)
        {
            var result = await _command.Handle(command).ConfigureAwait(true) as CommandResult;

            if (result.Success)
            {
                return Ok();
            }
            else
            {
                return UnprocessableEntity(result.Messages);
            }
        }
      
        [HttpPost]
        [Route("Cadastrar")]
        [Authorize(Roles = "Cliente")]
        public async Task<IActionResult> CriarReservaAsync([FromBody]CadastrarReservaCommand command)
        {
            var result = await _commandReserva.Handle(command).ConfigureAwait(true) as CommandResult;

            if (result.Success)
            {
                return Ok();
            }
            else
            {
                return UnprocessableEntity(result.Messages);
            }
        }
        [HttpGet]
        [Route("{id}/Consultar")]
        [Authorize(Roles = "Cliente")]
        public async Task<IActionResult> ConsultarReservaAsync([FromRoute][Required] Guid id)
        {
            var result = await _queryReserva.Handle(new ConsultarReservaQuery() { Id = id}).ConfigureAwait(true) as QueryResult;

            if (result.Success)
            {
                return Ok();
            }
            else
            {
                return UnprocessableEntity(result.Messages);
            }
        }
        [HttpDelete]
        [Route("{id}/Deletar")]
        [Authorize(Roles = "Cliente")]
        public async Task<IActionResult> DeletarReservaAsync([FromRoute][Required] Guid id)
        {
            var result = await _commandDeletar.Handle(new DeletarReservaCommand() { Id = id }).ConfigureAwait(true) as CommandResult;

            if (result.Success)
            {
                return Ok();
            }
            else
            {
                return UnprocessableEntity(result.Messages);
            }
        }
        
    }
 }
