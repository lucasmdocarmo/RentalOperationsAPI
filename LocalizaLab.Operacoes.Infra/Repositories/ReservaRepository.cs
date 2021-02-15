using LocalizaLab.Operacoes.Domain.Entities.Contratos;
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
    public class ReservaRepository : BaseRepository<Reserva>, IReservaRepository
    {
        public ReservaRepository(OperacoesContext db) : base(db) { }

        public async Task<Reserva> GetByCodigoReserva(string codigoReserva)
        {
            var result = await base.Search(x => x.CodigoReserva == codigoReserva).ConfigureAwait(true);

            return result.FirstOrDefault();
        }
    }
}
