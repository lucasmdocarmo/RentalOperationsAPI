using Flunt.Notifications;
using LocalizaLab.Operacoes.Application.Queries.Reservas;
using LocalizaLab.Operacoes.Domain.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LocalizaLab.Operacoes.Application.Queries
{
    public class ReservaQueries : Notifiable, IQueryHandler<ConsultarReservaQuery>
    {
        public ValueTask<IQueryResult> Handle(ConsultarReservaQuery command)
        {
            throw new NotImplementedException();
        }
    }
}
