using ApiCofidisMicroCredit.Model.Client;
using ApiCofidisMicroCredit.Model.Login.Shared;
using ApiCofidisMicroCredit.Repository;
using AutoFixture;
using NUnit.Framework;

namespace ApiCofidisMicroCreditTests.Repository
{
    public class ChaveMovelDigitalRepositoryTests
    {
        private readonly Fixture fixture = new();

        [Test]
        public void Authenticate_Success()
        {
            ChaveMovelDigitalRepository chaveMovelDigitalRepository = new();

            var result = chaveMovelDigitalRepository.Authenticate(fixture.Create<CMDRequest>());

            Assert.IsNotNull(result);
            Assert.That(result.GetType(), Is.EqualTo(typeof(Client)));
        }
    }
}
