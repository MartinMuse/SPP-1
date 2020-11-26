using System.IO;
using System.Xml.Serialization;

namespace Main
{
    public class XMLSerializerAdapter : ISerializer
    {
        public void Serialize(object o, StreamWriter stream)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(o.GetType());
            xmlSerializer.Serialize(stream, o);
            stream.WriteLine("\n");
            stream.Flush();
        }
    }
}