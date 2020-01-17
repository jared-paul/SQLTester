using Tester.src.Common.Result;

namespace Tester.src.Microservices.Disk.Result
{
    class DiskDataSetCollection : AbstractTimeDataSet<DiskDataSet>
    {
        public DiskDataSetCollection() : base()
        { }

        public override string GetName()
        {
            return "DiskDataSetCollection";
        }
    }
}
