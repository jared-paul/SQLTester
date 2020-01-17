namespace Tester.src.Common.Tasks.CompositeTasks.Observer
{
    /// <summary>
    /// An interface that represents a task is observable by other "ITaskObserver" tasks
    /// </summary>
    interface IObservableTask<E>
    {
        /// <summary>
        /// Adds an observer task to observe this tasks status
        /// </summary>
        /// <param name="taskObserver">the task that will observe this one</param>
        void AddObserver(ITaskObserver<E> threadObserver);

        /// <summary>
        /// Notifies the status of this task to all of the observers subscribed to this task 
        /// </summary>
        /// <param name="state">state of the task</param>
        void NotifyObservers(State state);

        /// <summary>
        /// Updates an element that is shared across all observers/observables that are connected
        /// </summary>
        /// <param name="element">the element to be updated</param>
        void UpdateSharedElement(E element);
    }
}
