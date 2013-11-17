using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DefiantCode.WebApiProfiler;
using DefiantCode.WebApiProfiler.Tests.Helpers;
using System.Net.Http;
using System.Net;
using System.Threading;
using System.Linq;
using System.Web.Http;
using StackExchange.Profiling;

namespace DefiantCode.WebApiProfiler.Tests
{
    [TestClass]
    public class ProfilerMessageHandlerTests
    {
        [TestMethod]
        public void HandlerShouldAddProfileLocationHeader()
        {
            var profiler = new MiniProfiler("http://fake");
            var config = new HttpConfiguration();
            var route = config.Routes.MapHttpRoute("Profiler", "profiler/results/{id}", new { controller = "ProfilerResultsController" });
            var innerhandler = new FakeDelegatingHandler();
            innerhandler.Message = new HttpResponseMessage(HttpStatusCode.OK);
            var client = new HttpMessageInvoker(new ProfilerMessageHandler(route, profiler) { InnerHandler = innerhandler });
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "http://fake/test");
            requestMessage.SetConfiguration(config);
            
            var message = client.SendAsync(requestMessage, new CancellationToken(false)).Result;
            
            Assert.IsTrue(message.Headers.Contains(Constants.ProfilerResultsHeaderName), "HTTP Header {0} was not found in collection", Constants.ProfilerResultsHeaderName);
            var headerValue = message.Headers.GetValues(Constants.ProfilerResultsHeaderName).First();
            Assert.AreEqual<string>("http://fake/profiler/results/" + profiler.Id, headerValue, "Didn't receive expected header value");

        }
    }
}
