using LocalizaLab.Operacoes.Application.Command;
using LocalizaLab.Operacoes.Application.Command.Usuario;
using LocalizaLab.Operacoes.Domain.Command.Handlers;
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

        public OperadorController(ICommandHandler<CadastrarOperadorCommand> commandOperador)
        {
            _commandOperador = commandOperador;
        }

        [HttpPost]
        [Route("Cadastrar")]
        [Authorize(Roles = "Cliente, Operador")]
        public async Task<IActionResult> CriarOperadorAsync(CadastrarOperadorCommand command)
        {
            var result = await _commandOperador.Handle(command).ConfigureAwait(true) as CommandResult;

            if (result.Success)
            {
                return Ok();
            }
            else
            {
                return UnprocessableEntity();
            }
        }
    }
}
