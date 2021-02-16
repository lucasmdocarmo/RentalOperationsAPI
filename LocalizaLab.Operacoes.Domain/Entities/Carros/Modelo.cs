using Flunt.Validations;
using LocalizaLab.Operacoes.Domain.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocalizaLab.Operacoes.Domain.Entities
{
    public sealed class Modelo : BaseEntity
    {
        internal Modelo() { }
        public Modelo(string nome, Guid marcaId)
        {
            AddNotifications(new Contract()
               .IsNotNullOrEmpty(nome, "Nome", "Nome Obrigatorio.")
               .IsNull(marcaId, "Marca", "Marca Obrigatorio"));

            if (Valid)
            {
                Nome = nome;
                MarcaId = marcaId;
            }
        }

        public string Nome { get; private set; }
        public Guid MarcaId { get; set; }
        public Marca Marca { get; set; }
        public IReadOnlyCollection<Veiculos> Veiculos { get; set; }
        public void AlterarMarca(Marca marca)
        {
            this.Marca = marca;
        }
        public void AlterarNome(string nome)
        {
            this.Nome = nome;
        }
    }
}
