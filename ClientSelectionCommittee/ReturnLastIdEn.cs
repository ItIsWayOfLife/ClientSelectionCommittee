using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientSelectionCommittee
{
    class ReturnLastIdEn
    {
        public string ReturnLastId()
        {
            string message = "ReturnLastIdEn ";

            TcpClient client = null;
            try
            {
                client = new TcpClient("127.0.0.1", 1234);
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
                    throw new Exception("Ошибка сервера: " + message);
                }
                else
                {
                    return message;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка " + ex);
            }
            finally
            {
                client.Close();
            }

            return null;
        }
    }
}
