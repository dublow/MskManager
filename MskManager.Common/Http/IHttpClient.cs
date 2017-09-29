using System;
using System.Threading.Tasks;

namespace MskManager.Common.Http
{
    public interface IHttpClient
    {
        string Get(string uri);
        Task<string> GetAsync(string uri);
    }
}
