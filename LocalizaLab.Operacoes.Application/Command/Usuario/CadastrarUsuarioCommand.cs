﻿using Flunt.Notifications;
using LocalizaLab.Operacoes.Domain.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocalizaLab.Operacoes.Application.Command.Usuario
{
    public class CadastrarUsuarioCommand  : Notifiable, ICommand
    {
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public bool Validate()
        {
            return true;
        }
    }
}
