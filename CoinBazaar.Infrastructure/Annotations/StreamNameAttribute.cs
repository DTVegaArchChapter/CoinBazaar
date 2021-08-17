namespace CoinBazaar.Infrastructure.Annotations
{
    using System;
    using System.Linq;

    [AttributeUsage(AttributeTargets.Class)]
    public sealed class StreamNameAttribute : Attribute
    {
        public string Value { get; set; }

        public StreamNameAttribute(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(value));
            }

            Value = value;
        }

        public static string GetStreamName<TStreamName>()
        {
            if (typeof(TStreamName)
                    .GetCustomAttributes(typeof(StreamNameAttribute), true)
                    .FirstOrDefault() is StreamNameAttribute streamAttribute)
            {
                return streamAttribute.Value;
            }

            throw new InvalidOperationException("AggregateRoot must have \"Stream\" attribute.");
        }
    }
}
