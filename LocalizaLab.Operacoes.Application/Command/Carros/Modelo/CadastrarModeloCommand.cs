using Flunt.Notifications;
using LocalizaLab.Operacoes.Domain.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocalizaLab.Operacoes.Application.Command.Modelos
{
    public class CadastrarModeloCommand : Notifiable, ICommand
    {
        public string Nome { get; set; }
        public Guid MarcaId { get; set; }
        public bool Validate()
        {
            return true;
        }
    }
}
