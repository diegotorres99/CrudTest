using WebCrud.DAL.Repository;
using WebCrud.Models;

namespace WebCrud.BLL.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly IGenericRepository<Category> _categoryRepository;
        public CategoryService(IGenericRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<bool> Insert(Category entity)
        {
            // Call the Insert method from the repository to add a new category
            return await _categoryRepository.Insert(entity);
        }

        public async Task<bool> Delete(int id)
        {
            // Call the Delete method from the repository to delete a category by ID
            return await _categoryRepository.Delete(id);
        }

        public async Task<IQueryable<Category>> GetAll()
        {
            var categories = await _categoryRepository.GetAll();
            return categories.AsQueryable();
        }

        public Task<bool> Update(Category entity)
        {
            throw new NotImplementedException();
        }

        public Task<Category> GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
