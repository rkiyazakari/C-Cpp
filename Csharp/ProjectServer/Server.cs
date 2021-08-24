using System;
using System.Net;
using System.Threading;
using ProjectServer.Handlers;
using Serilog;
using SharedProject.Utils;

namespace ProjectServer
{
    internal class Server
    {
        private readonly string _uri = $"http://{Constants.LocalHost}:{Constants.Port}/";
        private int _count;

        public Server()
        {
            _count = 0;
        }

        public void Start()
        {
            var listener = new HttpListener();
            listener.Prefixes.Add(_uri);
            listener.Start();
            // Console.WriteLine("Listening...");

            Log.Information($"Listening on port {Constants.Port}...");

            while (true)
            {
                var listenerContext = listener.GetContext();
                if (listenerContext.Request.IsWebSocketRequest)
                {
                    ProcessRequest(listenerContext);
                }
                else
                {
                    listenerContext.Response.StatusCode = 400;
                    listenerContext.Response.Close();
                }
            }
        }

        public async void ProcessRequest(HttpListenerContext listenerContext)
        {
            try
            {
                var webSocketContext = await listenerContext.AcceptWebSocketAsync(null);
                Interlocked.Increment(ref _count);
                Log.Information($"New connection from: {webSocketContext.Origin}");

                new Thread(new WebSocketHandler(webSocketContext).Receive).Start();
            }
            catch (Exception e)
            {
                listenerContext.Response.StatusCode = 500;
                listenerContext.Response.Close();
                Log.Error("Exception: {0}", e);
            }
        }
    }
}