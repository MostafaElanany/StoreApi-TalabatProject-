using dataa.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 
using System.Threading.Tasks;

namespace Repos.Interfaces
{
    public interface IUnitWork
    {
        IGenericRep<TEntity, Tkey> Rep<TEntity, Tkey>() where TEntity : BaseEntity<Tkey>;
        Task<int> completeAsync();
   
    }
}
