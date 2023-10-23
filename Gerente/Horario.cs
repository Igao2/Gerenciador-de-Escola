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
        private DataTable horarios = new DataTable();
        private DataTable turmas = new DataTable();
        private void Horario_Load(object sender, EventArgs e)
        {

            try
            {
                Connection con = new Connection();
                con.conectar();
                string hr = "SELECT  t.descTurma as Turma,   Hora,   d1.NomeDisciplina AS Segunda,  d2.NomeDisciplina AS Terca,    d3.NomeDisciplina AS Quarta,    d4.NomeDisciplina AS Quinta, d5.NomeDisciplina AS Sexta FROM    horario as h     inner join Turma t on t.CodTurma = h.CodTurma LEFT JOIN   disciplinas AS d1 ON h.Segunda = d1.CodDisciplina LEFT JOIN   disciplinas AS d2 ON h.Terca = d2.CodDisciplina LEFT JOIN    disciplinas AS d3 ON h.Quarta = d3.CodDisciplina LEFT JOIN disciplinas AS d4 ON h.Quinta = d4.CodDisciplina LEFT JOIN disciplinas AS d5 ON h.Sexta = d5.CodDisciplina";
                string sql = "Select * From Disciplinas";
                string Sql = "Select * From Matriz";
                string xql = "Select descTurma as Turma from Turma";
                SQLiteDataAdapter hor = new SQLiteDataAdapter(hr, con.sq);
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, con.sq);
                SQLiteDataAdapter adapter1 = new SQLiteDataAdapter(Sql, con.sq);
                SQLiteDataAdapter xq = new SQLiteDataAdapter(xql, con.sq);
                hor.Fill(horarios);
                adapter.Fill(disciplinas);
                adapter1.Fill(matriz);
                xq.Fill(turmas);
                dataGridView1.DataSource = horarios;
                comboBox2.DataSource = turmas;
                comboBox2.ValueMember = "Turma";
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

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void comboBox2_SelectedValueChanged(object sender, EventArgs e)
        {
            

        }

        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            DataTable a = new DataTable();
            a.Columns.Add("Turma");
            a.Columns.Add("Hora");
            a.Columns.Add("Segunda");
            a.Columns.Add("Terça");
            a.Columns.Add("Quarta");
            a.Columns.Add("Quinta");
            a.Columns.Add("Sexta");
            if (comboBox1.SelectedItem.ToString() == "Todas")
            {
                dataGridView1.DataSource = horarios;
            }
            else
            {
                DataRow[] resultados = horarios.Select("Turma ='" + comboBox2.Text + "'");
                for (int i = 0; i < resultados.Length; i++)
                {
                    string[] results =
                    {
                    resultados[i][0].ToString(),
                    resultados[i][1].ToString(),
                    resultados[i][2].ToString(),
                    resultados[i][3].ToString(),
                    resultados[i][4].ToString(),
                    resultados[i][5].ToString(),
                    resultados[i][6].ToString()

                };
                    a.Rows.Add(results);

                }
                dataGridView1.DataSource = a;
            }




          

         }
    }

       

       

       

     
        

       
}

