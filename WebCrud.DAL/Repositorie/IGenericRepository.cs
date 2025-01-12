using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebCrud.DAL.Repository
{
    public interface IGenericRepository<TEntityModel> where TEntityModel : class
    {
        // Insert a new entity
        Task<bool> Insert(TEntityModel entity);

        // Update an existing entity
        Task<bool> Update(TEntityModel entity);

        // Delete an entity by its ID
        Task<bool> Delete(int id);

        // Get all entities (e.g., all products, categories, etc.)
        Task<IEnumerable<TEntityModel>> GetAll();

        // Get an entity by its ID
        Task<TEntityModel> GetById(int id);
    }
}
