﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xceed.Document.NET;
using Xceed.Words.NET;

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
                string Sql = "SELECT Aluno.nomeAluno as Aluno,Motivo,Turma.descTurma as Turma,Data from Ocorrencias \r\ninner join Aluno on Aluno.CodAluno = Ocorrencias.CodAluno\r\ninner join Turma on Turma.CodTurma = Aluno.CodTurma";
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

        private void button4_Click(object sender, EventArgs e)
        {
            if(dataGridView2.SelectedRows.Count>0)
            {
                saveFileDialog1.Filter = "docx files (*.docx)|*.docx";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string nomearquivo = saveFileDialog1.FileName;
                    string texto = "\r\nOcorrência Escolar";
                    var doc = DocX.Create(nomearquivo, Xceed.Document.NET.DocumentTypes.Document);
                    doc.InsertParagraph(texto).FontSize(25).Font("Arial");
                    string[] valores =
                    {
                     dataGridView2.SelectedRows[0].Cells[0].Value.ToString(),
                    dataGridView2.SelectedRows[0].Cells[1].Value.ToString(),
                    dataGridView2.SelectedRows[0].Cells[2].Value.ToString(),
                    dataGridView2.SelectedRows[0].Cells[3].Value.ToString()
                };

                    doc.InsertParagraph("\r\n\r\nData: " + valores[3] + "\r\n\r\nAluno(a): " + valores[0] + "\r\nTurma: " + valores[2] + "\r\n\r\nMotivo da Ocorrência:\r\n" + valores[1] + "\r\n\r\n\r\nAssinatura do Professor(a): _______________________ \r\n\r\r\n\r\nAssinatura do Responsável:_______________________").FontSize(14).Font("Arial");
                    doc.Save();

                }
            }
            else
            {
                MessageBox.Show("Nenhuma Ocorrência Selecionada!", "Alerta do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView3.SelectedRows.Count > 0)
            {
                saveFileDialog1.Filter = "docx files (*.docx)|*.docx";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string nomearquivo = saveFileDialog1.FileName;
                    var doc = DocX.Create(nomearquivo, Xceed.Document.NET.DocumentTypes.Document);
                    if (dataGridView3.SelectedRows[0].Cells[1].Value.ToString()=="E")
                    {
                        
                        string texto = "\nExpulsão";
                        
                        doc.InsertParagraph(texto).FontSize(25).Font("Arial");
                        string[] valores =
                        {
                     dataGridView3.SelectedRows[0].Cells[0].Value.ToString(),
                    dataGridView3.SelectedRows[0].Cells[1].Value.ToString(),
                    dataGridView3.SelectedRows[0].Cells[2].Value.ToString(),
                    dataGridView3.SelectedRows[0].Cells[3].Value.ToString(),
                    dataGridView3.SelectedRows[0].Cells[4].Value.ToString()
                    };
                        doc.InsertParagraph("\r\n\r\nData: " + valores[4] + "\r\n\r\nAluno(a): " + valores[0] + "\r\nTurma: " + valores[3] + "\r\n\r\nMotivo da Expulsão:\r\n" + valores[2] + "\r\n\r\n\r\nAssinatura do Professor(a): _______________________ \r\n\r\r\nAssinatura do Responsável:_______________________").FontSize(14).Font("Arial");
                        doc.Save();

                    }
                    if (dataGridView3.SelectedRows[0].Cells[1].Value.ToString()=="S")
                    {
                        string texto = "\nSuspensão";

                        doc.InsertParagraph(texto).FontSize(25).Font("Arial");
                        string[] valores =
                        {
                     dataGridView3.SelectedRows[0].Cells[0].Value.ToString(),
                    dataGridView3.SelectedRows[0].Cells[1].Value.ToString(),
                    dataGridView3.SelectedRows[0].Cells[2].Value.ToString(),
                    dataGridView3.SelectedRows[0].Cells[3].Value.ToString(),
                    dataGridView3.SelectedRows[0].Cells[4].Value.ToString()    
                        };
                        doc.InsertParagraph("\r\n\r\nData: " + valores[4] + "\r\n\r\nAluno(a): " + valores[0] + "\r\nTurma: " + valores[3] + "\r\n\r\nMotivo da Suspensão:\r\n" + valores[2] + "\r\n\r\n\r\nAssinatura do Diretor(a): _______________________ \r\n\r\r\nAssinatura do Responsável:_______________________").FontSize(14).Font("Arial");
                        doc.Save();

                    }

                    

                }
            }
            else
            {
                MessageBox.Show("Nenhuma Suspensão/Expulsão Selecionada!", "Alerta do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow clickedRow = dataGridView3.Rows[e.RowIndex];
            clickedRow.Selected = true;
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow clickedRow = dataGridView2.Rows[e.RowIndex];
            clickedRow.Selected = true;
        }
    }
}
