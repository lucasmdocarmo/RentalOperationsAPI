using Flunt.Notifications;
using LocalizaLab.Operacoes.Domain.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocalizaLab.Operacoes.Application.Presentation.Results.Operador
{
    public class ListarOperadoresResult
    {
        public ICollection<OperadoresQueryList> Operadores { get; set; }
    }
    public class OperadoresQueryList
    {
        public string Nome { get; set; }
        public string Matricula { get; set; }
    }
}
