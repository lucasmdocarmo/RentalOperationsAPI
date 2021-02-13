using LocalizaLab.Operacoes.Application.Command;
using LocalizaLab.Operacoes.Application.Command.Reservas;
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
    public class ReservasController : ControllerBase
    {
        private readonly ICommandHandler<SimularReservaCommand> _command;

        public ReservasController(ICommandHandler<SimularReservaCommand> command)
        {
            _command = command;
        }

        [HttpPost]
        [Route("Simular")]
        [Authorize(Roles = "Cliente")]
        public async Task<IActionResult> CriarReservaAsync(SimularReservaCommand command)
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
    }
}
