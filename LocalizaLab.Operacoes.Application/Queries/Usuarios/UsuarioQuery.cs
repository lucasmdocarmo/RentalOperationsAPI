using LocalizaLab.Operacoes.Domain.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocalizaLab.Operacoes.Application.Queries.Models
{
    public class UsuarioQuery : IQuery
    {
        public string Login { get; set; }
        public string Senha { get; set; }
    }
}
