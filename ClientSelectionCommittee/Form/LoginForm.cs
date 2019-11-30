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
            textBox2.UseSystemPasswordChar = true;
        }

        private void Button1_Click(object sender, EventArgs e)
        {

            // если тексбоксы непустые
            if (textBox1.Text != String.Empty && textBox2.Text != String.Empty)
            {

                try
                {
                    login.Login = textBox1.Text;
                    login.Password = textBox2.Text;

                    user = login.LoginTo();

                    if (user != null)
                    {
                        MessageBox.Show("Добро пожаловать " + user.Firstname);

                        MainForm mainForm = new MainForm(user);
                        mainForm.Show();

                        textBox1.Text = "";
                        textBox2.Text = "";

                        // свор форму
                        this.Hide();
                        user = null;
                    }
                    else
                    {          
                        label3.Text = "Неверный логин или пароль";
                    }

                }
                catch (Exception ex)
                {
                }
            }
            
             

            label4.Text = ("Укажите логин и пароль");

        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            // ссылка на сайт ПолесГУ
            System.Diagnostics.Process.Start("http://www.polessu.by");

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
