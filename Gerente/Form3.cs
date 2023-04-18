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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        private DataTable table = new DataTable();
        private string b = "";
        private string valor = "";
        private void Form3_Load(object sender, EventArgs e)
        {
            Connection con = new Connection();
            try
            {
                con.conectar();
                string sql = "Select * From Disciplinas";
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, con.sq);
                adapter.Fill(table);
                dataGridView1.DataSource = table;
                con.desconectar();
            }
            catch(Exception E)
            {
                MessageBox.Show(E.Message);
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            
           
            Connection con = new Connection();
            con.conectar();
            string sql = "UPDATE Disciplinas Set NomeDisciplina = '" + dataGridView1.SelectedRows[0].Cells[0] + "',CodDisciplina = '"+dataGridView1.SelectedRows[0].Cells[1].Value+"',cargaHoraria = '"+dataGridView1.SelectedRows[0].Cells[2].Value+"'WHERE NomeDisciplina ='" + b + "'";
            SQLiteCommand command = new SQLiteCommand(sql, con.sq);
            command.ExecuteNonQuery();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            b = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            label1.Text = b;
        }
    }
}
