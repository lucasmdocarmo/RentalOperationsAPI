using LocalizaLab.Operacoes.Domain.Entities.Carros;
using LocalizaLab.Operacoes.Domain.Shared.Repository;
using LocalizaLab.Operacoes.Infra.Context;
using LocalizaLab.Operacoes.Infra.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocalizaLab.Operacoes.Infra.Repositories
{
    public class AgendamentosRepository : BaseRepository<Agendamento>, IAgendamentosRepository
    {
        public AgendamentosRepository(OperacoesContext db) : base(db)
        {
        }
    }
}
