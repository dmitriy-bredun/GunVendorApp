using Microsoft.Extensions.DependencyInjection;

namespace SDK.xUnitTests
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IGunRepository, GunRepositoryInMemory>();
        }
    }
}
