using Flunt.Notifications;
using LocalizaLab.Operacoes.Domain.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocalizaLab.Operacoes.Application.Command.Contratos
{
    public class PagarContratoCommand : Notifiable, ICommand
    {
        public decimal Valor { get; set; }
        public bool Status { get; set; }
        public string Bandeira { get; set; }
        public string NumeroCartao { get; set; }
        public string DataExpiracao { get; set; }
        public string CVV { get; set; }
        public DateTime DataPagamento { get; set; }
        public Guid ContratoId { get; set; }

        public bool Validate()
        {
            return true;
        }
    }
}
