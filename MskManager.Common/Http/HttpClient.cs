using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MskManager.Common.Http
{
    public class HttpClient : IHttpClient
    {
        public T Get<T>(string uri, Func<string, T> parser)
        {
            var webRequest = WebRequest.Create(uri);
            using (var rs = webRequest.GetResponse())
            {
                using (var webResponse = new StreamReader(rs.GetResponseStream()))
                {
                    return parser(webResponse.ReadToEnd());
                }
            }
        }

        public async Task<T> GetAsync<T>(string uri, Func<Task<string>, Task<T>> parser)
        {
            var webRequest = WebRequest.Create(uri);
            using (var rs = await webRequest.GetResponseAsync())
            {
                using (var webResponse = new StreamReader(rs.GetResponseStream()))
                {
                    return await parser(webResponse.ReadToEndAsync());
                }
            }
        }
    }
}
