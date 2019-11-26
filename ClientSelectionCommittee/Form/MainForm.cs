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
    public partial class MainForm : Form
    {
        List<TrainingDirectionSend> trainingDirectionSends = null;
     
        private void LoadDataServ()
        {
            trainingDirectionSends = new GetTrainingDirectionSend().GetTD();

            if (trainingDirectionSends == null)
            {
                MessageBox.Show("Ошибка. Связь с сервером отсутствует!");
            }
        }

        public MainForm()
        {
            InitializeComponent();

            try
            {
                LoadDataServ();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка. Связь с сервером отсутствует! ({ex})") ;
            }         
        }

        // заполн таблицы
        private void DrawData()
        {
            foreach (TrainingDirectionSend t in trainingDirectionSends)
            {
                // id нов строки
                int rowNumber = dataGridView1.Rows.Add();
                // запись в столбцы
                dataGridView1.Rows[rowNumber].Cells[0].Value = t.Id;
                dataGridView1.Rows[rowNumber].Cells[1].Value = t.NameBudgetOrCharge;
                dataGridView1.Rows[rowNumber].Cells[2].Value = t.NameFormStudy;
                dataGridView1.Rows[rowNumber].Cells[3].Value = t.NameTrainingPeriod;
                dataGridView1.Rows[rowNumber].Cells[4].Value = t.CodeSpecialty;
                dataGridView1.Rows[rowNumber].Cells[5].Value = t.ShortNameFaculty;
                dataGridView1.Rows[rowNumber].Cells[6].Value = t.ShortNameDepartment;
                dataGridView1.Rows[rowNumber].Cells[7].Value = t.FullNameSpecialty;
            }
        }


        // возв выдел направл
        private TrainingDirectionSend FlagTrainingDirection()
        {
            TrainingDirectionSend td = null;
            try
            {
                // индекс строки а не id направл
                int index = dataGridView1.CurrentRow.Index;

                foreach (TrainingDirectionSend t in trainingDirectionSends)
                {
                    if ((int)dataGridView1[0, index].Value == t.Id)
                    {
                        td = t;
                    }
                }
            }
            catch (Exception)
            {
                return td;
            }

            return td;
        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            DrawData();
        }

        

        private void ToolStripButton1_Click(object sender, EventArgs e)
        {

        }

        private void ToolStripButton2_Click(object sender, EventArgs e)
        {
            TrainingDirectionSend tdForMess =  FlagTrainingDirection();

            MinutelyTrainingDirection minutelyTrainingDirection = new MinutelyTrainingDirection(tdForMess);
            minutelyTrainingDirection.Show();
                
        }
    }
}
