using WebCrud.DAL.Repository;
using WebCrud.Models;

namespace WebCrud.BLL.Service
{
    public class ProductService : IProductService
    {
        private readonly IGenericRepository<Product> _productRepository;
        public ProductService(IGenericRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<IQueryable<Product>> GetAll()
        {
            var products = await _productRepository.GetAll();
            return products.AsQueryable(); 
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Insert(Product entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Product entity)
        {
            throw new NotImplementedException();
        }
    }
}
