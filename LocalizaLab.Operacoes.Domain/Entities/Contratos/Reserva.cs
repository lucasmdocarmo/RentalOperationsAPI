using LocalizaLab.Operacoes.Domain.Entities.Clientes;
using LocalizaLab.Operacoes.Domain.Extensions;
using LocalizaLab.Operacoes.Domain.Shared.Entities;
using LocalizaLab.Operacoes.Domain.ValueObjects.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocalizaLab.Operacoes.Domain.Entities.Contratos
{
    public sealed class Reserva :BaseEntity
    {
        internal Reserva() { }

    public Reserva(string agencia, Guid clienteId, Guid veiculosId, ETipoGrupoReserva grupo,
            DateTime dataInicioReserva, DateTime dataFimREserva, int diarias, bool simulado)
        {
            Agencia = agencia;
            CodigoReserva = GenerateCodigoReserva(agencia);
            ClienteId = clienteId;
            VeiculosId = veiculosId;
            Grupo = grupo;
            DataInicioReserva = dataInicioReserva;
            DataFimReserva = dataFimREserva;
            Diarias = diarias;
            Simulado = simulado;
            CalcularValorSimulado();
            if (!Simulado)
            {
                CalcularValorTotalReserva();
            }
        }

        public string Agencia { get; private set; }
        public string CodigoReserva { get; private set; }
        public ETipoGrupoReserva Grupo { get; private set; }
        public DateTime DataInicioReserva { get; private set; }
        public DateTime DataFimReserva { get; private set; }
        public int Diarias { get; private set; }
        public decimal ValorSimulado { get; private set; }
        public decimal ValorAdicionarGrupo { get; private set; }
        public bool Simulado { get; set; }
        public decimal ValorReserva { get; set; }
        // EF
        public Guid ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public Guid VeiculosId { get; set; }
        public Veiculos Veiculos { get; set; }

        private void CalcularValorTotalReserva()
        {
            switch (Grupo)
            {
                case ETipoGrupoReserva.A:
                    ValorAdicionarGrupo += 10;
                    break;
                case ETipoGrupoReserva.B:
                    ValorAdicionarGrupo += 15;
                    break;
                case ETipoGrupoReserva.C:
                    ValorAdicionarGrupo += 20;
                    break;
                case ETipoGrupoReserva.D:
                    ValorAdicionarGrupo += 15;
                    break;
                case ETipoGrupoReserva.E:
                    ValorAdicionarGrupo += 50;
                    break;
            }
            ValorReserva = Diarias / 24 * ValorAdicionarGrupo;
        }
        public void CalcularValorSimulado()
        {
            switch(Grupo)
            {
                case ETipoGrupoReserva.A:
                    ValorAdicionarGrupo += 10;
                    break;
                case ETipoGrupoReserva.B:
                    ValorAdicionarGrupo += 15;
                    break;
                case ETipoGrupoReserva.C:
                    ValorAdicionarGrupo += 20;
                    break;
                case ETipoGrupoReserva.D:
                    ValorAdicionarGrupo += 15;
                    break;
                case ETipoGrupoReserva.E:
                    ValorAdicionarGrupo += 50;
                    break;
            }

            ValorSimulado = Diarias / 24 * ValorAdicionarGrupo; 
        }
        public string GenerateCodigoReserva(string agencia)
        {
            return OperacoesCodigosGenerator.GenerateCodigoReserva(agencia);
        }

    }
}
