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
    public partial class UpdateEnrolleeForm : Form
    {
        List<TrainingDirectionSend> trainingDirectionSends = null;

        EnrolleeSend enrolleeSend = null;

        TrainingDirectionSend trainingDirectionSend = null;

        public UpdateEnrolleeForm(EnrolleeSend enrollee, TrainingDirectionSend trainingDS, List<TrainingDirectionSend> trainingDs)
        {
            InitializeComponent();
            enrolleeSend = enrollee;
            trainingDirectionSend = trainingDS;
            trainingDirectionSends = trainingDs;
        }

        // заполнение данных абит
        private void DataFilling()
        {
            textBoxPassSer.Text = enrolleeSend.EnrolleePassportSeries;
            textBoxPassPersNumb.Text = enrolleeSend.EnrolleePassportPersonalNumber;
            textBoxPassNumber.Text = enrolleeSend.EnrolleePassportNumber;
            textBoxIssueBy.Text = enrolleeSend.EnrolleePassportIssuedBy;
            textBoxDataIss.Text = enrolleeSend.EnrolleeDateOfIssue.ToShortDateString();
            textBoxDateFit.Text = enrolleeSend.EnrolleeDateExpiry.ToShortDateString();

            textBoxEduc.Text = enrolleeSend.EnrolleeEducation;
            textBoxNumEduc.Text = enrolleeSend.EnrolleeNumberCertificateOrDiploma;
            textBoxScore.Text = enrolleeSend.EnrolleeAverageGradeOfCertificateOrDiploma.ToString();

            comboBoxLevEd.Text = enrolleeSend.NameLevelEducation;
            textBoxLastName.Text = enrolleeSend.EnrolleeLastname;
            textBoxFirstName.Text = enrolleeSend.EnrolleeFirstname;
            textBoxPatronomic.Text = enrolleeSend.EnrolleePatronymic;
            comboBoxSex.Text = enrolleeSend.EnrolleeSex;
            textBoxDataOfBirth.Text = enrolleeSend.EnrolleeDateOfBirth.ToShortDateString();
            textBoxAddress.Text = enrolleeSend.EnrolleeAddress;

            textBoxNameLastPlaceEd.Text = enrolleeSend.EnrolleeLastPlaceOfStudy;
            textBoxAddressLastPl.Text = enrolleeSend.EnrolleeAddressLastPlaceOfStudy;
            textBoxDataGrad.Text = enrolleeSend.EnrolleeGraduationDate.ToShortDateString();

            comboBoxCons.Text = enrolleeSend.NameConcession;
            textBoxPhoneNum.Text = enrolleeSend.EnrolleePhoneNumber;
            textBoxPostCode.Text = enrolleeSend.EnrolleePostcode;
            textBoxEmail.Text = enrolleeSend.EnrolleeEmail;
            textBoxDescr.Text = enrolleeSend.EnrolleeAdditionalInformation;

            textBoxFirstScore.Text = enrolleeSend.EnrolleeScoreOfTheFirstEntranceTest.ToString();
            textBoxSecondScore.Text = enrolleeSend.EnrolleeScoreOfTheSecondEntranceTest.ToString();
            textBoxThirdScore.Text = enrolleeSend.EnrolleeScoreOfTheThirdEntranceTest.ToString();
        }

        // заполн данных напр подг
        private void DrawInfoTranDir()
        {
            label34.Text = trainingDirectionSend.FullNameSpecialty;
            label35.Text = trainingDirectionSend.NameTrainingPeriod;
            label39.Text = trainingDirectionSend.NameFormStudy;
            label40.Text = trainingDirectionSend.NameBudgetOrCharge;
        }

        // метод выпадания блоков
        private void ComboBoxData()
        {
            try
            {
                // выпадающ блоки
                // счит данных из табл уров образования
                List<LevelEducationSend> levels = new GetLevelEducationSend().GetData();

                string[] st = new string[levels.Count];
                for (int i = 0; i < st.Length; i++)
                {
                    st[i] = levels[i].NameLevelEducation;
                }
                comboBoxLevEd.Items.AddRange(st);

                // счит данных из табл уров льготы
                List<ConcessionSend> concessions = new GetConcessionSend().GetData();

                string[] st1 = new string[concessions.Count + 1];
                st1[0] = "без льготы";

              
                for (int i = 1; i < st1.Length; i++)
                {
                    st1[i] = concessions[i - 1].NameConcession;

                }
                comboBoxCons.Items.AddRange(st1);

                if (enrolleeSend.NameConcession == null || enrolleeSend.NameConcession == "")
                    comboBoxCons.Text = st1[0];


                    // выбор пола
                    string[] st2 = new string[] { "мужской", "женский" };

                comboBoxSex.Items.AddRange(st2);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateEnrollee_Load(object sender, EventArgs e)
        {
            DataFilling();
            DrawInfoTranDir();
            ComboBoxData();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            MainForm mainForm = new MainForm(true);
            mainForm.Show();
        }

        // счит нов напр
        private void Button4_Click(object sender, EventArgs e)
        {
            string idTD = null;

            using (StreamReader reader = new StreamReader(@"UpdateDTEnrollee.txt"))
            {
                idTD = reader.ReadLine();
            }
            if (idTD != null)
                trainingDirectionSend = trainingDirectionSends.Where(p => p.Id.ToString() == idTD).First();

            DataFilling();
            DrawInfoTranDir();
            if (enrolleeSend.NameConcession == null || enrolleeSend.NameConcession == "")
                comboBoxCons.Text = "без льготы";
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                enrolleeSend.EnrolleePassportSeries = textBoxPassSer.Text;
                enrolleeSend.EnrolleePassportNumber = textBoxPassNumber.Text;
                enrolleeSend.EnrolleePassportPersonalNumber = textBoxPassPersNumb.Text;
                enrolleeSend.EnrolleePassportIssuedBy = textBoxIssueBy.Text;
                enrolleeSend.EnrolleeDateOfIssue = Convert.ToDateTime(textBoxDataIss.Text);
                enrolleeSend.EnrolleeDateExpiry = Convert.ToDateTime(textBoxDateFit.Text);

                enrolleeSend.NameLevelEducation = comboBoxLevEd.Text;
                enrolleeSend.EnrolleeLastname = textBoxLastName.Text;
                enrolleeSend.EnrolleeFirstname = textBoxFirstName.Text;
                enrolleeSend.EnrolleePatronymic = textBoxPatronomic.Text;
                enrolleeSend.EnrolleeSex = comboBoxSex.Text;
                enrolleeSend.EnrolleeDateOfBirth = Convert.ToDateTime(textBoxDataOfBirth.Text);
                enrolleeSend.EnrolleeAddress = textBoxAddress.Text;

                enrolleeSend.NameConcession = comboBoxCons.Text;
                enrolleeSend.EnrolleePhoneNumber = textBoxPhoneNum.Text;
                enrolleeSend.EnrolleePostcode = textBoxPostCode.Text;
                enrolleeSend.EnrolleeEmail = textBoxEmail.Text;
                enrolleeSend.EnrolleeAdditionalInformation = textBoxDescr.Text;

                enrolleeSend.EnrolleeEducation = textBoxEduc.Text;
                enrolleeSend.EnrolleeNumberCertificateOrDiploma = textBoxNumEduc.Text;
                enrolleeSend.EnrolleeAverageGradeOfCertificateOrDiploma = Convert.ToInt32(textBoxScore.Text);

                enrolleeSend.EnrolleeScoreOfTheFirstEntranceTest = Convert.ToInt32(textBoxFirstScore.Text);
                enrolleeSend.EnrolleeScoreOfTheSecondEntranceTest = Convert.ToInt32(textBoxSecondScore.Text);
                enrolleeSend.EnrolleeScoreOfTheThirdEntranceTest = Convert.ToInt32(textBoxThirdScore.Text);

                enrolleeSend.EnrolleeLastPlaceOfStudy = textBoxNameLastPlaceEd.Text;
                enrolleeSend.EnrolleeAddressLastPlaceOfStudy = textBoxAddressLastPl.Text;
                enrolleeSend.EnrolleeGraduationDate = Convert.ToDateTime(textBoxDataGrad.Text);

                enrolleeSend.IdDirectionTraining = trainingDirectionSend.Id;

                string st = new UpdateEnrollee().UpdateEnrolleeTo(enrolleeSend);

                if (st.Contains("Ошибка"))
                {
                    MessageBox.Show(st, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Данные успешно изменены", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Необходимо заполнить все поля правильно", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    
    }
}
