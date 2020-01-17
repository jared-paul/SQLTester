using System.Collections.Generic;
using Tester.src.Common.Result;
using Tester.src.Common.Tasks.Pool;

namespace Tester.src.Common.Tasks.CompositeTasks.Observer
{
    class TaskReporter : ITaskReporter
    {
        private readonly ITaskPool taskPool;
        private readonly List<IDataSet> returnedDataSetPool;

        public TaskReporter(ITaskPool taskPool)
        {
            this.taskPool = taskPool;
            this.returnedDataSetPool = new List<IDataSet>();
        }

        public void ReportBack<T>(T dataSet, CompositeTaskType taskType, State state) where T : IDataSet
        {
            returnedDataSetPool.Add(dataSet);

            if (IsPoolFull())
            {
                taskPool.Return(returnedDataSetPool, state);
                returnedDataSetPool.Clear();
            }
        }

        /// <summary>
        /// Checks if the pool is filled with all the expected result sets
        /// </summary>
        /// <returns>true if all the expected results have been returned</returns>
        private bool IsPoolFull()
        {
            if (returnedDataSetPool.Count == taskPool.GetNumberOfTasks())
            {
                return true;
            }

            return false;
        }
    }
}
