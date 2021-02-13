using LocalizaLab.Operacoes.Domain.Entities.Clientes;
using LocalizaLab.Operacoes.Domain.Shared.Repository;
using LocalizaLab.Operacoes.Infra.Context;
using LocalizaLab.Operacoes.Infra.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace LocalizaLab.Operacoes.Infra.Repositories
{
    public class ClienteRepository : BaseRepository<Cliente>, IClienteRepository
    {
        public ClienteRepository(OperacoesContext db) : base(db) { }

        public async Task<IQueryable<Cliente>> GetClienteByCPF(string cpf)
        {
            var result = await base.Search(user => user.CPF.Numero == cpf).ConfigureAwait(true);

            return result.AsQueryable();
        }
    }
}
