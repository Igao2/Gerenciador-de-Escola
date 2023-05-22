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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Gerente
{
    public partial class Aluno : Form
    {
        public Aluno()
        {
            InitializeComponent();
        }
        private DataSet dataSet = new DataSet();
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
                
                string sql = "SELECT nomeAluno,CPF,nomeResponsavel,telefoneCasa,Turma.descTurma as Turma From Aluno " +
                    "Inner join Turma on Turma.CodTurma = Aluno.CodTurma ";
                string Sql = "Select * From Turma";
                string SqL = "Select * From Matriz";
                SQLiteDataAdapter sQLiteData = new SQLiteDataAdapter(sql, con.sq);
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(Sql, con.sq);
                SQLiteDataAdapter sQ = new SQLiteDataAdapter(SqL,con.sq);
                sQLiteData.Fill(dataSet, "Alunos");
                adapter.Fill(dataSet, "Turmas");
                sQ.Fill(dataSet, "Matriz");
              
                con.desconectar();
            }
             catch(Exception E )
            {
                MessageBox.Show(E.Message,"Alerta do Sistema",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        private void inicializar()
        {
            DataTable aluno = dataSet.Tables["Alunos"];
            for(int i = 0; i<aluno.Rows.Count;i++)
            {
                DataRow linhaAluno = aluno.Rows[i];
                if (linhaAluno.RowState != DataRowState.Deleted)
                {

                    ListViewItem lvi = new ListViewItem(linhaAluno["nomeAluno"].ToString());
                    lvi.SubItems.Add(linhaAluno["CPF"].ToString());
                    lvi.SubItems.Add(linhaAluno["nomeResponsavel"].ToString());
                    lvi.SubItems.Add(linhaAluno["telefoneCasa"].ToString());
                    lvi.SubItems.Add(linhaAluno["Turma"].ToString());

                    listView1.Items.Add(lvi);
                }
            }
            DataTable turmas = dataSet.Tables["Turmas"];
        
            for(int i = 0; i<turmas.Rows.Count;i++)
            {
                DataRow linhaTurmas = turmas.Rows[i];
                comboBox1.Items.Add(linhaTurmas.ItemArray[2].ToString());
                comboBox2.Items.Add(linhaTurmas.ItemArray[2].ToString());
                ListViewItem item = new ListViewItem(linhaTurmas.ItemArray[0].ToString());
                item.SubItems.Add(linhaTurmas.ItemArray[1].ToString());
                item.SubItems.Add(linhaTurmas.ItemArray[2].ToString());
                listView2.Items.Add(item);
            }
            DataTable matriz = dataSet.Tables["Matriz"];
            for(int i = 0; i<matriz.Rows.Count;i++)
            {
                DataRow linhaMatriz = matriz.Rows[i];
                comboBox4.Items.Add(linhaMatriz.ItemArray[0].ToString());
               
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
                DataTable alunos = dataSet.Tables["Alunos"];
                DataRow[] resultados = alunos.Select("Turma ='" + comboBox1.Text + "'");
                listView1.Items.Clear();
                for(int i = 0; i<resultados.Length;i++)
                {
                    ListViewItem item = new ListViewItem(resultados[i][0].ToString());
                    item.SubItems.Add(resultados[i][1].ToString());
                    item.SubItems.Add(resultados[i][2].ToString());
                    item.SubItems.Add(resultados[i][3].ToString());
                    listView1.Items.Add(item);
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
                    DataTable turmas = dataSet.Tables["Turmas"];
                    int codTurma = 0;
                    for(int i = 0; i<turmas.Rows.Count;i++)
                    {
                        DataRow linhaturma = turmas.Rows[i];
                        if (linhaturma.ItemArray[1].ToString() == comboBox2.SelectedItem.ToString()) ;
                        {
                            codTurma = int.Parse(linhaturma.ItemArray[0].ToString());
                        }
                    }
                    string SQL = "INSERT INTO Aluno VALUES('"+textBox1.Text+"','"+maskedTextBox2.Text+"','"+textBox3.Text+"','"+maskedTextBox1.Text+"','"+codTurma+"')";
                    SQLiteCommand command = new SQLiteCommand(SQL, con.sq);
                    command.ExecuteNonQuery();
                    con.desconectar();

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
    }
}
