using System;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Runtime;
using Android.Util;
using Android.Views;
using AndroidX.Core.Content;
using AndroidX.RecyclerView.Widget;

namespace GroupRecyclerView.Widgets
{
    /// <summary>
    /// GroupRecyclerView cappable of moving the item between the groups
    /// </summary>
    [Register("grouprecyclerview.widgets.DraggableGroupRecyclerView")]
    public class DraggableGroupRecyclerView
        : GroupRecyclerView, IDraggableGroupRecyclerView
    {
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
        public DraggableGroupRecyclerView(Context context, IAttributeSet attrs, int defStyleAttr, IGroupRecyclerViewAdapter adapter)
            : base(context, attrs, defStyleAttr, adapter)
        {
            ItemGroupMoveSimpleCallback moveCallback = new ItemGroupMoveSimpleCallback(this);
            ItemTouchHelper itemTouchHelper = new ItemTouchHelper(moveCallback);
            itemTouchHelper.AttachToRecyclerView(this);
        }

        #region IDraggableGroupRecyclerView implementation
        /// <summary>
        /// Handle move of viewHolder1 into viewHolder2 position
        /// </summary>
        /// <param name="viewHolder1"></param>
        /// <param name="viewHolder2"></param>
        /// <returns></returns>
        public bool OnMove(RecyclerView.ViewHolder viewHolder1, RecyclerView.ViewHolder viewHolder2)
        {
            return true;
        }
        #endregion

        /// <summary>
        /// Constructor required for inflated instances
        /// </summary>
        /// <param name="javaReference"></param>
        /// <param name="transfer"></param>
        protected DraggableGroupRecyclerView(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }

        /// <summary>
        /// Custom GroupRecyclerViewAdapter which adds selectable item background
        /// </summary>
        private class DraggableGroupRecyclerViewAdapter : GroupRecyclerViewAdapter
        {
            public DraggableGroupRecyclerViewAdapter(Context context)
                : base(context)
            {
            }

            public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
            {
                RecyclerView.ViewHolder viewHolder = base.OnCreateViewHolder(parent, viewType);

                if (viewHolder.ItemView == null)
                {
                    return viewHolder;
                }

                if (!(viewHolder is IGroupRecyclerViewHolder groupRecyclerViewHolder))
                {
                    return viewHolder;
                }

                if (groupRecyclerViewHolder.Id == GroupViewType)
                {
                    return viewHolder;
                }

                viewHolder.ItemView.Background = new ColorDrawable(Color.White);

                TypedValue selectableItemBackgroundTypedValue = new TypedValue();

                if (!Context.Theme.ResolveAttribute(Resource.Attribute.selectableItemBackground, selectableItemBackgroundTypedValue, true))
                {
                    return viewHolder;
                }

                int selectableItemBackgroundResource = selectableItemBackgroundTypedValue.ResourceId;
                viewHolder.ItemView.Foreground = ContextCompat.GetDrawable(Context, selectableItemBackgroundResource);

                return viewHolder;
            }
        }
    }
}
