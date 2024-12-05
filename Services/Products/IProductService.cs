using AppServices.Products.Create;
using AppServices.Products.Update;

namespace AppServices.Products;

public interface IProductService
{

    Task<ServiceResult<List<ProductDto>>> GetAllAsync();
    Task<ServiceResult<List<ProductDto>>> GetTopPriceProductAsync(int count);
    Task<ServiceResult<ProductDto?>> GetByIdAsync(int id);
    Task<ServiceResult<List<ProductDto>>> GetPagedAllAsync(int pageNumber, int pageSize); // New method

    Task<ServiceResult<CreateProductResponse>> CreateAsync(CreateProductRequest request);
    Task<ServiceResult> UpdateAsync(int id, UpdateProductRequest request);
    Task<ServiceResult> UpdateStockAsync(UpdateProductStockRequest request);
    Task<ServiceResult> DeleteAsync(int id);
}