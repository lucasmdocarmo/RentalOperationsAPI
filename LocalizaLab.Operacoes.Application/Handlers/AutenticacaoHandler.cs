using Flunt.Notifications;
using LocalizaLab.Operacoes.Application.Command;
using LocalizaLab.Operacoes.Application.Command.Autenticacao;
using LocalizaLab.Operacoes.Domain.Command;
using LocalizaLab.Operacoes.Domain.Command.Handlers;
using LocalizaLab.Operacoes.Domain.Extensions;
using LocalizaLab.Operacoes.Domain.Shared.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using LocalizaLab.Operacoes.Application.Handlers.Results;
using LocalizaLab.Operacoes.Identity.Service;
using Microsoft.Extensions.Configuration;

namespace LocalizaLab.Operacoes.Application.Handlers
{
    public class AutenticacaoHandler : Notifiable, ICommandHandler<AutenticarUsuarioCommand>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IOperadorRepository _operadorRepository;
        private readonly IConfiguration _config;

        public AutenticacaoHandler(IUsuarioRepository usuarioRepository, IClienteRepository clienteRepository, IOperadorRepository operadorRepository, IConfiguration config)
        {
            _usuarioRepository = usuarioRepository;
            _clienteRepository = clienteRepository;
            _operadorRepository = operadorRepository;
            _config = config;
        }

        public async ValueTask<ICommandResult> Handle(AutenticarUsuarioCommand command)
        {
            var autenticarResult = new AutenticarUsuarioCommandResult();
            if (!command.Validate())
            {
                AddNotifications(command);
                return new CommandResult(false, base.Notifications);
            }

            var usuarioResult = await _usuarioRepository.GetUsuarioByLogin(command.Login).ConfigureAwait(true);
            if (usuarioResult == null)
            {
                var operadorResult = await _operadorRepository.GetOperadorByMatricula(command.Login).ConfigureAwait(true);
                if (operadorResult == null)
                {
                    AddNotification("Usuario/Operador", "Usuario/Operador Nao Encontrado");
                    return new CommandResult(false, base.Notifications);
                }
                else
                {
                    var token = IdentityTokenService.GenerateToken(operadorResult.Nome, operadorResult.Perfil, "");
                    autenticarResult.OperadorResult = new OperadorResult()
                    {
                        Matricula = operadorResult.Matricula,
                        Perfil = operadorResult.Perfil,
                        Senha = operadorResult.Senha,
                        Nome = operadorResult.Nome,
                        Token = token
                    };
                }
            }
            else
            {
                var token = IdentityTokenService.GenerateToken(usuarioResult.Nome, usuarioResult.Perfil, "");
                autenticarResult.UsuarioResult = new UsuarioResult()
                {
                    CPF = usuarioResult.CPF,
                    Login = usuarioResult.Login,
                    Perfil = usuarioResult.Perfil,
                    Senha = usuarioResult.Senha,
                    Nome = usuarioResult.Nome,
                    Token = token
                };

            }
            return new CommandResult(true, autenticarResult);

        }
    }
}
