using WebCrud.Models;

namespace WebCrud.BLL.Service
{
    public class CategoryService : ICategoryService
    {

        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<bool> Insert(Category entity)
        {
            return await _categoryRepository.Insert(entity);
        }

        public async Task<bool> Delete(int id)
        {
            return await _categoryRepository.Delete(id);
        }

        public async Task<IQueryable<Category>> GetAll()
        {
            var categories = await _categoryRepository.GetAll();
            return categories.AsQueryable();
        }

        public async Task<bool> Update(Category entity)
        {
            var existingCategory = await _categoryRepository.GetById(entity.nIdCategori);
            if (existingCategory == null)
            {
                throw new KeyNotFoundException($"Category with ID {entity.nIdCategori} not found.");
            }

            existingCategory.cNombCateg = entity.cNombCateg;
            existingCategory.cEsActiva = entity.cEsActiva;

            return await _categoryRepository.Update(existingCategory);
        }

        public async Task<Category> GetById(int id)
        {
            var category = await _categoryRepository.GetById(id);
            if (category == null)
            {
                throw new KeyNotFoundException($"Category with ID {id} not found.");
            }

            return category;
        }

        public async Task<int> GetNextCategoryId()
        {
            var idCategory = await _categoryRepository.GetNextCategoryId();
            if (idCategory <= 0)
            {
                throw new KeyNotFoundException($"Next category ID not found.");
            }

            return idCategory;
        }
    }
}
