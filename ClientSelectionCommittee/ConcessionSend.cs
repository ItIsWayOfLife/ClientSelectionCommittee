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
    public class ConcessionSend
    {
        public int Id { get; set; }
        public string NameConcession { get; set; }


        // запись в xml
        public static void WriteToXml(string words)
        {
            File.Delete("DeserializeFile/ConcessionSend.xml");
            using (StreamWriter writer = new StreamWriter("DeserializeFile/ConcessionSend.xml"))
            {
                writer.Write(words);
            }
        }


        public static List<ConcessionSend> DeserializeFileXml()
        {
            using (FileStream fs = new FileStream("DeserializeFile/ConcessionSend.xml", FileMode.Open))
            {
                XmlSerializer formatter = new XmlSerializer(typeof(List<ConcessionSend>));

                return (List<ConcessionSend>)formatter.Deserialize(fs);
            }
        }

    }
}