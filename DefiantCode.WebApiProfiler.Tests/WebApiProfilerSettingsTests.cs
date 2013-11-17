using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace DefiantCode.WebApiProfiler.Tests
{
    [TestClass]
    public class WebApiProfilerSettingsTests
    {
        [TestMethod]
        public void ConfigureShouldAddHandlerToHttpConfiguration()
        {
            var config = new System.Web.Http.HttpConfiguration();
            WebApiProfilerSettings.Configure(config);
            Assert.IsTrue(config.MessageHandlers.Any(x => x is ProfilerMessageHandler), "ProfilerMessageHandler was not found in MessageHandlers collection");
        }

        [TestMethod]
        public void ConfigureShouldAddRouteHttpConfiguration()
        {
            var config = new System.Web.Http.HttpConfiguration();
            WebApiProfilerSettings.Configure(config);
            Assert.IsTrue(config.Routes.ContainsKey("Profiler"), "There should be a route named Profiler in the collection");
            Assert.AreEqual<string>("profiler/results/{id}", config.Routes["Profiler"].RouteTemplate, "Route parameter is invalid");
        }
    }
}
