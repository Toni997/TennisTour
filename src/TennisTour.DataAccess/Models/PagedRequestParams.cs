using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisTour.DataAccess.Models
{
    public class PagedRequestParams
    {
        private const int maxPageSize = 50;

        public int PageNumber { get; set; } = 1;
        private int _pageSize = 20;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > maxPageSize) ? maxPageSize : value;
        }
    }
}
