﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientSelectionCommittee
{
    public partial class MainForm : Form
    {
        List<TrainingDirectionSend> trainingDirectionSends = null;

        UserSend userSend = null;
      
        public MainForm(UserSend userSend)
        {
            InitializeComponent();

            button1.Visible = false;

            this.userSend = userSend;

            try
            {
                LoadDataServ();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка. Связь с сервером отсутствует! ({ex})") ;
            }         
        }

        public MainForm()
        {
            InitializeComponent();

            button1.Visible = false;

            try
            {
                LoadDataServ();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка. Связь с сервером отсутствует! ({ex})");
            }
        }

        private void LoadDataServ()
        {
            trainingDirectionSends = new GetTrainingDirectionSend().GetTD();

            if (trainingDirectionSends == null)
            {
                MessageBox.Show("Ошибка. Связь с сервером отсутствует!");
            }
        }

        public MainForm(bool bl)
        {
            InitializeComponent();

            toolStrip.Visible = false;

            try
            {
                LoadDataServ();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка. Связь с сервером отсутствует! ({ex})");
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

        private void DrawData(List<TrainingDirectionSend> tds)
        {
            foreach (TrainingDirectionSend t in tds)
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
            toolStripComboBox1.Text = arrayFac[0];
            toolStripComboBox1.Items.AddRange(arrayFac);
            toolStripComboBox2.Text = arraySearch[0];
            toolStripComboBox2.Items.AddRange(arraySearch);


         //   toolStripLabel1.Text = $"{userSend.Lastname} {userSend.Firstname[0]}. {userSend.Patronymic[0]}.";
        }

        private void ToolStripButton1_Click(object sender, EventArgs e)
        {
            AddEnrolleeForm addEnrolleeForm = new AddEnrolleeForm(FlagTrainingDirection());
            addEnrolleeForm.Show();
        }

        private void ToolStripButton2_Click(object sender, EventArgs e)
        {
            MinutelyTrainingDirection minutelyTrainingDirection = new MinutelyTrainingDirection(FlagTrainingDirection());
            minutelyTrainingDirection.Show();               
        }

        // отк окно абит
        private void ToolStripButton3_Click(object sender, EventArgs e)
        {
            EnrolleeForm enrolleeForm = new EnrolleeForm(trainingDirectionSends);
            enrolleeForm.Show();
        }

        // запись выбранного направл
        private void Button1_Click(object sender, EventArgs e)
        {
            using (StreamWriter writer = new StreamWriter(@"UpdateDTEnrollee.txt"))
            {
                writer.Write(FlagTrainingDirection().Id.ToString());
            }

            this.Close();
        }

        // событие откт нач форму
        private void MainForm_FormClosed_1(object sender, FormClosedEventArgs e)
        {
            // UserSend.Exit();
          //  Form loginForm = Application.OpenForms[0];
          //  loginForm.Show();
        }

        private void ToolStripComboBox1_Click(object sender, EventArgs e)
        {

        }

        string[] arrayFac = new string[5] {"Все" ,"ФОЗОЖ", "ФБД", "ЭФ", "БФ"};
        string[] arraySearch = new string[8] {"Поиск", "Номеру направления", "Бюджет/Платно", "Форме", "Периоду","Коду специальности","Кафедре", "Специальности"};

        private void ToolStripButton6_Click(object sender, EventArgs e)
        {
            if (toolStripComboBox2.Text == arraySearch[0] && toolStripComboBox1.Text == arrayFac[0])
            {
                MessageBox.Show("Выберите по каким критериям будет вестись поиск");
            }
        }

        private void ToolStripButton7_Click(object sender, EventArgs e)
        {
            DrawData();
            toolStripComboBox1.Text = arrayFac[0];
            toolStripComboBox2.Text = arraySearch[0];
            toolStripTextBox1.Text = "";
        }

        private void ToolStripButton8_Click(object sender, EventArgs e)
        {
            if (toolStrip1.Text != arrayFac[0])
            {
                // удаление данных с datagv (обратный цикл)
                for (int i = dataGridView1.RowCount - 1; i >= 0; i--)
                {
                    dataGridView1.Rows.Remove(dataGridView1.Rows[i]);
                }

                DrawData(trainingDirectionSends.Where(p => p.ShortNameFaculty== toolStripComboBox1.Text).ToList());
            }
        }
    }
}
