using Flunt.Notifications;
using LocalizaLab.Operacoes.Domain.Command;
using LocalizaLab.Operacoes.Domain.ValueObjects.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocalizaLab.Operacoes.Application.Command.Contratos
{
    public class CadastrarContratoCommand : Notifiable, ICommand
    {
        public string Agencia { get; private set; }
        public DateTime DataAberturaContrato { get; set; }
        public Guid ClienteId { get; set; }
        /// <summary>
        ///
        /// </summary>
        public Guid ReservaId { get; set; }
        public ItensContratoCommand ItensContrato { get; set; }

        public bool Validate()
        {
            return true;
        }
    }
    public class ItensContratoCommand
    {
        public ETipoItemReserva Item { get; set; }
        public decimal ValorItem { get; set; }
    }
}
