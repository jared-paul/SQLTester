using System;
using System.Diagnostics;
using Tester.src.Common.Tasks.CompositeTasks.System;
using Tester.src.Microservices.Disk.Result;

namespace Tester.src.Microservices.Disk
{
    class DiskTask : AbstractSystemTask<DiskDataSetCollection, DiskDataSet>
    {
        private Lazy<PerformanceCounter> avgDiskQueueLength;
        private Lazy<PerformanceCounter> readBytesPerSecond;
        private Lazy<PerformanceCounter> writeBytesPerSecond;
        private Lazy<PerformanceCounter> avgReadPerSecond;
        private Lazy<PerformanceCounter> avgWritePerSecond;

        public DiskTask(string processName) : base(processName)
        {
        }

        protected override void OnTick()
        {

        }

        public override void OnUpdateSharedElement(long updatedTime)
        {
            DiskDataSet diskDataSet = new DiskDataSet(
                            avgDiskQueueLength.Value.NextValue(),
                            readBytesPerSecond.Value.NextValue(),
                            writeBytesPerSecond.Value.NextValue(),
                            avgReadPerSecond.Value.NextValue(),
                            avgWritePerSecond.Value.NextValue()
                        );
            collection.InsertData(updatedTime, diskDataSet);
        }

        protected override void BuildCounters()
        {
            avgDiskQueueLength = new Lazy<PerformanceCounter>(() =>
            {
                return new PerformanceCounter("PhysicalDisk", "Avg. Disk Queue Length", "_Total");
            });

            readBytesPerSecond = new Lazy<PerformanceCounter>(() =>
            {
                return new PerformanceCounter("PhysicalDisk", "Disk Read Bytes/sec", "_Total");
            });

            writeBytesPerSecond = new Lazy<PerformanceCounter>(() =>
            {
                return new PerformanceCounter("PhysicalDisk", "Disk Write Bytes/sec", "_Total");
            });

            avgReadPerSecond = new Lazy<PerformanceCounter>(() =>
            {
                return new PerformanceCounter("PhysicalDisk", "Avg. Disk sec/Read", "_Total");
            });

            avgWritePerSecond = new Lazy<PerformanceCounter>(() =>
            {
                return new PerformanceCounter("PhysicalDisk", "Avg. Disk sec/Write", "_Total");
            });
        }
    }
}
