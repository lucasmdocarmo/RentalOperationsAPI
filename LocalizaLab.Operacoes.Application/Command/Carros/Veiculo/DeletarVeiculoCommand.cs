using Flunt.Notifications;
using LocalizaLab.Operacoes.Domain.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocalizaLab.Operacoes.Application.Command.Carros
{
    public class DeletarVeiculoCommand: Notifiable, ICommand
    {
        public Guid Id { get; set; }

        public bool Validate()
        {
            return true;
        }
    }
}
