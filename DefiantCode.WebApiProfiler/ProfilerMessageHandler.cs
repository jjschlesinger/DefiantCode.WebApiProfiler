using StackExchange.Profiling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Routing;

namespace DefiantCode.WebApiProfiler
{
    public class ProfilerMessageHandler : DelegatingHandler
    {
        private readonly IHttpRoute _profilerRoute;
        private readonly MiniProfiler _profiler;
        public ProfilerMessageHandler(IHttpRoute profilerRoute) : this(profilerRoute, MiniProfiler.Current)
        {

        }
        internal ProfilerMessageHandler(IHttpRoute profilerRoute, MiniProfiler profiler)
        {
            _profilerRoute = profilerRoute;
            _profiler = profiler;
        }
        protected override async System.Threading.Tasks.Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            var resultsUri = request.GetUrlHelper().Link("Profiler", new { id = _profiler.Id });
            var response = await base.SendAsync(request, cancellationToken);
            response.Headers.Add(Constants.ProfilerResultsHeaderName, resultsUri);
            return response;
        }
    }
}
