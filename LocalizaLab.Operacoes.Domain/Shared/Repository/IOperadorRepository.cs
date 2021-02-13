using LocalizaLab.Operacoes.Domain.Contracts.Repository.Base;
using LocalizaLab.Operacoes.Domain.Entities.Clientes;
using LocalizaLab.Operacoes.Domain.Entities.Usuarios;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LocalizaLab.Operacoes.Domain.Shared.Repository
{
    public interface IOperadorRepository : IBaseRepository<Operador>
    {
        Task<Operador> GetOperadorByCPF(string cpf);
        Task<Operador> GetOperadorByMatricula(string matricula);
    }
}
