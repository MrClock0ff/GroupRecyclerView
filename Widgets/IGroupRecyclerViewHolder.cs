using System;
namespace GroupRecyclerView.Widgets
{
    /// <summary>
    /// GroupRecyclerView view holder model
    /// </summary>
    public interface IGroupRecyclerViewHolder
    {
        /// <summary>
        /// Triggered when clicked this view
        /// </summary>
        event EventHandler<EventArgs> Click;

        /// <summary>
        /// Triggered when long clicked this view
        /// </summary>
        event EventHandler<EventArgs> LongClick;

        /// <summary>
        /// View id
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// View assciated data context
        /// </summary>
        object DataContext { get; set; }

        /// <summary>
        /// Invoke when this view is attached to window
        /// </summary>
        void OnAttachedToWindow();

        /// <summary>
        /// Invoke when this view is detached from window
        /// </summary>
        void OnDetachedFromWindow();
    }
}
