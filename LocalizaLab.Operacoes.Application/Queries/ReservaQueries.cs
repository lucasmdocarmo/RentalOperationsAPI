using AutoMapper;
using Flunt.Notifications;
using LocalizaLab.Operacoes.Application.Queries.Base;
using LocalizaLab.Operacoes.Application.Queries.Reservas;
using LocalizaLab.Operacoes.Domain.Queries;
using LocalizaLab.Operacoes.Domain.Shared.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LocalizaLab.Operacoes.Application.Queries
{
    public class ReservaQueries : Notifiable, IQueryHandler<ConsultarReservaQuery>
    {
        private readonly IReservaRepository _reservaRepository;
        private readonly IMapper _mapper;

        public ReservaQueries(IReservaRepository reservaRepository, IMapper mapper)
        {
            _reservaRepository = reservaRepository;
            _mapper = mapper;
        }

        public async ValueTask<IQueryResult> Handle(ConsultarReservaQuery command)
        {
            var queryResult = new QueryResult();
            var reserva = await _reservaRepository.GetById(command.Id);

            if (reserva == null)
            {
                AddNotification("Reserva", "Reserva Nao Encontrada.");
                return new QueryResult(false, base.Notifications);
            }

            queryResult.Result = reserva;
            return new QueryResult(true, reserva);

        }
    }
}
