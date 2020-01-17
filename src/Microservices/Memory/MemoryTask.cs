using System;
using System.Diagnostics;
using Tester.src.Common.Tasks.CompositeTasks.System;
using Tester.src.Microservices.Memory.Result;

namespace Tester.src.Microservices.Memory
{
    class MemoryTask : AbstractSystemTask<MemoryDataSetCollection, MemoryDataSet>
    {
        private Lazy<PerformanceCounter> memoryAvailableBytes;
        private Lazy<PerformanceCounter> memoryWorkingSet;
        private Lazy<PerformanceCounter> memoryCacheBytes;
        private Lazy<PerformanceCounter> memoryPoolNonpagedBytes;

        public MemoryTask(string processName) : base(processName)
        {

        }

        protected override void OnTick()
        {
        }

        public override void OnUpdateSharedElement(long sharedElement)
        {
            MemoryDataSet memoryDataSet = new MemoryDataSet(
                            memoryAvailableBytes.Value.NextValue(),
                            memoryWorkingSet.Value.NextValue(),
                            memoryCacheBytes.Value.NextValue(),
                            memoryPoolNonpagedBytes.Value.NextValue()
            );
            collection.InsertData(sharedElement, memoryDataSet);
        }

        protected override void BuildCounters()
        {
            memoryAvailableBytes = new Lazy<PerformanceCounter>(() =>
            {
                return new PerformanceCounter("Memory", "Available Bytes", null);
            });

            memoryWorkingSet = new Lazy<PerformanceCounter>(() =>
            {
                return new PerformanceCounter("Process", "Working Set", "_Total");
            });

            memoryCacheBytes = new Lazy<PerformanceCounter>(() =>
            {
                return new PerformanceCounter("Memory", "Cache Bytes", null);
            });

            memoryPoolNonpagedBytes = new Lazy<PerformanceCounter>(() =>
            {
                return new PerformanceCounter("Memory", "Pool Nonpaged Bytes", null);
            });
        }
    }
}
