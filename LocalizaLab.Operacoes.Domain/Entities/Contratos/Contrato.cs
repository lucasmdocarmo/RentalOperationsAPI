using Flunt.Validations;
using LocalizaLab.Operacoes.Domain.Entities.Clientes;
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
        public Contrato(string agencia, Guid clienteId, decimal valorReserva, DateTime dataAberturaContrato)
        {
            Codigo = GenerateCodigo(agencia);
            Agencia = agencia;
            ClienteId = clienteId;
            ValorTotal = ObterValorReserva(valorReserva);
            DataAberturaContrato = dataAberturaContrato;
        }
        public string Codigo { get; private set; }
        public string Agencia { get; private set; }
        public decimal ValorTotal { get; set; }
        public DateTime DataAberturaContrato { get; set; }

        public Cliente Cliente { get; private set; }
        public Guid ClienteId { get; private set; }

        public DadosPagamentos DadosPagamentos { get; private set; }
        public IList<DadosItemContrato> DadosItemContrato { get; private set; }
        public DadosDevolucao DadosDevolucao { get; private set; }
        public string GenerateCodigo(string agencia)
        {
            return OperacoesCodigosGenerator.GenerateCodigoContrato(agencia);
        }
        public decimal ObterValorReserva(decimal valor)
        {
            return ValorTotal = valor;
        }

        public void AdcionarItemContrato(DadosItemContrato dadosItemContrato)
        {
            DadosItemContrato.Add(dadosItemContrato);
        }
    }
    public class DadosPagamentos : BaseEntity
    {
        internal DadosPagamentos() { }

        public DadosPagamentos(decimal valor, bool status, string bandeira, string numeroCartao, string dataExpiracao, string cVV, DateTime dataPagamento, Guid contratoId)
        {
            Valor = valor;
            Status = status;
            Bandeira = bandeira;
            NumeroCartao = numeroCartao;
            DataExpiracao = dataExpiracao;
            CVV = cVV;
            DataPagamento = dataPagamento;
            ContratoId = contratoId;

            AddNotifications(new Contract()
               .Requires()
               .IsLowerOrEqualsThan(0, Valor, "Pagamento.Total", "O total não pode ser zero"));
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
    }
    public class DadosItemContrato : BaseEntity
    {
        internal DadosItemContrato() { }
        public DadosItemContrato(ETipoItemReserva item, decimal valorItem, Guid contratoId)
        {
            Item = item;
            ValorItem = valorItem;
            ContratoId = contratoId;
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

        public bool CarroLimpo { get; private set; }
        public bool TanqueCheio { get; private set; }
        public bool Amassado { get; private set; }
        public bool Arranhado { get; private set; }
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
