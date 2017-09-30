using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MskManager.Common.Exceptions
{
    public class HttpClientException : Exception
    {
        public HttpClientException(string message, Exception exception) : base(message, exception)
        { }
    }
}
