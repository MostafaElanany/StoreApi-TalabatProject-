using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repos.Spectiction
{
    public class ProductSpectiction
    {
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }

        public string? Sort { get; set; }

        public int PageIndex { get; set; } = 1;

        private int _pageSize=5;
        private const int maxpagesize = 50;
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = (value > maxpagesize) ? maxpagesize : value; }
        }

        private string? _search;

        public string? Search
        {
            get { return _search; }
            set { _search = value?.Trim().ToLower(); }
        }





    }
}
