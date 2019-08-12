using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace App
{
    public class HttpListenerServer : IServer
    {
        //监听器
        private readonly HttpListener _httpListener;
        //监听的url集合
        private readonly string[] _urls;

        public HttpListenerServer(params string[] urls)
        {
            _httpListener = new HttpListener();
            _urls = urls.Any() ? urls : new string[] { "http://localhost:5000/" }; 
        }

        /// <summary>
        /// 启动服务器
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        public async Task StartAsync(RequestDelegate handler)
        {
            #region 启动监听
            Array.ForEach(_urls, url => _httpListener.Prefixes.Add(url));
            _httpListener.Start();
            Console.WriteLine("Server started and is listening on:{0}", string.Join(';', _urls));
            #endregion

            while (true)
            {
                //获取监听的上下文
                var listenerContext = await _httpListener.GetContextAsync();


                var feature = new HttpListenerFeature(listenerContext);
                var features = new FeatureCollection()
                                .Set<IHttpRequestFeature>(feature)
                                .Set<IHttpResponseFeature>(feature);
                var httpContext = new HttpContext(features);

                await handler(httpContext);
                listenerContext.Response.Close();
            }


        }
    }

    public static partial class Extensions
    {
        /// <summary>
        /// IWebHostBuilder使用服务器的扩展方法
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="urls"></param>
        /// <returns></returns>
        public static IWebHostBuilder UseHttpListener(this IWebHostBuilder builder, params string[] urls)
        => builder.UseServer(new HttpListenerServer(urls));
    }
}
