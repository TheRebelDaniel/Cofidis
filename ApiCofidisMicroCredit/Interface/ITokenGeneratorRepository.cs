namespace ApiCofidisMicroCredit.Interface
{
    public interface ITokenGeneratorRepository
    {
        public string GenerateToken(string clientId);
    }
}
