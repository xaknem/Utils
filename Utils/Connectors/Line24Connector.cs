using System.Collections.Generic;
using System.Net.Http;
using Utils.Entities;
using Utils.Interfaces;
using Utils.Logic.Authorization;

namespace Utils.Connectors
{
    public class Line24Connector : ILine24Connector<Call>
    {
        private readonly HttpClient _client = new HttpClient();

        public Line24Connector(Credentials credentials)
        {
            _client.DefaultRequestHeaders
                .Add("Authorization", "Basic " + new CredentialsEncoder().Encode(credentials));
        }
        

        public ICollection<Call> GetMissedCalls()
        {
            throw new System.NotImplementedException();
        }

        public ICollection<Call> GetOutgoingCallsByNumber(string number)
        {
            throw new System.NotImplementedException();
        }

        public ICollection<Call> GetIncomingCallsByNumber(string number)
        {
            throw new System.NotImplementedException();
        }
    }
}