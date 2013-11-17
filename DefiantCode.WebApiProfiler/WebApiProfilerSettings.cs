using StackExchange.Profiling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace DefiantCode.WebApiProfiler
{
    public static class WebApiProfilerSettings
    {
        public static void Configure(HttpConfiguration config)
        {
            var route = config.Routes.MapHttpRoute("Profiler", "profiler/results/{id}", new { controller = "ProfilerResultsController" });
            var ignoredPaths = new List<string>(MiniProfiler.Settings.IgnoredPaths);
            ignoredPaths.Add("/profiler/");
            MiniProfiler.Settings.IgnoredPaths = ignoredPaths.ToArray();
            config.MessageHandlers.Add(new ProfilerMessageHandler(route, MiniProfiler.Current));
        }
    }
}
