using Microsoft.AspNetCore.Mvc;
using WebCrud.BLL.Service;
using WebCrud.Models;
using WebCrudTest.Models.ViewModels;

namespace WebCrudTest.Controllers
{
    public class CategoryController : Controller
    {
        private ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;

        }
        public IActionResult Category()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                var query = await _categoryService.GetAll(); 

                var list = query.Select(c => new VMCategory()
                {
                    Id = c.nIdCategori,           
                    Name = c.cNombCateg,        
                    isActive = c.cEsActiva        
                }).ToList();

                return StatusCode(StatusCodes.Status200OK, list); 
            }
            catch (Exception ex)
            {
                return new JsonResult(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] VMCategory model)
        {
            try
            {
                var newModel = new Category()
                {
                    nIdCategori = model.Id,
                    cNombCateg = model.Name,
                    cEsActiva = model.isActive
                };

                bool response = await _categoryService.Insert(newModel);

                return StatusCode(StatusCodes.Status200OK, new
                {
                    valor = response
                });

            }
            catch (Exception ex)
            {
                return new JsonResult(ex);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] VMCategory model)
        {
            try
            {
                var existingCategory = await _categoryService.GetById(model.Id);
                if (existingCategory == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new
                    {
                        message = "Category not found"
                    });
                }

                existingCategory.cNombCateg = model.Name;
                existingCategory.cEsActiva = model.isActive;

                bool response = await _categoryService.Update(existingCategory);

                return StatusCode(StatusCodes.Status200OK, new
                {
                    valor = response
                });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { error = ex.Message });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                bool result = await _categoryService.Delete(id);

                if (result)
                {
                    return Ok(new { message = "Category deleted successfully" });
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error deleting category" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred: " + ex.Message });
            }
        }

    }
}
