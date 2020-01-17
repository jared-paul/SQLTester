using Tester.src.Common.Result;

namespace Tester.src.Microservices.Memory.Result
{
    class MemoryDataSet : AbstractDataSet
    {
        public float AvailableBytes { get; }
        public float WorkingSet { get; }
        public float CacheBytes { get; }
        public float PoolNonpagedBytes { get; }

        public MemoryDataSet(float availableBytes, float workingSet, float cacheBytes, float poolNonpagedBytes)
        {
            AvailableBytes = availableBytes;
            WorkingSet = workingSet;
            CacheBytes = cacheBytes;
            PoolNonpagedBytes = poolNonpagedBytes;
        }

        public override string GetName()
        {
            return "MemoryDataSet";
        }
    }
}
