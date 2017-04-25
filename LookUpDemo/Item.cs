using System;
using System.Linq;

namespace LookUpDemo
{
    public class Item
    {
        public int Id {get; set;}
        public string Name { get; set; }
        public DateTime Date { get; set; }

        public Item(int n, DateTime date)
        {
            Id = n;
            Name = $"Item-{n}";
            Date = date.AddHours(-n);
        }

        public static Item[] GetItems()
        {
            var now = new DateTime(2017, 1, 1);
            return Enumerable.Range(1, 1000000).Select(n => new Item(n, now)).ToArray();
        }
    }
}
