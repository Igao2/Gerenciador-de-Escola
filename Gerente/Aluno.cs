using Microsoft.VisualBasic.ApplicationServices;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xceed.Document.NET;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Text.Json;
using Python.Runtime;
using System.Text.RegularExpressions;

namespace Gerente
{
    public partial class Aluno : Form
    {
        public Aluno()
        {
            InitializeComponent();
        }
        public DataTable Alunos = new DataTable();
        private DataTable Turmas = new DataTable();
        private DataTable Matriz = new DataTable();
        private bool status = false;
        private string periodo = "";
        private void Aluno_Load(object sender, EventArgs e)
        {
            
            carregar();
            inicializar();
        }

        private void carregar()
        {
         
            try
            {
                Connection con = new Connection();
                con.conectar();
                
                string sql = "SELECT nomeAluno as Aluno,CPF,nomeResponsavel as Responsável,telefoneCasa as TelefonedeCasa,Turma.descTurma as Turma From Aluno " +
                    "Inner join Turma on Turma.CodTurma = Aluno.CodTurma ";
                string Sql = "Select * From Turma";
                string SqL = "Select * From Matriz";
                SQLiteDataAdapter sQLiteData = new SQLiteDataAdapter(sql, con.sq);
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(Sql, con.sq);
                SQLiteDataAdapter sQ = new SQLiteDataAdapter(SqL,con.sq);
                sQLiteData.Fill(Alunos);
                dataGridView1.DataSource = Alunos;
                adapter.Fill(Turmas);
                dataGridView2.DataSource = Turmas;
                sQ.Fill(Matriz);
              
                con.desconectar();
            }
             catch(Exception E )
            {
                MessageBox.Show(E.Message,"Alerta do Sistema",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        private void inicializar()
        {
            
            
            
           
            for(int i = 0; i<Matriz.Rows.Count;i++)
            {
                DataRow linhaMatriz = Matriz.Rows[i];
                comboBox4.Items.Add(linhaMatriz.ItemArray[0].ToString());
               
            }
            for(int i = 0; i<Turmas.Rows.Count;i++)
            {
                DataRow linhaTurma = Turmas.Rows[i];
                comboBox2.Items.Add(linhaTurma.ItemArray[2].ToString());
                comboBox1.Items.Add(linhaTurma.ItemArray[2].ToString());
            }
            comboBox1.Items.Add("Todas");
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
               
            DataTable a = new DataTable();
            a.Columns.Add("Nome do Aluno");
            a.Columns.Add("CPF");
            a.Columns.Add("Nome do Responsável");
            a.Columns.Add("Telefone de Casa");
            a.Columns.Add("Turma");
            if (comboBox1.SelectedItem.ToString() == "Todas")
            {
                dataGridView1.DataSource = Alunos;
            }
            else
            {
                DataRow[] resultados = Alunos.Select("Turma ='" + comboBox1.Text + "'");
                for (int i = 0; i < resultados.Length; i++)
                {
                    string[] results =
                    {
                    resultados[i][0].ToString(),
                    resultados[i][1].ToString(),
                    resultados[i][2].ToString(),
                    resultados[i][3].ToString(),
                    resultados[i][4].ToString()

                };
                    a.Rows.Add(results);

                }
                dataGridView1.DataSource = a;
            }

            
        
            
            
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Hide();
            groupBox1.Show();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && maskedTextBox2.Text != "" && textBox3.Text != "" && maskedTextBox1.Text != "" && comboBox2.SelectedItem.ToString() != "")
            {
                try
                {
                    Connection con = new Connection();
                    con.conectar();
                  
                    int codTurma = 0;
                    for(int i = 0; i<Turmas.Rows.Count;i++)
                    {
                        DataRow linhaturma = Turmas.Rows[i];
                        if (linhaturma.ItemArray[2].ToString() == comboBox2.SelectedItem.ToString()) 
                        {
                            codTurma = int.Parse(linhaturma.ItemArray[0].ToString());
                        }
                    }
                    string SQL = "INSERT INTO Aluno VALUES('"+textBox1.Text+"','"+maskedTextBox2.Text+"','"+textBox3.Text+"','"+maskedTextBox1.Text+"','"+codTurma+"')";
                    SQLiteCommand command = new SQLiteCommand(SQL, con.sq);
                    command.ExecuteNonQuery();
                    con.desconectar();
                    string[] valores =
                    {
                        textBox1.Text,
                        maskedTextBox2.Text,
                        textBox3.Text,
                        maskedTextBox1.Text,
                        codTurma.ToString()
                    };

                    Alunos.Rows.Add(valores);
                    textBox1.Clear();
                    maskedTextBox1.Clear();
                    maskedTextBox2.Clear();
                }
                catch(Exception E)
                {
                    MessageBox.Show(E.Message, "Alerta do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Preencha todos os campos", "Alerta do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (textBox6.Text != "" && textBox5.Text != "" && status == true && comboBox4.Text !="")
            {
                try
                {
                    Connection con = new Connection();
                    con.conectar();
                    int a = int.Parse(textBox5.Text);
                    int b = int.Parse(comboBox4.SelectedItem.ToString());

                    string SQL = "INSERT INTO Turma Values('"+a+"','"+comboBox4.Text+"','" + textBox5.Text + "','"+periodo+"')";
                    SQLiteCommand command = new SQLiteCommand(SQL, con.sq);
                    command.ExecuteNonQuery();
                    con.desconectar();

                    textBox5.Clear();
                    textBox6.Clear();
                    radioButton1.Checked = false;
                    radioButton2.Checked = false;
                    radioButton3.Checked = false;
                    periodo = "";
                }
                catch (Exception E)
                {
                    MessageBox.Show(E.Message, "Alerta do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Preencha todos os campos", "Alerta do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton1.Checked)
            {
                radioButton2.Checked = false;
                radioButton3.Checked = false;
                status = true;
                periodo = "Manhã";

            }
            
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton2.Checked)
            {
                radioButton1.Checked = false;
                radioButton3.Checked = false;
                status = true;
                periodo = "Tarde";

            }
           
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton3.Checked)
            {
                radioButton2.Checked = false;
                radioButton1.Checked = false;
                status = true;
                periodo = "Noite";
            }
           
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            label12.Text = comboBox4.Text;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "xlsx files (*.xlsx)|*.xlsx";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string a = openFileDialog1.FileName;
                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.Commercial;

                var package = new ExcelPackage(a);
                var workbook = package.Workbook;

                var sheets = workbook.Worksheets[0];
                DataTable table = new DataTable();

                table = sheets.Cells["A:E"].ToDataTable();
                for(int i = 0; i<table.Rows.Count;i++)
                {
                    DataRow row = table.Rows[i];
                    try
                    {
                        Connection con = new Connection();
                        con.conectar();
                        string sql = "INSERT INTO Aluno VALUES ('" + row.ItemArray[0].ToString() + "','" + row.ItemArray[1].ToString() + "','" + row.ItemArray[2].ToString() + "','" + row.ItemArray[3].ToString() + "','" + row.ItemArray[4].ToString() + "')";
                        SQLiteCommand comm = new SQLiteCommand(sql, con.sq);
                        comm.ExecuteNonQuery();
                        string[] valores =
                        {
                            row.ItemArray[0].ToString(),
                            row.ItemArray[1].ToString(),
                            row.ItemArray[2].ToString(),
                            row.ItemArray[3].ToString(),
                            row.ItemArray[4].ToString()
                        };
                        Alunos.Rows.Add(valores);
                        
                    }
                    catch(Exception E)
                    {
                        MessageBox.Show(E.Message);
                    }
                }

                



            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = Alunos;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Ocorrencias oc = new Ocorrencias();
            oc.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DataTable a = new DataTable();
            a.Columns.Add("Nome do Aluno");
            a.Columns.Add("CPF");
            a.Columns.Add("Nome do Responsável");
            a.Columns.Add("Telefone de Casa");
            a.Columns.Add("Turma");
         
            DataView view = new DataView(Alunos);


            foreach (DataRowView views in view)
            {
                if (Regex.Match(views["Aluno"].ToString(), textBox2.Text, RegexOptions.IgnoreCase).Success)
                {
                    string[] dados =
                {
                    views["Aluno"].ToString(),
                    views["CPf"].ToString(),
                    views["Responsável"].ToString(),
                    views["TelefonedeCasa"].ToString(),
                    views["Turma"].ToString()

                };
                    a.Rows.Add(dados);
                    dataGridView1.DataSource = a;
                }

                
            }
            textBox2.Text = "";


        }
    }
}
