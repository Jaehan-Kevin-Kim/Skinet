namespace Core.Specifications
{
    public class ProductSpecParams
    {
        private const int MaxPageSize = 50;
        public int PageIndex { get; set; } = 1;

        private int _pageSize = 6; // It can be overwrited by user
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        // 위가 다 pagination을 위한 옵션값

        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public string Sort { get; set; }

    }
}