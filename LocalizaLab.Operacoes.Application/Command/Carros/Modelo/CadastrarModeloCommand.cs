using Flunt.Notifications;
using Flunt.Validations;
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
            AddNotifications(new Contract()
              .IsNotNullOrEmpty(Nome, "Marca.Nome", "Nome Obrigatorio")
              .IsNotNull(MarcaId, "Marca","Marca Obrigatório"));

            if (base.Invalid) { return false; }
            return true;
        }
    }
}
