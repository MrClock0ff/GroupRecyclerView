using System;
using System.Collections.Generic;

namespace GroupRecyclerView.Widgets
{
    /// <summary>
    /// GroupRecyclerView adapter
    /// </summary>
    public interface IGroupRecyclerViewAdapter
    {
        /// <summary>
        /// Triggered on item click
        /// </summary>
        event EventHandler<object> ItemClick;

        /// <summary>
        /// Triggered on item long click
        /// </summary>
        event EventHandler<object> ItemLongClick;

        /// <summary>
        /// Collection of the items in the adapter
        /// </summary>
        IEnumerable<IItemGroup> ItemsSource { get; set; }

        /// <summary>
        /// Group view type
        /// </summary>
        int GroupViewType { get; }

        /// <summary>
        /// Group item view type
        /// </summary>
        int ItemViewType { get; }
    }
}
