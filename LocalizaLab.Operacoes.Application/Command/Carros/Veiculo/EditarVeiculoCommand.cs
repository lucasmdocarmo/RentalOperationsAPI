using Flunt.Notifications;
using LocalizaLab.Operacoes.Domain.Command;
using LocalizaLab.Operacoes.Domain.ValueObjects.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocalizaLab.Operacoes.Application.Command.Carros
{
    public class EditarVeiculoCommand : Notifiable, ICommand
    {
        public Guid Id { get; set; }
        public string Placa { get;  set; }
        public string Ano { get;  set; }
        public string ValorHora { get;  set; }
        public ETipoCombustivel Combustivel { get; set; }
        public string LimitePortaMalas { get; set; }
        public ETipoCategoria Categoria { get; set; }
        public EditarModelo Modelo { get; set; }
       
        public bool Validate()
        {
            return true;
        }
    }
    public class EditarModelo
    {
        public Guid Id { get; private set; }
        public string Nome { get;  set; }
        public EditarMarca Marca { get; set; }
    }
    public class EditarMarca
    {
        public Guid Id { get; private set; }
        public string Nome { get; set; }
        public string Pais { get; set; }
    }
}
