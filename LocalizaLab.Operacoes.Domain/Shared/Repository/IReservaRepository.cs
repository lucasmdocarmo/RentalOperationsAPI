using LocalizaLab.Operacoes.Domain.Contracts.Repository.Base;
using LocalizaLab.Operacoes.Domain.Entities.Contratos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LocalizaLab.Operacoes.Domain.Shared.Repository
{
    public interface IReservaRepository : IBaseRepository<Reserva>
    {
        Task<Reserva> GetByCodigoReserva(string codigoReserva);
    }
}
