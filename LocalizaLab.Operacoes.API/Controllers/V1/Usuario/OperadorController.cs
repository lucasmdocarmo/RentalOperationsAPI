using LocalizaLab.Operacoes.Application.Command;
using LocalizaLab.Operacoes.Application.Command.Usuario;
using LocalizaLab.Operacoes.Application.Queries.Base;
using LocalizaLab.Operacoes.Application.Queries.Usuarios;
using LocalizaLab.Operacoes.Domain.Command.Handlers;
using LocalizaLab.Operacoes.Domain.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocalizaLab.Operacoes.API.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{api-version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [ApiController]
    [Authorize]
    public class OperadorController : ControllerBase
    {
        private readonly ICommandHandler<CadastrarOperadorCommand> _commandOperador;
        private readonly IQueryHandler<ListarOperadores> _queryOperador;

        public OperadorController(ICommandHandler<CadastrarOperadorCommand> commandOperador, IQueryHandler<ListarOperadores> queryOperador)
        {
            _commandOperador = commandOperador;
            _queryOperador = queryOperador;
        }

        [HttpPost]
        [Route("Cadastrar")]
        [Authorize(Roles = "Cliente, Operador")]
        public async Task<IActionResult> CriarOperadorAsync([FromBody] CadastrarOperadorCommand command)
        {
            var result = await _commandOperador.Handle(command).ConfigureAwait(true) as CommandResult;

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
        [Route("Listar")]
        [Authorize(Roles = "Cliente, Operador")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Listar()
        {
            var resultado = await _queryOperador.Handle(new ListarOperadores()).ConfigureAwait(true) as QueryResult;
            if (resultado is null)
            {
                return NotFound(resultado.Messages);
            }
            return Ok(resultado);
        }
    }
}
