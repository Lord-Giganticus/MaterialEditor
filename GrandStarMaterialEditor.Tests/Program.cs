global using MaterialEditor.Lib;
global using BenchmarkDotNet.Attributes;
global using BenchmarkDotNet.Running;

namespace GrandStarMaterialEditor.Tests;

class Program
{
    static void Main(string[] args)
    {
#if RELEASE
        BenchmarkRunner.Run<Bench>();
#endif
    }
}