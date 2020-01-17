using Newtonsoft.Json;

namespace Tester.src.Common.Result
{
    abstract class AbstractDataSet : IDataSet
    {
        public abstract string GetName();

        public string Serialize()
        {

            return "\"" + GetName() + "\":" + JsonConvert.SerializeObject(this);
        }
    }
}
