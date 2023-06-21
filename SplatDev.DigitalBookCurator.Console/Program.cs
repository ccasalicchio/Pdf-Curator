using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using SplatDev.DigitalBookCurator.Core.Context;
using SplatDev.DigitalBookCurator.Core.Models;
using SplatDev.DigitalBookCurator.Core.Repositories;
using SplatDev.DigitalBookCurator.Core.Services;

namespace SplatDev.DigitalBookCurator.Console;

class Program
{
    static async Task Main(string[] args)
    {
        var builder = new ConfigurationBuilder();
        builder.SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);


        IConfiguration config = builder.Build();
        var curatorSettings = config.GetSection("CuratorSettings");
        config.Bind(curatorSettings);

        CuratorSettings settings = new()
        {
            Origin = config["CuratorSettings:Origin"] ?? "",
            Destination = config["CuratorSettings:Destination"] ?? "",
            DeleteEmptyFolders = bool.Parse(config["CuratorSettings:DeleteEmptyFolders"] ?? "false")
        };

        using IHost host = Host.CreateDefaultBuilder(args)
            .ConfigureServices(services =>
            {
                services.AddDbContext<CuratorDbContext>(
        options => options.UseSqlite("name=ConnectionStrings:CuratorDb"));
                services.AddScoped<IBookRepository, BookRepository>();
                services.AddScoped<FileManagerService>();
            }).ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();
            }
            ).Build();
        await Process.ProcessFiles(host.Services, settings, args);
        host.Run();
    }
}