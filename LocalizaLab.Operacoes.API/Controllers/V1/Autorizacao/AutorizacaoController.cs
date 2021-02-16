using LocalizaLab.Operacoes.Application.Command;
using LocalizaLab.Operacoes.Application.Command.Autenticacao;
using LocalizaLab.Operacoes.Application.Command.Carros;
using LocalizaLab.Operacoes.Application.Queries.Base;
using LocalizaLab.Operacoes.Application.Queries.Models;
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
    public class AutorizacaoController : ControllerBase
    {
        private readonly IQueryHandler<UsuarioQuery> _commandQuery;
        private readonly ICommandHandler<AutenticarUsuarioCommand> _commandAutenticar;

        public AutorizacaoController(IQueryHandler<UsuarioQuery> commandQuery, ICommandHandler<AutenticarUsuarioCommand> commandAutenticar)
        {
            _commandQuery = commandQuery;
            _commandAutenticar = commandAutenticar;
        }

        [HttpPost]
        [Route("Login")]
        [Authorize(Roles = "Cliente, Operador")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        public async Task<IActionResult> Login([FromBody] UsuarioQuery user)
        {
            var resultado = await _commandQuery.Handle(user).ConfigureAwait(true) as QueryResult;

            return Accepted(resultado);
        }
        [HttpPost]
        [Route("Autenticar")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        public async Task<IActionResult> Authenciar([FromBody] AutenticarUsuarioCommand user)
        {
            var resultado = await _commandAutenticar.Handle(user).ConfigureAwait(true) as CommandResult;

            return Accepted(resultado);
        }
    }
}

