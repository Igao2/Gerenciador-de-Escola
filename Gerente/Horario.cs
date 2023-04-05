﻿using System;
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

namespace Gerente
{
    public partial class Horario : Form
    {
        public Horario()
        {
            InitializeComponent();
        }
        int dia = 0;
        int contador = 0;
        List<Tuple<string, string, string, string, string, string, string>> segunda = new List<Tuple<string, string, string, string, string, string, string>>();
        List<Tuple<string, string, string, string, string, string, string>> terca = new List<Tuple<string, string, string, string, string, string, string>>();
        List<Tuple<string, string, string, string, string, string, string>> quarta = new List<Tuple<string, string, string, string, string, string, string>>();
        List<Tuple<string, string, string, string, string, string, string>> quinta = new List<Tuple<string, string, string, string, string, string, string>>();
        List<Tuple<string, string, string, string, string, string, string>> sexta = new List<Tuple<string, string, string, string, string, string, string>>();
        List<Tuple<string, string, string, string, string, string, string>> horario = new List<Tuple<string, string, string, string, string, string, string>>();
        private DataSet dataSet = new DataSet();
        private void Horario_Load(object sender, EventArgs e)
        {
            try
            {
                Connection connection = new Connection();
                connection.conectar();
                string sql = "SELECT CodGrade, Disciplinas.NomeDisciplina FROM GradeCurricular" +
                    " INNER JOIN Disciplinas on Disciplinas.CodDisciplina = GradeCurricular.CodDisciplina GROUP BY CodGrade";
                SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(sql, connection.sq);

                dataAdapter.Fill(dataSet, "Grade");
                DataTable table = dataSet.Tables["Grade"];
                for(int i = 0;i<table.Rows.Count;i++)
                {
                    DataRow linhaGrade = table.Rows[i];
                    comboBox1.Items.Add(linhaGrade.ItemArray[0].ToString());
                }
                
            }
            catch(Exception E)
            {
                MessageBox.Show(E.Message,"Alerta do Sistema",MessageBoxButtons.OK,MessageBoxIcon.Error);
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
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string nomearquivo = saveFileDialog1.FileName;
                string texto = "Segunda: \n" +
                    "" + horario[0].Item1 + " " + segunda[0].Item1 + "\n" +
                    "" + horario[0].Item2 + " " + segunda[0].Item2 + "\n" +
                    "" + horario[0].Item3 + " " + segunda[0].Item3 + "\n" +
                    "" + horario[0].Item4 + " " + segunda[0].Item4 + "\n" +
                    "" + horario[0].Item5 + " " + segunda[0].Item5 + "\n" +
                     "" + horario[0].Item6 + " " + segunda[0].Item6 + "\n" +
                      "" + horario[0].Item7 + " " + segunda[0].Item7 + "\n" +
                    " \n Terça-Feira: \n" +
                    "" + horario[1].Item1 + " " + terca[0].Item1 + "\n" +
                    "" + horario[1].Item2 + " " + terca[0].Item2 + "\n" +
                    "" + horario[1].Item3 + " " + terca[0].Item3 + "\n" +
                    "" + horario[1].Item4 + " " + terca[0].Item4 + "\n" +
                    "" + horario[1].Item5 + " " + terca[0].Item5 + "\n" +
                     "" + horario[1].Item6 + " " + terca[0].Item6 + "\n" +
                      "" + horario[1].Item7 + " " + terca[0].Item7 + "\n" +
                    " \n Quarta-Feira: \n" +
                    "" + horario[2].Item1 + " " + quarta[0].Item1 + "\n" +
                    "" + horario[2].Item2 + " " + quarta[0].Item2 + "\n" +
                    "" + horario[2].Item3 + " " + quarta[0].Item3 + "\n" +
                    "" + horario[2].Item4 + " " + quarta[0].Item4 + "\n" +
                    "" + horario[2].Item5 + " " + quarta[0].Item5 + "\n" +
                    "" + horario[2].Item6 + " " + quarta[0].Item6 + "\n" +
                    "" + horario[2].Item7 + " " + quarta[0].Item7 + "\n" +
                    " \n Quinta-Feira: \n" +
                    "" + horario[3].Item1 + " " + quinta[0].Item1 + "\n" +
                    "" + horario[3].Item2 + " " + quinta[0].Item2 + "\n" +
                    "" + horario[3].Item3 + " " + quinta[0].Item3 + "\n" +
                    "" + horario[3].Item4 + " " + quinta[0].Item4 + "\n" +
                    "" + horario[3].Item5 + " " + quinta[0].Item5 + "\n" +
                    "" + horario[3].Item6 + " " + quinta[0].Item6 + "\n" +
                    "" + horario[3].Item7 + " " + quinta[0].Item7 + "\n" +
                    " \n Sexta-Feira: \n" +
                    "" + horario[4].Item1 + " " + sexta[0].Item1 + "\n" +
                    "" + horario[4].Item2 + " " + sexta[0].Item2 + "\n" +
                    "" + horario[4].Item3 + " " + sexta[0].Item3 + "\n" +
                    "" + horario[4].Item4 + " " + sexta[0].Item4 + "\n" +
                    "" + horario[4].Item5 + " " + sexta[0].Item5 + "\n" +
                    "" + horario[4].Item6 + " " + sexta[0].Item6 + "\n" +
                    "" + horario[4].Item7 + " " + sexta[0].Item7 + "\n";




                var doc = DocX.Create(nomearquivo, Xceed.Document.NET.DocumentTypes.Document);
                doc.InsertParagraph(texto);
                doc.Save();
                
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            dia = 2;
            contador++;
            toolStripMenuItem1.Text = toolStripMenuItem2.Text;
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            dia = 3;
            contador++;
            toolStripMenuItem1.Text = toolStripMenuItem3.Text;
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            dia = 4;
            contador++;
            toolStripMenuItem1.Text = toolStripMenuItem4.Text;
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            dia = 5;
            contador++;
            toolStripMenuItem1.Text = toolStripMenuItem5.Text;
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            dia = 6;
            contador++;
            toolStripMenuItem1.Text = toolStripMenuItem6.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(toolStripMenuItem1.Text == "Dia")
            {
                MessageBox.Show("Escolha o dia!!", "Alerta do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else
            {
                if (dia == 2)
                {
                    segunda.Add(Tuple.Create(materias1.SelectedItem.ToString(), materias2.SelectedItem.ToString(), materias3.SelectedItem.ToString(), materias4.SelectedItem.ToString(), materias5.SelectedItem.ToString(), materias6.SelectedItem.ToString(), materias7.SelectedItem.ToString()));
                    horario.Add(Tuple.Create(maskedTextBox1.Text, maskedTextBox2.Text, maskedTextBox3.Text, maskedTextBox4.Text, maskedTextBox5.Text, maskedTextBox6.Text, maskedTextBox7.Text));

                }
                if (dia == 3)
                {
                    terca.Add(Tuple.Create(materias1.SelectedItem.ToString(), materias2.SelectedItem.ToString(), materias3.SelectedItem.ToString(), materias4.SelectedItem.ToString(), materias5.SelectedItem.ToString(), materias6.SelectedItem.ToString(), materias7.SelectedItem.ToString()));
                    horario.Add(Tuple.Create(maskedTextBox1.Text, maskedTextBox2.Text, maskedTextBox3.Text, maskedTextBox4.Text, maskedTextBox5.Text, maskedTextBox6.Text, maskedTextBox7.Text));

                }
                if (dia == 4)
                {
                    quarta.Add(Tuple.Create(materias1.SelectedItem.ToString(), materias2.SelectedItem.ToString(), materias3.SelectedItem.ToString(), materias4.SelectedItem.ToString(), materias5.SelectedItem.ToString(), materias6.SelectedItem.ToString(), materias7.SelectedItem.ToString()));
                    horario.Add(Tuple.Create(maskedTextBox1.Text, maskedTextBox2.Text, maskedTextBox3.Text, maskedTextBox4.Text, maskedTextBox5.Text, maskedTextBox6.Text, maskedTextBox7.Text));

                }
                if (dia == 5)
                {
                    quinta.Add(Tuple.Create(materias1.SelectedItem.ToString(), materias2.SelectedItem.ToString(), materias3.SelectedItem.ToString(), materias4.SelectedItem.ToString(), materias5.SelectedItem.ToString(), materias6.SelectedItem.ToString(), materias7.SelectedItem.ToString()));
                    horario.Add(Tuple.Create(maskedTextBox1.Text, maskedTextBox2.Text, maskedTextBox3.Text, maskedTextBox4.Text, maskedTextBox5.Text, maskedTextBox6.Text, maskedTextBox7.Text));

                }
                if (dia == 6)
                {
                    sexta.Add(Tuple.Create(materias1.SelectedItem.ToString(), materias2.SelectedItem.ToString(), materias3.SelectedItem.ToString(), materias4.SelectedItem.ToString(), materias5.SelectedItem.ToString(), materias6.SelectedItem.ToString(), materias7.SelectedItem.ToString()));
                    horario.Add(Tuple.Create(maskedTextBox1.Text, maskedTextBox2.Text, maskedTextBox3.Text, maskedTextBox4.Text, maskedTextBox5.Text, maskedTextBox6.Text, maskedTextBox7.Text));

                }
                if (segunda.Count != 0 && terca.Count != 0 && quarta.Count != 0 && quinta.Count != 0 && sexta.Count != 0)
                {
                    ListViewItem item = new ListViewItem(horario[0].Item1);
                    ListViewItem item2 = new ListViewItem(horario[0].Item2);
                    ListViewItem item3 = new ListViewItem(horario[0].Item3);
                    ListViewItem item4 = new ListViewItem(horario[0].Item4);
                    ListViewItem item5 = new ListViewItem(horario[0].Item5);
                    ListViewItem item6 = new ListViewItem(horario[0].Item6);
                    ListViewItem item7 = new ListViewItem(horario[0].Item7);

                    item.SubItems.Add(segunda[0].Item1);
                    item2.SubItems.Add(segunda[0].Item2);
                    item3.SubItems.Add(segunda[0].Item3);
                    item4.SubItems.Add(segunda[0].Item4);
                    item5.SubItems.Add(segunda[0].Item5);
                    item6.SubItems.Add(segunda[0].Item6);
                    item7.SubItems.Add(segunda[0].Item7);

                    item.SubItems.Add(terca[0].Item1);
                    item2.SubItems.Add(terca[0].Item2);
                    item3.SubItems.Add(terca[0].Item3);
                    item4.SubItems.Add(terca[0].Item4);
                    item5.SubItems.Add(terca[0].Item5);
                    item6.SubItems.Add(terca[0].Item6);
                    item7.SubItems.Add(terca[0].Item7);

                    item.SubItems.Add(quarta[0].Item1);
                    item2.SubItems.Add(quarta[0].Item2);
                    item3.SubItems.Add(quarta[0].Item3);
                    item4.SubItems.Add(quarta[0].Item4);
                    item5.SubItems.Add(quarta[0].Item5);
                    item6.SubItems.Add(quarta[0].Item6);
                    item7.SubItems.Add(quarta[0].Item7);

                    item.SubItems.Add(quinta[0].Item1);
                    item2.SubItems.Add(quinta[0].Item2);
                    item3.SubItems.Add(quinta[0].Item3);
                    item4.SubItems.Add(quinta[0].Item4);
                    item5.SubItems.Add(quinta[0].Item5);
                    item6.SubItems.Add(quinta[0].Item6);
                    item7.SubItems.Add(quinta[0].Item7);

                    item.SubItems.Add(sexta[0].Item1);
                    item2.SubItems.Add(sexta[0].Item2);
                    item3.SubItems.Add(sexta[0].Item3);
                    item4.SubItems.Add(sexta[0].Item4);
                    item5.SubItems.Add(sexta[0].Item5);
                    item6.SubItems.Add(sexta[0].Item6);
                    item7.SubItems.Add(sexta[0].Item7);


                    listView1.Items.AddRange(new ListViewItem[] { item, item2, item3, item4, item5, item6, item7 });
                }

            }
            
            


        }

        private void maskedTextBox4_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void maskedTextBox2_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void maskedTextBox5_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (contador >= 5)
            {
                saveFileDialog1.Filter = "xlsx files (*.xlsx)|*.xlsx";

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string a = saveFileDialog1.FileName;
                    ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.Commercial;

                    var package = new ExcelPackage(a);
                    var workbook = package.Workbook;

                    var sheets = workbook.Worksheets.Add("Horário " + textBox1.Text + "(" + textBox2.Text + ")");

                    sheets.Cells["A1"].Value = "Horário";
                    sheets.Cells["A2"].Value = horario[0].Item1;
                    sheets.Cells["A3"].Value = horario[0].Item2;
                    sheets.Cells["A4"].Value = horario[0].Item3;
                    sheets.Cells["A5"].Value = horario[0].Item4;
                    sheets.Cells["A6"].Value = horario[0].Item5;
                    sheets.Cells["A7"].Value = horario[0].Item6;
                    sheets.Cells["A8"].Value = horario[0].Item7;

                    sheets.Cells["B1"].Value = "Segunda";
                    sheets.Cells["C1"].Value = "Terça";
                    sheets.Cells["D1"].Value = "Quarta";
                    sheets.Cells["E1"].Value = "Quinta";
                    sheets.Cells["F1"].Value = "Sexta";

                    for (int i = 0; i < 5; i++)
                    {
                        sheets.Cells["B2"].Value = segunda[0].Item1;
                        sheets.Cells["B3"].Value = segunda[0].Item2;
                        sheets.Cells["B4"].Value = segunda[0].Item3;
                        sheets.Cells["B5"].Value = segunda[0].Item4;
                        sheets.Cells["B6"].Value = segunda[0].Item5;
                        sheets.Cells["B7"].Value = segunda[0].Item6;
                        sheets.Cells["B8"].Value = segunda[0].Item7;
                        if (i == 1)
                        {
                            sheets.Cells["C2"].Value = terca[0].Item1;
                            sheets.Cells["C3"].Value = terca[0].Item2;
                            sheets.Cells["C4"].Value = terca[0].Item3;
                            sheets.Cells["C5"].Value = terca[0].Item4;
                            sheets.Cells["C6"].Value = terca[0].Item5;
                            sheets.Cells["C7"].Value = terca[0].Item6;
                            sheets.Cells["C8"].Value = terca[0].Item7;
                        }
                        if (i == 2)
                        {
                            sheets.Cells["D2"].Value = quarta[0].Item1;
                            sheets.Cells["D3"].Value = quarta[0].Item2;
                            sheets.Cells["D4"].Value = quarta[0].Item3;
                            sheets.Cells["D5"].Value = quarta[0].Item4;
                            sheets.Cells["D6"].Value = quarta[0].Item5;
                            sheets.Cells["D7"].Value = quarta[0].Item6;
                            sheets.Cells["D8"].Value = quarta[0].Item7;
                        }
                        if (i == 3)
                        {
                            sheets.Cells["E2"].Value = quinta[0].Item1;
                            sheets.Cells["E3"].Value = quinta[0].Item2;
                            sheets.Cells["E4"].Value = quinta[0].Item3;
                            sheets.Cells["E5"].Value = quinta[0].Item4;
                            sheets.Cells["E6"].Value = quinta[0].Item5;
                            sheets.Cells["E7"].Value = quinta[0].Item6;
                            sheets.Cells["E8"].Value = quinta[0].Item7;

                        }
                        if (i == 4)
                        {
                            sheets.Cells["F2"].Value = sexta[0].Item1;
                            sheets.Cells["F3"].Value = sexta[0].Item2;
                            sheets.Cells["F4"].Value = sexta[0].Item3;
                            sheets.Cells["F5"].Value = sexta[0].Item4;
                            sheets.Cells["F6"].Value = sexta[0].Item5;
                            sheets.Cells["F7"].Value = sexta[0].Item6;
                            sheets.Cells["F8"].Value = sexta[0].Item7;
                        }

                    }
                    package.SaveAs(a);

                    

                }





            }
        }
        public void limpar()
        {
            materias1.Items.Clear();
            materias2.Items.Clear();
            materias3.Items.Clear();
            materias4.Items.Clear();
            materias5.Items.Clear();
            materias6.Items.Clear();
            materias7.Items.Clear();
        }
        private void maskedTextBox6_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                Connection con = new Connection();
                con.conectar();
                string sql = "INSERT INTO Disciplinas VALUES('"+textBox10.Text+"','"+textBox3.Text+"','" + textBox4.Text + "')";
                SQLiteCommand command = new SQLiteCommand(sql, con.sq);
                command.ExecuteNonQuery();
                con.desconectar();
            }
            catch(Exception E)
            {
                MessageBox.Show(E.Message);
            }
        }

