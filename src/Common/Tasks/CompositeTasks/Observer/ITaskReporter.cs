using Tester.src.Common.Result;

namespace Tester.src.Common.Tasks.CompositeTasks.Observer
{
    /// <summary>
    /// The task reporter which all tasks report their results back to
    /// </summary>
    interface ITaskReporter
    {
        /// <summary>
        /// Reports back to the main reporter the dataset and tasktype once the task is finished
        /// </summary>
        /// <typeparam name="T">IDataSet type</typeparam>
        /// <param name="dataSet">The dataset to report back</param>
        /// <param name="taskType">The type of task that was consumed</param>
        void ReportBack<T>(T dataSet, CompositeTaskType taskType, State state) where T : IDataSet;
    }
}
