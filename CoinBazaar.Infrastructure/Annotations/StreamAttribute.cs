using System;

namespace CoinBazaar.Infrastructure.Annotations
{
    public class StreamName : Attribute
    {
        public string Value { get; set; }
        public StreamName(string value)
        {
            Value = value;
        }
    }
}
