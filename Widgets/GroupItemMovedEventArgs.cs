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
        public GroupItemMovedEventArgs(IItemGroup sourceGroup, object sourceItem, IItemGroup targetGroup, object targetItem)
        {
            SourceGroup = sourceGroup;
            TargetGroup = targetGroup;
            SourceItem = sourceItem;
            TargetItem = targetItem;
        }

        public IItemGroup SourceGroup { get; }

        public IItemGroup TargetGroup { get; }

        /// <summary>
        /// Moving object
        /// </summary>
        public object SourceItem { get; }

        /// <summary>
        /// Moving object inserted into target object position
        /// </summary>
        public object TargetItem { get; }
    }
}
