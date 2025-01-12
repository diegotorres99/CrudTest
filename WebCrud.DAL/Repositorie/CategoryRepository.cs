using System.Data;
using System.Data.SqlClient;
using WebCrud.DAL.Helpers;
using WebCrud.DAL.Repository;
using WebCrud.Models;

namespace WebCrud.DAL.Repositorie
{
    public class CategoryRepository : IGenericRepository<Category>
    {
        private readonly IDatabaseHelper _databaseHelper;

        public CategoryRepository(IDatabaseHelper databaseHelper)
        {
            _databaseHelper = databaseHelper;
        }

        public async Task<bool> Insert(Category entity)
        {
            try
            {
                using (var connection = _databaseHelper.GetConnection()) 
                {
                    using (var command = new SqlCommand("Usp_Ins_Co_Categoria", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@nIdCategori", entity.nIdCategori); 
                        command.Parameters.AddWithValue("@cNombCateg", entity.cNombCateg);
                        command.Parameters.AddWithValue("@cEsActiva", entity.cEsActiva);

                        var rowsAffected = await command.ExecuteNonQueryAsync(); 

                        return rowsAffected > 0; 
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while inserting the category.", ex);
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                using (var connection = _databaseHelper.GetConnection())
                {
                    using (var command = new SqlCommand("Usp_Del_Co_Categoria", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@nIdCategori", id);
                        int rowsAffected = await command.ExecuteNonQueryAsync();

                        return rowsAffected > 0 || rowsAffected == -1;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting category", ex);
            }
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            var categories = new List<Category>();

            using (var connection = _databaseHelper.GetConnection())
            {
                using (var command = new SqlCommand("Usp_Sel_Co_Categorias", connection)) // Assuming you have a stored procedure to get categories
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            categories.Add(new Category
                            {
                                nIdCategori = reader.GetInt32(reader.GetOrdinal("nIdCategori")),
                                cNombCateg = reader.GetString(reader.GetOrdinal("cNombCateg")),
                                cEsActiva = reader.GetBoolean(reader.GetOrdinal("cEsActiva"))
                            });
                        }
                    }
                }
            }

            return categories;
        }

        public async Task<Category> GetById(int id)
        {
            Category category = null;

            using (var connection = _databaseHelper.GetConnection())
            {
                using (var command = new SqlCommand("Usp_Sel_Co_CategoriaById", connection)) 
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@nIdCategori", SqlDbType.Int) { Value = id });

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync()) 
                        {
                            category = new Category
                            {
                                nIdCategori = reader.GetInt32(reader.GetOrdinal("nIdCategori")),
                                cNombCateg = reader.GetString(reader.GetOrdinal("cNombCateg")),
                                cEsActiva = reader.GetBoolean(reader.GetOrdinal("cEsActiva"))
                            };
                        }
                    }
                }
            }

            return category;
        }

        public async Task<bool> Update(Category entity)
        {
            try
            {
                using (var connection = _databaseHelper.GetConnection())
                {
                    using (var command = new SqlCommand("Usp_Upd_Co_Categoria", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@nIdCategori", SqlDbType.Int) { Value = entity.nIdCategori });
                        command.Parameters.Add(new SqlParameter("@cNombCateg", SqlDbType.NVarChar, 100) { Value = entity.cNombCateg });
                        command.Parameters.Add(new SqlParameter("@cEsActiva", SqlDbType.Bit) { Value = entity.cEsActiva });

                        int rowsAffected = await command.ExecuteNonQueryAsync();

                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while updating the category.", ex);
            }
        }

    }
}
