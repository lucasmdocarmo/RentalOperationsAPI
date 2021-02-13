using LocalizaLab.Operacoes.Application.Command;
using LocalizaLab.Operacoes.Application.Command.Carros;
using LocalizaLab.Operacoes.Application.Command.Modelos;
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
    public class ModeloController : ControllerBase
    {
        private readonly ICommandHandler<CadastrarModeloCommand> _command;

        public ModeloController(ICommandHandler<CadastrarModeloCommand> command)
        {
            _command = command;
        }

        [HttpPost]
        [Route("Cadastrar")]
        [Authorize(Roles = "Cliente")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status412PreconditionFailed)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CriarModeloAsync(CadastrarModeloCommand command)
        {
            var result = await _command.Handle(command).ConfigureAwait(true) as CommandResult;

            if (result.Success)
            {
                return Ok();
            }
            else
            {
                return UnprocessableEntity();
            }
        }
        [HttpDelete]
        [Route("Deletar/{id}")]
        [Authorize(Roles = "Cliente")]
        public async Task<IActionResult> DeletarModeloAsync([FromRoute] Guid id)
        {
            return Ok();
        }
    }
}
