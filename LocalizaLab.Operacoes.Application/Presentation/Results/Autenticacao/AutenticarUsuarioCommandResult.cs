using LocalizaLab.Operacoes.Domain.ValueObjects.Clientes;
using LocalizaLab.Operacoes.Domain.ValueObjects.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocalizaLab.Operacoes.Application.Handlers.Results
{
    public class AutenticarUsuarioCommandResult
    {
        public UsuarioResult UsuarioResult { get; set; }
        public OperadorResult OperadorResult { get; set; }
    }
    public class UsuarioResult
    {
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public ETipoPerfil Perfil { get; set; }
        public CPF CPF { get;  set; }
        public string Token { get; set; }
    }
    public class OperadorResult
    {
        public string Matricula { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        public ETipoPerfil Perfil { get; set; }
        public string Token { get; set; }
    }
}
