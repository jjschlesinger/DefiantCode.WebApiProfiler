using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DefiantCode.WebApiProfiler.Controller;
using StackExchange.Profiling;
using System.Linq;
using StackExchange.Profiling.Storage;
using NSubstitute;
using System.Collections.Generic;

namespace DefiantCode.WebApiProfiler.Tests
{
    [TestClass]
    public class ProfilerResultsControllerTests
    {
        [TestMethod]
        public void GetAllProfilerIdsWithDefaultParams()
        {
            var expectedProfilerIds = new List<Guid>();
            var profiler = new MiniProfiler("http://fake");
            expectedProfilerIds.Add(profiler.Id);
            var storage = NSubstitute.Substitute.For<IStorage>();
            storage.List(25).Returns(expectedProfilerIds);

            MiniProfiler.Settings.Storage = storage;
            
            var controller = new ProfilerResultsController();
            var profilerIds = controller.Get().ToList();

            Assert.AreEqual<Guid>(expectedProfilerIds.First(), profilerIds.First());
        }

        [TestMethod]
        public void GetProfiler()
        {
            var expectedProfiler = new MiniProfiler("http://fake");
            var storage = NSubstitute.Substitute.For<IStorage>();
            storage.Load(expectedProfiler.Id).Returns(expectedProfiler);

            MiniProfiler.Settings.Storage = storage;

            var controller = new ProfilerResultsController();
            var profiler = controller.Get(expectedProfiler.Id);

            Assert.AreEqual<MiniProfiler>(expectedProfiler, profiler);
        }
    }
}
