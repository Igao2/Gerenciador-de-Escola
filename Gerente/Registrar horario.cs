using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Gerente
{
    public partial class Registrar_horario : Form
    {
        public Registrar_horario()
        {
            InitializeComponent();
        }
        private DataTable disponbilidade = new DataTable();
        private DataTable Turmas = new DataTable();
        private DataTable horario = new DataTable();

        private void Registrar_horario_Load(object sender, EventArgs e)
        {
            groupBox1.Hide();
            button4.Hide();
            button5.Hide();
            try
            {
                Connection con = new Connection();
                con.conectar();

                string sql = "Select CodTurma,descTurma from Turma";
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, con.sq);
                adapter.Fill(Turmas);
                foreach(DataRow row in Turmas.Rows)
                {
                    string a = row["descTurma"].ToString();
                    comboBox1.Items.Add(a);
                }
            }
            catch(Exception E)
            {
                MessageBox.Show(E.Message); 
            }
            horario.Columns.Add("Horario");
            horario.Columns.Add("Segunda");
            horario.Columns.Add("Terca");
            horario.Columns.Add("Quarta");
            horario.Columns.Add("Quinta");
            horario.Columns.Add("Sexta");
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime dt1 = DateTime.ParseExact(maskedTextBox1.Text, "HH:mm", CultureInfo.InvariantCulture);
            DateTime dt2 = DateTime.ParseExact(maskedTextBox2.Text, "HH:mm", CultureInfo.InvariantCulture);
            string[] valores =
           {
                "--",
                "--",
                "--",
                "--",
                "--",
                "--",
            };
            int numero = int.Parse(textBox1.Text);
            for (int i = 0; i < numero; i++)
            {
                if(i==0)
                {
                    valores[0] = dt1.ToShortTimeString();
                }
                else
                {
                    if(dt2>dt1)
                    {
                        dt1 = dt1.AddMinutes(50);
                        valores[0] = dt1.ToShortTimeString();
                    }
                    
                }
                horario.Rows.Add(valores);
            }


            int countMateria = 0;

            preencher(numero);
            
            

           
            dataGridView1.DataSource = horario;
            button4.Show();
          
            Ver(true);
        }
        private void preencher(int numero)
        {
            for (int i = 0; i < disponbilidade.Rows.Count; i++)
            {

                DataRow dataRow = disponbilidade.Rows[i];
                bool seguidoSegunda = false;
                bool seguidoTerca = false;
                bool seguidoQuarta = false;
                bool seguidoQuinta = false;
                bool seguidoSexta = false;
                int countSegunda = 0;
                int countTerca = 0;
                int countQuarta = 0;
                int countQuinta = 0;
                int countSexta = 0;
                int count = 0;
                for (int j = 0; j < horario.Rows.Count; j++)
                {
                    count = 0;
                    foreach (DataRow nome in horario.Rows)
                    {
                        if (nome["Segunda"].ToString() == dataRow.ItemArray[1].ToString())
                        {
                            countSegunda++;
                            count++;
                        }
                        if (nome["Terca"].ToString() == dataRow.ItemArray[1].ToString())
                        {
                            countTerca++;
                            count++;
                        }
                        if (nome["Quarta"].ToString() == dataRow.ItemArray[1].ToString())
                        {
                            countQuarta++;
                            count++;
                        }
                        if (nome["Quinta"].ToString() == dataRow.ItemArray[1].ToString())
                        {
                            countQuinta++;
                            count++;
                        }
                        if (nome["Sexta"].ToString() == dataRow.ItemArray[1].ToString())
                        {
                            countSexta++;
                            count++;
                        }
                    }
                    DataRow row = horario.Rows[j];
                    int a = int.Parse(dataRow["CH"].ToString());
                   
                    int contas = a / 4;
                    double conta = Math.Round((double)contas);
                    bool verificar()
                    {
                        bool verdade = false;
                        if(conta>count)
                        {
                            verdade = true;
                        }
                        else
                        {
                            verdade = false;
                        }
                        return verdade;
                    }
                    if  (verificar())
                    {
                        if (countSegunda < 2 && verificar())
                        {
                            if (row["Segunda"].ToString() == "--" && dataRow["Segunda"].ToString() == "S" && !seguidoSegunda)
                            {


                                row["Segunda"] = dataRow["Disciplina"];
                                seguidoSegunda = true;
                                countSegunda++;
                                count++;


                            }
                            else
                            {
                                seguidoSegunda = false;
                               
                            }

                        }
                        if (countTerca < 3 && verificar())
                        {
                            if (row["Terca"].ToString() == "--" && dataRow["Terca"].ToString() == "S" && !seguidoTerca)
                            {

                                if (row["Segunda"].ToString() == dataRow["Disciplina"].ToString())
                                {
                                 }
                                else
                                {
                                    row["Terca"] = dataRow["Disciplina"];
                                    seguidoTerca = true;
                                    countTerca++;
                                    count++;
                                   
                                }

                            }
                            else
                            {
                                seguidoTerca = false;
                                
                            }
                            
                        }
                        if (countQuarta < 2 && verificar() )
                        {
                            if (row["Quarta"].ToString() == "--" && dataRow["Quarta"].ToString() == "S" && !seguidoQuarta)
                            {

                                if (row["Terca"].ToString() == dataRow["Disciplina"].ToString())
                                {
                                    int c = 3;
                                }
                                else
                                {
                                    row["Quarta"] = dataRow["Disciplina"];
                                    seguidoQuarta = true;
                                    countQuarta++;
                                    count++;
                                }

                            }
                            else
                            {
                                seguidoQuarta = false;
                                
                            }

                        }
                        if (countQuinta < 3 && verificar())
                        {
                            if (row["Quinta"].ToString() == "--" && dataRow["Quinta"].ToString() == "S" && !seguidoQuinta)
                            {

                                if (row["Quarta"].ToString() == dataRow["Disciplina"].ToString())
                                {
                                    
                                }
                                else
                                {
                                    row["Quinta"] = dataRow["Disciplina"];
                                    seguidoQuinta = true;
                                    countQuinta++;
                                    count++;
                                   
                                }

                            }
                            else
                            {
                                seguidoQuinta = false;
                               
                            }

                        }
                        if (countSexta < 2 && verificar())

                        {
                            if (row["Sexta"].ToString() == "--" && dataRow["Sexta"].ToString() == "S" && !seguidoSexta)
                            {

                                if (row["Quinta"].ToString() == dataRow["Disciplina"].ToString())
                                {
                                    

                                }
                                else
                                {
                                    row["Sexta"] = dataRow["Disciplina"];
                                    seguidoSexta = true;
                                    countSexta++;
                                    count++;
                                    
                                }

                            }
                            else
                            {
                                seguidoSexta = false;
                            }
                            
                        }


                        countSegunda = 0;
                        countTerca = 0;
                        countQuarta = 0;
                        countQuinta = 0;
                        countSexta = 0;
                    }

                }
            }
        }
        private void Ver(bool inicio)
        {
            dataGridView3.Rows.Clear();
            List<Tuple<string, int>> contador = new List<Tuple<string, int>>();
            string texto = "Faltam a quantidade relatada de aulas das seguintes matérias:";
            for(int i = 0; i< disponbilidade.Rows.Count; i++)
            {
                DataRow disp = disponbilidade.Rows[i];
                int a = int.Parse(disp["CH"].ToString());

                int contas = a / 4;
                double conta = Math.Round((double)contas);
                int count = 0;
                foreach(DataRow dr in horario.Rows)
                {
                    if (dr["Segunda"].ToString() == disp["Disciplina"].ToString())
                    {
                        count++;
                    }
                    if (dr["Terca"].ToString() == disp["Disciplina"].ToString())
                    {
                        count++;
                    }
                    if (dr["Quarta"].ToString() == disp["Disciplina"].ToString())
                    {
                        count++;
                    }
                    if (dr["Quinta"].ToString() == disp["Disciplina"].ToString())
                    {
                        count++;
                    }
                    if (dr["Sexta"].ToString() == disp["Disciplina"].ToString())
                    {
                        count++;
                    }
                    
                }
                if ((int)conta > count)
                {
                    int x = (int)conta - count;
                    contador.Add(Tuple.Create(disp["Disciplina"].ToString(), x));

                }
            }
            for(int j = 0; j < contador.Count; j ++)
            {
                string[] valores =
                {
                    contador[j].Item1,
                    contador[j].Item2.ToString()
                };
                dataGridView3.Rows.Add(valores);
                texto = texto +"\n Matéria: "+ contador[j].Item1+ " Quantidade: " + contador[j].Item2;
            }
            if (inicio)
            {
                MessageBox.Show(texto, "Alerta do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            groupBox1.Show();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Connection con = new Connection();
                con.conectar();

                string sql = "Select Professor.Nome,Disciplinas.NomeDisciplina as Disciplina,Disciplinas.cargaHoraria as CH,Disponibilidade_Professor.Segunda,\r\nDisponibilidade_Professor.Terca,Disponibilidade_Professor.Quarta,Disponibilidade_Professor.Quinta,Disponibilidade_Professor.Sexta \r\nfrom Turma \r\ninner join GradeCurricular on GradeCurricular.CodMatriz = Turma.CodMatriz\r\ninner join Disciplinas on Disciplinas.CodDisciplina = GradeCurricular.CodDisciplina\r\ninner join Professor on Professor.Disciplina = Disciplinas.CodDisciplina\r\ninner join Disponibilidade_Professor on Disponibilidade_Professor.CodProfessor = Professor.CodProfessor\r\nWhere descTurma = '"+comboBox1.Text+"' Order By Disciplinas.NomeDisciplina";
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, con.sq);
                adapter.Fill(disponbilidade);
                dataGridView2.DataSource = disponbilidade;
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string codturma = "";
            foreach(DataRow row in Turmas.Rows)
            {
                
                if(comboBox1.Text == row.ItemArray[1].ToString())
                {
                    codturma = row.ItemArray[0].ToString();
                }
            }
            
            
            
            Connection con = new Connection();

            
            try
            {
                con.conectar();
                string Sql = "SELECT NomeDisciplina,CodDisciplina from Disciplinas";
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(Sql,con.sq);
                DataTable disciplinas = new DataTable();
                List<Tuple<string,string, string, string, string, string>> codDisciplinas = new List<Tuple< string,string, string, string, string, string>>();
                adapter.Fill(disciplinas);
                for (int i = 0; i < horario.Rows.Count; i++)
                {
                    DataRow hor = horario.Rows[i];
                    string[] cdis = new string[6];
                    cdis[0] = hor.ItemArray[0].ToString();
                    
                    for(int j = 0; j < disciplinas.Rows.Count;j++)
                    {
                        DataRow disciplina = disciplinas.Rows[j];
                      
                        if (hor["Segunda"].ToString() == disciplina.ItemArray[0].ToString())
                        {
                            cdis[1] = disciplina.ItemArray[1].ToString();
                        }
                        if (hor["Terca"].ToString() == disciplina.ItemArray[0].ToString())
                        {
                            cdis[2] = disciplina.ItemArray[1].ToString();
                        }
                        if (hor["Quarta"].ToString() == disciplina.ItemArray[0].ToString())
                        {
                            cdis[3] = disciplina.ItemArray[1].ToString();
                        }
                        if (hor["Quinta"].ToString() == disciplina.ItemArray[0].ToString())
                        {
                            cdis[4] = disciplina.ItemArray[1].ToString();
                        }
                        if (hor["Sexta"].ToString() == disciplina.ItemArray[0].ToString())
                        {
                            cdis[5] = disciplina.ItemArray[1].ToString();
                        }
                        
                    }
                    codDisciplinas.Add(Tuple.Create(cdis[0], cdis[1], cdis[2], cdis[3], cdis[4], cdis[5]));

                }
               for(int i = 0; i<codDisciplinas.Count; i++)
                {
                    try
                    {
                        string sql = "INSERT INTO Horario VALUES('"+codturma+"','"+ codDisciplinas[i].Item1 + "','" + codDisciplinas[i].Item2 + "','" + codDisciplinas[i].Item3 + "','" + codDisciplinas[i].Item4 + "','" + codDisciplinas[i].Item5 + "','" + codDisciplinas[i].Item6 + "')";
                        SQLiteCommand command = new SQLiteCommand(sql,con.sq);
                        command.ExecuteNonQuery();

                    }

                    catch(Exception err)
                    {
                        MessageBox.Show(err.Message, "Alerta do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    
                }
               

                
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message, "Alerta do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "xlsx|*.xlsx|xls|*.xls";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string a = saveFileDialog1.FileName;
                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.Commercial;
                var package = new ExcelPackage(a);
                var workbook = package.Workbook;
                workbook.Worksheets.Add("Horário "+comboBox1.Text);
                var sheets = workbook.Worksheets[0];
                for (int i = 0; i < horario.Columns.Count; i++)
                {
                    sheets.Cells[1, i + 1].Value = horario.Columns[i].ColumnName;
                }


                for (int row = 0; row < horario.Rows.Count; row++)
                {
                    for (int col = 0; col < horario.Columns.Count; col++)
                    {
                        sheets.Cells[row + 2, col + 1].Value = horario.Rows[row][col];
                    }
                }

                package.SaveAs(a);

            }
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.ReadOnly = false;
            button5.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dataGridView1.ReadOnly = true;
            button5.Hide();
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                
                var newValue = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

                horario.Rows[e.RowIndex][e.ColumnIndex] = newValue;
            }
            
            Ver(false);
        }
    }
}
