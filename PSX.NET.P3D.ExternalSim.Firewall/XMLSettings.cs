using System;
using System.IO;
using System.Xml.Serialization;

namespace PSX.NET.P3D.ExternalSim.Firewall
{
    public static class XMLSettings
    {
        public static void Save<T>(this T objectToSerialize, string name)
        {
            string file = Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments), name + ".xml");

            using (var stream = new FileStream(file, FileMode.Create))
            {
                new XmlSerializer(typeof(T)).Serialize(stream, objectToSerialize);
            }
        }

        public static T Load<T>(string name)
        {
            string file = Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments), name + ".xml");
            using (var stream = new FileStream(file, FileMode.Open))
            {
                XmlSerializer s = new XmlSerializer(typeof(T));
                return (T)s.Deserialize(stream);
            }
        }

        public static bool Exist(string name)
        {
            string file = Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments), name + ".xml");

            return File.Exists(file);
        }
    }
}