        private void domainUpDown1_SelectedItemChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void materias6_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            maskedTextBox1.Clear();
            maskedTextBox2.Clear();
            maskedTextBox3.Clear();
            maskedTextBox4.Clear();
            maskedTextBox5.Clear();
            textBox1.Clear();
            textBox2.Clear();

            segunda.RemoveAt(0);
            terca.RemoveAt(0);
            quarta.RemoveAt(0);
            quinta.RemoveAt(0);
            sexta.RemoveAt(0);
            for (int i = 0; i < horario.Count; i++)
            {
                horario.RemoveAt(i);
            }
            listView1.Items.Clear();
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
           
            try
            {
                limpar();

                Connection con = new Connection();
                con.conectar();
                string sql = "SELECT CodGrade,Disciplinas.NomeDisciplina FROM GradeCurricular " +
                    "Inner join Disciplinas on Disciplinas.CodDisciplina = GradeCurricular.CodDisciplina WHERE GradeCurricular.CodGrade = '" + int.Parse(comboBox1.Text) + "'";
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, con.sq);
                adapter.Fill(dataSet, "Disciplinas");
                DataTable dis = dataSet.Tables["Disciplinas"];
                for(int i = 0; i<dis.Rows.Count;i++)
                {
                    DataRow disciplina = dis.Rows[i];
                    
                    materias1.Items.Add(disciplina.ItemArray[1].ToString());
                    materias2.Items.Add(disciplina.ItemArray[1].ToString());
                    materias3.Items.Add(disciplina.ItemArray[1].ToString());
                    materias4.Items.Add(disciplina.ItemArray[1].ToString());
                    materias5.Items.Add(disciplina.ItemArray[1].ToString());
                    materias6.Items.Add(disciplina.ItemArray[1].ToString());
                    materias7.Items.Add(disciplina.ItemArray[1].ToString());
                }
                dataSet.Tables["Disciplinas"].Reset();

            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                Connection con = new Connection();
                con.conectar();
                string sql = "INSERT INTO GradeCurricular VALUES('"+textBox5.Text+"','"+maskedTextBox8.Text+"','"+textBox6.Text+"')";
                SQLiteCommand command = new SQLiteCommand(sql, con.sq);
                command.ExecuteNonQuery();
                con.desconectar();
            }
            catch(Exception E)
            {
                MessageBox.Show(E.Message);
            }
        }

        private void maskedTextBox8_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
            
        }
    }
}