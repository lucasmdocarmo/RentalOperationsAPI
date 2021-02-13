using LocalizaLab.Operacoes.Domain.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LocalizaLab.Operacoes.Domain.Contracts.Repository.Base
{
    public interface IBaseRepository<TEntity> : IDisposable where TEntity : BaseEntity
    {
        Task Add(TEntity entity);
        Task<TEntity> GetById(Guid id);
        Task<IQueryable<TEntity>> GetAllFromQuery();
        Task<IEnumerable<TEntity>> GetAll();
        Task Update(TEntity entity);
        Task Remove(Guid id);
        Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> predicate);
        Task<bool> SaveChanges();
    }
}
