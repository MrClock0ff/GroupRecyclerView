using AndroidX.RecyclerView.Widget;

namespace GroupRecyclerView.Widgets
{
    /// <summary>
    /// GroupRecyclerView cappable of moving the item between the groups
    /// </summary>
    public interface IDraggableGroupRecyclerView
    {
        /// <summary>
        /// Handle move of viewHolder1 into viewHolder2 position
        /// </summary>
        /// <param name="viewHolder1"></param>
        /// <param name="viewHolder2"></param>
        /// <returns></returns>
        bool OnMove(RecyclerView.ViewHolder viewHolder1, RecyclerView.ViewHolder viewHolder2);
    }
}
