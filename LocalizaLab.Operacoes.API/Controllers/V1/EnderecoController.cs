using LocalizaLab.Operacoes.Application.Command;
using LocalizaLab.Operacoes.Application.Command.Usuario;
using LocalizaLab.Operacoes.Domain.Command.Handlers;
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
    public class EnderecoController : ControllerBase
    {
        private readonly ICommandHandler<CadastrarEnderecoCommand> _commandCadastrarEndereco;

        public EnderecoController(ICommandHandler<CadastrarEnderecoCommand> commandCadastrarEndereco)
        {
            _commandCadastrarEndereco = commandCadastrarEndereco;
        }

        [HttpPost]
        [Route("Cadastrar")]
        public async Task<IActionResult> CriarEnderecoAsync(CadastrarEnderecoCommand command)
        {
            var result = await _commandCadastrarEndereco.Handle(command).ConfigureAwait(true) as CommandResult;

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
