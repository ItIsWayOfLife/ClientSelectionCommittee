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
    public partial class AddEnrolleeForm : Form
    {
        // направление подготовки
        TrainingDirectionSend trainingDirectionSend = null;

        // нов абитуриент
        EnrolleeSend enrolleeSend_ = null;


        // ур образов
       // List<LevelEducation> levelEducations = null;

        // льготы
       // List<Concession> concessions = null;

        public AddEnrolleeForm(TrainingDirectionSend td)
        {
            InitializeComponent();
            trainingDirectionSend = td;
        }

        // инфо о направл подготовки
        private void DrawInfoTranDir()
        {
            label34.Text = trainingDirectionSend.FullNameSpecialty;
            label35.Text = trainingDirectionSend.NameTrainingPeriod;
            label39.Text = trainingDirectionSend.NameFormStudy;
            label40.Text = trainingDirectionSend.NameBudgetOrCharge;
        }


        private void AddEnrolleeForm_Load(object sender, EventArgs e)
        {
            DrawInfoTranDir();
            ComboBoxData();
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
                comboBox1.Items.AddRange(st);
                comboBox1.Text = st[0];
                // счит данных из табл уров льготы
                List<ConcessionSend> concessions = new GetConcessionSend().GetData();

                string[] st1 = new string[concessions.Count+1];
                st1[0] = "без льготы";
                for (int i = 1; i < st1.Length; i++)
                {
                    st1[i] = concessions[i-1].NameConcession;

                }
                comboBox2.Items.AddRange(st1);
                comboBox2.Text = st1[0];
                // выбор пола
                string[] st2 = new string[] { "мужской", "женский" };

                comboBox3.Items.AddRange(st2);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {

            try
            {
                enrolleeSend_ = new EnrolleeSend();
                enrolleeSend_.EnrolleePassportSeries = textBox1.Text;
                enrolleeSend_.EnrolleePassportNumber = textBox2.Text;
                enrolleeSend_.EnrolleePassportPersonalNumber = textBox3.Text;
                enrolleeSend_.EnrolleePassportIssuedBy = textBox4.Text;
                enrolleeSend_.EnrolleeDateOfIssue = Convert.ToDateTime(textBox5.Text);
                enrolleeSend_.EnrolleeDateExpiry = Convert.ToDateTime(textBox6.Text);

                enrolleeSend_.NameLevelEducation = comboBox1.Text;
                enrolleeSend_.EnrolleeLastname = textBox9.Text;
                enrolleeSend_.EnrolleeFirstname = textBox8.Text;
                enrolleeSend_.EnrolleePatronymic = textBox7.Text;
                enrolleeSend_.EnrolleeSex = comboBox3.Text;
                enrolleeSend_.EnrolleeDateOfBirth = Convert.ToDateTime(textBox14.Text);
                enrolleeSend_.EnrolleeAddress = textBox18.Text;

                enrolleeSend_.NameConcession = comboBox2.Text;
                enrolleeSend_.EnrolleePhoneNumber = textBox17.Text;
                enrolleeSend_.EnrolleePostcode = textBox16.Text;
                enrolleeSend_.EnrolleeEmail = textBox20.Text;
                enrolleeSend_.EnrolleeAdditionalInformation = textBox19.Text;

                enrolleeSend_.EnrolleeEducation = textBox22.Text;
                enrolleeSend_.EnrolleeNumberCertificateOrDiploma = textBox21.Text;
                enrolleeSend_.EnrolleeAverageGradeOfCertificateOrDiploma = Convert.ToInt32(textBox13.Text);

                enrolleeSend_.EnrolleeScoreOfTheFirstEntranceTest = Convert.ToInt32(textBox25.Text);
                enrolleeSend_.EnrolleeScoreOfTheSecondEntranceTest = Convert.ToInt32(textBox24.Text);
                enrolleeSend_.EnrolleeScoreOfTheThirdEntranceTest = Convert.ToInt32(textBox23.Text);

                enrolleeSend_.EnrolleeLastPlaceOfStudy = textBox12.Text;
                enrolleeSend_.EnrolleeAddressLastPlaceOfStudy = textBox11.Text;
                enrolleeSend_.EnrolleeGraduationDate = Convert.ToDateTime(textBox10.Text);

                enrolleeSend_.IdDirectionTraining = trainingDirectionSend.Id;


                // добавляем в бд
                new AddEnrollee().AddEnrolleeTo(enrolleeSend_);

                // отображение данных
                SearchNewEnrolle();

            }
            catch (Exception)
            {
                MessageBox.Show("Необходимо заполнить все поля правильно", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SearchNewEnrolle()
        {
            label45.Text = new ReturnLastIdEn().ReturnLastId();
            label46.Text = enrolleeSend_.EnrolleeLastname;
            label47.Text =  enrolleeSend_.EnrolleeFirstname;
        }

       // добавление документации
        private void AddDocument()
        {
            if (enrolleeSend_ != null)
            {

                // нов список документов
                List<DocumentsSend> documents_ = new List<DocumentsSend>();

                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    documents_.Add(new DocumentsSend(
                        Convert.ToInt32(label45.Text),
                         dataGridView1[0, i].Value.ToString(),
                          dataGridView1[1, i].Value.ToString(),
                           dataGridView1[2, i].Value.ToString()
                        ));
                }

                new AddDoc().AddDocum(documents_);                
            }
            else
                MessageBox.Show("добавьте абитуриента");

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                AddDocument();
                MessageBox.Show("Успешно добавлено");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: "+ex);
            }
        }
    }
}
