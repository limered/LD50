using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Random = UnityEngine.Random;

namespace SystemBase.Utils
{
    public static class LinqExtensions
    {
        public static T NthElement<T>(this IEnumerable<T> coll, int n)
        {
            return coll.OrderBy(x => x).Skip(n - 1).FirstOrDefault();
        }

        [Pure]
        public static List<T> Randomize<T>(this List<T> list)
        {
            var oldList = list.ToList();
            var result = new List<T>(list.Count);
            while (oldList.Count > 0)
            {
                var rnd = (int) (Random.value * oldList.Count);
                result.Add(oldList[rnd]);
                oldList.RemoveAt(rnd);
            }
            return result;
        }
        
        public static IEnumerable<T> AddDefaultCount<T>(this IEnumerable<T> coll, int n) where T : new()
        {
            var result = new List<T>();
            for (var i = 0; i < n; i++)
            {
                result.Add(new T());
            }

            return coll.Concat(result);
        }
    }
}
