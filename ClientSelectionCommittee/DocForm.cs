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

        public DocForm(EnrolleeSend enrollee)
        {
            InitializeComponent();
            this.enrollee = enrollee;
            toolStripLabel2.Text = enrollee.Id.ToString();
        }

        private void DocForm_Load(object sender, EventArgs e)
        {
            DrawData();
        }

        private void DrawData()
        {
            foreach (DocumentsSend d in new GetDoc().GetData(enrollee.Id))
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
    }
}
