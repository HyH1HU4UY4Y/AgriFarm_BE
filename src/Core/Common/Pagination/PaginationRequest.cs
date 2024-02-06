using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedApplication.Pagination
{
    public class PaginationRequest
    {

        const int maxPageSize = 30;
        //[JsonProperty("number")]
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 5;

        public PaginationRequest(int? pageNumber = null, int? pageSize = null)
        {
            PageNumber = pageNumber??1;
            PageSize = pageSize??_pageSize;
        }

        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
    }
}
