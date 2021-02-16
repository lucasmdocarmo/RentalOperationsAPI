using Flunt.Validations;
using LocalizaLab.Operacoes.Domain.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocalizaLab.Operacoes.Domain.Entities
{
    public sealed class Marca :BaseEntity
    {
        internal Marca() { }
        public Marca(string nome, string pais)
        {
            AddNotifications(new Contract()
                .IsNotNullOrEmpty(nome, "Nome", "Nome Obrigatorio.")
                .IsNotNullOrEmpty(pais, "Pais", "Pais Obrigatorio"));

            if(Valid)
            {
                Nome = nome;
                Pais = pais;
            }

        }

        public string Nome { get; private set; }
        public string Pais { get;  private set; }

        public IReadOnlyCollection<Modelo> Modelos { get; set; }
        public void EditarMarca(string nome, string pais)
        {
            this.Nome = nome;
            this.Pais = pais;
        }
    }
}
