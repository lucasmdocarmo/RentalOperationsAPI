using Flunt.Notifications;
using Flunt.Validations;
using LocalizaLab.Operacoes.Domain.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocalizaLab.Operacoes.Application.Command.Marca
{
    public class CadastrarMarcaCommand : Notifiable, ICommand
    {
        public string Nome { get; set; }
        public string Pais { get; set; }

        public bool Validate()
        {
            AddNotifications(new Contract()
                .IsNotNullOrEmpty(Nome, "CadastrarMarcaCommand.Nome", "Nome Obrigatorio")
                .IsNotNullOrEmpty(Pais, "CadastrarMarcaCommand.Pais", "Pais Obrigatorio")
            );

            if (base.Invalid) { return false; }
            return true;
        }

    }
}
