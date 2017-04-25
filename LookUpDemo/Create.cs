using BenchmarkDotNet.Attributes;
using System.Collections.Generic;
using System.Linq;

namespace LookUpDemo
{
    public class Create
    {
        private Item[] items;

        [Setup]
        public void Setup()
        {
            items = Item.GetItems();
        }

        [Benchmark]
        public Dictionary<int, IEnumerable<string>> CreateDictionary()
        {
            return items
                .GroupBy(i => i.Date.Month)
                .ToDictionary(g => g.Key, g => g.Select(i => i.Name).AsEnumerable());
        }

        [Benchmark]
        public ILookup<int, string> CreateLookup()
        {
            return items.ToLookup(i => i.Date.Month, i => i.Name);
        }
    }
}
