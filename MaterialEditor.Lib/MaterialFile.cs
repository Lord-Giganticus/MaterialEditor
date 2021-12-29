namespace MaterialEditor.Lib
{
    public record struct MaterialFile
    {
        public TevColor?[][] TevColors { get; set; }
        [JsonIgnore]
        private JArray Array { get; set; }

        public MaterialFile(string text) : this() => InitLines(text);

        public MaterialFile(FileInfo file) : this(File.ReadAllText(file.FullName)) { }

        public MaterialFile(StreamReader sr) : this(sr.ReadToEnd()) { }

        public JToken this[int index]
        {
            get => Array[index];
            set => Array[index] = value;
        }

        private void InitLines(string text)
        {
            Array = JArray.Parse(text);
            var colors = new TevColor?[Array.Count][];
            for (int i = 0; i < Array.Count; i++)
                    colors[i] = Array[i]["TevColors"].ToObject<TevColor?[]>();
            TevColors = colors;
        }

        public override string ToString()
        {
            for (int index = 0; index < Array.Count; index++)
                Array[index]["TevColors"] = JArray.FromObject(TevColors[index]);
            return Array.ToString(Formatting.Indented);
        }

        public static implicit operator MaterialFile(MAT3 mat) =>
            new(JsonConvert.SerializeObject(mat.m_Materials));
    }
}
