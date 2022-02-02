using System;
using System.Collections.Generic;
using Android.Content;
using Android.Runtime;
using Android.Util;
using AndroidX.RecyclerView.Widget;

namespace GroupRecyclerView.Widgets
{
    /// <summary>
    /// RecyclerView presenting grouped items collection
    /// </summary>
    [Register("grouprecyclerview.widgets.GroupRecyclerView")]
    public class GroupRecyclerView : RecyclerView
    {
        private readonly IGroupRecyclerViewAdapter _adapter;
        private bool _disposed;

        /// <summary>
        /// Triggered on item click
        /// </summary>
        public event EventHandler<object> ItemClick;

        /// <summary>
        /// Triggered on item long click
        /// </summary>
        public event EventHandler<object> ItemLongClick;

        /// <summary>
        /// GroupRecyclerView collection of items
        /// </summary>
        public IEnumerable<IItemGroup> ItemsSource
        {
            get
            {
                return _adapter.ItemsSource;
            }

            set
            {
                if (_adapter.ItemsSource == value)
                {
                    return;
                }

                _adapter.ItemsSource = value;
            }
        }

        /// <summary>
        /// Create new GroupRecyclerView instance
        /// </summary>
        /// <param name="context"></param>
        public GroupRecyclerView(Context context)
            : this(context, default)
        {
        }

        /// <summary>
        /// Create new GroupRecyclerView instance
        /// </summary>
        /// <param name="context"></param>
        /// <param name="attrs"></param>
        public GroupRecyclerView(Context context, IAttributeSet attrs)
            : this(context, attrs, default)
        {
        }

        /// <summary>
        /// Create new GroupRecyclerView instance
        /// </summary>
        /// <param name="context"></param>
        /// <param name="attrs"></param>
        /// <param name="defStyleAttr"></param>
        public GroupRecyclerView(Context context, IAttributeSet attrs, int defStyleAttr)
            : this(context, attrs, defStyleAttr, new GroupRecyclerViewAdapter(context))
        {
        }

        /// <summary>
        /// Create new GroupRecyclerView instance
        /// </summary>
        /// <param name="context"></param>
        /// <param name="attrs"></param>
        /// <param name="defStyleAttr"></param>
        /// <param name="adapter"></param>
        public GroupRecyclerView(Context context, IAttributeSet attrs, int defStyleAttr, IGroupRecyclerViewAdapter adapter)
            : base (context, attrs, defStyleAttr)
        {
            var currentLayoutManager = GetLayoutManager();

            if (currentLayoutManager == null)
            {
                // LinearLayout may need SupportsPredictiveItemAnimations to return false
                // Look for IndexOutOfBoundsException
                SetLayoutManager(new LinearLayoutManager(context));
            }

            _adapter = adapter;

            if (_adapter == null)
            {
                _adapter = new GroupRecyclerViewAdapter(context);
            }

            _adapter.ItemClick += Adapter_ItemClick;
            _adapter.ItemLongClick += Adapter_ItemLongClick;
            SetAdapter(_adapter as RecyclerView.Adapter);
        }

        /// <summary>
        /// Constructor required for inflated instances
        /// </summary>
        /// <param name="javaReference"></param>
        /// <param name="transfer"></param>
        protected GroupRecyclerView(IntPtr javaReference, JniHandleOwnership transfer)
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
                _adapter.ItemClick -= Adapter_ItemClick;
                _adapter.ItemLongClick -= Adapter_ItemLongClick;
            }

            base.Dispose(disposing);
        }

        /// <summary>
        /// Handle item click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="item">Item from the items source</param>
        private void Adapter_ItemLongClick(object sender, object item)
        {
            ItemLongClick?.Invoke(this, item);
        }

        /// <summary>
        /// Handle item long click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="item">Item from the items source</param>
        private void Adapter_ItemClick(object sender, object item)
        {
            ItemClick?.Invoke(this, item);
        }
    }
}
