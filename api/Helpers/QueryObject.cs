using System;
using System.ComponentModel.DataAnnotations;

namespace api.Helpers
{
    public class QueryObject
    {
        public string? Symbol { get; set; } = null;
        public string? ComapnyName { get; set; } = null;
        public string? SortBy { get; set; } = null;
        public Boolean isDesending { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;

    }
}
