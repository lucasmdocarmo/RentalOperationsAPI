using LocalizaLab.Operacoes.Domain.ValueObjects.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocalizaLab.Operacoes.Application.Queries.Results
{
    public class OperadorResult
    {
        public string Matricula { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        public ETipoPerfil Perfil { get; set; }
    }
}
