using System;

namespace Tester.src.Common.Tasks.CompositeTasks
{
    class CompositeTaskAttr : Attribute
    {
        internal CompositeTaskAttr(string name)
        {
            this.Name = name;
        }

        public string Name { get; }
    }

    /// <summary>
    /// Type of task
    /// </summary>
    public enum CompositeTaskType
    {
        ASYNCHRONOUS,
        SYNCHRONOUS
    }
}
