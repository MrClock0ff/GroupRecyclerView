using System.Collections;

namespace GroupRecyclerView.Extensions
{
    /// <summary>
    /// Enumerable convinience methods
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Get item count
        /// </summary>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        public static int Count(this IEnumerable enumerable)
        {
            if (enumerable == null)
            {
                return 0;
            }

            if (enumerable is ICollection collection)
            {
                return collection.Count;
            }

            int count = 0;
            IEnumerator enumerator = enumerable.GetEnumerator();

            while (enumerator.MoveNext())
            {
                count++;
            }

            return count;
        }
    }
}
