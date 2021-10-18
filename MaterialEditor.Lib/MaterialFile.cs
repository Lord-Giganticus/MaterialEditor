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

        private MaterialFile() => TevColors = new TevColor[][] { };

        public MaterialFile(string text) : this() => InitLines(text);

        public MaterialFile(FileInfo file) : this(File.ReadAllText(file.FullName)) { }

        public MaterialFile(StreamReader sr) : this(sr.ReadToEnd()) { }

        protected void InitLines(string text)
        {
            var colors = new List<TevColor[]>();
            Array = JArray.Parse(text);
            foreach (var obj in Array.Children<JObject>())
                foreach (var prop in obj.Properties())
                    if (prop.Name is "TevColors")
                        colors.Add(prop.Value.ToObject<TevColor[]>());
            TevColors = colors.ToArray();
        }

        public string Serialize(int[] indexes)
        {
            foreach (var index in indexes)
                Array[index]["TevColors"] = JArray.FromObject(TevColors[index]);
            return Array.ToString(Formatting.Indented);
        }
    }
}
