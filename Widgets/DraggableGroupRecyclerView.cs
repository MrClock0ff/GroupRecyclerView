using System;
using Android.Content;
using Android.Runtime;
using Android.Util;
using AndroidX.RecyclerView.Widget;

namespace GroupRecyclerView.Widgets
{
    /// <summary>
    /// GroupRecyclerView cappable of moving the item between the groups
    /// </summary>
    [Register("grouprecyclerview.widgets.DraggableGroupRecyclerView")]
    public class DraggableGroupRecyclerView : GroupRecyclerView
    {
        IDraggableGroupRecyclerViewAdapter _adapter;
        private bool _disposed;

        /// <summary>
        /// Triggered when group item moved
        /// </summary>
        public event EventHandler<GroupItemMovedEventArgs> ItemMoved;

        /// <summary>
        /// Create new DraggableGroupRecyclerView instance
        /// </summary>
        /// <param name="context"></param>
        public DraggableGroupRecyclerView(Context context)
            : this(context, default)
        {
        }

        /// <summary>
        /// Create new DraggableGroupRecyclerView instance
        /// </summary>
        /// <param name="context"></param>
        /// <param name="attrs"></param>
        public DraggableGroupRecyclerView(Context context, IAttributeSet attrs)
            : this(context, attrs, default)
        {
        }

        /// <summary>
        /// Create new DraggableGroupRecyclerView instance
        /// </summary>
        /// <param name="context"></param>
        /// <param name="attrs"></param>
        /// <param name="defStyleAttr"></param>
        public DraggableGroupRecyclerView(Context context, IAttributeSet attrs, int defStyleAttr)
            : this(context, attrs, defStyleAttr, new DraggableGroupRecyclerViewAdapter(context))
        {
        }

        /// <summary>
        /// Create new DraggableGroupRecyclerView instance
        /// </summary>
        /// <param name="context"></param>
        /// <param name="attrs"></param>
        /// <param name="defStyleAttr"></param>
        /// <param name="adapter"></param>
        public DraggableGroupRecyclerView(Context context, IAttributeSet attrs, int defStyleAttr, IDraggableGroupRecyclerViewAdapter adapter)
            : base(context, attrs, defStyleAttr, adapter)
        {
            if (!(adapter is IDraggableGroupRecyclerViewAdapter))
            {
                throw new Exception($"Wrong adapter type. Expected: {nameof(IDraggableGroupRecyclerViewAdapter)}.");
            }

            _adapter = adapter;

            ItemGroupMoveSimpleCallback moveCallback = new ItemGroupMoveSimpleCallback(adapter);
            ItemTouchHelper itemTouchHelper = new ItemTouchHelper(moveCallback);
            itemTouchHelper.AttachToRecyclerView(this);

            adapter.ItemMoved += AdapterItemMoved_Handler;
        }

        /// <summary>
        /// Constructor required for inflated instances
        /// </summary>
        /// <param name="javaReference"></param>
        /// <param name="transfer"></param>
        protected DraggableGroupRecyclerView(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
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
                if (_adapter != null)
                {
                    _adapter.ItemMoved -= AdapterItemMoved_Handler;
                    _adapter = null;
                }
            }

            base.Dispose(disposing);
        }

        /// <summary>
        /// Handle adapter item moved event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AdapterItemMoved_Handler(object sender, GroupItemMovedEventArgs e)
        {
            ItemMoved?.Invoke(this, e);
        }
    }
}
