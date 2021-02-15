using LocalizaLab.Operacoes.Domain.Contracts.Repository.Base;
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

    public class DadosItemContratoRepository : BaseRepository<DadosItemContrato>, IDadosItemContratoRepository
    {
        public DadosItemContratoRepository(OperacoesContext db) : base(db)
        {
        }
    }
    public class DevolucaoContratoRepository : BaseRepository<DadosDevolucao>, IDadosContratoDevolucaoRepository
    {
        public DevolucaoContratoRepository(OperacoesContext db) : base(db)
        {
        }
    }
    public class DadosPagamentoRepository : BaseRepository<DadosPagamentos>, IDadosPagamentosRepository
    {
        public DadosPagamentoRepository(OperacoesContext db) : base(db)
        {
        }
    }
}
