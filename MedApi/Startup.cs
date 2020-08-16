namespace MedApi.API
{
    using Application.Commands.AddMedication;
    using Application.Interfaces;
    using MediatR;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Repository;
    using Repository.Repositories;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddApplicationPart(typeof(Startup).Assembly);

            services.AddControllers();
            
            services.AddMediatR(typeof(AddMedicationCommandHandler));
            services.AddScoped<IMedicationRepository, MedicationRepository>();

            ConfigureDatabase(services);
        }

        public virtual void ConfigureDatabase(IServiceCollection services)
        {
            services.AddDbContext<MedApiDbContext>(options => 
                options.UseSqlServer(@"Integrated Security=True;Persist Security Info=False;Initial Catalog=MedApi;Data Source=(localdb)\mssqllocaldb"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
