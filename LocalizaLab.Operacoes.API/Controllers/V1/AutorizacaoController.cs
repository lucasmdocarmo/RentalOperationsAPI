using LocalizaLab.Operacoes.Application.Command.Autenticacao;
using LocalizaLab.Operacoes.Application.Command.Carros;
using LocalizaLab.Operacoes.Application.Queries.Models;
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
    [Route("api/v{api-version:apiVersion}/[controller]")]
    [ApiController]
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
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UsuarioQuery user)
        {
            var resultado = await _commandQuery.Handle(user);

            return Ok(resultado);
        }
        [HttpPost]
        [Route("Autenticar")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenciar([FromBody] AutenticarUsuarioCommand user)
        {
            var resultado = await _commandAutenticar.Handle(user);

            return Ok(resultado);
        }
    }
}

