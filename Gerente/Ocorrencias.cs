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

namespace Gerente
{
    public partial class Ocorrencias : Form
    {
        public Ocorrencias()
        {
            InitializeComponent();
        }

        private DataTable aluno = new DataTable();
        private DataTable ocorrencias = new DataTable();
        private DataTable suspexp = new DataTable();
        private void Ocorrencias_Load(object sender, EventArgs e)
        {
            dataGridView1.Hide();
            try
            {
                Connection con = new Connection();
                con.conectar();
                string sql = "Select nomeAluno as Aluno,CodAluno from Aluno";
                string Sql = "SELECT Aluno.nomeAluno as Aluno,Motivo,Turma.descTurma,Data as Turma from Ocorrencias \r\ninner join Aluno on Aluno.CodAluno = Ocorrencias.CodAluno\r\ninner join Turma on Turma.CodTurma = Aluno.CodTurma";
                string sQL = "SELECT Aluno.nomeAluno as Aluno,ES,Motivo,Turma.descTurma,Data as Turma from SuspExp \r\ninner join Aluno on Aluno.CodAluno = SuspExp.CodAluno\r\ninner join Turma on Turma.CodTurma = Aluno.CodTurma";
                SQLiteDataAdapter sQLiteData = new SQLiteDataAdapter(sQL, con.sq);
                sQLiteData.Fill(suspexp);
                dataGridView3.DataSource = suspexp;
                SQLiteDataAdapter qLiteDataAdapter = new SQLiteDataAdapter(Sql, con.sq);
                qLiteDataAdapter.Fill(ocorrencias);
                dataGridView2.DataSource = ocorrencias;
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, con.sq);
                adapter.Fill(aluno);
                dataGridView1.DataSource = aluno;

            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message,"Alerta do Sistema",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            comboBox1.Items.Add("Expulsão");
            comboBox1.Items.Add("Suspensão");
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(button2.Text == "Ver lista de alunos")
            {
                dataGridView2.Hide();
                dataGridView1.Show();
                button2.Text = "Ver lista de ocorrências";
            }
            else
            {
                dataGridView1.Hide();
                dataGridView2.Show();
                button2.Text = "Ver lista de alunos";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Connection con = new Connection();
                con.conectar();
                DataTable comparar = new DataTable();
                string nomealuno = "";
                string turma = "";
                string sql = "Select nomeAluno,CodAluno,Turma.descTurma from Aluno inner join Turma on Turma.CodTurma = Aluno.CodTurma";
                SQLiteDataAdapter ad = new SQLiteDataAdapter(sql, con.sq);
                ad.Fill(comparar);
                foreach(DataRow row in comparar.Rows)
                {
                    if (textBox1.Text == row["CodAluno"].ToString())
                    {
                        nomealuno = row["nomeAluno"].ToString();
                        turma = row["descTurma"].ToString();
                    }
                }
                string comand = "Insert into Ocorrencias Values('"+textBox1.Text+"','"+textBox2.Text+"','"+maskedTextBox2.Text+"')";
                SQLiteCommand com = new SQLiteCommand(comand, con.sq);
                com.ExecuteNonQuery();
                string[] valores =
                {
                    nomealuno,textBox2.Text,turma,maskedTextBox2.Text
                };
                ocorrencias.Rows.Add(valores);
                con.desconectar();
                textBox1.Clear();
                textBox2.Clear();
                maskedTextBox2.Clear();
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message, "Alerta do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.SelectedItem.ToString()=="Expulsão")
            {
                label4.Text = "Motivo da Expulsão";
                label6.Text = "Motivo da Expulsão";
                button3.Text = "Registrar Expulsão";

            }
            if(comboBox1.SelectedItem.ToString() == "Suspensão")
            {
                label4.Text = "Motivo da Suspensão";
                label6.Text = "Motivo da Suspensão";
                button3.Text = "Registrar Suspensão";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                Connection con = new Connection();
                con.conectar();
                DataTable comparar = new DataTable();
                string nomealuno = "";
                string turma = "";
                string sql = "Select nomeAluno,CodAluno,Turma.descTurma from Aluno inner join Turma on Turma.CodTurma = Aluno.CodTurma";
                SQLiteDataAdapter ad = new SQLiteDataAdapter(sql, con.sq);
                ad.Fill(comparar);
                foreach (DataRow row in comparar.Rows)
                {
                    if (textBox3.Text == row["CodAluno"].ToString())
                    {
                        nomealuno = row["nomeAluno"].ToString();
                        turma = row["descTurma"].ToString();
                    }
                }
                string expsp = "";
                if(comboBox1.SelectedItem.ToString()=="Expulsão")
                {
                    expsp = "E";
                }
                if(comboBox1.SelectedItem.ToString()=="Suspensão")
                {
                    expsp = "S";
                }
                string comand = "Insert into SuspExp Values('" + textBox3.Text + "','" + expsp+ "','" + textBox4.Text + "','"+maskedTextBox1.Text+"')";
                SQLiteCommand com = new SQLiteCommand(comand, con.sq);
                com.ExecuteNonQuery();
                string[] valores =
                {
                    nomealuno,expsp,textBox4.Text,turma,maskedTextBox1.Text
                };
               suspexp.Rows.Add(valores);
                con.desconectar();
                textBox3.Clear();
                textBox4.Clear();
                maskedTextBox1.Clear();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Alerta do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
