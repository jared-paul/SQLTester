using System;
using System.Collections.Generic;
using Tester.src.Common.Result;
using Tester.src.Common.Tasks.CompositeTasks;
using Tester.src.Common.Tasks.CompositeTasks.Control;

namespace Tester.src.Common.Tasks.Pool
{
    /// <summary>
    /// Represents a collection of composite tasks
    /// </summary>
    interface ITaskPool
    {
        /// <summary>
        /// The executor to execute all of the tasks submited
        /// </summary>
        CompositeTaskExecutor GetCompositeTaskController();

        /// <summary>
        /// Submits a task to the queue or runtime, depends on the type of task
        /// </summary>
        /// <typeparam name="T">IDataSet type</typeparam>
        /// <param name="compositeTask">the task to submit</param>
        void SubmitCompositeTask<T>(ICompositeTask<T> compositeTask) where T : IDataSet;

        /// <summary>
        /// Needed to confirm that all results are finished and reported back successfully
        /// </summary>
        /// <returns>the total number of tasks submitted</returns>
        int GetNumberOfTasks();

        /// <summary>
        /// Checks to see if the expected number of results are produced for the number of tasks given
        /// </summary>
        /// <returns>true if all tasks in the task pool are finished</returns>
        State GetState();

        /// <summary>
        /// Sets the current state of the app (ex. should die, reset, etc)
        /// </summary>
        /// <param name="state">the state of the app</param>
        void SetState(State state);

        /// <summary>
        /// Called once the result set pool is full
        /// </summary>
        /// <param name="action">what you need to do with all of the data sets returned</param>
        void OnReturn(Action<List<IDataSet>, State> action);

        /// <summary>
        /// Called when reporting back, includes the state of the app, calls the Action delegate OnFinish
        /// </summary>
        /// <param name="callback">The list of results from all the submitted tasks</param>
        void Return(List<IDataSet> callback, State state);
    }
}