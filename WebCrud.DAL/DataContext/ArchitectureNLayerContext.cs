namespace ToDo.DAL.DataContext
{
    public class ArchitectureNLayerContext
    {
        private readonly string _connectionString;

        public ArchitectureNLayerContext(string connectionString)
        {
            _connectionString = connectionString;
        }
    }

}

