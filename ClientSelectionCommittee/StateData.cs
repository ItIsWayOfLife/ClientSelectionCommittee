using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ClientSelectionCommittee
{
    [Serializable]
    public  class StateData
    {
        public  double CountEnrolee {get;set;}
        public  double CountEnroleeBigjet { get; set; }
        public  double CountEnroleePay { get; set; }
        public  double PercentBrest { get; set; }
        public  double PercentGomel { get; set; }
        public  double PercentGrodno { get; set; }
        public  double PercentVitebsk { get; set; }
        public  double PercentMinsk { get; set; }
        public  double PercentMogilevsk { get; set; }
        public  double PercentMinskregion { get; set; }
        public  double EmrolleeThisPoint { get; set; }


        public static StateData GetData(string login)
        {
           StateData stateData = null;

            string message = "GetStateData ";

            message += login;

            TcpClient client = null;
            try
            {
                client = GetTcpClient.GetTcpClient_;
                NetworkStream stream = client.GetStream();

                // Преобразуем сообщение в массив байтов
                byte[] data = Encoding.Unicode.GetBytes(message);

                // Отправляем сообщение
                stream.Write(data, 0, data.Length);
                //

                // Получаем ответ сервера
                data = new byte[256];
                StringBuilder response = new StringBuilder();
                int bytes = 0;
                do
                {
                    bytes = stream.Read(data, 0, data.Length);
                    response.Append(Encoding.Unicode.GetString(data, 0, bytes));
                }
                while (stream.DataAvailable);
                message = response.ToString();

                if (message.Contains("Ошибка"))
                {
                    throw new Exception("Ошибка сервера");
                }
                else
                {
                    WriteToXml(message);
                    stateData = DeserializeFileXml();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка " + ex);

            }
            finally
            {
                client.Close();
            }

            return stateData;
        }


        // запись в xml
        public static void WriteToXml(string words)
        {
            File.Delete("DeserializeFile/GetStateData.xml");
            using (StreamWriter writer = new StreamWriter("DeserializeFile/GetStateData.xml"))
            {
                writer.Write(words);
            }
        }


        public static StateData DeserializeFileXml()
        {
            using (FileStream fs = new FileStream("DeserializeFile/GetStateData.xml", FileMode.Open))
            {
                XmlSerializer formatter = new XmlSerializer(typeof(StateData));

                return (StateData)formatter.Deserialize(fs);
            }
        }
    }
}
