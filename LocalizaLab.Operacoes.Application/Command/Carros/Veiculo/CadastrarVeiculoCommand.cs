using Flunt.Notifications;
using Flunt.Validations;
using LocalizaLab.Operacoes.Domain.Command;
using LocalizaLab.Operacoes.Domain.Entities;
using LocalizaLab.Operacoes.Domain.Entities.Contratos;
using LocalizaLab.Operacoes.Domain.ValueObjects.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocalizaLab.Operacoes.Application.Command.Carros
{
    public sealed class CadastrarVeiculoCommand : Notifiable, ICommand
    {
        public CadastrarVeiculoCommand(string placa, string ano, string valorHora, ETipoCombustivel combustivel,
            string limitePortaMalas, ETipoCategoria categoria, Guid modeloId)
        {
            Placa = placa;
            Ano = ano;
            ValorHora = valorHora;
            Combustivel = combustivel;
            LimitePortaMalas = limitePortaMalas;
            Categoria = categoria;
            ModeloId = modeloId;
        }

        public string Placa { get; set; }
        public string Ano { get; set; }
        public string ValorHora { get; set; }
        public ETipoCombustivel Combustivel { get; set; }
        public string LimitePortaMalas { get; set; }
        public ETipoCategoria Categoria { get; set; }
        public Guid ModeloId { get; set; }
        public bool Validate()
        {
            AddNotifications(new Contract()
                 .IsNotNullOrEmpty(Placa, "CriarVeiculoCommand.Placa","Placa Obrigatorio")
             );

            if (base.Invalid) { return false; }
            return true;
        }
    }
}
