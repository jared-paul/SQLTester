using Tester.src.Common.Result;

namespace Tester.src.Microservices.Disk.Result
{
    class DiskDataSet : AbstractDataSet
    {
        public float AvgDiskQueueLength { get; }
        public float ReadBytesPerSecond { get; }
        public float WriteBytesPerSecond { get; }
        public float AvgReadPerSecond { get; }
        public float AvgWritePerSecond { get; }

        public DiskDataSet(
            float avgDiskQueueLength,
            float readBytesPerSecond,
            float writeBytesPerSecond,
            float avgReadPerSecond,
            float avgWritePerSecond
            )
        {
            this.AvgDiskQueueLength = avgDiskQueueLength;
            this.ReadBytesPerSecond = readBytesPerSecond;
            this.WriteBytesPerSecond = writeBytesPerSecond;
            this.AvgReadPerSecond = avgReadPerSecond;
            this.AvgWritePerSecond = avgWritePerSecond;
        }

        public override string GetName()
        {
            return "DiskDataSet";
        }
    }
}
