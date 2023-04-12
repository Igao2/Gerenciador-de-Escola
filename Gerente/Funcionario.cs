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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Gerente
{
    public partial class Funcionario : Form
    {
        public Funcionario()
        {
            InitializeComponent();
        }
        private DataSet _DataSet;
        public string discip;
        
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
            
            _DataSet = new DataSet();
            
           
            adapter1.Fill(_DataSet, "Professor");
            adapter2.Fill(_DataSet, "Disciplinas");
            
        }
        private void carregaLista()
        {

            DataTable Funcionario = _DataSet.Tables["Funcionario"];
            DataTable Professor = _DataSet.Tables["Professor"];
            
           
            listView2.Items.Clear();

            for (int i = 0; i < Professor.Rows.Count; i++)
            {
                DataRow colunaProfessor = Professor.Rows[i];


                if (colunaProfessor.RowState != DataRowState.Deleted)
                {

                    ListViewItem lvi = new ListViewItem(colunaProfessor["Nome"].ToString());
                    lvi.SubItems.Add(colunaProfessor["CPF"].ToString());
                    lvi.SubItems.Add(colunaProfessor["Salario"].ToString());
                    lvi.SubItems.Add(colunaProfessor["NomeDisciplina"].ToString());

                    listView2.Items.Add(lvi);
                }
            }
            DataTable Disciplinas = _DataSet.Tables["Disciplinas"];
            for(int i = 0; i< Disciplinas.Rows.Count;i++)
            {
                DataRow Disciplina = Disciplinas.Rows[i];
                comboBox1.Items.Add(Disciplina["NomeDisciplina"]);
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
                    ListViewItem item = new ListViewItem(textBox4.Text);
                    item.SubItems.Add(textBox8.Text);
                    item.SubItems.Add(textBox3.Text);
                    item.SubItems.Add(comboBox1.Text);
                    listView2.Items.Add(item);
                    textBox4.Clear();
                    textBox8.Clear();
                    textBox3.Clear();

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
            DataTable table = _DataSet.Tables["Disciplinas"];
            for(int i = 0; i< table.Rows.Count;i++)
            {
                DataRow codDisciplina = table.Rows[i];
                if (comboBox1.Text == codDisciplina.ItemArray[0].ToString())
                {
                    discip = codDisciplina.ItemArray[1].ToString();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listView2.SelectedItems.Count>0)
            {
                textBox5.Text = listView2.SelectedItems[0].Text;
                textBox7.Text = listView2.SelectedItems[0].Text;

            }
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
                ListViewItem item = listView2.SelectedItems[0];
                listView2.Items.Remove(item);
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
                    listView2.SelectedItems[0].SubItems[2].Text = textBox6.Text;
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
    }
}
