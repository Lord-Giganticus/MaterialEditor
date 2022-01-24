namespace MaterialEditor.Lib
{
    public record struct TexHeader
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

        public static TexHeaderFile GetTexHeaders(JArray arr) => (arr.ToObject<TexHeader?[]>(), arr);

        public static TexHeaderFile GetTexHeaders(string text) => GetTexHeaders(JArray.Parse(text));

        public static TexHeaderFile GetTexHeaders(StreamReader sr) => GetTexHeaders(sr.ReadToEnd());

        public static TexHeaderFile GetTexHeaders(FileInfo file) => GetTexHeaders(File.ReadAllText(file.FullName));
    }
}
