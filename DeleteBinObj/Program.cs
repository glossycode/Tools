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
        static log4net.ILog _log;
        static void Main(string[] args)
        {
            Logger.Setup();

            _log = log4net.LogManager.GetLogger(nameof(Program));
            _log.Info("deleting bin and obj folders. sure ? [Y]");
            ConsoleKeyInfo key = Console.ReadKey();
            if (key.Key == ConsoleKey.Y)
            {
                try
                {
                    string d = Environment.CurrentDirectory;
                    DeleteFolders(d, "bin");
                    DeleteFolders(d, "obj");
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        private static void DeleteFolders(string d, string folder)
        {
            foreach (string d2 in Directory.GetDirectories(d, folder, SearchOption.AllDirectories))
            {
                try
                {
                    _log.Info(d2);
                    Directory.Delete(d2, true); //
                }
                catch (Exception ex)
                {
                    _log.Error($"error deleting {d2}: {ex}");
                }                
            }
        }
    }
}
