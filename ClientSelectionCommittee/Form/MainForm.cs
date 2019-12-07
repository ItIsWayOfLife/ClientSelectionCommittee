using System;
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
                MessageBox.Show($"Ошибка. Связь с сервером отсутствует! ({ex})");
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
            trainingDirectionSends = new GetTrainingDirectionSend().GetTD(userSend.Login);

            if (trainingDirectionSends == null)
            {
                MessageBox.Show("Ошибка. Связь с сервером отсутствует!");
            }
        }

        bool secodOpen = false;

        public MainForm(bool bl, UserSend userSend)
        {
            InitializeComponent();

            secodOpen = true;

            this.userSend = userSend;

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
            try
            {

                // удаление данных с datagv (обратный цикл)
                for (int i = dataGridView1.RowCount - 1; i >= 0; i--)
                {
                    dataGridView1.Rows.Remove(dataGridView1.Rows[i]);
                }

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
            } catch (Exception)
            {

            }
        }

        private void DrawData(List<TrainingDirectionSend> tds)
        {
            try
            {

                // удаление данных с datagv (обратный цикл)
                for (int i = dataGridView1.RowCount - 1; i >= 0; i--)
                {
                    dataGridView1.Rows.Remove(dataGridView1.Rows[i]);
                }

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
            catch (Exception)
            {

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


           toolStripLabel1.Text = $"{userSend.Lastname} {userSend.Firstname[0]}. {userSend.Patronymic[0]}.";
        }

        private void ToolStripButton1_Click(object sender, EventArgs e)
        {
            AddEnrolleeForm addEnrolleeForm = new AddEnrolleeForm(FlagTrainingDirection(), userSend);
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
            EnrolleeForm enrolleeForm = new EnrolleeForm(trainingDirectionSends, userSend);
            enrolleeForm.Show();
        }

        // запись выбранного направл
        private void Button1_Click(object sender, EventArgs e)
        {
           
        }

        // событие откт нач форму
        private void MainForm_FormClosed_1(object sender, FormClosedEventArgs e)
        {
            if (secodOpen != true)
            {

                new UserExit().Exit(userSend.Id);
                Form loginForm = Application.OpenForms[0];
                loginForm.Show();
            }
        }

        private void ToolStripComboBox1_Click(object sender, EventArgs e)
        {

        }

        string[] arrayFac = new string[5] {"Все" ,"ФОЗОЖ", "ФБД", "ЭФ", "БФ"};
        string[] arraySearch = new string[8] {"Поиск", "Номеру направления", "Бюджет/Платно", "Форме", "Периоду","Коду специальности","Кафедре", "Специальности"};

        private void ToolStripButton6_Click(object sender, EventArgs e)
        {
            if (toolStripComboBox2.Text == arraySearch[0] || toolStripTextBox1.Text == "")
            {
                MessageBox.Show("Выберите по каким критериям будет вестись поиск и укажите что ищите");
                return;
            }
            else if (toolStripComboBox2.Text == arraySearch[1])
            {
                int id;
                bool bl = Int32.TryParse(toolStripTextBox1.Text, out id);
                if (bl)
                {
                    DrawData(TrainingDirectionSend.SearchById(id, trainingDirectionSends));
                }
                else
                {
                    MessageBox.Show("Укажите число");
                }
            }
            else if (toolStripComboBox2.Text == arraySearch[2])
            {
                DrawData(trainingDirectionSends.Where(p=>p.NameBudgetOrCharge.ToLower().Contains(toolStripTextBox1.Text.ToLower())).ToList());
            }
            else if (toolStripComboBox2.Text == arraySearch[3])
            {
                DrawData(trainingDirectionSends.Where(p => p.NameFormStudy.ToLower().Contains(toolStripTextBox1.Text.ToLower())).ToList());
            }
            else if (toolStripComboBox2.Text == arraySearch[4])
            {
                DrawData(trainingDirectionSends.Where(p => p.NameTrainingPeriod.ToLower().Contains(toolStripTextBox1.Text.ToLower())).ToList());
            }
            else if (toolStripComboBox2.Text == arraySearch[5])
            {
                DrawData(trainingDirectionSends.Where(p => p.CodeSpecialty.ToLower().Contains(toolStripTextBox1.Text.ToLower())).ToList());
            }
            else if (toolStripComboBox2.Text == arraySearch[6])
            {
                DrawData(trainingDirectionSends.Where(p => p.ShortNameDepartment.ToLower().Contains(toolStripTextBox1.Text.ToLower())).ToList());
            }
            else if (toolStripComboBox2.Text == arraySearch[7])
            {
                DrawData(trainingDirectionSends.Where(p => p.FullNameSpecialty.ToLower().Contains(toolStripTextBox1.Text.ToLower())).ToList());
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
            if (toolStripComboBox1.Text.ToLower() != arrayFac[0].ToLower())
            {              
                DrawData(trainingDirectionSends.Where(p => p.ShortNameFaculty == toolStripComboBox1.Text).ToList());
            }
            else
            {
                DrawData();
            }
           
        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            using (StreamWriter writer = new StreamWriter(@"UpdateDTEnrollee.txt"))
            {
                writer.Write(FlagTrainingDirection().Id.ToString());
            }

            this.Close();
        }

        private void ToolStripButton5_Click(object sender, EventArgs e)
        {
            Chat chat = new Chat(userSend);
            chat.Show();
        }

        private void АдминПанельToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (userSend.AdminRights == "+")
            {
                AdminForm adminForm = new AdminForm(userSend);
                adminForm.Show();
            }
            else
            {
                MessageBox.Show("У вас нет прав администратора", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ОПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show(Inquiry.Read(Inquiry.programInfo), "Справка о программе");
            }
            catch (Exception)
            { }
        }

        private void ВыходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataExport.ExportToExcel(dataGridView1);
        }

        private void ОРазработчикеToolStripMenuItem_Click(object sender, EventArgs e)
        {

            try
            {
                MessageBox.Show(Inquiry.Read(Inquiry.aboutTheDeveloperInfo), "Справка о разработчике");
            }
            catch (Exception)
            { }
        }

        private void ПомощьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show(Inquiry.Read(Inquiry.mainFormInfoFile), "Помощь");
            }
            catch (Exception)
            { }
        }
    }
}
