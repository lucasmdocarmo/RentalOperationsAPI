using Flunt.Notifications;
using LocalizaLab.Operacoes.Domain.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocalizaLab.Operacoes.Application.Command.Contratos
{
    public class BaixarContratoCommand : Notifiable,ICommand
    {
        public Guid Id { get; set; }

        public bool Validate()
        {
            return true;
        }
    }
}
