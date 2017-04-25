using BenchmarkDotNet.Attributes;
using System.Collections.Generic;
using System.Linq;

namespace LookUpDemo
{
    public class GetValue
    {
        private Item[] items;
        private Dictionary<int, IEnumerable<string>> dict;
        private ILookup<int, string> lookup;

        [Setup]
        public void Setup()
        {
            items = Item.GetItems();
            dict = items
                .GroupBy(i => i.Date.Month)
                .ToDictionary(g => g.Key, g => g.Select(i => i.Name).AsEnumerable());
            lookup = items.ToLookup(i => i.Date.Month, i => i.Name);
        }

        [Benchmark]
        public List<string> GetFromArray()
        {
            return items.Where(i => i.Date.Month == 2)
                .Select(i => i.Name)
                .ToList();
        }

        [Benchmark]
        public List<string> GetFromDictionary()
        {
            return dict[2].ToList();
        }

        [Benchmark]
        public List<string> GetFromLookup()
        {
            return lookup[2].ToList();
        }
    }
}
