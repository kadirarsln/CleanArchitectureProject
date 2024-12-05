using System.Net;
using App.Repositories;
using App.Repositories.Products;
using AppServices.Products.Create;
using AppServices.Products.Update;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AppServices.Products;

public class ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork, IMapper mapper) : IProductService
{
    public async Task<ServiceResult<List<ProductDto>>> GetTopPriceProductAsync(int count)
    {
        var product = await productRepository.GetTopPriceProductAsync(count);
        //var productDto = products.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock)).ToList();
        var productDto = mapper.Map<List<ProductDto>>(product);

        return ServiceResult<List<ProductDto>>.Success(productDto);
    }
    public async Task<ServiceResult<ProductDto?>> GetByIdAsync(int id)
    {
        var product = await productRepository.GetByIdAsync(id);

        if (product is null)
        {
            return ServiceResult<ProductDto?>.Failure("Product not found", HttpStatusCode.NotFound);
        }
        //var productDto = new ProductDto(product.Id, product.Name, product.Price, product.Stock);
        var productDto = mapper.Map<ProductDto>(product);

        return ServiceResult<ProductDto?>.Success(productDto);
    }
    public async Task<ServiceResult<CreateProductResponse>> CreateAsync(CreateProductRequest request)
    {
        var anyProduct = await productRepository.Where(p => p.Name == request.Name).AnyAsync();
        if (anyProduct)
        {
            return ServiceResult<CreateProductResponse>.Failure("Ürün ismi veritabanında bulunmaktadır.", HttpStatusCode.BadRequest);
        }

        var product = new Product
        {
            Name = request.Name,
            Price = request.Price,
            Stock = request.Stock
        };

        await productRepository.AddAsync(product);
        await unitOfWork.SaveChangesAsync();

        return ServiceResult<CreateProductResponse>.SuccessAsCreated(new CreateProductResponse(product.Id), $"api/products/{product.Id}");
    }
    public async Task<ServiceResult> UpdateAsync(int id, UpdateProductRequest request)
    {
        //Fast fail: Önce başarısız durumu döneriz. Daha sonra olumlu durumları döneriz.

        //Guard Clause : else olmadan yazılan if bloğu.

        var product = await productRepository.GetByIdAsync(id);

        if (product is null)
        {
            return ServiceResult.Failure("Product not found", HttpStatusCode.NotFound);
        }

        product.Name = request.Name;
        product.Price = request.Price;
        product.Stock = request.Stock;

        productRepository.Update(product);
        await unitOfWork.SaveChangesAsync();

        return ServiceResult.Success(HttpStatusCode.NoContent);
    }
    public async Task<ServiceResult> UpdateStockAsync(UpdateProductStockRequest request)
    {
        var product = await productRepository.GetByIdAsync(request.ProductId);

        if (product is null)
        {
            return ServiceResult.Failure("Product not found", HttpStatusCode.NotFound);
        }

        product.Stock = request.Stock;

        productRepository.Update(product);
        await unitOfWork.SaveChangesAsync();

        return ServiceResult.Success(HttpStatusCode.NoContent);
    }
    public async Task<ServiceResult> DeleteAsync(int id)
    {
        var product = await productRepository.GetByIdAsync(id);

        if (product is null)
        {
            return ServiceResult.Failure("Product not found", HttpStatusCode.NotFound);
        }

        productRepository.Delete(product);
        await unitOfWork.SaveChangesAsync();

        return ServiceResult.Success(HttpStatusCode.NoContent);
    }
    public async Task<ServiceResult<List<ProductDto>>> GetAllAsync()
    {
        var products = await productRepository.GetAll().ToListAsync();
        //var productDto = products.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock)).ToList();
        var productsDto = mapper.Map<List<ProductDto>>(products);

        return ServiceResult<List<ProductDto>>.Success(productsDto);
    }
    public async Task<ServiceResult<List<ProductDto>>> GetPagedAllAsync(int pageNumber, int pageSize)
    {
        // 1-10 ilk 10 kayıt skip(0).Take(10)
        // 11-20 11. kayıttan başla skip(10).Take(10)
        // 21-30 21. kayıttan başla skip(20).Take(10)

        var products = await productRepository.GetAll()
            .Skip((pageNumber - 1) * pageSize)  //Atlama sayısı burada hesaplanır.
            .Take(pageSize)   //pageSize kadarını alacaksın.
            .ToListAsync();

        //var productDto = products.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock)).ToList();
        var productsDto = mapper.Map<List<ProductDto>>(products);

        return ServiceResult<List<ProductDto>>.Success(productsDto);
    }
}
