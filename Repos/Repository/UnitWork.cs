using dataa.context;
using dataa.Entities;
using Repos.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repos.Repository
{
    public class UnitWork : IUnitWork
    {
        private readonly storecontext _context;
        private Hashtable _rep;

        public UnitWork(storecontext context)
        {
            _context = context;
        }
        public async Task<int> completeAsync()
        =>await _context.SaveChangesAsync();

        public IGenericRep<TEntity, Tkey> Rep<TEntity, Tkey>() where TEntity : BaseEntity<Tkey>
        {
            if(_rep == null )
                _rep = new Hashtable(); 
            var entitykey=typeof(TEntity).Name;
            if(!_rep.ContainsKey(entitykey))
            {
                var reptype=typeof(GenericRep<,>);

                var repInstance= Activator.CreateInstance(reptype.MakeGenericType(typeof(TEntity),typeof(Tkey)),_context);
                _rep.Add(entitykey, repInstance);
            }
            return (IGenericRep<TEntity, Tkey>)_rep[entitykey];

        }
    }
}
