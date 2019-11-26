using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TestClient
{
    [Serializable]
    public class LevelEducationSend
    {
        public int Id { get; set; }
        public string NameLevelEducation { get; set; }


        // запись в xml
        public static void WriteToXml(string words)
        {
            File.Delete("DeserializeFile/LevelEducationSend.xml");
            using (StreamWriter writer = new StreamWriter("DeserializeFile/LevelEducationSend.xml"))
            {
                writer.Write(words);
            }
        }


        public static List<LevelEducationSend> DeserializeFileXml()
        {
            using (FileStream fs = new FileStream("DeserializeFile/LevelEducationSend.xml", FileMode.Open))
            {
                XmlSerializer formatter = new XmlSerializer(typeof(List<LevelEducationSend>));

                return (List<LevelEducationSend>)formatter.Deserialize(fs);
            }
        }

    }
}