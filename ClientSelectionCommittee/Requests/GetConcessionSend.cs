﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientSelectionCommittee
{
    class GetConcessionSend
    {
        public List<ConcessionSend> GetData()
        {
            List<ConcessionSend> concessionSends = null;

            string message = "GetConcessionSend";

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

                Console.WriteLine(message);

                if (message.Contains("Ошибка"))
                {
                    throw new Exception("Ошибка сервера");
                }
                else
                {
                    ConcessionSend.WriteToXml(message);
                    concessionSends = ConcessionSend.DeserializeFileXml();
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

            return concessionSends;
        }
    }
}
