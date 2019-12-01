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
    public partial class AdminForm : Form
    {
        private UserSend userSend;

        List<HistorySend> historySends = null;

        public AdminForm(UserSend userSend)
        {
            InitializeComponent();

            this.userSend = userSend;
            LoadData();
        }

        // загрузка данных с серв
        private void LoadData()
        {
            try
            {
                historySends = new GetHistorySend().GetData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            DrawData();
        }

        // заполн таблицы
        private void DrawData()
        {
            // удаление данных с datagv (обратный цикл)
            for (int i = dataGridView1.RowCount - 1; i >= 0; i--)
            {
                dataGridView1.Rows.Remove(dataGridView1.Rows[i]);
            }

            foreach (HistorySend t in historySends)
            {
                // id нов строки
                int rowNumber = dataGridView1.Rows.Add();
                // запись в столбцы
                dataGridView1.Rows[rowNumber].Cells[0].Value = t.Id;
                dataGridView1.Rows[rowNumber].Cells[1].Value = t.IdEnrollee;
                dataGridView1.Rows[rowNumber].Cells[2].Value = t.Operation;
                dataGridView1.Rows[rowNumber].Cells[3].Value = t.CreateAt;
            }
        }

        // заполн таблицы
        private void DrawData(List<HistorySend> his)
        {
            // удаление данных с datagv (обратный цикл)
            for (int i = dataGridView1.RowCount - 1; i >= 0; i--)
            {
                dataGridView1.Rows.Remove(dataGridView1.Rows[i]);
            }

            foreach (HistorySend t in his)
            {
                // id нов строки
                int rowNumber = dataGridView1.Rows.Add();
                // запись в столбцы
                dataGridView1.Rows[rowNumber].Cells[0].Value = t.Id;
                dataGridView1.Rows[rowNumber].Cells[1].Value = t.IdEnrollee;
                dataGridView1.Rows[rowNumber].Cells[2].Value = t.Operation;
                dataGridView1.Rows[rowNumber].Cells[3].Value = t.CreateAt;
            }
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {
            toolStripLabel4.Text = $"{userSend.Lastname} {userSend.Firstname[0]}. {userSend.Patronymic[0]}.";
        }

        private void ToolStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
