using System.Collections.Generic;

namespace Consumer.API.Models
{
    public class PaginatedList<T>
    {
        public List<T> Items { get; set; }
        public int TotalRowCount { get; set; }
        public int FilteredRowCount { get; set; }
    }
}
