using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkingWithRangesGeneric.Classes
{
    public static class GeneralExtensions
    {
        /// <summary>
        /// Provides the ability to obtain a range for a list
        /// </summary>
        /// <typeparam name="T">List type</typeparam>
        /// <param name="list">Actual list</param>
        /// <param name="range"><see cref="Range"/></param>
        /// <returns></returns>
        /// <remarks>
        /// ranges are not supported for list which this method
        /// fills this gap
        /// </remarks>
        public static List<T> GetRange<T>(this List<T> list, Range range)
        {
            var (start, length) = range.GetOffsetAndLength(list.Count);
            return list.GetRange(start, length);
        }
    }
}
