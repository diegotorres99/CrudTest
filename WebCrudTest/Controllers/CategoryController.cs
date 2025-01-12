using Microsoft.AspNetCore.Mvc;
using WebCrud.BLL.Service;
using WebCrud.Models;
using WebCrudTest.Models.ViewModels;

namespace WebCrudTest.Controllers
{
    public class CategoryController : Controller
    {
        //private readonly ILogger _logger;
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
                var query = await _categoryService.GetAll(); // Fetch all categories from the service

                var list = query.Select(c => new VMCategory()
                {
                    Id = c.nIdCategori,           // Map the ID
                    Name = c.cNombCateg,        // Map the category name
                    isActive = c.cEsActiva        // Map the active status
                }).ToList();

                return StatusCode(StatusCodes.Status200OK, list); // Return the list as a 200 response
            }
            catch (Exception ex)
            {
                return new JsonResult(ex); // Return any exception as a JSON response
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
                // Check if the Category exists
                var existingCategory = await _categoryService.GetById(model.Id);
                if (existingCategory == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new
                    {
                        message = "Category not found"
                    });
                }

                // Update the category with the new values from the model
                existingCategory.cNombCateg = model.Name;
                existingCategory.cEsActiva = model.isActive;

                // Call the service to update the category in the database
                bool response = await _categoryService.Update(existingCategory);

                // Return response status based on success/failure
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                // Check if the Category exists
                var category = await _categoryService.GetById(id);
                if (category == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new
                    {
                        message = "Category not found"
                    });
                }

                // Call the service to delete the category from the database
                bool response = await _categoryService.Delete(id);

                // Return response status based on success/failure
                if (response)
                {
                    return StatusCode(StatusCodes.Status200OK, new
                    {
                        message = "Category deleted successfully"
                    });
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new
                    {
                        message = "Error deleting category"
                    });
                }
            }
            catch (Exception ex)
            {
                return new JsonResult(new { error = ex.Message });
            }
        }

    }
}
