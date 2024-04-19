using dataa.Entities;
using Repos.Spectiction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repos.Interfaces
{
    public interface IGenericRep<TEntity,Tkey> where TEntity : BaseEntity<Tkey>

    {
        Task<TEntity> getByIdAsync(int id);
        Task<IReadOnlyList<TEntity>> getallasync();
        Task<TEntity> getWithSpecsByIdAsync(ISpectiction<TEntity> specs);
        Task<IReadOnlyList<TEntity>> getallWithSpecsasync(ISpectiction<TEntity> specs);
        Task<int> CountSpectionssasync(ISpectiction<TEntity> specs);
        Task addasync  (TEntity entity);
        void update(TEntity entity);
        void delete(TEntity entity);



    }
}
