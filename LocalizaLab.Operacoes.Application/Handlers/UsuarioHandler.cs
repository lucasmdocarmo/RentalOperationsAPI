using Flunt.Notifications;
using LocalizaLab.Operacoes.Application.Command;
using LocalizaLab.Operacoes.Application.Command.Usuario;
using LocalizaLab.Operacoes.Domain.Command;
using LocalizaLab.Operacoes.Domain.Command.Handlers;
using LocalizaLab.Operacoes.Domain.Entities.Clientes;
using LocalizaLab.Operacoes.Domain.Entities.Usuarios;
using LocalizaLab.Operacoes.Domain.Extensions;
using LocalizaLab.Operacoes.Domain.Shared.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace LocalizaLab.Operacoes.Application.Handlers
{
    public class UsuarioHandler : Notifiable, ICommandHandler<CadastrarOperadorCommand>, ICommandHandler<CadastrarUsuarioCommand>,
                                              ICommandHandler<CadastrarClienteCommand>, ICommandHandler<CadastrarEnderecoCommand>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly IOperadorRepository _operadorRepository;

        public UsuarioHandler(IUsuarioRepository usuarioRepository, IClienteRepository clienteRepository,
                              IEnderecoRepository enderecoRepository, IOperadorRepository operadorRepository)
        {
            _usuarioRepository = usuarioRepository;
            _clienteRepository = clienteRepository;
            _enderecoRepository = enderecoRepository;
            _operadorRepository = operadorRepository;
        }

        public async ValueTask<ICommandResult> Handle(CadastrarOperadorCommand command)
        {
            if (!command.Validate())
            {
                AddNotifications(command);
                return new CommandResult(false, base.Notifications);
            }
            var entity = new Operador(command.Matricula,command.Nome,command.Senha);

            await _operadorRepository.Add(entity);
            var result = await _operadorRepository.SaveChanges().ConfigureAwait(true);

            if (!result) { return new CommandResult(false); }

            return new CommandResult(true);
        }

        public async ValueTask<ICommandResult> Handle(CadastrarUsuarioCommand command)
        {
            if (!command.Validate())
            {
                AddNotifications(command);
                return new CommandResult(false, base.Notifications);
            }
            var entity = new Usuario(command.Nome,command.Login,command.Senha);
            if (entity.Login.IsValidCpf())
            {
                var clienteResult = await _clienteRepository.GetClienteByCPF(entity.Login).ConfigureAwait(true);
                if (clienteResult == null)
                {
                    var entityOperador = new Operador(command.Login, command.Nome, command.Senha);

                    await _operadorRepository.Add(entityOperador);
                    await _operadorRepository.SaveChanges().ConfigureAwait(true);
                }
                else
                {
                    entity.VincularClienteAoUsuario(clienteResult.FirstOrDefault());
                    await _usuarioRepository.Add(entity);
                }
            }
            else { AddNotification("CPF", "CPF Invalido."); }

            var result = await _usuarioRepository.SaveChanges().ConfigureAwait(true);

            if (!result) { return new CommandResult(false); }

            return new CommandResult(true);
        }

        public async ValueTask<ICommandResult> Handle(CadastrarClienteCommand command)
        {
            if (!command.Validate())
            {
                AddNotifications(command);
                return new CommandResult(false, base.Notifications);
            }

            var entity = new Cliente(command.Nome, command.Identidade,command.Email, command.DataNascimento, command.CPF);

            await _clienteRepository.Add(entity);
            var result = await _clienteRepository.SaveChanges().ConfigureAwait(true);

            if (!result) { return new CommandResult(false); }

            return new CommandResult(true);
        }

        public async ValueTask<ICommandResult> Handle(CadastrarEnderecoCommand command)
        {
            if (!command.Validate())
            {
                AddNotifications(command);
                return new CommandResult(false, base.Notifications);
            }

            var entity = new Endereco(command.CEP, command.Logradouro, command.Numero, command.Complemento, command.Cidade, command.Estado, new Guid());

            await _enderecoRepository.Add(entity);
            var result = await _enderecoRepository.SaveChanges().ConfigureAwait(true);

            if (!result) { return new CommandResult(false); }

            return new CommandResult(true);
        }
    }
}
