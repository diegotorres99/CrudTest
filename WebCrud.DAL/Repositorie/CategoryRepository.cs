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
                using (var connection = _databaseHelper.GetConnection()) // Assuming _databaseHelper provides the connection
                {
                    using (var command = new SqlCommand("Usp_Ins_Co_Categoria", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters to the command
                        command.Parameters.AddWithValue("@nIdCategori", entity.nIdCategori); // User-defined ID
                        command.Parameters.AddWithValue("@cNombCateg", entity.cNombCateg);
                        command.Parameters.AddWithValue("@cEsActiva", entity.cEsActiva);

                        //connection.Open(); // Open the connection
                        var rowsAffected = await command.ExecuteNonQueryAsync(); // Execute the command

                        return rowsAffected > 0; // Return true if rows were inserted
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception as required
                throw new ApplicationException("An error occurred while inserting the category.", ex);
            }
        }


        public async Task<bool> Delete(int id)
        {
            try
            {
                using (var connection = _databaseHelper.GetConnection())
                {
                    // Define the SQL command to delete a category by its ID
                    using (var command = new SqlCommand("Usp_Del_Co_Categoria", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameter for the category ID
                        command.Parameters.AddWithValue("@nIdCategori", id);

                        // Open connection and execute the query
                        await connection.OpenAsync();

                        // Execute the delete command and check if any rows were affected
                        int rowsAffected = await command.ExecuteNonQueryAsync();

                        // If rows were affected, deletion was successful
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any errors (logging, rethrow, etc.)
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


        public Task<Category> GetById(int id)
        {
            throw new NotImplementedException();
        }


        public Task<bool> Update(Category entity)
        {
            throw new NotImplementedException();
        }
    }
}
