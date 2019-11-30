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
    public partial class AddDocForm : Form
    {
        int idEnrollee;
        // new doc
        DocumentsSend documentsSend = null;

        public AddDocForm(int id)
        {
            InitializeComponent();
            idEnrollee = id;
        }

        private void AddDocForm_Load(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                documentsSend = new DocumentsSend();
                documentsSend.IdEnrollee = idEnrollee;
                documentsSend.NameDocument = textBox1.Text;
                documentsSend.NumberDocument = textBox2.Text;
                documentsSend.Description = textBox3.Text;

                new AddDoc().AddDocum(new List<DocumentsSend>() { documentsSend });

                MessageBox.Show("Данные успешно добавленны");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " +ex.ToString());
            }
        }
    }
}
