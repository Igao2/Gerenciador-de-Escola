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
            //this.Visible = false;
            GetSet get = new GetSet();
            
string b = Interaction.InputBox("Digite seu e-mail cadastrado");
            get.seta(b);
            string q = "sdfkjsdfkl";
            Criptografia criptografia = new Criptografia();
            string[] login = criptografia.Criptografar(b, q);
            Connection con = new Connection();
            try
            {
                int z = 0;
                con.conectar();
                string sql = "SELECT Email FROM Login WHERE Email = '" + login[0] + "'";
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
                        string ez = Interaction.InputBox("Digite o código que foi enviado por notificação" , "Mensagem do sistema");
                        if (ez == s)
                        {
                            try
                            {
                                string pergunta = "";
                                string resposta = "";
                                string sqlAd = "Select emailUsuario,Pergunta_Seguranca.pergunta,resposta from pergunta_usuario inner join Pergunta_Seguranca on Pergunta_Seguranca.CodPergunta = pergunta_usuario.CodPergunta where emailUsuario = '" + b + "'";
                                SQLiteDataAdapter liteDataAdapter = new SQLiteDataAdapter(sqlAd, con.sq);
                                liteDataAdapter.Fill(dtSet, "segurança");
                                DataTable segura = dtSet.Tables["segurança"];
                                for(int i = 0; i < segura.Rows.Count; i++)
                                {
                                    DataRow linhaseg = segura.Rows[i];
                                    if (linhaseg.ItemArray[0].ToString()==b)
                                    {
                                        pergunta = linhaseg.ItemArray[1].ToString();
                                        resposta = linhaseg.ItemArray[2].ToString();
                                    }
                                }
                                string teste = Interaction.InputBox(pergunta, "Mensagem do Sistema");
                                if(teste == resposta)
                                {

                                    NovaSenha nova = new NovaSenha();
                                    nova.Show();
                                    
                                }
                                else
                                {
                                    MessageBox.Show("Resposta incorreta!", "Mensagem do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                             catch(Exception E)
                            {
                                MessageBox.Show(E.Message, "Alerta do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            
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
