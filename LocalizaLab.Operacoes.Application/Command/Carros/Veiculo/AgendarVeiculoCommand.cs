using Flunt.Notifications;
using Flunt.Validations;
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
            AddNotifications(new Contract()
              .IsGreaterThan(DataAgendamento, DateTime.Today, DataAgendamento.ToString(), "Data Agendamento deve ser futura ao dia de hoje.")
              .IsGreaterThan(Diarias, 0, Diarias.ToString(), "Diária deve ser maior do que 1.")
              .IsNotNullOrEmpty(CodigoAgencia, "Marca.CodigoAgencia", "Agencia Obrigatorio")
              .IsNotNull(ClienteId, "Cliente", "Cliente Obrigatório")
              .IsNotNull(VeiculoId, "Marca", "Marca Obrigatório"));

            if (base.Invalid) { return false; }
            return true;
        }
        public void SetVeiculoId(Guid id)
        {
            VeiculoId = id;
        }
    }
}
