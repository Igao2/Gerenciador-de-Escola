using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MessagingToolkit.QRCode.Codec;
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
            this.Close();
        }
    }
}
