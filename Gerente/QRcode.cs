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
using MessagingToolkit.QRCode.Codec;
using Microsoft.VisualBasic;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;

namespace Gerente
{
    public partial class QRcode : Form
    {
        public static Random rand = new Random();
        public static string s = rand.Next(100, 1000).ToString();
        public string texto = "Seu código: " + s;
        public QRcode()
        {
            InitializeComponent();
        }

        private void QRcode_Load(object sender, EventArgs e)
        {
            QRCodeEncoder qrCodecEncoder = new QRCodeEncoder();
            qrCodecEncoder.QRCodeBackgroundColor = System.Drawing.Color.White;
            qrCodecEncoder.QRCodeForegroundColor = System.Drawing.Color.Black;
            qrCodecEncoder.CharacterSet = "UTF-8";
            qrCodecEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            qrCodecEncoder.QRCodeScale = 6;
            qrCodecEncoder.QRCodeVersion = 0;
            qrCodecEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.Q;
           
            Image qrcode;
            qrcode = qrCodecEncoder.Encode(texto);
            pictureBox1.Image= qrcode;

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            string cod = Interaction.InputBox("Digite o código que você recebeu ao escanear:");
            if(cod!="")
            {
                if (cod == s)
                {
                    int y = 0;
                    GetSet get = new GetSet();
                    string b = get.geta();
                    Criptografia criptografia = new Criptografia();
                    string[] crip = criptografia.Criptografar(b, s.ToString());
                    try
                    {
                        DataTable table = new DataTable();
                        Connection con = new Connection();
                        string sql = "SELECT Pergunta_Seguranca.pergunta,emailUsuario From pergunta_usuario inner join Pergunta_Seguranca on Pergunta_Seguranca.CodPergunta = pergunta_usuario.CodPergunta WHERE emailUsuario ='" + crip[0] + "'";
                        con.conectar();
                        SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, con.sq);
                        adapter.Fill(table);
                        string pergunta = "";
                        for(int i = 0; i<table.Rows.Count;i++)
                        {
                            pergunta = table.Rows[i].ItemArray[0].ToString();
                        };
                        string reSposta = Interaction.InputBox(pergunta, "Pergunta de Segurança");
                        string SQl = "Select resposta from pergunta_usuario Where emailUsuario='"+crip[0] + "'AND resposta = '"+reSposta+"'";
                        SQLiteCommand command = new SQLiteCommand(sql, con.sq);
                        SQLiteDataReader sQLiteDataReader;
                        sQLiteDataReader = command.ExecuteReader();
                        while(sQLiteDataReader.Read())
                        {
                            y++;
                        }
                        if(y==1)
                        {
                            NovaSenha n = new NovaSenha();
                           
                            n.Show();
                            this.Close();

                        }
                        if(y==0)
                        {
                            MessageBox.Show("Resposta errada!", "Alerta do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch(Exception E)
                    {
                        MessageBox.Show(E.Message, "Alerta do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                   
                }
                else
                {
                    MessageBox.Show("Código incorreto, por favor tente novamente!", "Alerta do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Não deixe o código em branco", "Alerta do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }
    }
}
