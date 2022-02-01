using System.Collections;

namespace GroupRecyclerView.Widgets
{
    /// <summary>
    /// Items group
    /// </summary>
    /// <typeparam name="T">Type of the item in the group</typeparam>
    public interface IItemGroup : IEnumerable
    {
        /// <summary>
        /// Group title
        /// </summary>
        string GroupTitle { get; }
    }
}
