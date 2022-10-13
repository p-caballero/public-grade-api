namespace UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    public class ListVsIEnumerableTest
    {
        [Fact]
        public void Using_list()
        {
            var collection = new List<int>() { 2, 4, 6, 8, 10 };

            int count = 0;

            IEnumerable<int> result = collection.Select(x =>
            {
                count++;
                Console.WriteLine(x);

                return x * 2;
            });

            Assert.Equal(0, count);
        }

        [Fact]
        public void Using_list_tolist()
        {
            var collection = new List<int>() { 2, 4, 6, 8, 10 };

            int count = 0;

            IEnumerable<int> result = collection.Select(x =>
            {
                count++;
                Console.WriteLine(x);

                return x * 2;
            }).ToList();

            Assert.Equal(5, count);
        }

        [Fact]
        public void Using_list_toarray()
        {
            var collection = new List<int>() { 2, 4, 6, 8, 10 };

            int count = 0;

            var result = collection.Select(x =>
            {
                count++;
                Console.WriteLine(x);

                return x * 2;
            }).ToArray();

            Assert.Equal(5, count);
        }

        [Fact]
        public void Using_list_first()
        {
            var collection = new List<int>() { 2, 4, 6, 8, 10 };

            int count = 0;

            var result = collection.Select(x =>
            {
                count++;
                Console.WriteLine(x);

                return x * 2;
            }).First(x => x == 8);

            Assert.Equal(2, count);
        }

        [Fact]
        public void Using_list_ToDictionary()
        {
            var collection = new List<int>() { 2, 4, 6, 8, 10 };

            int count = 0;

            var result = collection.Select(x =>
            {
                count++;
                Console.WriteLine(x);

                return x * 2;
            }).ToDictionary(x => x, x => x % 2 == 0);

            Assert.Equal(5, count);
        }

        [Fact]
        public void Using_list_ToDictionary_as_switch()
        {
            var options = new Dictionary<int, string>()
            {
                { 1, "ONE" },
                { 2, "TWO" },
                { 3, "3" },
                { 4, "4A" },
            };

            int miVar = 3;
            if (!options.TryGetValue(miVar, out string myName))
            {
                myName = "NOT FOUND";
            }

            Console.WriteLine(myName);
        }
    }
}
