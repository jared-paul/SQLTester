using System.Collections.Generic;
using Tester.src.Common.Result;

namespace Tester.src.Common.Tasks.CompositeTasks.Observer
{
    /// <summary>
    /// Represents the implementation of an observable task that can be observed by ITaskObserver
    /// </summary>
    /// <typeparam name="T">IDataSet type</typeparam>
    abstract class AbstractObservableTask<T, E> : AbstractCompositeTask<T>, IObservableTask<E> where T : IDataSet
    {
        private List<ITaskObserver<E>> taskObservers;

        public AbstractObservableTask() : base()
        {
            this.taskObservers = new List<ITaskObserver<E>>();
        }

        /// <summary>
        /// Reports the dataset back to the task reporter
        /// </summary>
        /// <param name="dataSet">dataset to report back</param>
        public override void ReportBack(T dataSet, State state)
        {
            base.ReportBack(dataSet, state);
            NotifyObservers(state);
        }

        public void AddObserver(ITaskObserver<E> taskObserver)
        {
            this.taskObservers.Add(taskObserver);
        }

        public void NotifyObservers(State state)
        {
            foreach (ITaskObserver<E> taskObserver in taskObservers)
            {
                taskObserver.OnUpdateState(state);
            }
        }

        public void UpdateSharedElement(E updatedElement)
        {
            foreach (ITaskObserver<E> taskObserver in taskObservers)
            {
                taskObserver.OnUpdateSharedElement(updatedElement);
            }
        }
    }
}
