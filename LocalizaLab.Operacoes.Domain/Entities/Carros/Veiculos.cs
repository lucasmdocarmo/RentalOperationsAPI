using LocalizaLab.Operacoes.Domain.Entities.Carros;
using LocalizaLab.Operacoes.Domain.Entities.Contratos;
using LocalizaLab.Operacoes.Domain.Shared.Entities;
using LocalizaLab.Operacoes.Domain.ValueObjects.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocalizaLab.Operacoes.Domain.Entities
{
    public sealed class Veiculos : BaseEntity
    {
        internal Veiculos() { }
        public Veiculos(string placa, string ano, string valorHora, ETipoCombustivel combustivel,
            string limitePortaMalas, ETipoCategoria categoria, Guid modeloId)
        {
            Placa = placa;
            Ano = ano;
            ValorHora = valorHora;
            Combustivel = combustivel;
            LimitePortaMalas = limitePortaMalas;
            Categoria = categoria;
            ModeloId = modeloId;
            Reservado = false;
        }

        public string Placa { get; private set; }
        public string Ano { get; private set; }
        public string ValorHora { get; private set; }
        public ETipoCombustivel Combustivel { get; set; }
        public string LimitePortaMalas { get; set; }
        public ETipoCategoria Categoria { get; set; }
        public bool Reservado { get; set; }
        // EF
        public Guid ModeloId { get; set; }
        public Modelo Modelo { get; set; }
        public IReadOnlyCollection<Reserva> Reservas { get; set; }
        public Agendamento Agendamento { get; set; }
    }
}
