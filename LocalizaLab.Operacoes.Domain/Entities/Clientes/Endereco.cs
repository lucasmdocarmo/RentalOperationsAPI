using Flunt.Validations;
using LocalizaLab.Operacoes.Domain.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocalizaLab.Operacoes.Domain.Entities.Clientes
{
    public sealed class Endereco : BaseEntity
    {
        internal Endereco() { }
        public Endereco(string CEP, string logradouro, string numero, string complemento, string cidade, string estado, Guid clienteId)
        {
            this.CEP = CEP;
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Cidade = cidade;
            Estado = estado;
            ClienteId = clienteId;

            AddNotifications(new Contract()
                .Requires()
                .IsNotEmpty(base.Id, this.CEP, "CEP é Obrigatorio.")
            );

        }

        public string CEP { get; private set; }
        public string Logradouro { get; private set; }
        public string Numero { get; private set; }
        public string Complemento { get; private set; }
        public string Cidade { get; private set; }
        public string Estado { get; private set; }

        public Cliente Cliente { get; set; }
        public Guid ClienteId { get; set; }
    }
}
