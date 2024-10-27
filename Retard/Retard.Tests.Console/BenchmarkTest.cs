using Arch.LowLevel;
using BenchmarkDotNet.Attributes;
using FixedStrings;
using Retard.Core.Models.ValueTypes;

namespace Retard.Tests.Console
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
                s[i] = $"{i}";
            }

            return $"{0}";
        }

        [Benchmark]
        public NativeString Benchmark2()
        {
            using UnsafeArray<NativeString> s = new(10000);

            for (int i = 0; i < s.Length; ++i)
            {
                s[i] = $"{i}";
            }

            return $"{0}";
        }

        [Benchmark]
        public FixedString8 Benchmark3()
        {
            Span<FixedString8> s = stackalloc FixedString8[10000];

            for (int i = 0; i < s.Length; ++i)
            {
                s[i] = $"{i}";
            }

            return $"{0}";
        }

        [Benchmark]
        public FixedString16 Benchmark4()
        {
            Span<FixedString16> s = stackalloc FixedString16[10000];

            for (int i = 0; i < s.Length; ++i)
            {
                s[i] = $"{i}";
            }

            return $"{0}";
        }

        [Benchmark]
        public FixedString16 Benchmark5()
        {
            Span<FixedString16> s = stackalloc FixedString16[10000];

            for (int i = 0; i < s.Length; ++i)
            {
                s[i] = $"{i}";
            }

            return $"{0}";
        }

        [Benchmark]
        public FixedString32 Benchmark6()
        {
            Span<FixedString32> s = stackalloc FixedString32[10000];

            for (int i = 0; i < s.Length; ++i)
            {
                s[i] = $"{i}";
            }

            return $"{0}";
        }
    }
}
