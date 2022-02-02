using AndroidX.RecyclerView.Widget;

namespace GroupRecyclerView.Widgets
{
    /// <summary>
    /// Callback for handling item moving between groups in DraggableGroupRecyclerView
    /// </summary>
    public class ItemGroupMoveSimpleCallback : ItemTouchHelper.SimpleCallback
    {
        private IDraggableGroupRecyclerViewAdapter _recyclerViewAdapter;
        private bool _disposed;

        /// <summary>
        /// Create new callback instance
        /// </summary>
        /// <param name="draggableGroupRecyclerView"></param>
        public ItemGroupMoveSimpleCallback(IDraggableGroupRecyclerViewAdapter recyclerViewAdapter)
            : base(ItemTouchHelper.Up | ItemTouchHelper.Down | ItemTouchHelper.Start | ItemTouchHelper.End, 0)
        {
            _recyclerViewAdapter = recyclerViewAdapter;
        }

        #region ItemTouchHelper.SimpleCallback implementation
        public override bool OnMove(RecyclerView p0, RecyclerView.ViewHolder p1, RecyclerView.ViewHolder p2)
        {
            return _recyclerViewAdapter.OnMove(p1, p2);
        }

        public override void OnSwiped(RecyclerView.ViewHolder p0, int p1)
        {
        }
        #endregion

        public override int GetMovementFlags(RecyclerView recyclerView, RecyclerView.ViewHolder viewHolder)
        {
            if (!(viewHolder is IGroupRecyclerViewHolder groupRecyclerViewHolder))
            {
                return base.GetMovementFlags(recyclerView, viewHolder);
            }

            if (groupRecyclerViewHolder.Id != _recyclerViewAdapter.GroupViewType)
            {
                return base.GetMovementFlags(recyclerView, viewHolder);
            }

            // Prevent user from moving group items (group headers)
            return MakeMovementFlags(0, 0);
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
                _recyclerViewAdapter = null;
            }

            base.Dispose(disposing);
        }
    }
}
