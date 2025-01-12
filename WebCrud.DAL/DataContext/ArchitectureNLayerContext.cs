using System.Data;
using System.Data.SqlClient;
using WebCrud.Models;

namespace ToDo.DAL.DataContext
{
    public class ArchitectureNLayerContext
    {
        private readonly string _connectionString;

        public ArchitectureNLayerContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Method to retrieve all products using the stored procedure
        public IEnumerable<Product> GetAllProducts()
        {
            var products = new List<Product>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("Usp_Sel_Co_Productos", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // No parameters needed for getting all products, just execute the stored procedure
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            products.Add(new Product
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("nIdProduct")),
                                Name = reader.GetString(reader.GetOrdinal("cNombProdu")),
                                Price = reader.GetDecimal(reader.GetOrdinal("nPrecioProd")),
                                CategoryId = reader.GetInt32(reader.GetOrdinal("nIdCategori")),
                                CategoryName = reader.GetString(reader.GetOrdinal("cNombCateg")) // Assuming the stored procedure returns category name
                            });
                        }
                    }
                }
            }

            return products;
        }

        // Fetch all products for a given category using Usp_Sel_Co_Productos
        public IEnumerable<Product> GetProductsByCategory(int categoryId)
        {
            var products = new List<Product>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("Usp_Sel_Co_Productos", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add input parameter for category ID
                    command.Parameters.AddWithValue("@CategoryId", categoryId);

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            products.Add(new Product
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("nIdProduct")),
                                Name = reader.GetString(reader.GetOrdinal("cNombProdu")),
                                Price = reader.GetDecimal(reader.GetOrdinal("nPrecioProd")),
                                CategoryId = reader.GetInt32(reader.GetOrdinal("nIdCategori")),
                                CategoryName = reader.GetString(reader.GetOrdinal("cNombCateg")) // Assuming the stored procedure returns category name
                            });
                        }
                    }
                }
            }

            return products;
        }

        // Insert a new category using Usp_Ins_Co_Categoria
        public bool InsertCategory(Category category)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("Usp_Ins_Co_Categoria", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add input parameters
                    command.Parameters.AddWithValue("@CategoryId", category.nIdCategori);
                    command.Parameters.AddWithValue("@CategoryName", category.cNombCateg);
                    command.Parameters.AddWithValue("@IsActive", category.cEsActiva);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
    }

}

