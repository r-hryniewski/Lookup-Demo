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
        public void GetFromArray()
        {
            var x = items.Where(i => i.Date.Month == 1)
                .Select(i => i.Name)
                .ToList();
            var y = items.Where(i => i.Date.Month == 2).Select(i => i.Name).ToList();
            var z = items.Where(i => i.Date.Month == 3).Select(i => i.Name).ToList();
        }

        [Benchmark]
        public void GetFromDictionary()
        {
            var y = dict[2].ToList();
        }

        [Benchmark]
        public void GetFromLookup()
        {
            var y = lookup[2].ToList();
        }
    }
}
