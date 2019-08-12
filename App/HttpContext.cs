using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    /// <summary>
    /// 共享上下文
    /// </summary>
    public class HttpContext
    {
        public HttpRequest Request { get; }

        public HttpResponse Response { get; }

        public HttpContext(IFeatureCollection features)
        {
            Request = new HttpRequest(features);
            Response = new HttpResponse(features);
        }
    }

    /// <summary>
    /// 表示请求实体
    /// 会使用到IFeatureCollection接口
    /// </summary>
    public class HttpRequest
    {
        private readonly IHttpRequestFeature _feature;

        public HttpRequest(IFeatureCollection features) => _feature = features.Get<IHttpRequestFeature>();

        public Uri URl => _feature.Url;
        public NameValueCollection Headers => _feature.Headers;

        public Stream Body => _feature.Body;

       
    }

    /// <summary>
    /// 响应实体
    /// 需要用到IFeatureCollection接口
    /// </summary>
    public class HttpResponse
    {
        private readonly IHttpResponseFeature _feature;

        public HttpResponse(IFeatureCollection features) => _feature = features.Get<IHttpResponseFeature>();

        public int StatusCode
        {
            get => _feature.StatusCode;
            set => _feature.StatusCode = value;
        }

        public NameValueCollection Headers => _feature.Headers;

        public Stream Body => _feature.Body;
    }

    /// <summary>
    /// 响应的输出
    /// </summary>
    public static partial class Extensions
    {
        public static Task WriteAsync(this HttpResponse response,string contents)
        {
            var buffer = Encoding.UTF8.GetBytes(contents);
            return response.Body.WriteAsync(buffer, 0, buffer.Length);
        }
    }

}
