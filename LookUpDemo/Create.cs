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
        public Dictionary<int, IEnumerable<string>> CreateDictionaryWithGroupedEnumerable()
        {
            return items
                .GroupBy(i => i.Date.Month)
                .ToDictionary(g => g.Key, g => g.Select(i => i.Name).AsEnumerable());
        }

        [Benchmark]
        public Dictionary<int,string[]> CreateDictionaryWithArray()
        {
            return items
                .GroupBy(i => i.Date.Month)
                .ToDictionary(g => g.Key, g => g.Select(i => i.Name).ToArray());
        }
        [Benchmark]
        public Dictionary<int, List<string>> CreateDictionaryWithList()
        {
            return items
                .GroupBy(i => i.Date.Month)
                .ToDictionary(g => g.Key, g => g.Select(i => i.Name).ToList());
        }

        [Benchmark]
        public ILookup<int, string> CreateLookup()
        {
            return items.ToLookup(i => i.Date.Month, i => i.Name);
        }
    }
}
