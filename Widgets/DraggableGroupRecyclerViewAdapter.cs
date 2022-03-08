using System;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Util;
using Android.Views;
using AndroidX.Core.Content;
using AndroidX.RecyclerView.Widget;

namespace GroupRecyclerView.Widgets
{
    /// <summary>
    /// Custom GroupRecyclerViewAdapter which adds selectable item background
    /// </summary>
    public class DraggableGroupRecyclerViewAdapter
        : GroupRecyclerViewAdapter, IDraggableGroupRecyclerViewAdapter
    {
        public DraggableGroupRecyclerViewAdapter(Context context)
            : base(context)
        {
        }

        #region IDraggableGroupRecyclerViewAdapter interface implementation
        public event EventHandler<GroupItemMovedEventArgs> ItemMoved;

        public bool OnMove(RecyclerView.ViewHolder viewHolder1, RecyclerView.ViewHolder viewHolder2)
        {
            int fromPosition = viewHolder1.AdapterPosition;
            int toPosition = viewHolder2.AdapterPosition;

            // Do not move any item above the very first group item (group header) position
            if (toPosition <= 0)
            {
                return false;
            }

            IGroupRecyclerViewAdapterItem source = GetAdapterItem(fromPosition);
            IGroupRecyclerViewAdapterItem target = GetAdapterItem(toPosition);

            GroupItemMovedEventArgs args = new GroupItemMovedEventArgs(source.Parent, source.Item, target.Parent, target.Item);

            ItemMoved?.Invoke(this, args);

            return true;
        }
        #endregion

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
