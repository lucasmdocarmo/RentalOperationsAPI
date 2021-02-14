using LocalizaLab.Operacoes.Domain.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocalizaLab.Operacoes.Application.Queries.Reservas
{
    public class ConsultarReservaQuery : IQuery
    {
        public Guid Id { get; set; }
    }
}
