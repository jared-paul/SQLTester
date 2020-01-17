using Tester.src.Common.Result;

namespace Tester.src.Microservices.Memory.Result
{
    class MemoryDataSetCollection : AbstractTimeDataSet<MemoryDataSet>
    {
        public override string GetName()
        {
            return "MemoryDataSetCollection";
        }
    }
}
