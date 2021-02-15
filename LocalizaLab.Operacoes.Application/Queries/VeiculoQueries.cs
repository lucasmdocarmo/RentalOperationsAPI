using Flunt.Notifications;
using LocalizaLab.Operacoes.Application.Queries.Base;
using LocalizaLab.Operacoes.Application.Queries.Query;
using LocalizaLab.Operacoes.Application.Queries.Veiculos;
using LocalizaLab.Operacoes.Domain.Contracts.Repository;
using LocalizaLab.Operacoes.Domain.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LocalizaLab.Operacoes.Application.Queries
{
    public class VeiculoQueries : Notifiable, IQueryHandler<VeiculoQuery>, IQueryHandler<TodosVeiculosQuery>
    {
        private readonly IVeiculoRepository _veiculoRepository;

        public VeiculoQueries(IVeiculoRepository veiculoRepository)
        {
            _veiculoRepository = veiculoRepository;
        }

        public async ValueTask<IQueryResult> Handle(VeiculoQuery command)
        {
            var result = await _veiculoRepository.GetById(command.Id).ConfigureAwait(true);
            if(result != null)
            {
                return new QueryResult(true, result);
            }
            AddNotification("Veiculo", "Veiculo nao Encontrado.");
            return new QueryResult(false,base.Notifications);
        }

        public async ValueTask<IQueryResult> Handle(TodosVeiculosQuery command)
        {
            var result = await _veiculoRepository.GetAll().ConfigureAwait(true);
            return new QueryResult(true, result);
        }
    }
}
