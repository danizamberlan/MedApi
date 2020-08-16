namespace MedApi.ComponentTests
{
    using System.Net.Http;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.Extensions.Hosting;
    using Repository;

    public class MedicationTestFixture
    {
        public HttpClient Client;
        public MedApiDbContext DbContext;

        public MedicationTestFixture()
        {
            var hostBuilder = new HostBuilder()
                .ConfigureWebHost(webHost =>
                {
                    webHost.UseTestServer();
                    webHost.UseStartup<TestStartup>();
                });

            var host = hostBuilder.Start();

            Client = host.GetTestClient();

            DbContext = host.Services.GetService(typeof(MedApiDbContext)) as MedApiDbContext;
        }
    }
}
