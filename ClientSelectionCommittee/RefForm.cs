using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Printing;

namespace ClientSelectionCommittee
{
    public partial class RefForm : Form
    {
        EnrolleeSend enrolleeSend = null;

        public RefForm(EnrolleeSend enrolleeSend)
        {
            InitializeComponent();
            this.enrolleeSend = enrolleeSend;
        }

        private void FileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ToolStripButton2_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
        }

        string[] arrayTemplate = new string[] { "Пустой","Справка","Расписка"};

        private void RefForm_Load(object sender, EventArgs e)
        {
            toolStripLabel3.Text = $"{enrolleeSend.EnrolleeLastname} {enrolleeSend.EnrolleeFirstname[0]}. {enrolleeSend.EnrolleePatronymic[0]}.";
            toolStripComboBox1.Items.AddRange(arrayTemplate);
            toolStripComboBox1.Text = arrayTemplate[0];
        }

        /// <summary>
        /// Открытие существующего файла
        /// </summary>
        private void MenuFileOpen()
        {
            if (openFileDialog1.ShowDialog() ==
               System.Windows.Forms.DialogResult.OK &&
               openFileDialog1.FileName.Length > 0)
            {
                try
                {
                    richTextBox1.LoadFile(openFileDialog1.FileName,
                       RichTextBoxStreamType.RichText);
                }
                catch (System.ArgumentException ex)
                {
                    richTextBox1.LoadFile(openFileDialog1.FileName,
                       RichTextBoxStreamType.PlainText);
                }

                this.Text = "Файл [" + openFileDialog1.FileName + "]";
            }
        }

        private void ОткрытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MenuFileOpen();
        }

        /// <summary>
        /// Сохранение документа в новом файле
        /// </summary>
        private void MenuFileSaveAs()
        {
            if (saveFileDialog1.ShowDialog() ==
               System.Windows.Forms.DialogResult.OK &&
            saveFileDialog1.FileName.Length > 0)
            {
                richTextBox1.SaveFile(saveFileDialog1.FileName);
                this.Text = "Файл [" + saveFileDialog1.FileName + "]";

            }
        }

        private void СохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MenuFileSaveAs();
        }

        /// <summary>
        /// Настройка параметров страницы
        /// </summary>
        private void MenuFilePageSetup()
        {
            pageSetupDialog1.ShowDialog();
        }

        private void НастройкаСтраницыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                MenuFilePageSetup();
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка, принтер не найден.","Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// StringReader для печати содержимого редактора текста
        /// </summary>
        private StringReader m_myReader;

        /// <summary>
        /// Номер текущей распечатываемой страницы документа
        /// </summary>
        private uint m_PrintPageNumber;

        /// <summary>
        /// Предварительный просмотр перед печатью документа
        /// </summary>
        private void MenuFilePrintPreview()
        {
            m_PrintPageNumber = 1;

            string strText = this.richTextBox1.Text;
            m_myReader = new StringReader(strText);
            Margins margins = new Margins(100, 50, 50, 50);

            printDocument1.DefaultPageSettings.Margins = margins;
            printPreviewDialog1.ShowDialog();

            m_myReader.Close();
        }

        private void ПредварительныйПросмотрToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                MenuFilePrintPreview();
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка, принтер не найден.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Печать документа
        /// </summary>
        private void MenuFilePrint()
        {
            m_PrintPageNumber = 1;

            string strText = this.richTextBox1.Text;
            m_myReader = new StringReader(strText);

            Margins margins = new Margins(100, 50, 50, 50);
            printDocument1.DefaultPageSettings.Margins = margins;

            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                this.printDocument1.Print();
            }
            m_myReader.Close();
        }

        private void ПечатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                MenuFilePrint();
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка, принтер не найден.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void PrintDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            int lineCount = 0;       // счетчик строк
            float linesPerPage = 0;  // количество строк на одной странице
            float yLinePosition = 0; // текущая позиция при печати по
                                     // вертикальной оси
            string currentLine = null;  // текст текущей строки

            // Шрифт для печати текста
            Font printFont = this.richTextBox1.Font;

            // Кисть для печати текста
            SolidBrush printBrush = new SolidBrush(Color.Black);

            // Размер отступа слева
            float leftMargin = e.MarginBounds.Left;

            // Размер отступа сверху
            float topMargin = e.MarginBounds.Top +
               3 * printFont.GetHeight(e.Graphics);

            // Вычисляем количество строк на одной странице с учетом отступа
            linesPerPage = (e.MarginBounds.Height -
               6 * printFont.GetHeight(e.Graphics)) /
               printFont.GetHeight(e.Graphics);

            // Цикл печати всех строк страницы
            while (lineCount < linesPerPage &&
               ((currentLine = m_myReader.ReadLine()) != null))
            {
                // Вычисляем позицию очередной распечатываемой строки
                yLinePosition = topMargin + (lineCount *
                  printFont.GetHeight(e.Graphics));

                // Печатаем очередную строку
                e.Graphics.DrawString(currentLine, printFont, printBrush,
                  leftMargin, yLinePosition, new StringFormat());

                // Переходим к следующей строке
                lineCount++;
            }

            // Печать колонтитулов страницы

            // Номер текущей страницы
            string sPageNumber = "Page " + m_PrintPageNumber.ToString();

            // Вычисляем размеры прямоугольной области, занимаемой верхним
            // колонтитулом страницы
            SizeF stringSize = new SizeF();
            stringSize = e.Graphics.MeasureString(sPageNumber, printFont,
               e.MarginBounds.Right - e.MarginBounds.Left);

            // Печатаем номер страницы
            e.Graphics.DrawString(sPageNumber, printFont, printBrush,
               e.MarginBounds.Right - stringSize.Width, e.MarginBounds.Top,
               new StringFormat());

            // Печатаем имя файла документа
            e.Graphics.DrawString(this.Text, printFont, printBrush,
               e.MarginBounds.Left, e.MarginBounds.Top, new StringFormat());

            // Кисть для рисования горизонтальной линии,
            // отделяющей верхний колонтитул
            Pen colontitulPen = new Pen(Color.Black);
            colontitulPen.Width = 2;

            // Рисуем верхнюю линию
            e.Graphics.DrawLine(colontitulPen,
               leftMargin,
               e.MarginBounds.Top + printFont.GetHeight(e.Graphics) + 3,
               e.MarginBounds.Right, e.MarginBounds.Top +
               printFont.GetHeight(e.Graphics) + 3);

            // Рисуем линию, отделяющую нижний колонтитул документа
            e.Graphics.DrawLine(colontitulPen,
               leftMargin, e.MarginBounds.Bottom - 3,
               e.MarginBounds.Right, e.MarginBounds.Bottom - 3);

            // Печатаем текст нижнего колонтитула
            e.Graphics.DrawString(
            "SimpleNotepad",
               printFont, printBrush,
               e.MarginBounds.Left, e.MarginBounds.Bottom, new StringFormat());

            // Если напечатаны не все строки документа,
            // переходим к следующей странице
            if (currentLine != null)
            {
                e.HasMorePages = true;
                m_PrintPageNumber++;
            }

            // Иначе завершаем печать страницы
            else
                e.HasMorePages = false;

            // Освобождаем ненужные более ресурсы
            printBrush.Dispose();
            colontitulPen.Dispose();
        }

        private void ToolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
