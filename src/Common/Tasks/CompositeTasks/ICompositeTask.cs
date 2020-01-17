using Tester.src.Common.Result;
using Tester.src.Common.Tasks.CompositeTasks.Observer;

namespace Tester.src.Common.Tasks.CompositeTasks
{
    /// <summary>
    /// Represents the absolute base of a composite task
    /// </summary>
    interface ICompositeTask
    {
        /// <summary>
        /// The main block that gets called when executing the task
        /// </summary>
        void Run();

        /// <summary>
        /// sets the main shared task reporter
        /// </summary>
        /// <param name="taskReporter">the task reporter to report to</param>
        void SetTaskReporter(ITaskReporter taskReporter);

        /// <summary>
        /// The type of task
        /// </summary>
        /// <returns>asynchronous or synchronous</returns>
        CompositeTaskType GetTaskType();
    }

    /// <summary>
    /// Represents the generic type of composite task
    /// </summary>
    /// <typeparam name="T">IDataSet type</typeparam>
    interface ICompositeTask<T> : ICompositeTask where T : IDataSet
    {
        void ReportBack(T dataSet, State state);
    }
}
