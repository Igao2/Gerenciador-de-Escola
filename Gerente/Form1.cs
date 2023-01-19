using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gerente
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string arroba = "@";
            string com = ".com";
           
                Connection con = new Connection();
                con.conectar();
                string a = email.Text;
                string b = senha.Text;

            if (email.Text.Contains(arroba) && email.Text.Contains(com))
            {
                try
                {
                    string sqlsearch = "SELECT * FROM Login WHERE Email = " + email.Text + " AND Senha =" + senha.Text + "";
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlsearch, con.sq);
                    this.Hide();
                    Home h = new Home();
                    h.Show();

                }
                catch 
                {
                    MessageBox.Show("Email não encontrado!", "Alerta do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show("Digite um email válido!", "Alerta do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        
    }

        private void button2_Click(object sender, EventArgs e)
        {
                
                Connection con = new Connection();
                con.conectar();
            string arroba = "@";
            string com = ".com";
            if (email.Text.Contains(arroba) && email.Text.Contains(com))
            {

                try
                {
                    string sqlinsert = "INSERT INTO Login(Email,Senha) VALUES('"+email.Text+"','" +senha.Text+"')";
                    SQLiteCommand command = new SQLiteCommand(sqlinsert, con.sq);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Cadastro realizado, por favor faça o Login", "Mensagem do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch(Exception E)
                {
                    MessageBox.Show(E.Message.ToString());
                }

            }
            else
            {
                MessageBox.Show("Digite um email válido!", "Alerta do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
          
        }
    }
}
