using Microsoft.AspNetCore.Mvc;
using WebCrud.BLL.Service;
using WebCrudTest.Models.ViewModels;

namespace WebCrudTest.Controllers
{
    public class ProductController : Controller
    {
        //private readonly ILogger _logger;
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

        //[HttpPost]
        //public async Task<IActionResult> Update([FromBody] VMCategory model)
        //{
        //    try
        //    {
        //        var newModel = new Category()
        //        {
        //            nIdCategori = model.Id,
        //            cNombCateg = model.Nombre,
        //            cEsActiva = model.isActive
        //        };

        //        bool response = await _categoryService.Insert(newModel);

        //        return StatusCode(StatusCodes.Status200OK, new
        //        {
        //            valor = response
        //        });

        //    }
        //    catch (Exception ex)
        //    {
        //        return new JsonResult(ex);
        //    }
        //}
    }
}
