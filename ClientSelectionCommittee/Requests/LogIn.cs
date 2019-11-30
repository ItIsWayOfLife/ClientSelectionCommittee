using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Windows.Forms;

namespace ClientSelectionCommittee
{
    [Serializable]
    public class LogIn
    {
        public string Login { get; set; }
        public string Password { get; set; }

        // вход
        public UserSend LoginTo()
        {
            // объект пользователь
            UserSend userSend = null;

            // сериализация
            DataSerializable();
            // заголовок
            string message = "LoginTo ";
            // добавление заголовка
            message += ReadToXml();

            TcpClient client = null;
            try
            {
                client = GetTcpClient.GetTcpClient_;
                NetworkStream stream = client.GetStream();

                // Преобразуем сообщение в массив байтов
                byte[] data = Encoding.Unicode.GetBytes(message);

                // Отправляем сообщение
                stream.Write(data, 0, data.Length);

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
                    // запись в xml
                    WriteToXml(message);

                    // десериализация
                    using (FileStream fs = new FileStream("DeserializeFile/UserSend.xml", FileMode.Open))
                    {
                        XmlSerializer formatter = new XmlSerializer(typeof(UserSend));

                        userSend = (UserSend)formatter.Deserialize(fs);
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                client.Close();
            }

            return userSend;          
        }

        // сериализаци LogIn
        public void DataSerializable()
        {
            XmlSerializer formatter = new XmlSerializer(typeof(LogIn));

            // получаем поток, куда будем записывать сериализованный объект
            using (FileStream fs = new FileStream("SerializableFile/LogIn.xml", FileMode.Create))
            {
                formatter.Serialize(fs, this);
            }
        }

        // считывание xml LogIn
        public string ReadToXml()
        {
            string xmlData = null;

            using (StreamReader reader = new StreamReader("SerializableFile/LogIn.xml"))
            {
                xmlData = reader.ReadToEnd();
            }

            return xmlData;
        }

        // запись полученного UserSend
        public void WriteToXml(string words)
        {
            File.Delete("DeserializeFile/UserSend.xml");
            using (StreamWriter writer = new StreamWriter("DeserializeFile/UserSend.xml"))
            {
                writer.Write(words);
            }
        }
    }
}
