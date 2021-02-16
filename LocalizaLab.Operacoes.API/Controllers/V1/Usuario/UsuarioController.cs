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
    public class UsuarioController : ControllerBase
    {
        private readonly ICommandHandler<CadastrarUsuarioCommand> _commandUsuario;
        private readonly IQueryHandler<ListarUsuarios> _queryUsers;
        public UsuarioController(ICommandHandler<CadastrarUsuarioCommand> commandUsuario, IQueryHandler<ListarUsuarios> queryUsers)
        {
            _commandUsuario = commandUsuario;
            _queryUsers = queryUsers;
        }

        [HttpPost]
        [Route("Cadastrar")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CriarUsuarioAsync([FromBody] CadastrarUsuarioCommand command)
        {
            var result = await _commandUsuario.Handle(command).ConfigureAwait(true) as CommandResult;

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
            var resultado = await _queryUsers.Handle(new ListarUsuarios()).ConfigureAwait(true) as QueryResult;
            if (resultado is null)
            {
                return NotFound(resultado.Messages);
            }
            return Ok(resultado);
        }
    }
}
