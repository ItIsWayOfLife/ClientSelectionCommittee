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

        // получить данные с серв
         private void LoadDataServTD()
        {
            trainingDirectionSends = new GetTrainingDirectionSend().GetTD();

            if (trainingDirectionSends == null)
            {
                MessageBox.Show("Ошибка. Связь с сервером отсутствует!");
            }
        }

        // получить данные с серв
        private void LoadDataServEn()
        {
            enrolleeSends = new GetEnrollee().GetDataEn();

            if (enrolleeSends == null)
            {
                MessageBox.Show("Ошибка. Связь с сервером отсутствует!");
            }
        }

        public EnrolleeForm()
        {
            InitializeComponent();

            try
            {
                LoadDataServTD();
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
        }


        private void toolStripButton7_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {

        }

        private void EnrolleeForm_Load(object sender, EventArgs e)
        {
            DrawData();
        }

        private void ToolStripButton2_Click(object sender, EventArgs e)
        {
            // индекс строки а не id абит
            int index = dataGridView1.CurrentRow.Index;

            // id удаляемого абит
            int idDelEnrollee = (int)dataGridView1[0, index].Value;

            // удаление абит
            new DeleteEnrollee().Delte(idDelEnrollee);
        }
    }
}
