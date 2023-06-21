using BenchmarkDotNet.Attributes;



/*
 * // * Summary *
 * 
 * BenchmarkDotNet=v0.13.5, OS=Windows 11 (10.0.22621.1848/22H2/2022Update/SunValley2)
 * AMD Ryzen 5 3600, 1 CPU, 12 logical and 6 physical cores
 * .NET SDK=7.0.103
 *   [Host]     : .NET 6.0.14 (6.0.1423.7309), X64 RyuJIT AVX2
 *   DefaultJob : .NET 6.0.14 (6.0.1423.7309), X64 RyuJIT AVX2
 *   
 * |   Method |         Mean |      Error |     StdDev |   Gen0 | Allocated |
 * |--------- |-------------:|-----------:|-----------:|-------:|----------:|
 * |    Basic |     29.64 ns |   0.284 ns |   0.266 ns |      - |         - |
 * | Advanced | 18,746.77 ns | 142.003 ns | 132.830 ns | 4.5471 |   38072 B |
 * 
 */
namespace FizzBuzz
{
    [MemoryDiagnoser]
    public class FizzBuzz
    {

        //private static void Print(string value) => Console.WriteLine(value);
        //private static void Print(int value) => Console.WriteLine(value);

        private static void Print(string value) { }
        private static void Print(int value) { }

        #region Basic
        [Benchmark]
        public void Basic()
        {
            for (int i = 1; i <= 100; i++)
            {
                if (i % 3 == 0)
                {
                    Print("Fizz");
                    continue;
                }
                if (i % 5 == 0)
                {
                    Print("Fizz");
                    continue;
                }
                if (i % 3 == 0 && i % 5 == 0)
                {
                    Print("FizzBuzz");
                    continue;
                }
                Print(i);
            }
        }
        #endregion

        #region Advanced

        private static readonly Dictionary<int, string> _fizzBuzzDictionary = new() { { 3, "Fizz" }, { 5, "Buzz" } };

        private static readonly IEnumerable<int> _fizzBuzzKeys = _fizzBuzzDictionary.Select(d => d.Key);

        [Benchmark]
        public void Advanced()
        {
            for (int i = 1; i <= 100; i++)
            {
                List<string> words = GetWords(value: i);

                if (words.Any())
                {
                    Print(string.Join("", words));
                    continue;
                }

                Print(i);
            }
        }

        private static List<string> GetWords(int value) => _fizzBuzzKeys.Where(key => value % key == 0).Select(k => _fizzBuzzDictionary[k]).ToList();

        #endregion
    }
}