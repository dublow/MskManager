using MskManager.Common.Exceptions;
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
        public string Get(string uri)
        {
            try
            {
                var webRequest = WebRequest.Create(uri);
                using (var rs = webRequest.GetResponse())
                {
                    using (var webResponse = new StreamReader(rs.GetResponseStream()))
                    {
                        return webResponse.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new HttpClientException(uri, ex);
            }
        }

        public async Task<string> GetAsync(string uri)
        {
            try
            {
                var webRequest = WebRequest.Create(uri);
                using (var rs = await webRequest.GetResponseAsync())
                {
                    using (var webResponse = new StreamReader(rs.GetResponseStream()))
                    {
                        return await webResponse.ReadToEndAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new HttpClientException($"Async > {uri}", ex);
            }
        }
    }
}
