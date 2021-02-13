using LocalizaLab.Operacoes.Domain.Entities.Contratos;
using LocalizaLab.Operacoes.Domain.Shared.Repository;
using LocalizaLab.Operacoes.Infra.Context;
using LocalizaLab.Operacoes.Infra.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocalizaLab.Operacoes.Infra.Repositories
{
    public class ContratoRepository : BaseRepository<Contrato>, IContratoRepository
    {
        public ContratoRepository(OperacoesContext db) : base(db) { }
    }
}
