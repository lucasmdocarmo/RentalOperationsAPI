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
            CodigoAgencia = codigoAgencia;
            VeiculoId = veiculoId;
            DataAgendamento = dataAgendamento;
            CodigoAgendamento = GenerateCodigoReserva(codigoAgencia);
            ClientesId = clientesId;
            Diarias = diarias;
        }
        public int Diarias { get; set; }
        public string CodigoAgencia { get; set; }
        public string CodigoAgendamento { get; set; }
        public Guid VeiculoId { get; set; }
        public Veiculos Veiculo { get; set; }
        public decimal ValorFinal { get; set; }
        public DateTime DataAgendamento { get; set; }
        public decimal ValorAdicionarCategoria { get; private set; }
        public Cliente Clientes { get; set; }
        public Guid ClientesId { get; set; }
        public string GenerateCodigoReserva(string agencia)
        {
            return OperacoesCodigosGenerator.GenerateCodigoAgendamento(agencia);
        }
        public void GerarValorFinalAgendamento(string valorHoraVeiculo, ETipoCategoria categoria, int diarias)
        {

            switch (categoria)
            {
                case ETipoCategoria.Basico:
                    ValorAdicionarCategoria =+ Convert.ToInt32(valorHoraVeiculo) * 10;
                    break;
                case ETipoCategoria.Completo:
                    ValorAdicionarCategoria =+ Convert.ToInt32(valorHoraVeiculo) * 20;
                    break;
                case ETipoCategoria.Luxo:
                    ValorAdicionarCategoria =+ Convert.ToInt32(valorHoraVeiculo) * 30;
                    break;
            }

            ValorFinal = diarias / 24 * ValorAdicionarCategoria / 100;
        }
    }
}
