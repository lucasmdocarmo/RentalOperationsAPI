using LocalizaLab.Operacoes.Domain.Contracts.Repository;
using LocalizaLab.Operacoes.Domain.Entities;
using LocalizaLab.Operacoes.Infra.Context;
using LocalizaLab.Operacoes.Infra.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocalizaLab.Operacoes.Infra.Repositories
{
    public class VeiculoRepository : BaseRepository<Veiculos>, IVeiculoRepository
    {
        public VeiculoRepository(OperacoesContext db) : base(db) { }


    }
}
