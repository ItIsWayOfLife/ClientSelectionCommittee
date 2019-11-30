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
            documentsSends = new GetDoc().GetData(enrollee.Id);

        }

        private void DocForm_Load(object sender, EventArgs e)
        {
            DrawData();
        }

        private void DrawData()
        {
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
            AddDocForm addDocForm = new AddDocForm(FlagDoc().Id);
            addDocForm.Show();
        }

        private void ToolStripButton2_Click(object sender, EventArgs e)
        {
            UpdateDocForm updateDocForm = new UpdateDocForm(FlagDoc());
            updateDocForm.Show();
        }
    }
}
