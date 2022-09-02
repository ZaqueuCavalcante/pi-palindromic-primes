using Microsoft.EntityFrameworkCore;
using PiApi.Database;

namespace PiApi;

public class Startup
{
    public static string Cnn = "Server=35.199.95.198;Database=postgres;Uid=zaqueu;Pwd=???;";

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddRouting(options => options.LowercaseUrls = true);

        services.AddControllers();

        services.AddDbContext<PiApiDbContext>(options =>
        {
            options.UseNpgsql(Cnn);
            options.UseSnakeCaseNamingConvention();
        });
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseRouting();

        app.UseEndpoints(builder => { builder.MapControllers(); });
    }
}
