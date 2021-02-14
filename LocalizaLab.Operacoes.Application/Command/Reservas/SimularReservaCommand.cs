using Flunt.Notifications;
using LocalizaLab.Operacoes.Domain.Command;
using LocalizaLab.Operacoes.Domain.ValueObjects.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocalizaLab.Operacoes.Application.Command.Reservas
{
    public class SimularReservaCommand : Notifiable, ICommand
    {
        public string Agencia { get; set; }
        public string CodigoReserva { get; set; }
        public ETipoGrupoReserva Grupo { get; set; }
        public DateTime DataInicioReserva { get; set; }
        public DateTime DataFimReserva { get; set; }
        public int Diarias { get; set; }
        public decimal ValorSimulado { get; set; }
        public decimal ValorAdicionarGrupo { get; set; }
        public Guid ClienteId { get; set; }
        public Guid VeiculosId { get; set; }

        public bool Validate()
        {
            return true;
        }
    }
}
