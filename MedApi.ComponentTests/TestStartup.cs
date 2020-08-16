namespace MedApi.ComponentTests
{
    using API;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Repository;

    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration) : base(configuration) { }

        public override void ConfigureDatabase(IServiceCollection services)
        {
            services.AddDbContext<MedApiDbContext>(options => options.UseInMemoryDatabase("MedApi"));
        }
    }
}