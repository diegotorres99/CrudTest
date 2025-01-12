using System.Data.SqlClient;

namespace WebCrud.DAL.Helpers
{
    public interface IDatabaseHelper
    {
        SqlConnection GetConnection();
    }
}
