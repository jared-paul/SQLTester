using System.Threading.Tasks;
using Tester.src.Common.Result;
using Tester.src.Common.Tasks.CompositeTasks.Observer;
using Tester.src.Common.Tasks.Pool;

namespace Tester.src.Common.Tasks.CompositeTasks.Control
{
    /// <summary>
    /// This class will take in any composite task and pass it over to the desired consumers
    /// </summary>
    class CompositeTaskExecutor
    {
        private readonly ITaskReporter taskReporter;
        private readonly SyncCompositeTaskQueue syncTaskQueue;

        public CompositeTaskExecutor(ITaskPool taskPool)
        {
            this.taskReporter = new TaskReporter(taskPool);
            this.syncTaskQueue = new SyncCompositeTaskQueue();
            new SyncCompositeTaskConsumer(taskPool, syncTaskQueue).Start();
        }

        /// <summary>
        /// Submits a composite task of type T to the desired consumer
        /// </summary>
        /// <typeparam name="T">IDataSet type</typeparam>
        /// <param name="task">the task to consume</param>
        public void SubmitCompositeTask<T>(ICompositeTask<T> task) where T : IDataSet
        {
            task.SetTaskReporter(taskReporter);

            if (task.GetTaskType() == CompositeTaskType.ASYNCHRONOUS)
            {
                if (!(task is ITaskObserver))
                {
                    Task.Factory.StartNew(() =>
                                  {
                                      task.Run();
                                  });
                }
            }
            else if (task.GetTaskType() == CompositeTaskType.SYNCHRONOUS)
            {
                syncTaskQueue.Submit(task);
            }
        }

        public void Start()
        {
            //TODO: implement
        }
    }
}
