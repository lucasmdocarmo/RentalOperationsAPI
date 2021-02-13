using LocalizaLab.Operacoes.Application.Command;
using LocalizaLab.Operacoes.Application.Command.Carros;
using LocalizaLab.Operacoes.Application.Queries.Query;
using LocalizaLab.Operacoes.Application.Queries.Veiculos;
using LocalizaLab.Operacoes.Domain.Command;
using LocalizaLab.Operacoes.Domain.Command.Handlers;
using LocalizaLab.Operacoes.Domain.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LocalizaLab.Operacoes.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{api-version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class VeiculosController : ControllerBase
    {
        private readonly ICommandHandler<CadastrarVeiculoCommand> _command;
        private readonly ICommandHandler<DeletarVeiculoCommand> _commandDeletar;
        private readonly ICommandHandler<EditarVeiculoCommand> _commandAtualizar;
        private readonly IQueryHandler<VeiculoQuery> _commandQueryVeiculo;
        private readonly IQueryHandler<TodosVeiculosQuery> _commandTodosVeiculoQuery;

        public VeiculosController(ICommandHandler<CadastrarVeiculoCommand> command, ICommandHandler<DeletarVeiculoCommand> commandDeletar,
            ICommandHandler<EditarVeiculoCommand> commandAtualizar)
        {
            _command = command;
            _commandDeletar = commandDeletar;
            _commandAtualizar = commandAtualizar;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status412PreconditionFailed)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetALLVeiculoAsync()
        {
            var result = await _commandTodosVeiculoQuery.Handle(new TodosVeiculosQuery()).ConfigureAwait(true) as CommandResult;
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }

        }
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetVeiculoAsync([FromRoute][Required] Guid id)
        {
            var query = new VeiculoQuery() { Id = id };
            var result = await _commandQueryVeiculo.Handle(query).ConfigureAwait(true) as CommandResult;

            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }

        }

        [HttpPost]
        [Route("Cadastrar")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status412PreconditionFailed)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CriarVeiculoAsync(CadastrarVeiculoCommand command)
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
        public async Task<IActionResult> DeletarVeiculoAsync([FromRoute] Guid id)
        {
            var result = await _commandDeletar.Handle(new DeletarVeiculoCommand() {Id = id }).ConfigureAwait(true) as CommandResult;

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
        [Route("Atualizar/{id}")]
        public async Task<IActionResult> EditarVeiculoAsync([FromRoute] Guid id, EditarVeiculoCommand command)
        {
            command.Id = id;
            var result = await _commandAtualizar.Handle(command).ConfigureAwait(true) as CommandResult;

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
