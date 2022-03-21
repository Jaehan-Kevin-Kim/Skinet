namespace API.Dtos
{
    public class ProductToReturnDto
    {

        // 기존 Product.cs에서 ProductTypeId, ProductBrandId 제거 하고, ProductType, ProductBrand는 string으로 받도록 변환 함
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public string ProductType { get; set; }
        public string ProductBrand { get; set; }
    }
}
