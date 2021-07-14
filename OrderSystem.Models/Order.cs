using System;

namespace MediaValet.Models
{
    public class Order
    {
        public long OrderId { get; set; }
        public long MagicNumber { get; set; }
        public string OrderText { get; set; }
    }
}
