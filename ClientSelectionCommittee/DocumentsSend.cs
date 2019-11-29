﻿using System;
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

      

        //public static void DataSerializable()
        //{
        //    List<DocumentsSend> documentsSends = GetData();

        //    XmlSerializer formatter = new XmlSerializer(typeof(List<DocumentsSend>));

        //    // получаем поток, куда будем записывать сериализованный объект
        //    using (FileStream fs = new FileStream("SerializableFile/DocumentsSend.xml", FileMode.Create))
        //    {
        //        formatter.Serialize(fs, documentsSends);
        //    }
        //}

        //public static List<DocumentsSend> DataDeserialize()
        //{
        //    List<DocumentsSend> documentsSends = null;

        //    using (FileStream fs = new FileStream("DeserializeFile/DocumentsSend.xml", FileMode.Open))
        //    {
        //        XmlSerializer formatter = new XmlSerializer(typeof(List<DocumentsSend>));

        //        documentsSends = (List<DocumentsSend>)formatter.Deserialize(fs);
        //    }

        //    return documentsSends;
        //}

        //// запись в xml, возвращает xml запись 
        //public static string ReadToXml()
        //{
        //    string xmlData = null;

        //    using (StreamReader reader = new StreamReader("SerializableFile/DocumentsSend.xml"))
        //    {
        //        xmlData = reader.ReadToEnd();
        //    }

        //    return xmlData;
        //}

      
    }
}