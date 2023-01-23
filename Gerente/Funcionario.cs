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
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(Sqlite, connectDB.sq);
            _DataSet = new DataSet();
            adapter.Fill(_DataSet, "Funcionario");
        }
        private void carregaLista()
        {

            DataTable dtable = _DataSet.Tables["Funcionario"];


            listView1.Items.Clear();


            for (int i = 0; i < dtable.Rows.Count; i++)
            {
                DataRow drow = dtable.Rows[i];


                if (drow.RowState != DataRowState.Deleted)
                {

                    ListViewItem lvi = new ListViewItem(drow["Nome"].ToString());
                    lvi.SubItems.Add(drow["Cargo"].ToString());
                    lvi.SubItems.Add(drow["Salario"].ToString());


                    listView1.Items.Add(lvi);
                }
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if(listView1.SelectedItems.Count>0)
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
                string sql = "UPDATE Funcionario SET Salario = '" + salnovo.Text+ "' WHERE Nome = '" + nombre.Text + "' ";
                SQLiteCommand command = new SQLiteCommand(sql, connection.sq);
                command.ExecuteNonQuery();
                connection.desconectar();
                salnovo.Clear();
                nombre.Clear();
       
                
            }
            catch(Exception E)
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
    }
}
