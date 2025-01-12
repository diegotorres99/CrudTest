using Microsoft.AspNetCore.Mvc;
using WebCrud.BLL.Service;
using WebCrudTest.Models.ViewModels;

namespace WebCrudTest.Controllers
{
    public class ProductController : Controller
    {
        private IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;

        }
        public IActionResult Product()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> getProducts()
        {
            try
            {
                var query = await _productService.GetAll();

                var list = query.Select(c => new VMProduct()
                {
                    IdProducto = c.Id,
                    NombreProducto = c.Name,
                    NombreCategoria = c.CategoryName,
                    PrecioProducto = c.Price,
                }).ToList();

                return StatusCode(StatusCodes.Status200OK, list);
            }
            catch (Exception ex)
            {
                return new JsonResult(ex);
            }
        }

    }
}
