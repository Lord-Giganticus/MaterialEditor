using System;
using System.IO;
using MaterialEditor.Lib;

namespace MaterialEditor.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            var m = new MaterialFile(new FileInfo("grandstar_materials.json"));
            m.TevColors[1][0] = new TevColor((88, 29, 144));
            File.WriteAllText("test.json", m.Serialize(new int[]{ 1 }));
        }
    }
}
