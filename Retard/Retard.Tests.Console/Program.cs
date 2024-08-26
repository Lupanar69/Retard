using BenchmarkDotNet.Running;

namespace Retard.Tests.Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<BenchmarkTest>();
            System.Console.ReadLine();
        }
    }
}
