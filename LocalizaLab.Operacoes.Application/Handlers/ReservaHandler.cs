using Flunt.Notifications;
using LocalizaLab.Operacoes.Application.Command;
using LocalizaLab.Operacoes.Application.Command.Contratos;
using LocalizaLab.Operacoes.Application.Command.Reservas;
using LocalizaLab.Operacoes.Domain.Command;
using LocalizaLab.Operacoes.Domain.Command.Handlers;
using LocalizaLab.Operacoes.Domain.Contracts.Repository;
using LocalizaLab.Operacoes.Domain.Entities.Contratos;
using LocalizaLab.Operacoes.Domain.Shared.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LocalizaLab.Operacoes.Application.Handlers
{
    public class ReservaHandler : Notifiable, ICommandHandler<SimularReservaCommand>, ICommandHandler<AgendarReservaCommand>,
        ICommandHandler<CadastrarReservaCommand>, ICommandHandler<DeletarReservaCommand>
    {
        private readonly IVeiculoRepository _veiculoRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IReservaRepository _reservaRepository;

        public ReservaHandler(IVeiculoRepository veiculoRepository, IClienteRepository clienteRepository, IReservaRepository reservaRepository)
        {
            _veiculoRepository = veiculoRepository;
            _clienteRepository = clienteRepository;
            _reservaRepository = reservaRepository;
        }

        public async ValueTask<ICommandResult> Handle(SimularReservaCommand command)
        {
            if (!command.Validate())
            {
                AddNotifications(command);
                return new CommandResult(false, base.Notifications);
            }

            var entityVeiculo = await _veiculoRepository.GetById(command.VeiculosId).ConfigureAwait(true);
            var entityCliente = await _clienteRepository.GetById(command.ClienteId).ConfigureAwait(true);

            if(entityCliente == null)
            {
                AddNotification("Cliente","Cliente nao Encontrado.");
                return new CommandResult(false, base.Notifications);
            }
            if(entityVeiculo == null)
            {
                AddNotification("Veiculo","Veiculo Nao Encontrado");
                return new CommandResult(false, base.Notifications);
            }
            var entityReserva = new Reserva(command.Agencia, entityCliente.Id, entityVeiculo.Id, command.Grupo, command.DataInicioReserva, 
                command.DataFimReserva, command.Diarias);

            await _reservaRepository.Add(entityReserva);
            var result = await _reservaRepository.SaveChanges().ConfigureAwait(true);

            if(!result)
            {
                AddNotification("Reserva", "Erro ao Inserir Reserva");
                return new CommandResult(false, base.Notifications);
            }
            return new CommandResult(true);
        }

        public ValueTask<ICommandResult> Handle(AgendarReservaCommand command)
        {
            throw new NotImplementedException();
        }

        public ValueTask<ICommandResult> Handle(CadastrarReservaCommand command)
        {
            throw new NotImplementedException();
        }

        public ValueTask<ICommandResult> Handle(DeletarReservaCommand command)
        {
            throw new NotImplementedException();
        }
    }
}