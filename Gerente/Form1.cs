using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Migrations.Model;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.Devices;
using System.Diagnostics.Eventing.Reader;

namespace Gerente
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

      
        private DataSet dtSet = new DataSet();
        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reset();
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
                    Criptografia criptografia = new Criptografia();
                    string[] Login = criptografia.Criptografar(a,b);
                    
                    int z = 0;

                    string sql = "SELECT Email,Senha FROM Login WHERE Email = '" + Login[0] + "' AND Senha = '" + Login[1] + "' ";
                    SQLiteDataReader dr;
                    SQLiteCommand bou = new SQLiteCommand(sql,con.sq);
                    dr = bou.ExecuteReader();
                   
                  while(dr.Read())

                    {
                        z++;
                    }
                  if(z==1)
                    {
                        Home h = new Home();
                        this.Hide();
                        h.Show();
                    }
                  if(z==0)
                    {
                        MessageBox.Show("Dados Incorretos!", "Alerta do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);                    }

                    }
                catch (Exception E)
                {
                    MessageBox.Show(E.Message.ToString());
                    
                }
                
            }
           else
            {
                MessageBox.Show("Digite um email válido!", "Alerta do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        
    }

        private void button2_Click(object sender, EventArgs e)
        {

            Cadastro cad = new Cadastro();
            cad.Show();
          
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Esqueci esqueci = new Esqueci();
            esqueci.Show();
        }
    }
}
