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
    public class ClienteController : ControllerBase
    {
        private readonly ICommandHandler<CadastrarClienteCommand> _commandCadastrarCliente;

        public ClienteController(ICommandHandler<CadastrarClienteCommand> commandCadastrarCliente)
        {
            _commandCadastrarCliente = commandCadastrarCliente;
        }

        [HttpPost]
        [Route("Cadastrar")]
        [AllowAnonymous]
        public async Task<IActionResult> CriarClienteAsync(CadastrarClienteCommand command)
        {
            var result = await _commandCadastrarCliente.Handle(command).ConfigureAwait(true) as CommandResult;

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
