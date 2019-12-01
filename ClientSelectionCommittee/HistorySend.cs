using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ClientSelectionCommittee
{
    [Serializable]
    public class HistorySend
    {
        public int Id { get; set; }
        public int IdEnrollee { get; set; }
        public string Operation { get; set; }
        public DateTime CreateAt { get; set; }

        // запись в xml
        public static void WriteToXml(string words)
        {
            File.Delete("DeserializeFile/GetHis.xml");
            using (StreamWriter writer = new StreamWriter("DeserializeFile/GetHis.xml"))
            {
                writer.Write(words);
            }
        }

        public static List<HistorySend> DeserializeFileXml()
        {
            using (FileStream fs = new FileStream("DeserializeFile/GetHis.xml", FileMode.Open))
            {
                XmlSerializer formatter = new XmlSerializer(typeof(List<HistorySend>));

                return (List<HistorySend>)formatter.Deserialize(fs);
            }
        }
    }
}
