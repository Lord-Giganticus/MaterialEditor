global using MaterialEditor.Lib;
global using BenchmarkDotNet.Attributes;
global using BenchmarkDotNet.Running;
global using Newtonsoft.Json.Linq;

namespace GrandStarMaterialEditor.Tests;

class Program
{
    static void Main(string[] args)
    {
#if RELEASE
        BenchmarkRunner.Run<Bench>();
#elif DEBUG
        var m = new MaterialFile(new FileInfo("grandstar_materials.json"));
        m[1].TevColors[0] = new Color(88, 29, 144);
        (double, double, double, double) tup = (Color)m[1].TevColors[0];
#endif
    }
}