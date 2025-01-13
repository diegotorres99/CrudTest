using WebCrud.DAL.Repository;
using WebCrud.Models;

public interface ICategoryRepository : IGenericRepository<Category>
{
    Task<int> GetNextCategoryId();
    Task<bool> Delete(int id);
    Task<Category> GetById(int id);
    Task<IEnumerable<Category>> GetAll();
    Task<bool> Update(Category entity);
    Task<bool> Insert(Category entity);
}
