namespace MaterialEditor.Lib
{
    public record struct MaterialFile
    {
        private Material[] Materials { get; set; }

        public int Length => Materials.Length;

        private JArray Array { get; set; }

        public MaterialFile(string text) : this() => InitLines(text);

        public MaterialFile(FileInfo file) : this(File.ReadAllText(file.FullName)) { }

        public MaterialFile(StreamReader sr) : this(sr.ReadToEnd()) { }

        public Material this[int index]
        {
            get => Materials[index];
            set => Materials[index] = value;
        }

        private void InitLines(string text)
        {
            Array = JArray.Parse(text);
            Materials = Array.ToObject<Material[]>();
        }

        public override string ToString()
        {
            Array = JArray.FromObject(Materials);
            return Array.ToString(Formatting.Indented);
        }

        public static implicit operator MaterialFile(MAT3 mat) =>
            new(JsonConvert.SerializeObject(mat.m_Materials));
    }
}
