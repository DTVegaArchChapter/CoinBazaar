using CoinBazaar.Infrastructure.Annotations;
using System;
using System.Linq;

namespace CoinBazaar.Infrastructure.Extensions
{
    public static class EventSourcingAttributeExtensions
    {
        public static TValue GetAttributeValue<TAttribute, TValue>(this Type type, Func<TAttribute, TValue> valueSelector)
            where TAttribute : Attribute
        {
            var attribute = type.GetCustomAttributes(typeof(TAttribute), true)
                .FirstOrDefault() as TAttribute;

            if (attribute != null)
            {
                return valueSelector(attribute);
            }

            return default(TValue);
        }

        public static string GetStreamName<T>()
        {
            var streamAttribute = typeof(T)
                .GetCustomAttributes(typeof(StreamName), true)
                .FirstOrDefault() as StreamName;

            if (streamAttribute != null)
            {
                return streamAttribute.Value;
            }

            throw new NotImplementedException("AggregateRoot must have \"Stream\" attribute.");
        }
    }
}
