using System;
using System.Collections.Generic;
using System.Linq;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using GroupRecyclerView.Extensions;

namespace GroupRecyclerView.Widgets
{
    /// <summary>
    /// GroupRecyclerView adapter implementation
    /// </summary>
    public class GroupRecyclerViewAdapter : RecyclerView.Adapter, IGroupRecyclerViewAdapter
    {
        private List<RecyclerViewAdapterItem> _flattenItemsSource;
        private IEnumerable<IItemGroup> _itemsSource;
        private bool _disposed;

        /// <summary>
        /// Create new adapter instance
        /// </summary>
        /// <param name="context"></param>
        public GroupRecyclerViewAdapter(Context context)
        {
            Context = context;
            GroupViewType = View.GenerateViewId();
            ItemViewType = View.GenerateViewId();
        }

        /// <summary>
        /// Group view type
        /// </summary>
        public int GroupViewType { get; }

        /// <summary>
        /// Group item view type
        /// </summary>
        public int ItemViewType { get; }

        /// <summary>
        /// Current context instance
        /// </summary>
        public Context Context { get; private set; }

        #region IGroupRecyclerViewAdapter implementation
        /// <summary>
        /// Triggered on item click
        /// </summary>
        public event EventHandler<object> ItemClick;

        /// <summary>
        /// Triggered on item long click
        /// </summary>
        public event EventHandler<object> ItemLongClick;

        /// <summary>
        /// Collection of the items in the adapter
        /// </summary>
        public IEnumerable<IItemGroup> ItemsSource
        {
            get
            {
                return _itemsSource;
            }

            set
            {
                SetItemsSource(value);
            }
        }
        #endregion

        #region Adapter implementation
        public override int ItemCount
        {
            get
            {
                return _flattenItemsSource?.Count() ?? 0;
            }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            if (holder is IGroupRecyclerViewHolder groupRecyclerViewHolder)
            {
                RecyclerViewAdapterItem adapterItem = _flattenItemsSource[position];

                ((TextView)holder.ItemView).Text = adapterItem.IsGroup
                    ? ((IItemGroup)adapterItem.Item).GroupTitle
                    : adapterItem.Item.ToString();
                groupRecyclerViewHolder.DataContext = adapterItem.Item;
                AttachClickListeners(groupRecyclerViewHolder);
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View view = CreateView(parent, viewType);
            GroupRecyclerViewHolder viewHolder = new GroupRecyclerViewHolder(view)
            {
                Id = viewType
            };

            return viewHolder;
        }
        #endregion

        public override int GetItemViewType(int position)
        {
            return _flattenItemsSource[position].IsGroup
                ? GroupViewType
                : ItemViewType;
        }

        public override void OnViewAttachedToWindow(Java.Lang.Object holder)
        {
            base.OnViewAttachedToWindow(holder);

            if (holder is IGroupRecyclerViewHolder groupRecyclerViewHolder)
            {
                groupRecyclerViewHolder.OnAttachedToWindow();
            }
        }

        public override void OnViewDetachedFromWindow(Java.Lang.Object holder)
        {
            if (holder is IGroupRecyclerViewHolder groupRecyclerViewHolder)
            {
                groupRecyclerViewHolder.OnDetachedFromWindow();
            }

            base.OnViewDetachedFromWindow(holder);
        }

        public override void OnViewRecycled(Java.Lang.Object holder)
        {
            if (holder is IGroupRecyclerViewHolder groupRecyclerViewHolder)
            {
                groupRecyclerViewHolder.Click -= OnItemViewClick_Handler;
                groupRecyclerViewHolder.LongClick -= OnItemViewLongClick_Handler;
            }
        }

        public override bool OnFailedToRecycleView(Java.Lang.Object holder)
        {
            return true;
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
                _flattenItemsSource = null;
                _itemsSource = null;
                Context = null;
            }

            base.Dispose(disposing);
        }

        /// <summary>
        /// Set items source and notify about data set changes
        /// </summary>
        /// <param name="value"></param>
        private void SetItemsSource(IEnumerable<IItemGroup> value)
        {
            if (ReferenceEquals(_itemsSource, value))
            {
                return;
            }

            _itemsSource = value;

            if (Looper.MainLooper != Looper.MyLooper())
            {
                System.Diagnostics.Debug.WriteLine("Called from worker thread. RecyclerView will throw exception. ItemsSource has to be set only from main thread.", "ERROR");
            }

            FlattenItemsSource();
            NotifyDataSetChanged();
        }

        /// <summary>
        /// Flattens two dimmensional items source
        /// </summary>
        private void FlattenItemsSource()
        {
            var flattenItemsSource = new List<RecyclerViewAdapterItem>();

            foreach (IItemGroup group in ItemsSource)
            {
                flattenItemsSource.Add(new RecyclerViewAdapterItem(group, true));

                foreach (object item in group)
                {
                    flattenItemsSource.Add(new RecyclerViewAdapterItem(item, false));
                }
            }

            _flattenItemsSource = flattenItemsSource;
        }

        /// <summary>
        /// Create view for specific view type
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="viewType"></param>
        /// <returns></returns>
        private View CreateView(ViewGroup parent, int viewType)
        {
            int viewTemplateId = global::Android.Resource.Layout.SimpleListItem1;
            TextView view = LayoutInflater.FromContext(Context).Inflate(viewTemplateId, parent, false) as TextView;

            if (viewType == GroupViewType)
            {
                view.Background = new ColorDrawable(Color.Gray);
                view.SetAllCaps(true);
                view.SetTypeface(view.Typeface, TypefaceStyle.Bold);
            }

            return view;
        }

        /// <summary>
        /// Attach click handlers to recycler view holder
        /// </summary>
        /// <param name="groupRecyclerViewHolder"></param>
        private void AttachClickListeners(IGroupRecyclerViewHolder groupRecyclerViewHolder)
        {
            groupRecyclerViewHolder.Click -= OnItemViewClick_Handler;
            groupRecyclerViewHolder.LongClick -= OnItemViewLongClick_Handler;
            groupRecyclerViewHolder.Click += OnItemViewClick_Handler;
            groupRecyclerViewHolder.LongClick += OnItemViewLongClick_Handler;
        }

        /// <summary>
        /// Handle long item click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnItemViewLongClick_Handler(object sender, EventArgs e)
        {
            if (sender is IGroupRecyclerViewHolder groupRecyclerViewHolder)
            {
                ItemLongClick?.Invoke(this, groupRecyclerViewHolder.DataContext);
            }
        }

        /// <summary>
        /// Handle item click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnItemViewClick_Handler(object sender, EventArgs e)
        {
            if (sender is IGroupRecyclerViewHolder groupRecyclerViewHolder)
            {
                ItemClick?.Invoke(this, groupRecyclerViewHolder.DataContext);
            }
        }

        /// <summary>
        /// Recycler view adapter item wrapper
        /// </summary>
        private class RecyclerViewAdapterItem
        {
            /// <summary>
            /// Create new instance of item wrapper
            /// </summary>
            /// <param name="item"></param>
            /// <param name="isGroup"></param>
            public RecyclerViewAdapterItem(object item, bool isGroup)
            {
                Item = item;
                IsGroup = isGroup;
            }

            /// <summary>
            /// Wrapped item
            /// </summary>
            public object Item { get; }

            /// <summary>
            /// Indicates if this item is a group of items
            /// </summary>
            public bool IsGroup { get; }
        }
    }
}
