using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MaterialEditor.Lib
{
    public class MaterialFile
    {
        public TevColor[][] TevColors { get; set; }
        [JsonIgnore]
        protected JArray Array { get; set; }

        private MaterialFile() => TevColors = new TevColor[][]{};

        public MaterialFile(string text) : this() => InitLines(text);

        public MaterialFile(FileInfo file) : this(File.ReadAllText(file.FullName)) { }

        public MaterialFile(StreamReader sr) : this(sr.ReadToEnd()) { }

        protected void InitLines(string text)
        {
            Array = JArray.Parse(text);
            var colors = new TevColor[Array.Count][];
            for (int i = 0; i < Array.Count; i++)
                colors[i] = Array[i]["TevColors"].ToObject<TevColor[]>();
            TevColors = colors;
        }

        public string Serialize(int[] indexes)
        {
            foreach (var index in indexes)
                Array[index]["TevColors"] = JArray.FromObject(TevColors[index]);
            return Array.ToString(Formatting.Indented);
        }
    }
}
