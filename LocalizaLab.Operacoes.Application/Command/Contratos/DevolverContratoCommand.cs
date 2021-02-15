using Flunt.Notifications;
using LocalizaLab.Operacoes.Domain.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocalizaLab.Operacoes.Application.Command.Contratos
{
    public class DevolverContratoCommand : Notifiable, ICommand
    {
        public bool CarroLimpo { get; set; }
        public bool TanqueCheio { get; set; }
        public bool Amassado { get; set; }
        public bool Arranhado { get; set; }
        public Guid ContratoId { get; set; }

        public bool Validate()
        {
            return true;
        }
    }
}
