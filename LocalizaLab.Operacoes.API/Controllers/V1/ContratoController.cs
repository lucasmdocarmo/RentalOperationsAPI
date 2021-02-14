using LocalizaLab.Operacoes.Application.Command;
using LocalizaLab.Operacoes.Application.Command.Contratos;
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
    public class ContratoController : ControllerBase
    {
        private readonly ICommandHandler<CadastrarContratoCommand> _command;

        [HttpPost]
        [Route("Cadastrar")]
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
        [Route("Contrato/Download")]
        public async Task<IActionResult> DownloadContratoAsync(CadastrarContratoCommand command)
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
        [HttpPut]
        [Route("Contrato/Atualizar")]
        public async Task<IActionResult> AtualizarDadosContratoAsync(CadastrarContratoCommand command)
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
        [HttpPatch]
        [Route("Contrato/Itens")]
        public async Task<IActionResult> PatchItensContratoAsync(CadastrarContratoCommand command)
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
        [HttpPatch]
        [Route("Contrato/Reserva")]
        public async Task<IActionResult> PatchReservaAsync(CadastrarContratoCommand command)
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
        [HttpPatch]
        [Route("Contrato/Pagamento")]
        public async Task<IActionResult> PatchPagamentoAsync(CadastrarContratoCommand command)
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
