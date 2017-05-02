using BenchmarkDotNet.Attributes;
using System.Collections.Generic;
using System.Linq;

namespace LookUpDemo
{
    public class ForLoop
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
        public void ForThroughDictionaryWithEnumerable()
        {
            IEnumerable<string> x;
            for (int i = 1; i <= 12; i++)
            {
                if (dictAsEnumerable.TryGetValue(i, out IEnumerable<string> val))
                {
                    x = val;
                }
            }
        }

        [Benchmark]
        public void ForThroughDictionaryWithList()
        {
            IEnumerable<string> x;
            for (int i = 1; i <= 12; i++)
            {
                if (dictToList.TryGetValue(i, out List<string> val))
                {
                    x = val;
                }
            }
        }

        [Benchmark]
        public void ForThroughDictionaryWithArray()
        {
            IEnumerable<string> x;
            for (int i = 1; i <= 12; i++)
            {
                if (dictToArray.TryGetValue(i, out string[] val))
                {
                    x = val;
                }
            }
        }

        [Benchmark]
        public void ForThroughLookup()
        {
            IEnumerable<string> x;
            for (int i = 1; i <= 12; i++)
            {
                x = lookup[i];
            }
        }
    }
}
