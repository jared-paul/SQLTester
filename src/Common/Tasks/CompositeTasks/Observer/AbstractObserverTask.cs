using System.Threading.Tasks;
using Tester.src.Common.Result;

namespace Tester.src.Common.Tasks.CompositeTasks.Observer
{
    /// <summary>
    /// Represents the implementation of a task that can observe other tasks
    /// </summary>
    /// <typeparam name="T">IDataSet type</typeparam>
    abstract class AbstractObserverTask<T, E> : AbstractCompositeTask<T>, ITaskObserver<E> where T : IDataSet
    {
        protected State state;
        private bool hasStarted = false;

        public AbstractObserverTask()
        {
        }

        public void OnUpdateState(State state)
        {
            this.state = state;

            if (state == State.ALIVE && hasStarted == false)
            {
                Task.Factory.StartNew(() =>
                {
                    hasStarted = true;
                    Run();
                }).ConfigureAwait(false);
            }
            else if (state == State.RESET)
            {
                OnReset();
            }
            else if (state == State.FAILED)
            {
                OnFail();
            }
        }

        public abstract void OnUpdateSharedElement(E sharedElement);

        protected abstract void OnReset();

        protected abstract void OnFail();

        public override CompositeTaskType GetTaskType()
        {
            return CompositeTaskType.ASYNCHRONOUS;
        }
    }
}
