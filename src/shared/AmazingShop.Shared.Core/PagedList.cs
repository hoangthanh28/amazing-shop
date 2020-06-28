using System.Collections.Generic;
using System.Linq;

namespace AmazingShop.Shared.Core.Model
{
    public class PagedList<T> where T : class
    {
        public Paging Paging { get; set; } = new Paging();
        public IEnumerable<T> Data { get; set; } = new List<T>();
        public PagedList<T> Clone()
        {
            PagedList<T> result = MemberwiseClone() as PagedList<T>;
            if (Data != null)
            {
                result.Data = new List<T>(Data);
            }
            return result;
        }
        public static PagedList<T> Create(IEnumerable<T> data)
        {
            return new PagedList<T>()
            {
                Data = data,
                Paging = new Paging()
                {
                    TotalItemsCount = data.Count(),
                    TotalPages = 1
                }
            };
        }
    }

    public class Paging
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 20;
        public int TotalItemsCount { get; set; } = 1;
        public int TotalPages { get; set; } = 1;
        public double TotalDuration { get; set; }
        public double? QueryDuration { get; set; }
        public double? CountDuration { get; set; }
        public double? BuildingQueryDuration { get; set; }
    }
}
