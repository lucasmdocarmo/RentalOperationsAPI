using Flunt.Notifications;
using LocalizaLab.Operacoes.Domain.Command;
using LocalizaLab.Operacoes.Domain.ValueObjects.Clientes;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocalizaLab.Operacoes.Application.Command.Usuario
{
    public class CadastrarClienteCommand : Notifiable, ICommand
    {
        public string Nome { get; set; }
        public string Identidade { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
        public CPF CPF { get; set; }

        public bool Validate()
        {
            return true;
        }
    }
}
