using ApiCofidisMicroCredit.Model.Client;

namespace ApiCofidisMicroCredit.Interface
{
    public interface IDatabaseRepository
    {
        public List<T> ExecuteStoredProcedure<T>(string storedProcedureName, object? parameters = null);
    }
}
