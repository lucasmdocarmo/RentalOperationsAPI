using LocalizaLab.Operacoes.Domain.Contracts.Repository.Base;
using LocalizaLab.Operacoes.Domain.Entities.Pagamentos;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocalizaLab.Operacoes.Domain.Shared.Repository
{
    public interface IPagamentoRepository : IBaseRepository<Pagamento>
    {
    }
}
