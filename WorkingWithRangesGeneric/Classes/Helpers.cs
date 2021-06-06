using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkingWithRangesGeneric.Classes
{
    public class Helpers
    {
        /// <summary>
        /// A method to learn how indices work against generic lists.
        /// Not intended to use in an application.
        /// </summary>
        /// <typeparam name="T">type</typeparam>
        /// <param name="list">list to work with</param>
        /// <returns>list of <seealso cref="ElementContainer"/></returns>
        public static List<ElementContainer<T>> RangeDetails<T>(List<T> list)
        {
            var elementsList = list.Select((element, index) => new ElementContainer<T>
            {
                Value = element,
                StartIndex = new Index(index),
                EndIndex = new Index(Enumerable.Range(0, list.Count).Reverse().ToList()[index], true)
            }).ToList();

            return elementsList;
        }
    }
}
