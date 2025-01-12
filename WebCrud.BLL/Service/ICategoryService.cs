using WebCrud.Models;

namespace WebCrud.BLL.Service
{
    public interface ICategoryService
    {
        Task<bool> Insert(Category entity);
        Task<bool> Update(Category entity);
        Task<bool> Delete(int id);
        Task<IQueryable<Category>> GetAll();
        Task<Category> GetById(int id);
    }
}
