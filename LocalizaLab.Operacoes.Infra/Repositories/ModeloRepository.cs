using LocalizaLab.Operacoes.Domain.Entities;
using LocalizaLab.Operacoes.Domain.Shared.Repository;
using LocalizaLab.Operacoes.Infra.Context;
using LocalizaLab.Operacoes.Infra.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocalizaLab.Operacoes.Infra.Repositories
{
    public class ModeloRepository : BaseRepository<Modelo>, IModeloRepository
    {
        public ModeloRepository(OperacoesContext db) : base(db) { }
    }
}
