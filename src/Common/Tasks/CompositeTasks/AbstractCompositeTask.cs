using Tester.src.Common.Result;
using Tester.src.Common.Tasks.CompositeTasks.Observer;

namespace Tester.src.Common.Tasks.CompositeTasks
{
    /// <summary>
    /// The base implementation of ICompositeTask
    /// </summary>
    /// <typeparam name="T">IDataSet type</typeparam>
    abstract class AbstractCompositeTask<T> : ICompositeTask<T> where T : IDataSet
    {
        protected ITaskReporter taskReporter;

        protected AbstractCompositeTask()
        {

        }

        public abstract void Run();

        public virtual void ReportBack(T dataSet, State state)
        {
            this.taskReporter.ReportBack(dataSet, GetTaskType(), state);
        }

        public void SetTaskReporter(ITaskReporter taskReporter)
        {
            this.taskReporter = taskReporter;
        }

        public abstract CompositeTaskType GetTaskType();
    }
}
