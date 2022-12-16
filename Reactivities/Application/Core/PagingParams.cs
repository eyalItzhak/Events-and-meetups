using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Core
{
    public class PagingParams
    {
        private const int MaxPageSize = 50 ;

        public int PageNumber { get; set; }=1;

        private int _pageSIze=10; //defult value
        public int pageSize
        {
            get => _pageSIze;
            set => _pageSIze = (value>MaxPageSize) ?  MaxPageSize : value;
        }
        
    }
}