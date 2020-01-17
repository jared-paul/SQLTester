using System;
using System.Collections.Generic;
using Tester.src.Common.Result;
using Tester.src.Common.Tasks;
using Tester.src.Common.Tasks.CompositeTasks;
using Tester.src.Common.Tasks.CompositeTasks.Control;
using Tester.src.Common.Tasks.Pool;

namespace Tester.src.Aggregator.Tasks
{
    class TaskPool : ITaskPool
    {
        private State state;
        private int numberOfTasks = 0;

        private readonly CompositeTaskExecutor compositeTaskController;
        private Action<List<IDataSet>, State> action;

        public TaskPool(Action<List<IDataSet>, State> action) : this()
        {
            this.action = action;
        }

        public TaskPool()
        {
            this.compositeTaskController = new CompositeTaskExecutor(this);
        }

        public CompositeTaskExecutor GetCompositeTaskController()
        {
            return compositeTaskController;
        }

        public void SubmitCompositeTask<T>(ICompositeTask<T> compositeTask) where T : IDataSet
        {
            numberOfTasks++;
            this.compositeTaskController.SubmitCompositeTask(compositeTask);
        }

        public void Start()
        {
            this.compositeTaskController.Start();
        }

        public int GetNumberOfTasks()
        {
            return numberOfTasks;
        }

        public State GetState()
        {
            return state;
        }

        public void SetState(State state)
        {
            this.state = state;
        }

        public void OnReturn(Action<List<IDataSet>, State> action)
        {
            this.action = action;
        }

        public void Return(List<IDataSet> dataSets, State state)
        {
            this.action(dataSets, state);
            this.state = state;
        }
    }
}
