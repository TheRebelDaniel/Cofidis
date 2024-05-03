using ApiCofidisMicroCredit.Interface;
using ApiCofidisMicroCredit.Model.Client;
using ApiCofidisMicroCredit.Model.Login.Shared;
using AutoFixture;
using Microsoft.AspNetCore.Mvc;

namespace ApiCofidisMicroCredit.Repository
{
    public class ChaveMovelDigitalRepository : IChaveMovelDigitalRepository
    {
        public Client Authenticate(CMDRequest request)
        {
            //Returns fake Client for test porpuses
            Fixture fixture = new();
            return fixture.Create<Client>();
        }
    }
}
