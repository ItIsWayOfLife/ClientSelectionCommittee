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
    public partial class LoginForm : Form
    {
        LogIn login = null;
        UserSend user = null;

        public LoginForm()
        {
            InitializeComponent();
            login = new LogIn();
           
        }

        

        private void LoginForm_Load(object sender, EventArgs e)
        {
            
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                login.Login = textBox1.Text;
                login.Password = textBox2.Text;

                user = login.LoginTo();

                if (user == null)
                {
                    MessageBox.Show("Такого пользоваеля не существует.");
                }
                else
                {
                    MessageBox.Show("Добро пожаловать "+user.Firstname);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: "+ex);
            }
        }
    }
}
