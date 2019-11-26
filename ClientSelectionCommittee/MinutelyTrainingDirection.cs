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
    public partial class MinutelyTrainingDirection : Form
    {
        TrainingDirectionSend trainingDirectionSend_ = null;
        public MinutelyTrainingDirection(TrainingDirectionSend td)
        {
            InitializeComponent();
            trainingDirectionSend_ = td;
        }

        private void MinutelyTrainingDirection_Load(object sender, EventArgs e)
        {
            label2.Text = trainingDirectionSend_.FullNameSpecialty;
            label5.Text = trainingDirectionSend_.ShortNameSpecialty;
            label6.Text = trainingDirectionSend_.CodeSpecialty;
            label9.Text = trainingDirectionSend_.CodeSpecialization;
            label10.Text = trainingDirectionSend_.NameFormStudy;
            label13.Text = trainingDirectionSend_.NameTrainingPeriod;
            label14.Text = trainingDirectionSend_.FullNameFaculty;
            label21.Text = trainingDirectionSend_.FullNameDepartment;
            label22.Text = trainingDirectionSend_.NameBudgetOrCharge;
            label23.Text = trainingDirectionSend_.FirstIdEntranceTests;
            label24.Text = trainingDirectionSend_.SecondEntranceTests;
            label26.Text = trainingDirectionSend_.ThirdEntranceTests;
            label25.Text = trainingDirectionSend_.AdmissionPlan.ToString();
        }
    }
}
