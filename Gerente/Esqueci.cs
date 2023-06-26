using Microsoft.VisualBasic.Devices;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gerente
{
    public partial class Esqueci : Form
    {
        public Esqueci()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
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
                SQLiteCommand command = new SQLiteCommand(sql, con.sq);
                SQLiteDataReader dataReader;
                dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    z++;
                }
                if (z == 1)
                {
                    Network net = new Network();
                    if (net.IsAvailable == true)
                    {
                        int y = 0;
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
                            try
                            {
                                DataTable table = new DataTable();

                                string Sql = "SELECT Pergunta_Seguranca.pergunta,emailUsuario From pergunta_usuario inner join Pergunta_Seguranca on Pergunta_Seguranca.CodPergunta = pergunta_usuario.CodPergunta WHERE emailUsuario ='" + login[0] + "'";

                                SQLiteDataAdapter adapter = new SQLiteDataAdapter(Sql, con.sq);
                                adapter.Fill(table);
                                string pergunta = "";
                                foreach (DataRow row in table.Rows)
                                {
                                    pergunta = row.ItemArray[0].ToString();
                                };
                                string reSposta = Interaction.InputBox(pergunta, "Pergunta de Segurança");
                                string SQl = "Select resposta from pergunta_usuario Where emailUsuario='" + login[0] + "'AND resposta = '" + reSposta + "'";
                                SQLiteCommand Command = new SQLiteCommand(sql, con.sq);
                                SQLiteDataReader sQLiteDataReader;
                                sQLiteDataReader = Command.ExecuteReader();
                                while (sQLiteDataReader.Read())
                                {
                                    y++;
                                }
                                if (y == 1)
                                {
                                    NovaSenha n = new NovaSenha();

                                    n.Show();


                                }
                                if (y == 0)
                                {
                                    MessageBox.Show("Resposta errada!", "Alerta do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            catch (Exception E)
                            {
                                MessageBox.Show(E.Message, "Alerta do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                        }
                        else
                        {
                            MessageBox.Show("Código de verificação incorreto,por favor refaça o processo", "Alerta do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            
                        }
                    }
                    else
                    {
                        MessageBox.Show("Você não possui conexão com a internet, por favor tente o outro método de recuperação de senha!", "Alerta do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    }

                }
                if (z == 0)
                {
                    MessageBox.Show("Email não cadastrado!", "Alerta do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.Visible = true;
                }
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
                
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GetSet get = new GetSet();

            string b = Interaction.InputBox("Digite seu e-mail cadastrado");
            get.seta(b);
            QRcode qrcode = new QRcode();
            qrcode.Show();
        }
    }
}
