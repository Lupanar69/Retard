using BenchmarkDotNet.Attributes;
using FixedStrings;
using Retard.Core.Models.ValueTypes;

namespace Retard.Tests.Console
{
    [MemoryDiagnoser]
    public class StringBenchmarkTest
    {
        [Benchmark]
        public string String()
        {
            string[] s = new string[10000];

            for (int i = 0; i < s.Length; ++i)
            {
                s[i] = $"{i}";
            }

            return $"{0}";
        }

        [Benchmark]
        public NativeString NativeString()
        {
            Span<NativeString> s = stackalloc NativeString[10000];

            for (int i = 0; i < s.Length; ++i)
            {
                s[i] = $"{i}";
            }

            return $"{0}";
        }

        [Benchmark]
        public FixedString8 FixedString8()
        {
            Span<FixedString8> s = stackalloc FixedString8[10000];

            for (int i = 0; i < s.Length; ++i)
            {
                s[i] = $"{i}";
            }

            return $"{0}";
        }

        [Benchmark]
        public FixedString16 FixedString16()
        {
            Span<FixedString16> s = stackalloc FixedString16[10000];

            for (int i = 0; i < s.Length; ++i)
            {
                s[i] = $"{i}";
            }

            return $"{0}";
        }

        [Benchmark]
        public FixedString32 FixedString32()
        {
            Span<FixedString32> s = stackalloc FixedString32[10000];

            for (int i = 0; i < s.Length; ++i)
            {
                s[i] = $"{i}";
            }

            return $"{0}";
        }

        [Benchmark]
        public FixedString64 FixedString64()
        {
            Span<FixedString64> s = stackalloc FixedString64[10000];

            for (int i = 0; i < s.Length; ++i)
            {
                s[i] = $"{i}";
            }

            return $"{0}";
        }
    }
}
