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
       private  string[] Criptografar(string login,string senha)
        {
            string[] acesso = new string[2];
            char[] alfabeto =
            {
                'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p',
                'q','r','s','t','u','v','w','x','y','z'
            };
            char[] numeros =
            {
                '1','2','3','4','5','6','7','8','9','0'
            };
            char[] numeros2 = new char[10];
            char[] alfabeto2 = new char[26]; 
            for(int x = 0; x<numeros.Length;x++)
            {
                int b = (x + 7) % 10;
                numeros2[x] = numeros[b];
            }
            for(int i = 0;i<alfabeto.Length;i++)
            {
                int b = (i + 7) % 26;
                alfabeto2[i] = alfabeto[b];

            }
            StringBuilder str = new StringBuilder(login);
            
            for(int j = 0; j<login.Length;j++)
            {
                for (int i = 0; i < alfabeto.Length; i++)
                {
                    if (login[j].ToString().Contains(alfabeto[i]))
                    {
                        str[j] = alfabeto2[i];
                    }
                    
                }
            }
            acesso[0] = str.ToString();
            StringBuilder std = new StringBuilder(senha);

            for (int j = 0; j < senha.Length; j++)
            {
                for (int i = 0; i < alfabeto.Length; i++)
                {
                    if (senha[j].ToString().Contains(alfabeto[i]))
                    {
                        std[j] = alfabeto2[i];
                    }
                    
                }
                for(int x = 0;x<numeros.Length;x++)
                {
                    if (senha[j].ToString().Contains(numeros[x]))
                    {
                        std[j] = numeros2[x];
                    }
                }
            }
            acesso[1] = std.ToString();
            return acesso;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string[] acesso = Criptografar(email.Text, senha.Text);
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

                    string sql = "SELECT Email,Senha FROM Login WHERE Email = '" + acesso[0] + "' AND Senha = '" + acesso[1] + "' ";
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
                    
                    string[] login = Criptografar(email.Text, senha.Text);
                    string sqlinsert = "INSERT INTO Login(Email,Senha) VALUES('"+login[0]+"','" + login[1] +"')";
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
