namespace Tester.src.Common.Tasks.CompositeTasks.Observer
{
    interface ITaskObserver
    {

    }

    /// <summary>
    /// An interface that represents a task that can observe other "ITaskObservable" tasks
    /// </summary>
    interface ITaskObserver<E> : ITaskObserver
    {
        /// <summary>
        /// Updates the state of all of this observer based on the observed task
        /// </summary>
        /// <param name="state">the state of the observed task</param>
        void OnUpdateState(State state);

        void OnUpdateSharedElement(E sharedElement);
    }
}
