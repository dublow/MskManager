using Moq;
using MskManager.Common.Http;
using System;
using System.Threading.Tasks;

namespace MskManager.Common.Test.Http
{
    public class HttpClientBuilder
    {
        private readonly Mock<IHttpClient> _mock;

        private HttpClientBuilder()
        {
            _mock = new Mock<IHttpClient>();
        }

        public static HttpClientBuilder Create()
        {
            return new HttpClientBuilder();
        }

        public HttpClientBuilder WithGet(string uri, string result)
        {
            _mock
                .Setup(x => x.Get(uri))
                .Returns(result);

            return this;
        }

        public HttpClientBuilder WithGetAsync(string uri, string result)
        {
            _mock
                .Setup(x => x.GetAsync(uri))
                .Returns(Task.Run(() => result));

            return this;
        }

        public IHttpClient Build()
        {
            return _mock.Object;
        }
    }
}
