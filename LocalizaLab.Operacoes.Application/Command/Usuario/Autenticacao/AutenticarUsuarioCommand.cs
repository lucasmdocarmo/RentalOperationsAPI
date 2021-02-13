using Flunt.Notifications;
using LocalizaLab.Operacoes.Domain.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocalizaLab.Operacoes.Application.Command.Autenticacao
{
    public class AutenticarUsuarioCommand : Notifiable, ICommand
    {
        public string Login { get; set; }
        public string Senha { get; set; }

        public bool Validate()
        {
            return true;
        }
    }
}
