using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegaEventsBotDotNet
{
    public static class ColectionExtension
    {
        private static Random rng = new Random(Guid.NewGuid().GetHashCode());

        public static T RandomElement<T>(this IList<T> list)
        {
            var id = rng.Next(list.Count);
            return list[id];
        }

        public static T RandomElement<T>(this T[] array)
        {
            return array[rng.Next(array.Length)];
        }
    }
    class Utils
    {
    }
}
