using LocalizaLab.Operacoes.Application.Command;
using LocalizaLab.Operacoes.Application.Command.Base;
using LocalizaLab.Operacoes.Application.Command.Contratos;
using LocalizaLab.Operacoes.Domain.Command.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LocalizaLab.Operacoes.API.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{api-version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [ApiController]
    [Authorize]
    public class ContratoController : ControllerBase
    {
        private readonly ICommandHandler<CadastrarContratoCommand> _command;
        private readonly ICommandHandler<DevolverContratoCommand> _commandDevoler;
        private readonly ICommandHandler<PagarContratoCommand> _commandPagar;
        private readonly ICommandHandler<BaixarContratoCommand> _commandBaixar;

        public ContratoController(ICommandHandler<CadastrarContratoCommand> command, ICommandHandler<DevolverContratoCommand> commandDevoler, ICommandHandler<PagarContratoCommand> commandPagar, ICommandHandler<BaixarContratoCommand> commandBaixar)
        {
            _command = command;
            _commandDevoler = commandDevoler;
            _commandPagar = commandPagar;
            _commandBaixar = commandBaixar;
        }

        [HttpPost]
        [Route("Cadastrar")]
        [Authorize(Roles = "Cliente, Operador")]
        public async Task<IActionResult> CadastrarContratoAsync(CadastrarContratoCommand command)
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
        [HttpGet]
        [Route("{id}/Baixar")]
        [Authorize(Roles = "Cliente, Operador")]
        [Produces("application/pdf")]
        public async Task<IActionResult> DownloadContratoAsync([FromRoute][Required][Bind] Guid id)
        {
            var result = await _commandBaixar.Handle(new BaixarContratoCommand() { Id = id }).ConfigureAwait(true) as CommandFileResult;

            return File(result.ResultFile, "application/pdf");
        }

        [HttpPost]
        [Route("{id}/Devolver")]
        [Authorize(Roles = "Cliente, Operador")]

        public async Task<IActionResult> DevolverReservaAsync(DevolverContratoCommand command)
        {
            var result = await _commandDevoler.Handle(command).ConfigureAwait(true) as CommandResult;

            if (result.Success)
            {
                return Ok();
            }
            else
            {
                return UnprocessableEntity();
            }
        }
        [HttpPost]
        [Route("{id}/Pagar")]
        [Authorize(Roles = "Cliente, Operador")]
        public async Task<IActionResult> PatchPagamentoAsync(PagarContratoCommand command)
        {
            var result = await _commandPagar.Handle(command).ConfigureAwait(true) as CommandResult;

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
