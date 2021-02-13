using LocalizaLab.Operacoes.Domain.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocalizaLab.Operacoes.Application.Queries.Query
{
    public class VeiculoQuery : IQuery
    {
        public Guid Id { get; set; }
    }
}
