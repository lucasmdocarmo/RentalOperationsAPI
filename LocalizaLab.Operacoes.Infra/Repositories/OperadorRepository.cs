using LocalizaLab.Operacoes.Domain.Entities.Usuarios;
using LocalizaLab.Operacoes.Domain.Shared.Repository;
using LocalizaLab.Operacoes.Infra.Context;
using LocalizaLab.Operacoes.Infra.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalizaLab.Operacoes.Infra.Repositories
{
    public class OperadorRepository : BaseRepository<Operador>, IOperadorRepository
    {
        public OperadorRepository(OperacoesContext db) : base(db) { }

        public async Task<Operador> GetOperadorByCPF(string cpf)
        {
            var result = await base.Search(user => user.Matricula == cpf).ConfigureAwait(true);

            return result.FirstOrDefault();
        }

        public async Task<Operador> GetOperadorByMatricula(string matricula)
        {
            var result = await base.Search(user => user.Matricula.Contains(matricula)).ConfigureAwait(true);

            return result.FirstOrDefault();
        }
    }
}