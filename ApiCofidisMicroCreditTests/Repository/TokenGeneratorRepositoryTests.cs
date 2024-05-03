using ApiCofidisMicroCredit.Repository;
using AutoFixture;
using NUnit.Framework;

namespace ApiCofidisMicroCreditTests.Repository
{
    public class TokenGeneratorRepositoryTests
    {
        [Test]
        public void GenerateToken_Success()
        {
            TokenGeneratorRepository tokenGeneratorRepository = new();

            var result = tokenGeneratorRepository.GenerateToken("1234");

            Assert.That(result, Is.Not.Null);
        }
    }
}
