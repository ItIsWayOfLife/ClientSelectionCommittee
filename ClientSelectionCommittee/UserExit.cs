using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace ClientSelectionCommittee
{
    class UserExit
    {
        public void Exit(int id)
        {
            string message = "OutUser ";
            message += id.ToString();

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
            }
            catch (Exception ex)
            {
             
            }
            finally
            {
                client.Close();
            }
        }
    }
}

