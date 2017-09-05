using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MskManager.Common.Http
{
    public interface IHttpClient
    {
        T Get<T>(string uri, Func<string, T> parser);
        Task<T> GetAsync<T>(string uri, Func<Task<string>, Task<T>> parser);
    }
}
