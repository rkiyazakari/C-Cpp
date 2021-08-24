using ProjectServer.Services;
using Serilog;

namespace ProjectServer
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();


            DbServices.Init();


            var server = new Server();
            server.Start();
        }
    }
}