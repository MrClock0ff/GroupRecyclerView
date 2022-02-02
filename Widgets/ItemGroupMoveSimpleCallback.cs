using AndroidX.RecyclerView.Widget;

namespace GroupRecyclerView.Widgets
{
    /// <summary>
    /// Callback for handling item moving between groups in DraggableGroupRecyclerView
    /// </summary>
    public class ItemGroupMoveSimpleCallback : ItemTouchHelper.SimpleCallback
    {
        private IDraggableGroupRecyclerView _draggableGroupRecyclerView;
        private bool _disposed;

        /// <summary>
        /// Create new callback instance
        /// </summary>
        /// <param name="draggableGroupRecyclerView"></param>
        public ItemGroupMoveSimpleCallback(IDraggableGroupRecyclerView draggableGroupRecyclerView)
            : base(ItemTouchHelper.Up | ItemTouchHelper.Down | ItemTouchHelper.Start | ItemTouchHelper.End, 0)
        {
            _draggableGroupRecyclerView = draggableGroupRecyclerView;
        }

        #region ItemTouchHelper.SimpleCallback implementation
        public override bool OnMove(RecyclerView p0, RecyclerView.ViewHolder p1, RecyclerView.ViewHolder p2)
        {
            return _draggableGroupRecyclerView.OnMove(p1, p2);
        }

        public override void OnSwiped(RecyclerView.ViewHolder p0, int p1)
        {
        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            _disposed = true;

            if (disposing)
            {
                _draggableGroupRecyclerView = null;
            }

            base.Dispose(disposing);
        }
    }
}
