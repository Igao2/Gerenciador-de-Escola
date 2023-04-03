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
        
        private void Funcionario_Load(object sender, EventArgs e)
        {
            inicializar();
            carregardados();
            carregaLista();

        }

        private void inicializar()
        {
            listView1.View = View.Details;



            listView1.GridLines = true;


        }

        private void button1_Click(object sender, EventArgs e)
        {


            Connection con = new Connection();
            con.conectar();
            try
            {
                string sqlinsert = "INSERT INTO Funcionario(Nome,Cargo,Salario) VALUES('" + name.Text + "','" + carg.Text + "','" + money.Text + "')";
                SQLiteCommand command = new SQLiteCommand(sqlinsert, con.sq);
                command.ExecuteNonQuery();
                ListViewItem list = new ListViewItem(name.Text);
                list.SubItems.Add(carg.Text);
                list.SubItems.Add(money.Text);
                listView1.Items.Add(list);
                name.Text = "";
                carg.Text = "";
                money.Text = "";
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message.ToString(), "Alerta do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void carregardados()
        {
            Connection connectDB = new Connection();
            connectDB.conectar();
            string Sqlite = "SELECT Nome,Cargo,Salario FROM Funcionario";
            string sqlite = "SELECT Nome,CPF,Salario,Disciplinas.NomeDisciplina FROM Professor INNER JOIN Disciplinas on Disciplinas.CodDisciplina = Professor.Disciplina";
            string sqLite = "SELECT * FROM Disciplinas";
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(Sqlite, connectDB.sq);
            SQLiteDataAdapter adapter1 = new SQLiteDataAdapter(sqlite, connectDB.sq);
            SQLiteDataAdapter adapter2 = new SQLiteDataAdapter(sqLite, connectDB.sq);
            
            _DataSet = new DataSet();
            
            adapter.Fill(_DataSet, "Funcionario");
            adapter1.Fill(_DataSet, "Professor");
            adapter2.Fill(_DataSet, "Disciplinas");
        }
        private void carregaLista()
        {

            DataTable Funcionario = _DataSet.Tables["Funcionario"];
            DataTable Professor = _DataSet.Tables["Professor"];
            
            listView1.Items.Clear();
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
            for (int j = 0; j < Funcionario.Rows.Count; j++)
            {
                DataRow colunaFuncionario = Funcionario.Rows[j];

                if (colunaFuncionario.RowState != DataRowState.Deleted)
                {
                    ListViewItem item = new ListViewItem(colunaFuncionario["Nome"].ToString());
                    item.SubItems.Add(colunaFuncionario["Cargo"].ToString());
                    item.SubItems.Add(colunaFuncionario["Salario"].ToString());
                    

                    listView1.Items.Add(item);
                }
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem list = new ListViewItem();
                list = listView1.SelectedItems[0];
                nombre.Text = list.Text;
                textBox2.Text = list.Text;
            }





        }

        private void button2_Click(object sender, EventArgs e)
        {

            ListViewItem list = new ListViewItem();
            list = listView1.SelectedItems[0];
            list.SubItems.RemoveAt(2);
            list.SubItems.Add(salnovo.Text);

            try
            {
                Connection connection = new Connection();
                connection.conectar();
                string sql = "UPDATE Funcionario SET Salario = '" + salnovo.Text + "' WHERE Nome = '" + nombre.Text + "' ";
                SQLiteCommand command = new SQLiteCommand(sql, connection.sq);
                command.ExecuteNonQuery();
                connection.desconectar();
                salnovo.Clear();
                nombre.Clear();


            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message.ToString(), "Alerta do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {


            try
            {
                Connection connection = new Connection();
                connection.conectar();
                string sql = "DELETE FROM Funcionario WHERE Nome =  '" + textBox2.Text + "'";
                SQLiteCommand command = new SQLiteCommand(sql, connection.sq);
                command.ExecuteNonQuery();
                connection.desconectar();
                textBox2.Text = "";
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message.ToString(), "Alerta do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (textBox4.Text != "" && textBox3.Text != "" && textBox8.Text != "" && numericUpDown1.Value != 0)
            {
                try
                {
                    Connection con = new Connection();
                    con.conectar();
                    string sqlite = "INSERT INTO Professor(Nome,CPF,Salario,Disciplina) VALUES('" + textBox4.Text + "','" + textBox8.Text + "','" + textBox3.Text + "','" + numericUpDown1.Value + "')";
                    SQLiteCommand command = new SQLiteCommand(sqlite, con.sq);
                    command.ExecuteNonQuery();
                    ListViewItem item = new ListViewItem(textBox4.Text);
                    item.SubItems.Add(textBox8.Text);
                    item.SubItems.Add(textBox3.Text);
                    item.SubItems.Add(numericUpDown1.Value.ToString());
                    listView2.Items.Add(item);

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

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            DataTable Disciplinas = _DataSet.Tables["Disciplinas"];
            for (int i = 0; i < Disciplinas.Rows.Count; i++)
            {
                DataRow linhaDisciplinas = Disciplinas.Rows[i];
                if (numericUpDown1.Value.ToString() == linhaDisciplinas.ItemArray[1].ToString())
                {

                    textBox1.Text = linhaDisciplinas.ItemArray[0].ToString();
                }
                else
                {
                    textBox1.Text = "Nenhuma disciplina relacionada";
                }
            }
        }
    }
}
