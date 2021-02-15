using Flunt.Notifications;
using LocalizaLab.Operacoes.Domain.Command;
using LocalizaLab.Operacoes.Domain.ValueObjects.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LocalizaLab.Operacoes.Application.Command.Contratos
{
    public class CadastrarReservaCommand : Notifiable, ICommand
    {
        [Required]
        public string Agencia { get; set; }
        [Display(Description = "Codigo Reserva Simulada")]
        public string CodigoReserva { get; set; }
        [Required]
        public ETipoGrupoReserva Grupo { get; set; }
        [Required]
        public DateTime DataInicioReserva { get; set; }
        [Required]
        public DateTime DataFimReserva { get; set; }
        [Required]
        public int Diarias { get; set; }
        [Required]
        public Guid ClienteId { get; set; }
        [Required]
        public Guid VeiculosId { get; set; }

        public bool Validate()
        {
            return true;
        }
    }
}
