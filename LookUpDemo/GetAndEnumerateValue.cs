using BenchmarkDotNet.Attributes;
using System.Collections.Generic;
using System.Linq;

namespace LookUpDemo
{
    public class GetAndEnumerateValue
    {
        private Item[] items;
        private Dictionary<int, IEnumerable<string>> dictAsEnumerable;
        private ILookup<int, string> lookup;
        private Dictionary<int, string[]> dictToArray;
        private Dictionary<int, List<string>> dictToList;

        [Setup]
        public void Setup()
        {
            items = Item.GetItems();
            dictAsEnumerable = items
                .GroupBy(i => i.Date.Month)
                .ToDictionary(g => g.Key, g => g.Select(i => i.Name).AsEnumerable());

            dictToArray = items
                .GroupBy(i => i.Date.Month)
                .ToDictionary(g => g.Key, g => g.Select(i => i.Name).ToArray());

            dictToList = items
                .GroupBy(i => i.Date.Month)
                .ToDictionary(g => g.Key, g => g.Select(i => i.Name).ToList());

            lookup = items.ToLookup(i => i.Date.Month, i => i.Name);
        }

        [Benchmark]
        public IEnumerable<string> GetFromArray()
        {
            return items.Where(i => i.Date.Month == 2)
                .Select(i => i.Name)
                .ToList();
        }

        [Benchmark]
        public IEnumerable<string> GetFromDictionaryWithEnumerable()
        {
            return dictAsEnumerable[2].ToList();
        }

        [Benchmark]
        public IEnumerable<string> GetFromDictionaryWithList()
        {
            return dictToList[2].ToList();
        }

        [Benchmark]
        public IEnumerable<string> GetFromDictionaryWithArray()
        {
            return dictToArray[2].ToList();
        }

        [Benchmark]
        public IEnumerable<string> GetFromLookup()
        {
            return lookup[2].ToList();
        }
    }
}
