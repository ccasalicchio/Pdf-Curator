using Microsoft.Extensions.DependencyInjection;

using SplatDev.DigitalBookCurator.Core.Models;
using SplatDev.DigitalBookCurator.Core.Services;

namespace SplatDev.DigitalBookCurator.Console
{
    public class Process
    {
        private static bool CleanupFolders = false;
        public static async Task ProcessFiles(IServiceProvider hostProvider, CuratorSettings? settings, string[] args)
        {
            using IServiceScope serviceScope = hostProvider.CreateScope();
            IServiceProvider provider = serviceScope.ServiceProvider;
            var manager = provider.GetRequiredService<FileManagerService>();
            manager.OnBookAdded += Manager_OnBookAdded;
            manager.OnFolderDeleted += Manager_OnFolderDeleted;
            manager.OnBookCountChanged += Manager_OnBookCountChanged;
            manager.OnFolderCountChanged += Manager_OnFolderCountChanged;
            manager.OnFolderTraverse += Manager_OnFolderTraverse;
            manager.OnBookDeleted += Manager_OnBookDeleted;

            settings ??= new CuratorSettings();

            if (string.IsNullOrEmpty(settings.Origin))
                settings.Origin = new CuratorSettings().Origin;
            if (string.IsNullOrEmpty(settings.Destination))
                settings.Destination = new CuratorSettings().Destination;
            CleanupFolders = settings.DeleteEmptyFolders;

            string origin = settings.Origin;
            string destination = settings.Destination;
            if (args.Length > 0)
                origin = args[0];

            System.Console.WriteLine("Cataloging Books:\r\n++++++++++++++++++++++++++++++++");

            await manager.OrganizeFiles(origin, destination);

            System.Console.WriteLine("++++++++++++++++++++++++++++\r\nProcessing completed!");

            if (CleanupFolders)
            {
                System.Console.WriteLine("Deleting empty folders:\r\n++++++++++++++++++++++++++++++++");
                await manager.DeleteEmptyFolders(settings.Origin);
                System.Console.WriteLine("++++++++++++++++++++++++++++\r\nProcessing completed!");
            }
        }

        private static void Manager_OnBookDeleted(object? sender, string e)
        {
            System.Console.WriteLine($"Book Deleted: '{e}'");
        }

        private static void Manager_OnFolderTraverse(object? sender, string e)
        {
            System.Console.WriteLine($"Traversing: '{e}'");
        }

        private static void Manager_OnFolderCountChanged(object? sender, int e)
        {
            System.Console.Write($"#{e}. ");
        }

        private static void Manager_OnBookCountChanged(object? sender, int e)
        {
            System.Console.Write($"#{e}. ");
        }

        private static void Manager_OnFolderDeleted(object? sender, string e)
        {
            System.Console.WriteLine($"Folder Deleted: {e}");
        }

        private static void Manager_OnBookAdded(object? sender, string e)
        {
            System.Console.WriteLine(e);
        }
    }
}
