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
    public partial class EnrolleeForm : Form
    {

        List<TrainingDirectionSend> trainingDirectionSends = null;
        List<EnrolleeSend> enrolleeSends = null;
        UserSend userSend = null;
      

        // получить данные с серв
        private void LoadDataServEn()
        {
            enrolleeSends = new GetEnrollee().GetDataEn(userSend.Login);

            if (enrolleeSends == null)
            {
                MessageBox.Show("Ошибка. Связь с сервером отсутствует!");
            }
        }

        public EnrolleeForm(List<TrainingDirectionSend> td, UserSend userSend)
        {
            InitializeComponent();

            this.userSend = userSend;
            try
            {
                trainingDirectionSends = td;
                LoadDataServEn();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка. Связь с сервером отсутствует! ({ex})");
            }
        }



        // заполн таблицы
        private void DrawData()
        {
            // удаление данных с datagv (обратный цикл)
            for (int i = dataGridView1.RowCount - 1; i >= 0; i--)
            {
                dataGridView1.Rows.Remove(dataGridView1.Rows[i]);
            }

            foreach (EnrolleeSend e in enrolleeSends)
            {
                // id нов строки
                int rowNumber = dataGridView1.Rows.Add();
                // запись в столбцы
                dataGridView1.Rows[rowNumber].Cells[0].Value = e.Id;
                dataGridView1.Rows[rowNumber].Cells[1].Value = trainingDirectionSends.Where(p=>p.Id==e.IdDirectionTraining).FirstOrDefault().FullNameSpecialty;
                dataGridView1.Rows[rowNumber].Cells[2].Value = trainingDirectionSends.Where(p => p.Id == e.IdDirectionTraining).FirstOrDefault().NameFormStudy;
                dataGridView1.Rows[rowNumber].Cells[3].Value = trainingDirectionSends.Where(p => p.Id == e.IdDirectionTraining).FirstOrDefault().NameTrainingPeriod;
                dataGridView1.Rows[rowNumber].Cells[4].Value = trainingDirectionSends.Where(p => p.Id == e.IdDirectionTraining).FirstOrDefault().NameBudgetOrCharge;
                dataGridView1.Rows[rowNumber].Cells[5].Value = e.NameLevelEducation;
                dataGridView1.Rows[rowNumber].Cells[6].Value = e.NameConcession;
                dataGridView1.Rows[rowNumber].Cells[7].Value = e.EnrolleeDateOfRegistration;
                dataGridView1.Rows[rowNumber].Cells[8].Value = e.EnrolleeFirstname;
                dataGridView1.Rows[rowNumber].Cells[9].Value = e.EnrolleeLastname;
                dataGridView1.Rows[rowNumber].Cells[10].Value = e.EnrolleePatronymic;
                dataGridView1.Rows[rowNumber].Cells[11].Value = e.EnrolleeSex;
                dataGridView1.Rows[rowNumber].Cells[12].Value = e.EnrolleeDateOfBirth;
                dataGridView1.Rows[rowNumber].Cells[13].Value = e.EnrolleePassportSeries;
                dataGridView1.Rows[rowNumber].Cells[14].Value = e.EnrolleePassportNumber;
                dataGridView1.Rows[rowNumber].Cells[15].Value = e.EnrolleePassportPersonalNumber;
                dataGridView1.Rows[rowNumber].Cells[16].Value = e.EnrolleePassportIssuedBy;
                dataGridView1.Rows[rowNumber].Cells[17].Value = e.EnrolleeDateOfIssue;
                dataGridView1.Rows[rowNumber].Cells[18].Value = e.EnrolleeDateExpiry;
                dataGridView1.Rows[rowNumber].Cells[19].Value = e.EnrolleeAddress;
                dataGridView1.Rows[rowNumber].Cells[20].Value = e.EnrolleePostcode;
                dataGridView1.Rows[rowNumber].Cells[21].Value = e.EnrolleePhoneNumber;
                dataGridView1.Rows[rowNumber].Cells[22].Value = e.EnrolleeEducation;
                dataGridView1.Rows[rowNumber].Cells[23].Value = e.EnrolleeLastPlaceOfStudy;
                dataGridView1.Rows[rowNumber].Cells[24].Value = e.EnrolleeAddressLastPlaceOfStudy;
                dataGridView1.Rows[rowNumber].Cells[25].Value = e.EnrolleeGraduationDate;
                dataGridView1.Rows[rowNumber].Cells[26].Value = e.EnrolleeNumberCertificateOrDiploma;
                dataGridView1.Rows[rowNumber].Cells[27].Value = e.EnrolleeAverageGradeOfCertificateOrDiploma;
                dataGridView1.Rows[rowNumber].Cells[28].Value = e.EnrolleeScoreOfTheFirstEntranceTest;
                dataGridView1.Rows[rowNumber].Cells[29].Value = e.EnrolleeScoreOfTheSecondEntranceTest;
                dataGridView1.Rows[rowNumber].Cells[30].Value = e.EnrolleeScoreOfTheThirdEntranceTest;
                dataGridView1.Rows[rowNumber].Cells[31].Value = e.EnrolleeEmail;
                dataGridView1.Rows[rowNumber].Cells[32].Value = e.EnrolleeAdditionalInformation;
            }

            toolStripStatusLabel4.Text = dataGridView1.Rows.Count.ToString();
        }


        private void DrawData(List<EnrolleeSend> enrs)
        {
            // удаление данных с datagv (обратный цикл)
            for (int i = dataGridView1.RowCount - 1; i >= 0; i--)
            {
                dataGridView1.Rows.Remove(dataGridView1.Rows[i]);
            }

            foreach (EnrolleeSend e in enrs)
            {
                // id нов строки
                int rowNumber = dataGridView1.Rows.Add();
                // запись в столбцы
                dataGridView1.Rows[rowNumber].Cells[0].Value = e.Id;
                dataGridView1.Rows[rowNumber].Cells[1].Value = trainingDirectionSends.Where(p => p.Id == e.IdDirectionTraining).FirstOrDefault().FullNameSpecialty;
                dataGridView1.Rows[rowNumber].Cells[2].Value = trainingDirectionSends.Where(p => p.Id == e.IdDirectionTraining).FirstOrDefault().NameFormStudy;
                dataGridView1.Rows[rowNumber].Cells[3].Value = trainingDirectionSends.Where(p => p.Id == e.IdDirectionTraining).FirstOrDefault().NameTrainingPeriod;
                dataGridView1.Rows[rowNumber].Cells[4].Value = trainingDirectionSends.Where(p => p.Id == e.IdDirectionTraining).FirstOrDefault().NameBudgetOrCharge;
                dataGridView1.Rows[rowNumber].Cells[5].Value = e.NameLevelEducation;
                dataGridView1.Rows[rowNumber].Cells[6].Value = e.NameConcession;
                dataGridView1.Rows[rowNumber].Cells[7].Value = e.EnrolleeDateOfRegistration;
                dataGridView1.Rows[rowNumber].Cells[8].Value = e.EnrolleeFirstname;
                dataGridView1.Rows[rowNumber].Cells[9].Value = e.EnrolleeLastname;
                dataGridView1.Rows[rowNumber].Cells[10].Value = e.EnrolleePatronymic;
                dataGridView1.Rows[rowNumber].Cells[11].Value = e.EnrolleeSex;
                dataGridView1.Rows[rowNumber].Cells[12].Value = e.EnrolleeDateOfBirth;
                dataGridView1.Rows[rowNumber].Cells[13].Value = e.EnrolleePassportSeries;
                dataGridView1.Rows[rowNumber].Cells[14].Value = e.EnrolleePassportNumber;
                dataGridView1.Rows[rowNumber].Cells[15].Value = e.EnrolleePassportPersonalNumber;
                dataGridView1.Rows[rowNumber].Cells[16].Value = e.EnrolleePassportIssuedBy;
                dataGridView1.Rows[rowNumber].Cells[17].Value = e.EnrolleeDateOfIssue;
                dataGridView1.Rows[rowNumber].Cells[18].Value = e.EnrolleeDateExpiry;
                dataGridView1.Rows[rowNumber].Cells[19].Value = e.EnrolleeAddress;
                dataGridView1.Rows[rowNumber].Cells[20].Value = e.EnrolleePostcode;
                dataGridView1.Rows[rowNumber].Cells[21].Value = e.EnrolleePhoneNumber;
                dataGridView1.Rows[rowNumber].Cells[22].Value = e.EnrolleeEducation;
                dataGridView1.Rows[rowNumber].Cells[23].Value = e.EnrolleeLastPlaceOfStudy;
                dataGridView1.Rows[rowNumber].Cells[24].Value = e.EnrolleeAddressLastPlaceOfStudy;
                dataGridView1.Rows[rowNumber].Cells[25].Value = e.EnrolleeGraduationDate;
                dataGridView1.Rows[rowNumber].Cells[26].Value = e.EnrolleeNumberCertificateOrDiploma;
                dataGridView1.Rows[rowNumber].Cells[27].Value = e.EnrolleeAverageGradeOfCertificateOrDiploma;
                dataGridView1.Rows[rowNumber].Cells[28].Value = e.EnrolleeScoreOfTheFirstEntranceTest;
                dataGridView1.Rows[rowNumber].Cells[29].Value = e.EnrolleeScoreOfTheSecondEntranceTest;
                dataGridView1.Rows[rowNumber].Cells[30].Value = e.EnrolleeScoreOfTheThirdEntranceTest;
                dataGridView1.Rows[rowNumber].Cells[31].Value = e.EnrolleeEmail;
                dataGridView1.Rows[rowNumber].Cells[32].Value = e.EnrolleeAdditionalInformation;
            }

            toolStripStatusLabel4.Text = dataGridView1.Rows.Count.ToString();
        }



        private void EnrolleeForm_Load(object sender, EventArgs e)
        {
            DrawData();
            toolStripStatusLabel2.Text = enrolleeSends.Count.ToString();

            toolStripComboBox1.Text = arraySearch[0];
            toolStripComboBox1.Items.AddRange(arraySearch);
        }

        // удаление абитуриента
        private void ToolStripButton2_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Вы действительно хотите удалить эту строку?", "Удаление строки", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);

            switch (res)
            {
                case DialogResult.OK:
                    try
                    {
                        // индекс строки а не id абит
                        int index = dataGridView1.CurrentRow.Index;

                        // id удаляемого абит
                        int idDelEnrollee = (int)dataGridView1[0, index].Value;

                        // удаление абит
                        new DeleteEnrollee().Delte(idDelEnrollee);

                        LoadDataServEn(); 

                        
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
            }
        }

        // возв выдел абит
        private EnrolleeSend FlagEnrollee()
        {
            EnrolleeSend en = null;
            try
            {
                // индекс строки а не id аби
                int index = dataGridView1.CurrentRow.Index;

                foreach (EnrolleeSend e in enrolleeSends)
                {
                    if ((int)dataGridView1[0, index].Value == e.Id)
                    {
                        en = e;
                    }
                }
            }
            catch (Exception)
            {
                return en;
            }

            return en;
        }

        // обновление абитуриента
        private void ToolStripButton1_Click(object sender, EventArgs e)
        {
            UpdateEnrolleeForm updateEnrolleeForm = new UpdateEnrolleeForm(FlagEnrollee(), trainingDirectionSends.Where(p=>p.Id==FlagEnrollee().IdDirectionTraining).First(), trainingDirectionSends, userSend);
            updateEnrolleeForm.Show();
        }

        private void ToolStripButton4_Click(object sender, EventArgs e)
        {
            DocForm docForm = new DocForm(FlagEnrollee());
            docForm.Show();
        }

        private void ToolStripButton3_Click(object sender, EventArgs e)
        {
            LoadDataServEn();
            if (toolStripComboBox1.Text != arraySearch[0] || toolStripTextBox2.Text != "")
            {
                ToolStripButton5_Click(sender, e);
            }
            else
            {
                DrawData();
            }
        }

        string[] arraySearch = new string[] { "Поиск","Личный номер","Имя","Фамилия", "Серия паспорта", "Номер паспорта" };

        private void ToolStripButton5_Click(object sender, EventArgs e)
        {
            if (toolStripComboBox1.Text != arraySearch[0] || toolStripTextBox2.Text != "")
            {
                if (toolStripComboBox1.Text == arraySearch[1])
                {
                    DrawData(enrolleeSends.Where(p=>p.Id.ToString().ToLower().Contains(toolStripTextBox2.Text.ToLower())).ToList());
                }
                else if (toolStripComboBox1.Text == arraySearch[2])
                {
                    DrawData(enrolleeSends.Where(p => p.EnrolleeFirstname.ToString().ToLower().Contains(toolStripTextBox2.Text.ToLower())).ToList());
                }
                else if (toolStripComboBox1.Text == arraySearch[3])
                {
                    DrawData(enrolleeSends.Where(p => p.EnrolleeLastname.ToString().ToLower().Contains(toolStripTextBox2.Text.ToLower())).ToList());
                }
                else if (toolStripComboBox1.Text == arraySearch[4])
                {
                    DrawData(enrolleeSends.Where(p => p.EnrolleePassportSeries.ToString().ToLower().Contains(toolStripTextBox2.Text.ToLower())).ToList());
                }
                else if (toolStripComboBox1.Text == arraySearch[5])
                {
                    DrawData(enrolleeSends.Where(p => p.EnrolleePassportNumber.ToString().ToLower().Contains(toolStripTextBox2.Text.ToLower())).ToList());
                }

            }
            else
            {
                MessageBox.Show("Выберите критерии поиска","Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ToolStripButton6_Click(object sender, EventArgs e)
        {
            DrawData();
            toolStripComboBox1.Text = arraySearch[0];
            toolStripTextBox2.Text = "";
        }

        private void ToolStripButton7_Click(object sender, EventArgs e)
        {
            try
            {
                RefForm refForm = new RefForm(FlagEnrollee());
                refForm.Show();
            }
            catch (Exception)
            {
                MessageBox.Show("Выберите абитуриента","Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            }

        private void ToolStripButton8_Click(object sender, EventArgs e)
        {
            if (FlagEnrollee() != null)
            {
                EmailForm emailForm = new EmailForm(FlagEnrollee());
                emailForm.Show();
            }
            else
            {
                MessageBox.Show("Выберите абитуриента");
            }

        }

        private void ToolStripButton9_Click(object sender, EventArgs e)
        {
            StateForm stateForm = new StateForm(userSend);
            stateForm.Show();
        }

        private void ЭкспортВExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataExport.ExportToExcel(dataGridView1);
        }

        private void ПомощьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show(Inquiry.Read(Inquiry.enrolleeFormInfo), "Помощь");
            }
            catch (Exception)
            { }
        }
    }
}
