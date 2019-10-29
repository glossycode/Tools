using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeleteBO
{
    public class Logger
    {
        public static void Setup(bool append, bool ifNotAppendedDeleteFirst = true)
        {
            string logFile = @"DeleteBinObj-EventLog.txt";
            if (!append && ifNotAppendedDeleteFirst)
            {
                File.Delete(AppDomain.CurrentDomain.BaseDirectory + logFile);
            }

            Hierarchy hierarchy = (Hierarchy)LogManager.GetRepository();

            PatternLayout patternLayout = new PatternLayout();
            patternLayout.ConversionPattern = "%date [%thread] %-5level %logger - %message%newline";
            patternLayout.ConversionPattern = "%date %-5level %logger - %message%newline";
            patternLayout.ActivateOptions();

            RollingFileAppender roller = new RollingFileAppender();
            roller.AppendToFile = append;
            roller.File = logFile;
            roller.Layout = patternLayout;
            roller.MaxSizeRollBackups = 5;
            roller.MaximumFileSize = "1GB";
            roller.RollingStyle = RollingFileAppender.RollingMode.Size;
            roller.StaticLogFileName = true;
            roller.ActivateOptions();
            hierarchy.Root.AddAppender(roller);

            MemoryAppender memory = new MemoryAppender();
            memory.ActivateOptions();
            hierarchy.Root.AddAppender(memory);

            ConsoleAppender console = new ConsoleAppender();
            console.Layout = patternLayout;
            hierarchy.Root.AddAppender(console);
            
            hierarchy.Root.Level = Level.Info;
            hierarchy.Configured = true;
        }
    }
}
