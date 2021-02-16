using Flunt.Notifications;
using Flunt.Validations;
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
            AddNotifications(new Contract()
               .IsNotNull(Id, "Veiculo", "Id Obrigatório"));

            if (base.Invalid) { return false; }
            return true;
        }
    }
}
