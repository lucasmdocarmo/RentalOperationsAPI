using LocalizaLab.Operacoes.Domain.Contracts.Repository.Base;
using LocalizaLab.Operacoes.Domain.Entities.Contratos;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocalizaLab.Operacoes.Domain.Shared.Repository
{
    public interface IContratoRepository : IBaseRepository<Contrato> { }
    public interface IDadosItemContratoRepository : IBaseRepository<DadosItemContrato> { }
    public interface IDadosContratoDevolucaoRepository : IBaseRepository<DadosDevolucao> { }
    public interface IDadosPagamentosRepository : IBaseRepository<DadosPagamentos> { }
}
