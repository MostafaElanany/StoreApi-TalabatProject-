using dataa.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repos.Spectiction
{
    public class SpectictionELV<TEntity,Tkey> where TEntity : BaseEntity<Tkey>
    {

        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpectiction<TEntity> specs)
        {
            var query = inputQuery;
            if (specs.Criteria != null)
            {
                query=query.Where(specs.Criteria);

            }

            if (specs.OrderBy != null)
            {
                query = query.OrderBy(specs.OrderBy);

            }
            if (specs.OrderByDescending != null)
            {
                query = query.OrderByDescending(specs.OrderByDescending);

            }
            if (specs.IsPaginated)
            {
                query = query.Skip(specs.Skip).Take(specs.Take);

            }


            query = specs.includes.Aggregate(query, (current, includeExp) => current.Include(includeExp));

            return query;
        }
    }
}
