using dataa.context;
using dataa.Entities;
using Microsoft.EntityFrameworkCore;
using Repos.Interfaces;
using Repos.Spectiction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repos.Repository
{
    public class GenericRep<TEntity, Tkey> : IGenericRep<TEntity, Tkey>  where TEntity : BaseEntity<Tkey>
    {
        private readonly storecontext _context;

        public GenericRep(storecontext context) 
        {
            _context = context;
        }
        public async Task addasync(TEntity entity)
        =>await _context.Set<TEntity>().AddAsync(entity);

        public async Task<int> CountSpectionssasync(ISpectiction<TEntity> specs)
        {
            return await ApplySpectiction(specs).CountAsync();
        }

        public void delete(TEntity entity)
           => _context.Set<TEntity>().Remove(entity);
        

        public async Task<IReadOnlyList<TEntity>> getallasync()
             => await _context.Set<TEntity>().ToListAsync();

        public async Task<IReadOnlyList<TEntity>> getallWithSpecsasync(ISpectiction<TEntity> specs)
        {
            return await ApplySpectiction(specs).ToListAsync();
        }

        public async Task<TEntity> getByIdAsync(int id)
            =>  await _context.Set<TEntity>().FindAsync(id);

        public async Task<TEntity> getWithSpecsByIdAsync(ISpectiction<TEntity> specs)
        {
           return await ApplySpectiction(specs).FirstOrDefaultAsync();

        }

        public void update(TEntity entity)
            => _context.Set<TEntity>().Update(entity);

        private IQueryable<TEntity> ApplySpectiction(ISpectiction<TEntity> specs)
            => SpectictionELV<TEntity, Tkey>.GetQuery(_context.Set<TEntity>(), specs);


    }
}
