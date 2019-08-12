using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App
{
    public interface IWebHost
    {
        Task StartAsync();
    }

    /// <summary>
    /// 宿主
    /// </summary>
    public class WebHost : IWebHost
    {
        private readonly IServer _server;
        private readonly RequestDelegate _handler;

        public WebHost(IServer server,RequestDelegate handler)
        {
            _server = server;
            _handler = handler;
        }

        public Task StartAsync() => _server.StartAsync(_handler);
    }
}
