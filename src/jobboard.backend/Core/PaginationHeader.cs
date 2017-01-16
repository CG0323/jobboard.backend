using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jobboard.backend.Core
{
    public class PaginationHeader
    {
        public int TotalItems { get; set; }
        public PaginationHeader(int totalItems)
        {
            this.TotalItems = totalItems;
        }
    }
}
