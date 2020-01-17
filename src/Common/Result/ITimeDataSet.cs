using System.Collections.Generic;

namespace Tester.src.Common.Result
{
    interface ITimeDataSet : IDataSet
    {
        List<long> GetTimeStamps();
    }

    /// <summary>
    /// An interface that will contain a collection of type IDataSet
    /// </summary>
    /// <typeparam name="E">of type IDataSet</typeparam>
    interface ITimeDataSet<E> : ITimeDataSet where E : IDataSet
    {
        /// <summary>
        /// Gets a dictionary which contains all of the data over a time period
        /// long represents the time
        /// E represents the data
        /// </summary>
        /// <returns>A dictionary of all data</returns>
        Dictionary<long, E> GetAllData();

        /// <summary>
        /// A function to produce the data at any given timestamp
        /// </summary>
        /// <param name="millisecond">Timestamp to look for</param>
        /// <returns>An IDataSet that was produced at that timestamp</returns>
        E GetData(long millisecond);

        /// <summary>
        /// A function to add data to the collection
        /// </summary>
        /// <param name="millisecond">timestamp of occurance</param>
        /// <param name="e">IDataSet data</param>
        void InsertData(long millisecond, E e);
    }
}
