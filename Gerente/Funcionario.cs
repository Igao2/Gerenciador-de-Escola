using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Gerente
{
    public partial class Funcionario : Form
    {
        public Funcionario()
        {
            InitializeComponent();
            button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button5.FlatAppearance.BorderSize = 0;
            button5.FlatAppearance.MouseDownBackColor = Color.Transparent;
            button5.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button5.BackColor = Color.Transparent;
            button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button4.FlatAppearance.BorderSize = 0;
            button4.FlatAppearance.MouseDownBackColor = Color.Transparent;
            button4.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button4.BackColor = Color.Transparent;
            
        }
        private string segunda = "N";
        private string terca = "N";
        private string quarta = "N";
        private string quinta = "N";
        private string sexta = "N";
        private DataTable professor = new DataTable();
        public string discip;
        private DataTable disciplinas = new DataTable();
        
        private void Funcionario_Load(object sender, EventArgs e)
        {
           
            carregardados();
            carregaLista();

        }

       
        

       
        private void carregardados()
        {
            Connection connectDB = new Connection();
            connectDB.conectar();
          
            string sqlite = "SELECT Nome,CPF,Salario,Disciplinas.NomeDisciplina FROM Professor INNER JOIN Disciplinas on Disciplinas.CodDisciplina = Professor.Disciplina";
            string sqLite = "SELECT * FROM Disciplinas";
          
            SQLiteDataAdapter adapter1 = new SQLiteDataAdapter(sqlite, connectDB.sq);
            SQLiteDataAdapter adapter2 = new SQLiteDataAdapter(sqLite, connectDB.sq);
            
            
            
           
            adapter1.Fill(professor);
            adapter2.Fill(disciplinas);
            
        }
        private void carregaLista()
        {

            
            
           
            
            dataGridView1.DataSource = professor;

            
            for(int i = 0; i< disciplinas.Rows.Count;i++)
            {
                DataRow Disciplina = disciplinas.Rows[i];
                comboBox1.Items.Add(Disciplina.ItemArray[0].ToString());
            }
           
        }


      

       

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            if (textBox4.Text != "" && textBox3.Text != "" && textBox8.Text != "")
            {
                try
                {
                    Connection con = new Connection();
                    con.conectar();
                    string sqlite = "INSERT INTO Professor(Nome,CPF,Salario,Disciplina) VALUES('" + textBox4.Text + "','" + textBox8.Text + "','" + textBox3.Text + "','" + discip + "')";
                    SQLiteCommand command = new SQLiteCommand(sqlite, con.sq);
                    command.ExecuteNonQuery();
                    string sql = "SELECT * FROM Professor WHERE CodProfessor = LAST_INSERT_ROWID()";
                    SQLiteDataAdapter ad = new SQLiteDataAdapter(sql, con.sq);
                    DataTable b = new DataTable();
                    ad.Fill(b);
                    foreach(DataRow r in b.Rows)
                    {
                        int cod = int.Parse(r["CodProfessor"].ToString());
                        string SQL = "INSERT INTO Disponibilidade_Professor VALUES('" + cod + "','" + segunda + "','" + terca + "','" + quarta + "','" + quinta + "','" + sexta + "')";
                        SQLiteCommand command1 = new SQLiteCommand(SQL, con.sq);
                        command1.ExecuteNonQuery();
                    }

                    string[] dados =
                    {
                        textBox4.Text,
                        textBox8.Text,
                        textBox3.Text,
                        discip
                    };
                    professor.Rows.Add(dados);
                    textBox3.Clear();
                    textBox4.Clear();
                    textBox8.Clear();
                    con.desconectar();
                }
                catch (Exception E)
                {
                    MessageBox.Show(E.ToString(), "Alerta do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Preencha Todos os campos", "Alerta do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }



        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            for(int i = 0; i< disciplinas.Rows.Count;i++)
            {
                DataRow codDisciplina = disciplinas.Rows[i];
                if (comboBox1.Text == codDisciplina.ItemArray[0].ToString())
                {
                    discip = codDisciplina.ItemArray[1].ToString();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

       

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                Connection con = new Connection();
                con.conectar();
                string sql = "DELETE From Professor Where Nome = '" + textBox5.Text + "'";
                SQLiteCommand command = new SQLiteCommand(sql, con.sq);
                command.ExecuteNonQuery();
                con.desconectar();
                for(int i = 0; i< professor.Rows.Count; i++)
                {
                    DataRow row = professor.Rows[i];
                    if (row.ItemArray[0].ToString()==textBox5.Text)
                    {
                        professor.Rows.RemoveAt(i);
                    }
                }
            }
            catch(Exception E)
            {
                MessageBox.Show(E.Message, "Alerta do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if(textBox6.Text != "")
            {
                try
                {
                    Connection con = new Connection();
                    con.conectar();
                    string sql = "UPDATE Professor SET Salario = '" + textBox6.Text + "'Where Nome = '" + textBox7.Text + "'";
                    SQLiteCommand command = new SQLiteCommand(sql, con.sq);
                    command.ExecuteNonQuery();
                    con.desconectar();
                    for(int i=0; i < professor.Rows.Count; i++)
                    {
                        if(professor.Rows[i].ItemArray[0].ToString()==textBox7.Text)
                        {
                            professor.Rows[i].ItemArray[2] = textBox6.Text;
                            
                        }
                    }
                    textBox6.Clear();

                }
                catch (Exception E)
                {
                    MessageBox.Show(E.Message, "Alerta do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Preencha o campo de reatribuição de salário!", "Alerta do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
           
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                segunda = "S";
            }
            else
            {
                segunda = "N";
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                terca = "S";
            }
            else
            {
                terca = "N";
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                quinta = "S";
            }
            else
            {
                quinta = "N";
            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked)
            {
                sexta = "S";
            }
            else
            {
                sexta = "N";
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox7.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                quarta = "S";
            }
            else
            {
                quarta = "N";
            }
        }
    }
}
