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
        public string s = rand.Next(100, 1000).ToString();
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
            qrcode = qrCodecEncoder.Encode(s);
            pictureBox1.Image= qrcode;

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            string cod = Interaction.InputBox("Digite o código que você recebeu ao escanear:");
            if(s!="")
            {
                if (cod == s)
                {
                    string senha = Interaction.InputBox("Insira sua senha nova:");
                    try
                    {
                        Login log = new Login();
                        Connection con = new Connection();
                        con.conectar();
                        string update = "UPDATE Login SET Senha = '" + senha + "' WHERE Email ='" + log.b + "'";
                        SQLiteCommand command = new SQLiteCommand(update, con.sq);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Por favor faça o login!");
                        this.Close();
                    }
                    catch (Exception E)
                    {
                        MessageBox.Show(E.Message);
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
