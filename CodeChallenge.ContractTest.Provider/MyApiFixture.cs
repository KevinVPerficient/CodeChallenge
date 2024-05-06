using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using CodeChallenge.Api;

namespace CodeChallenge.ContractTest.Provider
{
    public class MyApiFixture : IDisposable
    {

        private readonly IHost server;
        public Uri ServerUri { get; }
        public MyApiFixture()
        {
            ServerUri = new Uri("http://localhost:7278");
            server = Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseUrls(ServerUri.ToString());
                    webBuilder.UseStartup<TestStartup>();
                })
                .Build();
            server.Start();
        }

        public void Dispose()
        {
            server.Dispose();
        }
    }
}
