using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeleteBinObj
{
    class Program
    {
        static log4net.ILog _logger;
        static bool _hadException = false;
        static int _Counter = 0;

        static void Main(string[] args)
        {
            Logger.Setup(false);

            _logger = log4net.LogManager.GetLogger(nameof(Program));
            _logger.Info("Deleting bin and obj folders. sure ? [Y]");
            ConsoleKeyInfo key = Console.ReadKey();
            Console.WriteLine();

            if (key.Key == ConsoleKey.Y)
            {
                try
                {
                    string d = Environment.CurrentDirectory;
                    DeleteSubFolders(d, new List<string>() { "bin", "obj" });
                }
                catch (System.Exception ex)
                {
                    _hadException = true;
                    Console.WriteLine(ex.Message);
                }
            }



            if (_hadException)
            {
                Console.WriteLine($"Had exception... press any key to terminate ");            
            }                       
            Console.WriteLine($"delete {_Counter} folders");
            Console.WriteLine($"press any key to terminate ");
            Console.ReadKey();            
        }
        private static void DeleteSubFolders(string path, List<string> folders)
        {            
            foreach (string d2 in Directory.EnumerateDirectories(path, "*", SearchOption.AllDirectories)
                .Where(s => folders.Contains(new DirectoryInfo(s).Name) && !s.Contains("\\packages\\")))
            {
                try
                {
                    _logger.Info($"Deleting : {d2}");
                    Directory.Delete(d2, true);
                    _Counter++;
                }
                catch (Exception ex)
                {
                    _hadException = true;
                    _logger.Error($"Error   : {ex}");
                }
            }
        }
    }
}
