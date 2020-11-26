using System.IO;
using Newtonsoft.Json;

namespace Main
{
    public class JSONSerializerAdapter : ISerializer
    {
        public void Serialize(object o, StreamWriter stream)
        {
            JsonSerializer serializer = new JsonSerializer()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.Indented
            };


            serializer.Serialize(stream, o);
            stream.WriteLine("\n");
            stream.Flush();
        }
    }
}