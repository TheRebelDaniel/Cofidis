using ApiCofidisMicroCredit.Interface;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;

namespace ApiCofidisMicroCredit.Repository
{
    [ExcludeFromCodeCoverage]
    public class DatabaseRepository : IDatabaseRepository
    {
        private readonly string _connectionString;

        public DatabaseRepository()
        {
           var _config = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               .Build();

            _connectionString = _config.GetConnectionString("SQLConnectionString");
        }

        public List<T> ExecuteStoredProcedure<T>(string storedProcedureName, object? parameters = null)
        {
            List<T> results = new();

            using (SqlConnection connection = new(_connectionString))
            {
                var command = new SqlCommand(storedProcedureName, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                if (parameters != null)
                {
                    foreach (var prop in parameters.GetType().GetProperties())
                    {
                        command.Parameters.AddWithValue("@" + prop.Name, prop.GetValue(parameters));
                    }
                }

                connection.Open();
                command.ExecuteNonQuery();

                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    T result = Activator.CreateInstance<T>();
                    foreach (var prop in typeof(T).GetProperties())
                    {
                        if (!reader.IsDBNull(reader.GetOrdinal(prop.Name)))
                        {
                            prop.SetValue(result, reader[prop.Name]);
                        }
                    }
                    results.Add(result);
                }
            }
            return results;
        }
    }
}