using BenchmarkDotNet.Attributes;
using Retard.Engine.Models.ValueTypes;

namespace Retard.Tests
{
    [MemoryDiagnoser]
    public class BenchmarkTest
    {
        [Benchmark]
        public string Benchmark1()
        {
            string[] s = new string[10000];

            for (int i = 0; i < s.Length; ++i)
            {
                s[i] = "a";
            }

            return "a";
        }

        [Benchmark]
        public NativeString Benchmark2()
        {
            NativeString[] s = new NativeString[10000];

            for (int i = 0; i < s.Length; ++i)
            {
                s[i] = "a";
            }

            return "a";
        }
    }
}
