using System;
using System.Threading.Tasks;
using System.Configuration;
using MrGroupDataLoader.Uploader;
using Serilog;
using Serilog.Core;

namespace MrGroupDataLoader
{
    class Program
    {
        static Logger logger  = new LoggerConfiguration().WriteTo.File($@"{Environment.CurrentDirectory}\Logs\Log_{DateTime.Now.ToString("d")}.txt").CreateLogger();
        static async Task Main(string[] args)
        {
            logger.Information("start integration property mr-group");

            DateTime start = DateTime.Now;

            UploaderProperty uploader = new UploaderProperty(ConfigurationManager.AppSettings["resource"], ConfigurationManager.AppSettings["login"],
                                                             ConfigurationManager.AppSettings["password"], ConfigurationManager.AppSettings["odata"],
                                                             logger);
            Methods methods = new Methods(uploader, ConfigurationManager.AppSettings["propertyForDischarge"].Split(","), logger);
            await methods.ProcessingRealtyObjects();

            DateTime end = DateTime.Now;
            TimeSpan time = end - start;

            logger.Information($"complete integration property mr-group \n Time execution (minutes): {time.TotalMinutes}");
        }
    }
}
