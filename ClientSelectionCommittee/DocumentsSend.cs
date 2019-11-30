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
    public class DocumentsSend
    {
        public int Id { get; set; }
        public int IdEnrollee { get; set; }
        public string NameDocument { get; set; }
        public string NumberDocument { get; set; }
        public string Description { get; set; }

        public DocumentsSend()
        { }

        public DocumentsSend(int idEnr, string nameDoc, string numberDoc, string descrip)
        {
            IdEnrollee = idEnr;
            NameDocument = nameDoc;
            NumberDocument = numberDoc;
            Description = descrip;
        }

        public static void DataSerializable(List<DocumentsSend> documentsSends)
        { 
            XmlSerializer formatter = new XmlSerializer(typeof(List<DocumentsSend>));

            // получаем поток, куда будем записывать сериализованный объект
            using (FileStream fs = new FileStream("SerializableFile/DocumentsSend.xml", FileMode.Create))
            {
                formatter.Serialize(fs, documentsSends);
            }
        }

        public static List<DocumentsSend> DataDeserialize()
        {
            List<DocumentsSend> documentsSends = null;

            using (FileStream fs = new FileStream("DeserializeFile/DocumentsSend.xml", FileMode.Open))
            {
                XmlSerializer formatter = new XmlSerializer(typeof(List<DocumentsSend>));

                documentsSends = (List<DocumentsSend>)formatter.Deserialize(fs);
            }

            return documentsSends;
        }

        // запись в xml, возвращает xml запись 
        public static string ReadToXml()
        {
            string xmlData = null;

            using (StreamReader reader = new StreamReader("SerializableFile/DocumentsSend.xml"))
            {
                xmlData = reader.ReadToEnd();
            }

            return xmlData;
        }


        // запись в xml
        public static void WriteToXml(string words)
        {
            File.Delete("DeserializeFile/GetDoc.xml");
            using (StreamWriter writer = new StreamWriter("DeserializeFile/GetDoc.xml"))
            {
                writer.Write(words);
            }
        }


        public static List<DocumentsSend> DeserializeFileXml()
        {
            using (FileStream fs = new FileStream("DeserializeFile/GetDoc.xml", FileMode.Open))
            {
                XmlSerializer formatter = new XmlSerializer(typeof(List<DocumentsSend>));

                return (List<DocumentsSend>)formatter.Deserialize(fs);
            }
        }

        public void ThisSerializable()
        {
            XmlSerializer formatter = new XmlSerializer(typeof(DocumentsSend));

            // получаем поток, куда будем записывать сериализованный объект
            using (FileStream fs = new FileStream("SerializableFile/UpdateDoc.xml", FileMode.Create))
            {
                formatter.Serialize(fs, this);
            }
        }

        public string ReadToXmlOne()
        {
            string xmlData = null;

            using (StreamReader reader = new StreamReader("SerializableFile/UpdateDoc.xml"))
            {
                xmlData = reader.ReadToEnd();
            }

            return xmlData;
        }

    }
}
