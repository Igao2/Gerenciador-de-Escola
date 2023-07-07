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
        private DataTable disciplinas = new DataTable();
        private DataTable matriz = new DataTable();
       
        private void Horario_Load(object sender, EventArgs e)
        {

            try
            {
                Connection con = new Connection();
                con.conectar();
                string sql = "Select * From Disciplinas";
                string Sql = "Select * From Matriz";
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, con.sq);
                SQLiteDataAdapter adapter1 = new SQLiteDataAdapter(Sql, con.sq);

                adapter.Fill(disciplinas);
                adapter1.Fill(matriz);
                comboBox1.DataSource = matriz;
                comboBox1.DisplayMember = "CodMatriz";
                comboBox1.ValueMember = "CodMatriz";
                comboBox3.DataSource = disciplinas;
                comboBox3.DisplayMember = "NomeDisciplina";
                comboBox3.ValueMember = "CodDisciplina";
                con.desconectar();

            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message, "Alerta do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                Connection con = new Connection();
                con.conectar();
                string sql = "INSERT INTO Disciplinas Values('" + textBox10.Text + "','" + textBox3.Text + "','" + textBox4.Text + "')";
                SQLiteCommand command = new SQLiteCommand(sql, con.sq);
                command.ExecuteNonQuery();
                string[] valores =
                {
                    textBox10.Text,textBox3.Text,textBox4.Text
                };
                disciplinas.Rows.Add(valores);
                textBox10.Clear();
                textBox3.Clear();
                textBox4.Clear();
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message, "Alerta do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }

       

       

       

     
        

       
    }

