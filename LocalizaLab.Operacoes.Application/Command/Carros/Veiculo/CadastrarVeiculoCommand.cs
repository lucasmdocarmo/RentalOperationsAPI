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
             .IsNull(Combustivel, Enum.GetName(typeof(ETipoCombustivel), this.Combustivel).ToString(),"Tipo Combustivel Inválido.")
             .IsNull(Categoria, Enum.GetName(typeof(ETipoCategoria), this.Categoria).ToString(), "Tipo Combustivel Inválido.")
             .IsNotNullOrEmpty(Placa, "Plca", "Placa Obrigatorio")
             .IsNotNullOrEmpty(Ano, "Ano", "Ano Obrigatorio")
             .IsNotNullOrEmpty(ValorHora, "Valor Hora", "ValorHora Obrigatorio")
             .IsGreaterThan(Convert.ToInt32(ValorHora),0,"","")
             .IsDigit(ValorHora,"Valor Hora","Valor deve ser um numero decimal válido")
             .IsDigit(LimitePortaMalas, "Limite Porta Malas", "Limite Porta Malas deve ser um numero válido")
             .IsNotNull(ModeloId, "Modelo", "Modelo Obrigatório"));

            if (base.Invalid) { return false; }
            return true;
        }
    }
}
