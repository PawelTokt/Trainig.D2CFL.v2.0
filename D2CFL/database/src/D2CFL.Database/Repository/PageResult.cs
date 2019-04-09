using System.Collections.Generic;

namespace D2CFL.Database.Repository
{
    public class PageResult<TItem>
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public IList<TItem> Items { get; set; }

        public int TotalCount { get; set; }
    }
}
