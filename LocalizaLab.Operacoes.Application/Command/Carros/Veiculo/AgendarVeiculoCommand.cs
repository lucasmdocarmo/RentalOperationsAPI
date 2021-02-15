using Flunt.Notifications;
using LocalizaLab.Operacoes.Domain.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocalizaLab.Operacoes.Application.Command.Carros.Veiculo
{
    public class AgendarVeiculoCommand : Notifiable, ICommand
    {
        public string CodigoAgencia { get; set; }
        public int Diarias { get; set; }
        public Guid ClienteId { get; set; }
        public Guid VeiculoId { get; private set; }
        public DateTime DataAgendamento { get; set; }
        public bool Validate()
        {
            return true;
        }
        public void SetVeiculoId(Guid id)
        {
            VeiculoId = id;
        }
    }
}
