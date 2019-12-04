using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ClientSelectionCommittee
{
    public partial class StateForm : Form
    {
        UserSend userSend;

        public StateForm(UserSend userSend)
        {
            InitializeComponent();
            this.userSend = userSend;
        }

        private void StateForm_Load(object sender, EventArgs e)
        {
            Print();
            
        }

        string[] nameGroup;
        double[] valueGroup;

        void Print()
        {
            int count = 7;
            nameGroup = new string[count];
            valueGroup = new double[count];

            StateData stateDatas = StateData.GetData(userSend.Login);


            label2.Text = stateDatas.CountEnrolee.ToString();
            label4.Text = stateDatas.EmrolleeThisPoint.ToString();

            valueGroup[0] = stateDatas.PercentMinsk;
            valueGroup[1] = stateDatas.PercentMinskregion;
            valueGroup[2] = stateDatas.PercentBrest;
            valueGroup[3] = stateDatas.PercentGomel;
            valueGroup[4] = stateDatas.PercentGrodno;
            valueGroup[5] = stateDatas.PercentVitebsk;
            valueGroup[6] = stateDatas.PercentMogilevsk;

            if (stateDatas.PercentMinsk!=0)
            nameGroup[0] = $"Минск ({stateDatas.PercentMinsk})";

            if (stateDatas.PercentMinskregion != 0)
                nameGroup[1] = $"Минская об ({stateDatas.PercentMinskregion})";

            if (stateDatas.PercentBrest != 0)
                nameGroup[2] = $"Брестская об ({stateDatas.PercentBrest})";

            if (stateDatas.PercentGomel != 0)
                nameGroup[3] = $"Гомельская об ({stateDatas.PercentGomel})";

            if (stateDatas.PercentGrodno != 0)
                nameGroup[4] = $"Гродненская об ({stateDatas.PercentGrodno})";

            if (stateDatas.PercentVitebsk != 0)
                nameGroup[5] = $"Витебская об ({stateDatas.PercentVitebsk})";

            if (stateDatas.PercentMogilevsk != 0)
                nameGroup[6] = $"Могилевская об ({stateDatas.PercentMogilevsk})";

            Сhart1.Series.Clear();
            // Форматировать диаграмму
            Сhart1.BackColor = Color.Gray;
            Сhart1.BackSecondaryColor = Color.WhiteSmoke;
            Сhart1.BackGradientStyle = GradientStyle.DiagonalRight;

            Сhart1.BorderlineDashStyle = ChartDashStyle.Solid;
            Сhart1.BorderlineColor = Color.Gray;
            Сhart1.BorderSkin.SkinStyle = BorderSkinStyle.Emboss;

            // Форматировать область диаграммы
            Сhart1.ChartAreas[0].BackColor = Color.Wheat;

            // Добавить и форматировать заголовок
            Сhart1.Titles.Add("Диаграммы");
            Сhart1.Titles[0].Font = new Font("Utopia", 16);

            Сhart1.Series.Add(new Series("ColumnSeries")
            {
                ChartType = SeriesChartType.Pie
            });

            Сhart1.Series["ColumnSeries"].Points.DataBindXY(nameGroup, valueGroup);

            Сhart1.ChartAreas[0].Area3DStyle.Enable3D = true;
        }
    }
}
