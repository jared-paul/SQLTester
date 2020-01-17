using System;
using System.Collections.Generic;
using Tester.src.Common.Exceptions;
using Tester.src.Common.Result;
using Tester.src.Common.Tasks;
using Tester.src.Common.Tasks.CompositeTasks;
using Tester.src.Common.Tasks.CompositeTasks.Observer;

namespace Tester.src.Aggregator.Tasks
{
    class TaskPoolBuilder
    {
        private readonly TaskPool taskPool;
        private ICompositeTask currentTask;

        public TaskPoolBuilder(TaskPool taskPool)
        {
            this.taskPool = taskPool;
        }

        public TaskPoolBuilder()
        {
            this.taskPool = new TaskPool();
        }

        public TaskPoolBuilder SubmitCompositeTask<T>(ICompositeTask<T> compositeTask) where T : IDataSet
        {
            taskPool.SubmitCompositeTask(compositeTask);
            this.currentTask = compositeTask;
            return this;
        }

        public TaskPoolBuilder Observe<E>(IObservableTask<E> observant)
        {
            if (!(currentTask is ITaskObserver<E>))
            {
                throw new OperationNotSupported("The current task must be an instance of ITaskObserver");
            }

            observant.AddObserver((ITaskObserver<E>)currentTask);
            return this;
        }

        public TaskPoolBuilder OnReturn(Action<List<IDataSet>, State> onPoolReturn)
        {
            taskPool.OnReturn(onPoolReturn);
            return this;
        }

        public TaskPool Build()
        {
            return taskPool;
        }
    }
}