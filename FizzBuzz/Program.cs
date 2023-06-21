using BenchmarkDotNet.Running;

namespace FizzBuzz
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<FizzBuzz>();
        }
    }
}