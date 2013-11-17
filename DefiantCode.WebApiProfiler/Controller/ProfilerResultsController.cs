using StackExchange.Profiling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace DefiantCode.WebApiProfiler.Controller
{
    public class ProfilerResultsController : ApiController
    {
        public IEnumerable<Guid> Get(int max = 25, DateTime? start = null, DateTime? finish = null)
        {
            return MiniProfiler.Settings.Storage.List(max, start, finish);
        }
        public MiniProfiler Get(Guid id)
        {
            return MiniProfiler.Settings.Storage.Load(id);
        }
    }
}
