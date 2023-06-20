using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Xceed.Words.NET;
using static System.Net.Mime.MediaTypeNames;
using OfficeOpenXml;
using System.IO;
using System.Data.SQLite;
using System.Data.Entity.Migrations.Model;
using Application = System.Windows.Forms.Application;
using OfficeOpenXml.Style;

namespace Gerente
{
    public partial class Horario : Form
    {
        public Horario()
        {
            InitializeComponent();
        }
       
        private void Horario_Load(object sender, EventArgs e)
        {
            
            Connection con = new Connection();
            con.conectar();

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripTextBox5_Click(object sender, EventArgs e)
        {
        }

        private void Segunda_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "docx files (*.docx)|*.docx";
           
                
            
        }

   
        private void button2_Click(object sender, EventArgs e)
        {

            Registrar_horario reg = new Registrar_horario();
            reg.Show();
        }
            
            


        }

       

       

       

     
        

       
    }

