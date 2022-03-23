using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetProductByIdAsync(int id);
        Task<IReadOnlyList<Product>> GetProductsAsync(); // 그냥 Product List를 리턴하는 method이기 때문에 쓰기나, 수정과 같은 기능이 필요없음. 따라서 IReadOnlyList라는 specific한 type으로 지정 해 줌.

        Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync();

        Task<IReadOnlyList<ProductType>> GetProductTypesAsync();

    }
}