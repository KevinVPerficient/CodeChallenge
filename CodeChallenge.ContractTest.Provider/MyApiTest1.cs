using PactNet.Infrastructure.Outputters;
using PactNet.Output.Xunit;
using PactNet.Verifier;
using Xunit.Abstractions;

namespace CodeChallenge.ContractTest.Provider
{
    public class MyApiTest1 : IClassFixture<MyApiFixture>
    {
        private readonly MyApiFixture _fixture;
        private readonly ITestOutputHelper _output;
        public MyApiTest1(MyApiFixture fixture, ITestOutputHelper output)
        {
            _fixture = fixture;
            _output = output;
        }
        [Fact]
        public async Task Test1()
        {
            var config = new PactVerifierConfig
            {
                Outputters = new List<IOutput>
                {
                    new XunitOutput(_output)
                },
            };

            IPactVerifier pactVerifier = new PactVerifier(config);
            var pactFile = new FileInfo("..\\..\\..\\..\\..\\CodeChallenge-Pact\\Contract.consumer\\pacts\\MyAPI consumer-ApiServices.json");
            pactVerifier.ServiceProvider("ApiServices", _fixture.ServerUri)
            .WithFileSource(pactFile)
            .WithProviderStateUrl(new Uri($"{_fixture.ServerUri}/api/Clients"))
            .Verify();
        }
    }
}
