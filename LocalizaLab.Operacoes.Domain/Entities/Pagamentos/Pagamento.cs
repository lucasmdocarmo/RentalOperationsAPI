using Flunt.Validations;
using LocalizaLab.Operacoes.Domain.Entities.Contratos;
using LocalizaLab.Operacoes.Domain.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocalizaLab.Operacoes.Domain.Entities.Pagamentos
{
    public sealed class Pagamento : BaseEntity
    {
        internal Pagamento() { }
        internal Pagamento(decimal valor, string bandeira, string numeroCartao,
            string dataExpiracao, string cVV, Guid contratoId)
        {
            Valor = valor;
            Bandeira = bandeira;
            NumeroCartao = numeroCartao;
            DataExpiracao = dataExpiracao;
            CVV = cVV;
            ContratoId = contratoId;
            AddNotifications(new Contract()
            .Requires()
            .IsLowerOrEqualsThan(0, Valor, "Pagamento.Total", "O total não pode ser zero")
        );
        }

        public Guid ContratoId { get; private set; }
        public decimal Valor { get; private set; }
        public string Bandeira { get; private set; }
        public string NumeroCartao { get; private set; }
        public string DataExpiracao { get; private set; }
        public string CVV { get; private set; }
        public bool IsValid { get; set; }
        public DadosPagamentos DadosPagamentos { get; private set; }
        public void VincularPagamento(string Bandeira, string NumeroCartao, string DataExpiracao, string CVV, decimal valor, Guid contratoid)
        {
            var pagamento = new Pagamento(valor, Bandeira, NumeroCartao, DataExpiracao, CVV, contratoid);
            pagamento.IsValid = true;
        }
    }
}
