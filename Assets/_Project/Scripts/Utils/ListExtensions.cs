using System;
using System.Collections.Generic;
using System.Linq;

namespace _Project.Scripts.Utils
{
    public static class ListExtensions
    {
        private static readonly Random Random = new();

        public static T RandomOrDefault<T>(
            this IEnumerable<T> source,
            Func<T, bool> predicate
        )
        {
            var array = source.Where(predicate).ToArray();
            return array.Length == 0
                ? default
                : array[Random.Next(array.Length)];
        }
    }
}