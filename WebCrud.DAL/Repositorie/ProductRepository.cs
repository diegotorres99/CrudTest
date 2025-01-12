using System.Data;
using System.Data.SqlClient;
using WebCrud.DAL.Helpers;
using WebCrud.Models;

namespace WebCrud.DAL.Repository
{
    public class ProductRepository : IGenericRepository<Product>
    {
        private readonly IDatabaseHelper _databaseHelper;
        public ProductRepository(IDatabaseHelper databaseHelper)
        {
            _databaseHelper = databaseHelper;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            var products = new List<Product>();

            using (var connection = _databaseHelper.GetConnection())
            {
                using (var command = new SqlCommand("Usp_Sel_Co_Productos", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            products.Add(new Product
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("nIdProduct")),
                                Name = reader.GetString(reader.GetOrdinal("cNombProdu")),
                                Price = reader.GetDecimal(reader.GetOrdinal("nPrecioProd")),
                                CategoryName = reader.GetString(reader.GetOrdinal("cNombCateg"))
                            });
                        }
                    }
                }
            }

            return products;
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetById(int id)
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
