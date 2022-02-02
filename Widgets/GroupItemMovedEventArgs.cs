using System;

namespace GroupRecyclerView.Widgets
{
    /// <summary>
    /// Item moved event arguments
    /// </summary>
    public class GroupItemMovedEventArgs : EventArgs
    {
        /// <summary>
        /// Create new event arguments instance
        /// </summary>
        public GroupItemMovedEventArgs(object source, object target)
        {
            Source = source;
            Target = target;
        }

        /// <summary>
        /// Moving object
        /// </summary>
        public object Source { get; }

        /// <summary>
        /// Moving object inserted into target object position
        /// </summary>
        public object Target { get; }
    }
}
