using System;
using System.Collections.Generic;

namespace CS_ReplaceForeachWithLinq
{
    public static class IEnumerableExtensions
    {
        public static void ForEach<T>( this IEnumerable<T> values , Action<T> action )
        {
            foreach( var value in values )
            {
                action( value );
            }
        }
    }
}
