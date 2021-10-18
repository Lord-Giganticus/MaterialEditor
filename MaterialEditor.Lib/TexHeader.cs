using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MaterialEditor.Lib
{
    public class TexHeader
    {
        public string Name;
        public string Format;
        public int AlphaSetting;
        public string WrapS;
        public string WrapT;
        public string PaletteFormat;
        public int MipMap;
        public bool EdgeLOD;
        public bool BiasClamp;
        public int MaxAniso;
        public string MinFilter;
        public string MagFilter;
        public double MinLOD;
        public double MaxLOD;
        public double LodBias;

        private TexHeader() { }

        public static TexHeader[] GetTexHeaders(JArray arr) => arr.ToObject<TexHeader[]>();

        public static TexHeader[] GetTexHeaders(string text) => GetTexHeaders(JArray.Parse(text));

        public static TexHeader[] GetTexHeaders(StreamReader sr) => GetTexHeaders(sr.ReadToEnd());

        public static TexHeader[] GetTexHeaders(FileInfo file) => GetTexHeaders(File.ReadAllText(file.FullName));

        public static JArray ToJArray(TexHeader[] arr) => JArray.FromObject(arr);

        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
    }
}
