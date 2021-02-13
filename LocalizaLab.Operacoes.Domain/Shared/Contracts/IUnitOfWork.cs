using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LocalizaLab.Operacoes.Domain.Shared.Contracts
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
        void Rollback();
        void Begin();
        bool CheckDatabaseStatus();
    }
}
