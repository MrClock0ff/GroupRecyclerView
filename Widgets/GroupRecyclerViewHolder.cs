using System;
using Android.Views;
using AndroidX.RecyclerView.Widget;

namespace GroupRecyclerView.Widgets
{
    /// <summary>
    /// GroupRecyclerView view implementation
    /// </summary>
    public class GroupRecyclerViewHolder
        : RecyclerView.ViewHolder, IGroupRecyclerViewHolder
    {
        private bool _disposed;

        #region IGroupRecyclerViewHolder implementation
        /// <summary>
        /// View id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// View associated data context
        /// </summary>
        public object DataContext { get; set; }

        /// <summary>
        /// Triggered when clicked this view
        /// </summary>
        public event EventHandler<EventArgs> Click;

        /// <summary>
        /// Triggered when long clicked this view
        /// </summary>
        public event EventHandler<EventArgs> LongClick;

        /// <summary>
        /// Invoke when this view is attached to window
        /// </summary>
        public void OnAttachedToWindow()
        {
            ItemView.Click += ItemView_Click;
            ItemView.LongClick += ItemView_LongClick;
        }

        /// <summary>
        /// Invoke when this view is detached from window
        /// </summary>
        public void OnDetachedFromWindow()
        {
            ItemView.Click -= ItemView_Click;
            ItemView.LongClick -= ItemView_LongClick;
        }
        #endregion

        /// <summary>
        /// Create view instance
        /// </summary>
        /// <param name="itemView"></param>
        public GroupRecyclerViewHolder(View itemView)
            : base(itemView)
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            _disposed = true;

            if (disposing)
            {
                ItemView.Click -= ItemView_Click;
                ItemView.LongClick -= ItemView_LongClick;
            }

            base.Dispose(disposing);
        }

        /// <summary>
        /// Handle long click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemView_LongClick(object sender, View.LongClickEventArgs e)
        {
            LongClick?.Invoke(this, e);
        }

        /// <summary>
        /// Handle click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemView_Click(object sender, EventArgs e)
        {
            Click?.Invoke(this, e);
        }
    }
}
