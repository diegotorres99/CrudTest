namespace WebCrud.DAL.Repository
{
    public interface IGenericRepository<TEntityModel> where TEntityModel : class
    {
        Task<bool> Insert(TEntityModel entity);
        Task<bool> Update(TEntityModel entity);
        Task<bool> Delete(int id);
        Task<IEnumerable<TEntityModel>> GetAll();
        Task<TEntityModel> GetById(int id);
    }
}
