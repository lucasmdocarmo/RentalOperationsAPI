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
    public class ReservaHandler : Notifiable, ICommandHandler<SimularReservaCommand>,
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
                command.DataFimReserva, command.Diarias, true);

            await _reservaRepository.Add(entityReserva);
            var result = await _reservaRepository.SaveChanges().ConfigureAwait(true);

            if(!result)
            {
                AddNotification("Reserva", "Erro ao Inserir Reserva");
                return new CommandResult(false, base.Notifications);
            }
            return new CommandResult(true);
        }

        public async ValueTask<ICommandResult> Handle(CadastrarReservaCommand command)
        {
            if (!command.Validate())
            {
                AddNotifications(command);
                return new CommandResult(false, base.Notifications);
            }
            var entityReservaSimulada = await _reservaRepository.GetByCodigoReserva(command.CodigoReserva).ConfigureAwait(true);
          
            var entityVeiculo = await _veiculoRepository.GetById(command.VeiculosId).ConfigureAwait(true);
            var entityCliente = await _clienteRepository.GetById(command.ClienteId).ConfigureAwait(true);

            if (entityCliente == null)
            {
                AddNotification("Cliente", "Cliente nao Encontrado.");
                return new CommandResult(false, base.Notifications);
            }
            if (entityVeiculo == null)
            {
                AddNotification("Veiculo", "Veiculo Nao Encontrado");
                return new CommandResult(false, base.Notifications);
            }
            if(entityVeiculo.Reservado)
            {
                AddNotification("Veiculo", "Este Veiculo Esta Reservado.");
                return new CommandResult(false, base.Notifications);
            }
            if (entityReservaSimulada != null)
            {
                var reservaSimulada = new Reserva(command.Agencia, entityCliente.Id, entityVeiculo.Id, command.Grupo, command.DataInicioReserva,
                command.DataFimReserva, command.Diarias, false);

                reservaSimulada.Simulado = false;
                await _reservaRepository.Add(reservaSimulada);
            }
            else
            {
                var entityReserva = new Reserva(command.Agencia, entityCliente.Id, entityVeiculo.Id, command.Grupo, command.DataInicioReserva,
                    command.DataFimReserva, command.Diarias, false);
                await _reservaRepository.Add(entityReserva);
            }

            var result = await _reservaRepository.SaveChanges().ConfigureAwait(true);
            if (!result)
            {
                AddNotification("Reserva", "Erro ao Inserir Reserva");
                return new CommandResult(false, base.Notifications);
            }
            return new CommandResult(true);
        }

        public async ValueTask<ICommandResult> Handle(DeletarReservaCommand command)
        {
            if (!command.Validate())
            {
                AddNotifications(command);
                return new CommandResult(false, base.Notifications);
            }

            var entityReserva = await _reservaRepository.GetById(command.Id).ConfigureAwait(true);
            var entityVeiculo = await _veiculoRepository.GetById(entityReserva.VeiculosId).ConfigureAwait(true);
            if (entityReserva == null)
            {
                AddNotification("Reserva", "Reserva Nao Encotnrada");
                return new CommandResult(false, base.Notifications);
            }
            {
                entityVeiculo.Reservado = false;
                await _veiculoRepository.Update(entityVeiculo);
                await _veiculoRepository.SaveChanges().ConfigureAwait(true);
            }

            await _reservaRepository.Remove(command.Id);
            var result = await _reservaRepository.SaveChanges().ConfigureAwait(true);

            if (!result)
            {
                AddNotification("Reserva", "Erro ao Deletar Reserva");
                return new CommandResult(false, base.Notifications);
            }
            return new CommandResult(true);
        }
    }
}