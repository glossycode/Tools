using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeleteBO
{
    class Program
    {
        static log4net.ILog _logger;
        static void Main(string[] args)
        {
            Logger.Setup(false);

            _logger = log4net.LogManager.GetLogger(nameof(Program));
            _logger.Info("Deleting bin and obj folders. sure ? [Y]");
            ConsoleKeyInfo key = Console.ReadKey();

            if (key.Key == ConsoleKey.Y)
            {
                try
                {
                    string d = Environment.CurrentDirectory;
                    DeleteSubFolders(d, new List<string>() { "bin", "obj" });
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        private static void DeleteSubFolders(string path, List<string> folders)
        {            
            foreach (string d2 in Directory.EnumerateDirectories(path, "*", SearchOption.AllDirectories)
                .Where(s => folders.Contains(new DirectoryInfo(s).Name)))
            {
                try
                {
                    _logger.Info($"Deleting : {d2}");
                    Directory.Delete(d2, true);
                }
                catch (Exception ex)
                {
                    _logger.Error($"Error   : {ex}");
                }
            }
        }
    }
}
