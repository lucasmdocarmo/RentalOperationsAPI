using LocalizaLab.Operacoes.Domain.Contracts.Repository.Base;
using LocalizaLab.Operacoes.Domain.Entities.Clientes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalizaLab.Operacoes.Domain.Shared.Repository
{
    public interface IClienteRepository: IBaseRepository<Cliente>
    {
        Task<IQueryable<Cliente>> GetClienteByCPF(string cpf);
    }
}
