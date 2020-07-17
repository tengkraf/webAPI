using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class PagedResult<T>
    {
        public List<T> Results { get; set; }
        public int TotalRowCount { get; set; }
    }
}
