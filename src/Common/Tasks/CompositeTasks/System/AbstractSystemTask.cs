using Tester.src.Common.Result;
using Tester.src.Common.Tasks.CompositeTasks.Observer;

namespace Tester.src.Common.Tasks.CompositeTasks.System
{
    /// <summary>
    /// Represents a task dealing with the host system in anyway
    /// </summary>
    /// <typeparam name="T">IDataSet type</typeparam>
    abstract class AbstractSystemTask<T, E> : AbstractObserverTask<T, long> where T : ITimeDataSet<E>, new() where E : IDataSet
    {
        /// <summary>
        /// The name of the process you want to monitor
        /// </summary>
        protected string processName;
        /// <summary>
        /// The ITimeDataSet (collection of datasets) with a specified timestamp
        /// </summary>
        protected T collection = new T();

        public AbstractSystemTask(string processName) : base()
        {
            this.processName = processName;
            BuildCounters();
        }

        public override void Run()
        {
            while (!(state == State.DEAD))
            {
                OnTick();
            }

            ReportBack(collection, State.DEAD);
        }

        protected abstract void OnTick();

        protected override void OnReset()
        {
            ReportBack(collection, State.RESET);
            collection = new T();
        }

        protected override void OnFail()
        {
            ReportBack(collection, State.FAILED);
            collection = new T();
        }

        protected abstract void BuildCounters();
    }
}
