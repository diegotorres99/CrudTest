using WebCrud.Models;

namespace WebCrud.BLL.Service
{
    public interface IProductService
    {
        Task<bool> Insert(Product entity);
        Task<bool> Update(Product entity);
        Task<bool> Delete(int id);
        Task<IQueryable<Product>> GetAll();
    }
}
