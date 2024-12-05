using AppServices.Products;
using AppServices.Products.Create;
using AppServices.Products.Update;
using AppServices.Products.UpdateStock;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace App.Api.Controllers
{
    public class ProductsController(IProductService productService) : CustomBaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllAsync() => 
            CreateActionResult(await productService.GetAllAsync());
        //    var serviceResult = await productService.GetAllAsync();
        //    return CreateActionResult(serviceResult);


        [HttpGet("{pageNumber:int}/{pageSize:int}")]
        public async Task<IActionResult> GetPagedAllAsync(int pageNumber,int pageSize) => 
            CreateActionResult(await productService.GetPagedAllAsync(pageNumber, pageSize));


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync(int id) => 
            CreateActionResult(await productService.GetByIdAsync(id));


        [HttpPost] 
        public async Task<IActionResult> CreateAsync(CreateProductRequest request) => 
            CreateActionResult(await productService.CreateAsync(request));


        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAsync(int id, UpdateProductRequest request) => 
            CreateActionResult(await productService.UpdateAsync(id, request));
        //{
        //    var result = await productService.UpdateAsync(id, request);
        //    return CreateActionResult(result);
        //}

        [HttpPatch("stock")]
        public async Task<IActionResult> UpdateStockAsync(UpdateProductStockRequest request) => 
            CreateActionResult(await productService.UpdateStockAsync(request));


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id) => 
            CreateActionResult(await productService.DeleteAsync(id));
    }
}
