﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientSelectionCommittee
{
    class UpdateEnrollee
    {
        public string UpdateEnrolleeTo(EnrolleeSend en, string login)
        {
            en.ThisSerializable();

            // заголовок
            string message = "UpdateEnrollee";

            message += login + " ";

            // добавление заголовка
            message += en.ReadToXml();

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
                MessageBox.Show("Ошибка: " + ex.ToString());
                return null;
            }
            finally
            {
                client.Close();
            }

            return message;
        }
    }
}
