using System.Collections.Generic;
using System.Linq;

namespace Tester.src.Common.Result
{
    abstract class AbstractTimeDataSet<E> : ITimeDataSet<E> where E : IDataSet
    {
        protected Dictionary<long, E> dataDictionary;

        public AbstractTimeDataSet()
        {
            this.dataDictionary = new Dictionary<long, E>();
        }

        public List<long> GetTimeStamps()
        {
            return dataDictionary.Keys.ToList();
        }

        public Dictionary<long, E> GetAllData()
        {
            return dataDictionary;
        }

        public E GetData(long millisecond)
        {
            return dataDictionary[millisecond];
        }

        public void InsertData(long millisecond, E dataSet)
        {
            dataDictionary.Add(millisecond, dataSet);
        }

        public abstract string GetName();

        public string Serialize()
        {
            string serialization = "";

            lock (dataDictionary)
            {
                foreach (KeyValuePair<long, E> entry in dataDictionary)
                {
                    serialization += "\"" + entry.Key + "\"" + ":" + entry.Value.Serialize();

                    if (!dataDictionary.Last().Equals(entry))
                    {
                        serialization += ",";
                    }
                }
            }

            return serialization;
        }
    }
}
