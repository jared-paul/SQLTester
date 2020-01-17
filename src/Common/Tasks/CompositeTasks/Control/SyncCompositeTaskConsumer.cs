using System.Threading.Tasks;
using Tester.src.Common.Tasks.Pool;

namespace Tester.src.Common.Tasks.CompositeTasks.Control
{
    /// <summary>
    /// A long running task that takes and consumes tasks in the SyncCompositeTaskQueue
    /// </summary>
    class SyncCompositeTaskConsumer
    {
        private readonly SyncCompositeTaskQueue taskQueue;
        private readonly ITaskPool taskPool;

        public SyncCompositeTaskConsumer(ITaskPool taskPool, SyncCompositeTaskQueue taskQueue)
        {
            this.taskQueue = taskQueue;
            this.taskPool = taskPool;
        }

        public void Start()
        {
            Task.Factory.StartNew(() =>
            {
                while (!(taskPool.GetState() == State.DEAD))
                {
                    if (!taskQueue.IsEmpty())
                    {
                        taskQueue.Take().Run();
                    }
                }
            }, TaskCreationOptions.LongRunning).ConfigureAwait(false);
        }
    }
}
