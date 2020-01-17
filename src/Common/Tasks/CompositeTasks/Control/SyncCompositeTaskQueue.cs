using System.Collections.Concurrent;
using Tester.src.Common.Result;

namespace Tester.src.Common.Tasks.CompositeTasks.Control
{
    /// <summary>
    /// A synchronous task queue which will execute tasks one after another
    /// </summary>
    class SyncCompositeTaskQueue
    {
        private readonly BlockingCollection<ICompositeTask> taskQueue;

        public SyncCompositeTaskQueue()
        {
            this.taskQueue = new BlockingCollection<ICompositeTask>();
        }

        /// <summary>
        /// Submits a composite task to be processed in the queue
        /// </summary>
        /// <typeparam name="T">IDataSet type</typeparam>
        /// <param name="task">The composite task to execute</param>
        public void Submit<T>(ICompositeTask<T> task) where T : IDataSet
        {
            taskQueue.Add(task);
        }

        /// <summary>
        /// Takes the next composite task off the queue while blocking the queue
        /// </summary>
        /// <returns>A composite task to consume</returns>
        public ICompositeTask Take()
        {
            return taskQueue.Take();
        }

        /// <summary>
        /// Checks if the current sync queue is empty
        /// </summary>
        /// <returns>true if empty, false if not empty</returns>
        public bool IsEmpty()
        {
            return taskQueue.Count == 0;
        }
    }
}
