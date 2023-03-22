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

namespace Gerente
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        public string b;
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
            bool certo = false;

            if (email.Text.Contains(arroba) && email.Text.Contains(com))
            {
                try
                {
                    int z = 0;

                    string sql = "SELECT Email,Senha FROM Login WHERE Email = '" + email.Text + "' AND Senha = '" + senha.Text + "' ";
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
                    certo = false;
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

        private void button3_Click(object sender, EventArgs e)
        {
            //this.Visible = false;
             b = Interaction.InputBox("Digite seu e-mail cadastrado");
            Connection con = new Connection();
            try
            {
                int z = 0;
                con.conectar();
                string sql = "SELECT Email FROM Login WHERE Email = '" + b + "'";
                SQLiteCommand command = new SQLiteCommand(sql,con.sq);
                SQLiteDataReader dataReader;
                dataReader = command.ExecuteReader();

                while(dataReader.Read())
                {
                    z++;
                }
                if(z==1)
                {
                    Network net = new Network();
                    if (net.IsAvailable==true)
                    {
                        Random rand = new Random();
                        string s = rand.Next(100, 1000).ToString();
                        MailMessage mail = new MailMessage("projetohelpy1@outlook.com", b);
                        mail.Subject = "Recuperar senha";
                        SmtpClient smtp = new SmtpClient("smtp.office365.com", 587);
                        smtp.UseDefaultCredentials = false;
                        mail.Body = "Digite esse código para recuperar sua senha " + s;
                        smtp.Credentials = new NetworkCredential("projetohelpy1@outlook.com", "1234@.com");
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                        string ez = Interaction.InputBox("Digite o código que foi enviado por notificação", "Mensagem do sistema");
                        if (ez == s)
                        {
                            string a = Interaction.InputBox("Digite sua nova senha");
                            try
                            {
                                string sqlupdate = "UPDATE Login SET Senha = '" + a + "'WHERE Email = '" + b + "'";
                                
                                SQLiteCommand com = new SQLiteCommand(sqlupdate, con.sq);
                                com.ExecuteNonQuery();
                            }
                            catch (Exception E)
                            {
                                MessageBox.Show(E.Message);
                            }

                            MessageBox.Show("Senha Atualizada com sucesso, por favor faça o login", "Mensagem do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Visible = true;
                        }
                        else
                        {
                            MessageBox.Show("Código de verificação incorreto,por favor refaça o processo", "Alerta do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.Visible = true;
                        }
                    }
                    else
                    {
                       QRcode qr = new QRcode();
                        qr.Show();
                        
                    }
                   
                }
                if(z==0)
                {
                    MessageBox.Show("Email não cadastrado!", "Alerta do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.Visible = true;
                }
            }
            catch(Exception E)
            {
                MessageBox.Show(E.Message);
                this.Visible = true;
            }
        }
    }
}
