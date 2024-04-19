using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace service.Helper
{
    public class PaginatedResultDto<T>
    {
        public PaginatedResultDto(int pageindex, int pageSize, int count, IReadOnlyList<T> data)
        {
            Pageindex = pageindex;
            PageSize = pageSize;
            Count = count;
            Data = data;
        }

        public int Pageindex { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }

        public IReadOnlyList<T> Data { get; set; }
    }
}
