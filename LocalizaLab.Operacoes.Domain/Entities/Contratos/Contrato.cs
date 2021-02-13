using LocalizaLab.Operacoes.Domain.Entities.Clientes;
using LocalizaLab.Operacoes.Domain.Entities.Pagamentos;
using LocalizaLab.Operacoes.Domain.Extensions;
using LocalizaLab.Operacoes.Domain.Shared.Entities;
using LocalizaLab.Operacoes.Domain.ValueObjects.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LocalizaLab.Operacoes.Domain.Entities.Contratos
{
    public class Contrato : BaseEntity
    {
        internal Contrato() { }
        public Contrato(string agencia, Guid clienteId, DadosReserva dadosReserva, DadosPagamentos dadosPagamentos, DadosItemContrato dadosItemContrato)
        {
            Codigo = GenerateCodigo(agencia);
            Agencia = agencia;
            ClienteId = clienteId;
            DadosReserva = dadosReserva;
            DadosPagamentos = dadosPagamentos;
            AdcionarItemContrato(dadosItemContrato);
        }
        public string Codigo { get; private set; }
        public string Agencia { get; private set; }
        public decimal ValorTotal { set { ObterValorReserva(); } get { return this.ValorTotal; } }
        public DateTime DataAberturaContreato { get; set; }

        // EF
        public Cliente Cliente { get; private set; }
        public Guid ClienteId { get; private set; }
        public DadosReserva DadosReserva { get; private set; }
        public DadosPagamentos DadosPagamentos { get; private set; }
        public IList<DadosItemContrato> DadosItemContrato { get; private set; }
        public DadosDevolucao DadosDevolucao { get; private set; }
        public string GenerateCodigo(string agencia)
        {
            return OperacoesCodigosGenerator.GenerateCodigoContrato(agencia);
        }
        public decimal ObterValorReserva()
        {
            return DadosReserva.RetornarValorReserva();
        }

        public void AdcionarItemContrato(DadosItemContrato dadosItemContrato)
        {
            DadosItemContrato.Add(dadosItemContrato);
        }
    }
    public class DadosReserva : BaseEntity
    {
        internal DadosReserva() { }

        public DadosReserva(Guid contratoId, ETipoGrupoReserva grupo, int diarias, Guid veiculosId, DateTime dataInicioReserva, DateTime dataFinalReserva, decimal valorPorHora)
        {
            ContratoId = contratoId;
            Grupo = grupo;
            Diarias = diarias;
            VeiculosId = veiculosId;
            DataInicioReserva = dataInicioReserva;
            DataFinalReserva = dataFinalReserva;
            ValorPorHora = valorPorHora;
            ConvertDiariasToHours(diarias);
            CalcularValorTotalReserva();
        }
        public decimal ValorReserva { get; set; } 
        public string CodigoReserva { get; set; }
        public decimal ValorPorHora { get; set; }
        public int Diarias { get; private set; }
        public decimal DiariasEmHoras { get; private set; }
        public DateTime DataInicioReserva { get; private set; }
        public DateTime DataFinalReserva { get; private set; }
        public ETipoGrupoReserva Grupo { get; private set; }

        // EF
        public Guid ContratoId { get; private set; }
        public Contrato Contrato { get; private set; }
        public Veiculos Veiculo { get; private set; }
        public Guid VeiculosId { get; private set; }

        //Domain Rules
        private void ConvertDiariasToHours(int diarias)
        {
            DiariasEmHoras = (decimal)diarias / 24;
        }
        private void CalcularValorTotalReserva()
        {
            ValorReserva = DiariasEmHoras * ValorPorHora;
        }
        public decimal RetornarValorReserva()
        {
            return ValorReserva;
        }
        public string GenerateCodigoReserva(string agencia)
        {
            return OperacoesCodigosGenerator.GenerateCodigoReserva(agencia);
        }
    }
    public class DadosPagamentos : BaseEntity
    {
        internal DadosPagamentos() { }
        public DadosPagamentos(Guid contratoId, decimal valor, DateTime dataPagamento)
        {
            ContratoId = contratoId;
            Valor = valor;
            DataPagamento = dataPagamento;
        }

        public decimal Valor { get; private set; }
        public bool Status { get; private set; }
        public string Bandeira { get; private set; }
        public string NumeroCartao { get; private set; }
        public string DataExpiracao { get; private set; }
        public string CVV { get; private set; }
        public DateTime DataPagamento { get; private set; }

        //EF
        public Guid ContratoId { get; private set; }
        public Contrato Contrato { get; private set; }
        public Pagamento Pagamento { get; private set; }
        public Guid PagamentoId { get; private set; }

        public void AdicionarPagamento(string Bandeira, string NumeroCartao, string DataExpiracao, string CVV, decimal valor, Guid contratoId)
        {
            Pagamento.VincularPagamento(Bandeira, NumeroCartao, DataExpiracao, CVV, valor, contratoId);
        }
    }
    public class DadosItemContrato : BaseEntity
    {
        internal DadosItemContrato() { }
        public DadosItemContrato(Guid contratoId, ETipoItemReserva item, decimal valorItem)
        {
            ContratoId = contratoId;
            Item = item;
            ValorItem = valorItem;
        }

        public ETipoItemReserva Item { get; private set; }
        public decimal ValorItem { get; private set; }

        public Guid ContratoId { get; private set; }
        public Contrato Contrato { get; private set; }
    }
    public class DadosDevolucao : BaseEntity
    {
        internal DadosDevolucao() { }
        public DadosDevolucao(bool carroLimpo, bool tanqueCheio, bool amassado, bool arranhado, Guid contratoId)
        {
            CarroLimpo = carroLimpo;
            TanqueCheio = tanqueCheio;
            Amassado = amassado;
            Arranhado = arranhado;
            ContratoId = contratoId;
            CalcularDanosGerais();
            CalcularValorFinal();
        }

        public bool CarroLimpo { get; private set; } = false;
        public bool TanqueCheio { get; private set; } = false;
        public bool Amassado { get; private set; } = false;
        public bool Arranhado { get; private set; } = false;
        public decimal PorcentagemTotalAdiconada { get; private set; }
        public decimal ValorContrato => CalcularValorFinal();
        private void CalcularDanosGerais()
        {
            if(CarroLimpo)
            {
                PorcentagemTotalAdiconada += 30;
            }
            if (TanqueCheio)
            {
                PorcentagemTotalAdiconada += 30;
            }
            if(Amassado)
            {
                PorcentagemTotalAdiconada += 30;
            }
            if (Arranhado)
            {
                PorcentagemTotalAdiconada += 30;
            }
        }
        private decimal CalcularValorFinal()
        {
           return ValorContrato * this.PorcentagemTotalAdiconada / 100;
        }

        public Guid ContratoId { get; private set; }
        public Contrato Contrato { get; private set; }
    }

}
