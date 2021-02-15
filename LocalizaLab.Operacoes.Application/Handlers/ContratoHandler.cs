using Flunt.Notifications;
using LocalizaLab.Operacoes.Application.Command;
using LocalizaLab.Operacoes.Application.Command.Base;
using LocalizaLab.Operacoes.Application.Command.Contratos;
using LocalizaLab.Operacoes.Domain.Command;
using LocalizaLab.Operacoes.Domain.Command.Handlers;
using LocalizaLab.Operacoes.Domain.Entities.Contratos;
using LocalizaLab.Operacoes.Domain.Shared.Repository;
using Microsoft.AspNetCore.Mvc;
using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace LocalizaLab.Operacoes.Application.Handlers
{

    public class ContratoHandler : Notifiable, ICommandHandler<CadastrarContratoCommand>, ICommandHandler<DevolverContratoCommand>, ICommandHandler<PagarContratoCommand>,
        ICommandHandler<BaixarContratoCommand>
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IContratoRepository _contratoRepository;
        private readonly IReservaRepository _reservaRepository;
        private readonly IDadosItemContratoRepository _itemRepository;
        private readonly IDadosContratoDevolucaoRepository _devolucaoepository;
        private readonly IDadosPagamentosRepository _pagamentoRepository;

        public ContratoHandler(IClienteRepository clienteRepository, IReservaRepository reservaRepository, IContratoRepository contratoRepository, IDadosItemContratoRepository itemRepository, IDadosContratoDevolucaoRepository devolucaoepository, IDadosPagamentosRepository pagamentoRepository)
        {
            _clienteRepository = clienteRepository;
            _reservaRepository = reservaRepository;
            _contratoRepository = contratoRepository;
            _itemRepository = itemRepository;
            _devolucaoepository = devolucaoepository;
            _pagamentoRepository = pagamentoRepository;
        }

        public async ValueTask<ICommandResult> Handle(CadastrarContratoCommand command)
        {
            if (!command.Validate())
            {
                AddNotifications(command);
                return new CommandResult(false, base.Notifications);
            }

            var entityReserva = await _reservaRepository.GetById(command.ReservaId).ConfigureAwait(true);
            var entityCliente = await _clienteRepository.GetById(command.ClienteId).ConfigureAwait(true);

            if (entityCliente is null)
            {
                AddNotification("Cliente", "Cliente nao Encontrado.");
                return new CommandResult(false, base.Notifications);
            }
            if (entityReserva is null)
            {
                AddNotification("Veiculo", "Reserva Nao Encontrado");
                return new CommandResult(false, base.Notifications);
            }

            var entityContrato = new Contrato(command.Agencia, entityCliente.Id, entityReserva.ValorReserva, command.DataAberturaContrato);
            await _contratoRepository.Add(entityContrato).ConfigureAwait(true);
            await _contratoRepository.SaveChanges().ConfigureAwait(true);

            var entityDadosItenContrato = new DadosItemContrato(command.ItensContrato.Item, command.ItensContrato.ValorItem, entityContrato.Id);
            await _itemRepository.Add(entityDadosItenContrato).ConfigureAwait(true);
            await _itemRepository.SaveChanges().ConfigureAwait(true);

            return new CommandResult(true);
        }

        public async ValueTask<ICommandResult> Handle(DevolverContratoCommand command)
        {
            if (!command.Validate())
            {
                AddNotifications(command);
                return new CommandResult(false, base.Notifications);
            }

            var entityContrato = await _contratoRepository.GetById(command.ContratoId).ConfigureAwait(true);
            if (entityContrato is null)
            {
                AddNotification("Contrato", "Contrato nao Encontrado.");
                return new CommandResult(false, base.Notifications);
            }

            var dadosDevolucao = new DadosDevolucao(command.CarroLimpo, command.TanqueCheio, command.Amassado, command.Arranhado, entityContrato.Id);
            await _devolucaoepository.Add(dadosDevolucao).ConfigureAwait(true);
            var result = await _devolucaoepository.SaveChanges().ConfigureAwait(true);

            if (!result)
            {
                AddNotification("Contrato", "Devolucao nao pode ser Registrada");
                return new CommandResult(false, base.Notifications);
            }
            return new CommandResult(true);
        }

        public async ValueTask<ICommandResult> Handle(PagarContratoCommand command)
        {
            if (!command.Validate())
            {
                AddNotifications(command);
                return new CommandResult(false, base.Notifications);
            }

            var entityContrato = await _contratoRepository.GetById(command.ContratoId).ConfigureAwait(true);
            if (entityContrato is null)
            {
                AddNotification("Contrato", "Contrato nao Encontrado.");
                return new CommandResult(false, base.Notifications);
            }

            var dadosPagamento = new DadosPagamentos(command.Valor, command.Status, command.Bandeira, command.NumeroCartao, command.DataExpiracao, command.CVV, command.DataPagamento, entityContrato.Id);
            await _pagamentoRepository.Add(dadosPagamento).ConfigureAwait(true);
            var result = await _pagamentoRepository.SaveChanges().ConfigureAwait(true);

            if (!result)
            {
                AddNotification("Contrato", "Devolucao nao pode ser Registrada");
                return new CommandResult(false, base.Notifications);
            }
            return new CommandResult(true);


        }

        public async ValueTask<ICommandResult> Handle(BaixarContratoCommand command)
        {
            if (!command.Validate())
            {
                AddNotifications(command);
                return new CommandResult(false, base.Notifications);
            }

            var entityContrato = await _contratoRepository.GetById(command.Id).ConfigureAwait(true);
            if (entityContrato is null)
            {
                AddNotification("Contrato", "Contrato nao Encontrado.");
                return new CommandResult(false, base.Notifications);
            }
            // sendo file to client
            BinaryFormatter bf = new BinaryFormatter();
            using var ms = new MemoryStream();
           
            byte[] bytes = Convert.FromBase64String(Convert.ToBase64String(ms.ToArray()));
            using (var stream = File.Create(Path.Combine("../Files", "Contrato.pdf")))
            {
                stream.Write(bytes, 0, bytes.Length);
            }

            return new CommandFileResult(bytes);
           

        }

    }
}
