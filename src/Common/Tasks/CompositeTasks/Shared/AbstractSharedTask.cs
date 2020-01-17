namespace Tester.src.Common.Tasks.CompositeTasks.Shared
{
    abstract class AbstractSharedTask<E> : ISharedTask<E>
    {
        /// <summary>
        /// Must be thread safe!
        /// </summary>
        protected E sharedElement;

        public AbstractSharedTask(E sharedElement)
        {
            this.sharedElement = sharedElement;
        }
    }
}
