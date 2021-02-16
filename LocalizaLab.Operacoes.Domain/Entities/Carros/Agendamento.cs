using Flunt.Validations;
using LocalizaLab.Operacoes.Domain.Entities.Clientes;
using LocalizaLab.Operacoes.Domain.Extensions;
using LocalizaLab.Operacoes.Domain.Shared.Entities;
using LocalizaLab.Operacoes.Domain.ValueObjects.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocalizaLab.Operacoes.Domain.Entities.Carros
{
    public class Agendamento : BaseEntity
    {
        internal Agendamento() { }

        public Agendamento(string codigoAgencia, Guid veiculoId, DateTime dataAgendamento, Guid clientesId, int diarias)
        {
            AddNotifications(new Contract()
                .IsNotNullOrEmpty(codigoAgencia, "Agencia", "Agencia Obrigatorio.")
                .IsNotNullOrEmpty(diarias.ToString(), "Diaria", "Diaria Obrigatorio")
                .IsNull(dataAgendamento, "Data", "Data Obrigatorio")
                .IsNull(veiculoId, "Veiculo", "Veiculo Obrigatorio."));

            if (Valid)
            {
                CodigoAgencia = codigoAgencia;
                VeiculoId = veiculoId;
                DataAgendamento = dataAgendamento;
                CodigoAgendamento = GenerateCodigoReserva(codigoAgencia);
                ClientesId = clientesId;
                Diarias = diarias;
            }
        }
        public int Diarias { get; private set; }
        public string CodigoAgencia { get; private set; }
        public string CodigoAgendamento { get; private set; }
        public Guid VeiculoId { get; private set; }
        public Veiculos Veiculo { get; private set; }
        public decimal ValorFinal { get; private set; }
        public DateTime DataAgendamento { get; private set; }
        public decimal ValorAdicionarCategoria { get; private set; }
        public Cliente Clientes { get; private set; }
        public Guid ClientesId { get; private set; }
        public string GenerateCodigoReserva(string agencia)
        {
            return OperacoesCodigosGenerator.GenerateCodigoAgendamento(agencia);
        }
        public void GerarValorFinalAgendamento(string valorHoraVeiculo, ETipoCategoria categoria, int diarias)
        {

            switch (categoria)
            {
                case ETipoCategoria.Basico:
                    ValorAdicionarCategoria = +Convert.ToInt32(valorHoraVeiculo) * 10;
                    break;
                case ETipoCategoria.Completo:
                    ValorAdicionarCategoria = +Convert.ToInt32(valorHoraVeiculo) * 20;
                    break;
                case ETipoCategoria.Luxo:
                    ValorAdicionarCategoria = +Convert.ToInt32(valorHoraVeiculo) * 30;
                    break;
            }

            ValorFinal = diarias / 24 * ValorAdicionarCategoria / 100;
        }
    }
}
