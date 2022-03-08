namespace GroupRecyclerView.Widgets
{
    /// <summary>
    /// Recycler view adapter item wrapper
    /// </summary>
    public interface IGroupRecyclerViewAdapterItem
    {
        /// <summary>
        /// Wrapped item
        /// </summary>
        object Item { get; }

        /// <summary>
        /// Wrapped item's parent
        /// </summary>
        IItemGroup Parent { get; }

        /// <summary>
        /// Indicates if this item is a group of items
        /// </summary>
        bool IsGroup { get; }
    }
}
