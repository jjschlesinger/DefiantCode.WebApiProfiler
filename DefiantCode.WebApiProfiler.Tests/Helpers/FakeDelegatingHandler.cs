using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DefiantCode.WebApiProfiler.Tests.Helpers
{
    class FakeDelegatingHandler : DelegatingHandler
    {
        public HttpResponseMessage Message { get; set; }
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (Message == null)
            {
                return base.SendAsync(request, cancellationToken);
            }
            return Task.Factory.StartNew(() => Message);
        }

    }
}
