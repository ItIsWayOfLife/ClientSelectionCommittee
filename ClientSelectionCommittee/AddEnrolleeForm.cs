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
        }
    }
}
