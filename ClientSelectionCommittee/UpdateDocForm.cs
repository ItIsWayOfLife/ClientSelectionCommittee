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
    public partial class UpdateDocForm : Form
    {
        DocumentsSend documentsSend = null;

        public UpdateDocForm(DocumentsSend documentsSend)
        {
            InitializeComponent();
            this.documentsSend = documentsSend;
        }

        void ViewData()
        {
            textBox1.Text = documentsSend.NameDocument;            
            textBox2.Text = documentsSend.NumberDocument;
            textBox3.Text = documentsSend.Description;
        }

        private void UpdateDocForm_Load(object sender, EventArgs e)
        {
            ViewData();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                documentsSend.NameDocument = textBox1.Text;
                documentsSend.NumberDocument = textBox2.Text;
                documentsSend.Description = textBox3.Text;

                string info = new UpdateDoc().Update(documentsSend);

                MessageBox.Show(info, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: "+ex.ToString());
            }          
        }
    }
}
