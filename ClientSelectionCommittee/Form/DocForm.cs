using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientSelectionCommittee
{
    public partial class DocForm : Form
    {
        // абитуриент
        EnrolleeSend enrollee = null;

        List<DocumentsSend> documentsSends = null;

        public DocForm(EnrolleeSend enrollee)
        {
            InitializeComponent();
            this.enrollee = enrollee;
            toolStripLabel2.Text = enrollee.Id.ToString();
            LoadData();
        }

        void LoadData()
        {
            documentsSends = new GetDoc().GetData(enrollee.Id);
        }

        private void DocForm_Load(object sender, EventArgs e)
        {
            DrawData();
            toolStripStatusLabel2.Text = documentsSends.Count.ToString();
            toolStripComboBox1.Items.AddRange(arraySearch);
            toolStripComboBox1.Text = arraySearch[0];
        }

        private void DrawData()
        {
            // удаление данных с datagv (обратный цикл)
            for (int i = dataGridView1.RowCount - 1; i >= 0; i--)
            {
                dataGridView1.Rows.Remove(dataGridView1.Rows[i]);
            }

            foreach (DocumentsSend d in documentsSends)
            {
                // id нов строки
                int rowNumber = dataGridView1.Rows.Add();
                // запись в столбцы
                dataGridView1.Rows[rowNumber].Cells[0].Value = d.Id;
                dataGridView1.Rows[rowNumber].Cells[1].Value = d.NameDocument;
                dataGridView1.Rows[rowNumber].Cells[2].Value = d.NumberDocument;
                dataGridView1.Rows[rowNumber].Cells[3].Value = d.Description;
            }

            toolStripStatusLabel4.Text = dataGridView1.Rows.Count.ToString();
        }

        private void DrawData(List<DocumentsSend> docs)
        {
            // удаление данных с datagv (обратный цикл)
            for (int i = dataGridView1.RowCount - 1; i >= 0; i--)
            {
                dataGridView1.Rows.Remove(dataGridView1.Rows[i]);
            }

            foreach (DocumentsSend d in docs)
            {
                // id нов строки
                int rowNumber = dataGridView1.Rows.Add();
                // запись в столбцы
                dataGridView1.Rows[rowNumber].Cells[0].Value = d.Id;
                dataGridView1.Rows[rowNumber].Cells[1].Value = d.NameDocument;
                dataGridView1.Rows[rowNumber].Cells[2].Value = d.NumberDocument;
                dataGridView1.Rows[rowNumber].Cells[3].Value = d.Description;
            }

            toolStripStatusLabel4.Text = dataGridView1.Rows.Count.ToString();
        }

        // возв выдел док
        private DocumentsSend FlagDoc()
        {
            DocumentsSend doc = null;
            try
            {
                // индекс строки а не id аби
                int index = dataGridView1.CurrentRow.Index;

                foreach (DocumentsSend d in documentsSends)
                {
                    if ((int)dataGridView1[0, index].Value == d.Id)
                    {
                        doc = d;
                    }
                }
            }
            catch (Exception)
            {
                return doc;
            }

            return doc;
        }

        private void ToolStripButton3_Click(object sender, EventArgs e)
        {
            new DeleteDoc().Delete(FlagDoc().Id);
        }

        private void ToolStripButton1_Click(object sender, EventArgs e)
        {
            AddDocForm addDocForm = new AddDocForm(enrollee.Id);
            addDocForm.Show();
        }

        private void ToolStripButton2_Click(object sender, EventArgs e)
        {
            UpdateDocForm updateDocForm = new UpdateDocForm(FlagDoc());
            updateDocForm.Show();
        }


        string[] arraySearch = new string[] { "Поиск", "Уникальному номеру", "Названию", "Номеру", "Описанию" };
        private void ToolStripButton5_Click(object sender, EventArgs e)
        {
            if (toolStripComboBox1.Text != arraySearch[0] || toolStripTextBox1.Text != "")
            {
                if (toolStripComboBox1.Text == arraySearch[1])
                {
                    DrawData(documentsSends.Where(p => p.Id.ToString().ToLower().Contains(toolStripTextBox1.Text.ToLower())).ToList());
                }
                else if (toolStripComboBox1.Text == arraySearch[2])
                {
                    DrawData(documentsSends.Where(p => p.NameDocument.ToString().ToLower().Contains(toolStripTextBox1.Text.ToLower())).ToList());
                }
                if (toolStripComboBox1.Text == arraySearch[3])
                {
                    DrawData(documentsSends.Where(p => p.NumberDocument.ToString().ToLower().Contains(toolStripTextBox1.Text.ToLower())).ToList());
                }
                if (toolStripComboBox1.Text == arraySearch[4])
                {
                    DrawData(documentsSends.Where(p => p.Description.ToString().ToLower().Contains(toolStripTextBox1.Text.ToLower())).ToList());
                }
            }
            else
            {
                MessageBox.Show("Укажите критерии и параметры поиска", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ToolStripButton4_Click(object sender, EventArgs e)
        {
            LoadData();

            if (toolStripComboBox1.Text != arraySearch[0] || toolStripTextBox1.Text != "")
            {
                ToolStripButton5_Click(sender, e);
            }
            else
            {
                DrawData();
            }
        }

        private void ToolStripButton6_Click(object sender, EventArgs e)
        {
            DrawData();
            toolStripComboBox1.Text = arraySearch[0];
            toolStripTextBox1.Text = "";
        }

        private void ЭкспортВExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataExport.ExportToExcel(dataGridView1); 
        }

        private void ФайлToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ПомощьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show(Inquiry.Read(Inquiry.docFormInfo), "Помощь");
            }
            catch (Exception)
            { }
        }
    }
}
