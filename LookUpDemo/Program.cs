using BenchmarkDotNet.Running;

namespace LookUpDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<Create>();
            BenchmarkRunner.Run<GetValue>();
        }
    }
}
