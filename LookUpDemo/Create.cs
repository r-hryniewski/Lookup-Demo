using BenchmarkDotNet.Attributes;
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
        public void CreateDictionary()
        {
            var x = items
                .GroupBy(i => i.Date.Month)
                .ToDictionary(g => g.Key, g => g.Select(i => i.Name).AsEnumerable());
        }

        [Benchmark]
        public void CreateLookup()
        {
            var x = items.ToLookup(i => i.Date.Month, i => i.Name);
        }
    }
}
