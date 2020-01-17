using Tester.src.Common.Result;

namespace Tester.src.Microservices.Listener.Result
{
    class ListenerDataSet : AbstractDataSet
    {
        public string TestName { get; }

        public ListenerDataSet(string testName)
        {
            this.TestName = testName;
        }

        public override string GetName()
        {
            return "Listener DataSet";
        }
    }
}
