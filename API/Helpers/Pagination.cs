using System.Collections.Generic;

namespace API.Helpers
{
    public class Pagination<T> where T : class
    {
        public Pagination(int pageIndex, int pageSize, int count, IReadOnlyList<T> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
            Data = data;
        }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; } // 이게 filter를 지난 후 products 갯수의 값
        public IReadOnlyList<T> Data { get; set; }
    }

}