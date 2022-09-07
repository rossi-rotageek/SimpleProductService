using Microsoft.AspNetCore.Mvc;
using SimpleService.BL;
using SimpleService.Core;
using SimpleService.Models;
using ILogger = Serilog.ILogger;

namespace SimpleService.Controllers;

[Route("api/product")]
[ApiController]
public class ProductController : BaseApiController
{
    private readonly IProductManager _productManager;

    public ProductController(IProductManager productManager)
    {
        _productManager = productManager;
    }

    [HttpGet]
    public async Task<IActionResult> ListProducts(string? searchTerms)
        => Ok(await _productManager.ListProducts(searchTerms ?? ""));

    [HttpGet]
    [Route("{productId:int}")]
    public async Task<IActionResult> GetProduct(int productId)
    {
        var getProductResult = await _productManager.GetProductById(productId);
        return getProductResult.Succeeded ? Ok(getProductResult.Value) : ResponseFromError(getProductResult.Error);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(CreateProductModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var product = await _productManager.CreateProduct(model.ToDto());
        return Ok(product);
    }
    
    [HttpDelete]
    [Route("{productId:int}")]
    public async Task<IActionResult> DeleteProduct(int productId)
    {
        var deleteProductResult = await _productManager.DeleteProduct(productId);
        return deleteProductResult.Succeeded ? Ok() : ResponseFromError(deleteProductResult.Error);
    }
    
    [HttpPut]
    [Route("{productId:int}")]
    public async Task<IActionResult> UpdateProduct([FromBody]CreateProductModel model, int productId)
    {
        var dto = model.ToDto();
        dto.Id = productId;
        
        var updateProductResult = await _productManager.UpdateProduct(dto);
        return updateProductResult.Succeeded ? Ok() : ResponseFromError(updateProductResult.Error);
    }
}