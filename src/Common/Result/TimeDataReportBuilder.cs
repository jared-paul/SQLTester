using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Tester.src.Common.Result
{
    class TimeDataReportBuilder
    {
        List<ITimeDataSet> timeDataSets;

        public TimeDataReportBuilder(List<ITimeDataSet> timeDataSets)
        {
            this.timeDataSets = timeDataSets;
        }

        public string GenerateJsonReport()
        {
            Dictionary<long, List<IDataSet>> dataDictionary = MergeGenericTimeDataSets(timeDataSets);

            string json = "";

            foreach (KeyValuePair<long, List<IDataSet>> entry in dataDictionary)
            {
                long timestamp = entry.Key;
                List<IDataSet> dataSets = entry.Value;

                json += "\"" + entry.Key + "\"" + ":{";

                foreach (IDataSet dataSet in dataSets)
                {
                    json += dataSet.Serialize();

                    if (dataSets.IndexOf(dataSet) != dataSets.Count - 1)
                    {
                        json += ",";
                    }
                }

                if (!dataDictionary.Last().Equals(entry))
                {
                    json += "},";
                }
            }

            return json;
        }

        private Dictionary<long, List<IDataSet>> MergeGenericTimeDataSets(List<ITimeDataSet> timeDataSets)
        {
            Dictionary<long, List<IDataSet>> merged = new Dictionary<long, List<IDataSet>>();

            foreach (dynamic timeDataSet in timeDataSets)
            {
                IDictionary dataDictionary = timeDataSet.GetAllData();

                foreach (DictionaryEntry entry in dataDictionary)
                {
                    long timestamp = (long)entry.Key;
                    IDataSet dataSet = (IDataSet)entry.Value;

                    if (!merged.ContainsKey(timestamp))
                    {
                        merged.Add(timestamp, new List<IDataSet>());
                    }

                    merged[timestamp].Add(dataSet);
                }
            }

            return merged;
        }
    }
}
