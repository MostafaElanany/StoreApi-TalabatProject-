using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repos.Spectiction
{
    public class BaseSpectiction<T> : ISpectiction<T>
    {

        public BaseSpectiction(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }
        public Expression<Func<T, bool>> Criteria { get; }

        public List<Expression<Func<T, object>>> includes { get; } = new List<Expression<Func<T, object>>>();

        public Expression<Func<T, object>> OrderBy { get; private set; }

        public Expression<Func<T, object>> OrderByDescending { get; private set; }

        public int Take { get; private set; }

        public int Skip { get; private set; }

        public bool IsPaginated { get; private set; }

        protected void AddInclude(Expression<Func<T, object>> includeExP)
         => includes.Add(includeExP);

        protected void AddOrderBy(Expression<Func<T, object>> orderBy)
            =>OrderBy = orderBy;
        protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescending)
        => OrderByDescending = orderByDescending;


        protected void ApplyPagination(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPaginated = true;


        }


    }
}
