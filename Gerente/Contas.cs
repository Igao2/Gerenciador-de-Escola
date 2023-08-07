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
    public partial class Contas : Form
    {
        public Contas()
        {
            InitializeComponent();
        }
        private DataTable contas = new DataTable();
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {


            label3.Text = "R$ " + textBox3.Text;
            Properties.Settings.Default.Caixa = float.Parse(textBox3.Text);
            Properties.Settings.Default.Save();
            textBox3.Clear();

        }

        private void Contas_Load(object sender, EventArgs e)
        {
            label3.Text = "R$ "+Properties.Settings.Default.Caixa.ToString();
            label7.Text = "R$ " + Properties.Settings.Default.Pagpen.ToString();

            try
            {
                Connection con = new Connection();
                con.conectar();
                string sql = "SELECT * FROM Contas";
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, con.sq);
                adapter.Fill(contas);
                dataGridView1.DataSource = contas;
                con.desconectar();

            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message,"Alerta do Sistema",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            int valor = 0;
            foreach(DataRow row in contas.Rows)
            {
                if (row["Status"].ToString()=="Pendente")
                {
                    valor = valor + int.Parse(row["Valor"].ToString());
                    Properties.Settings.Default.Pagpen = valor;
                    Properties.Settings.Default.Save();
                    label3.Text = "R$ " + Properties.Settings.Default.Caixa.ToString();
                }
                    
            DateTime dt = new DateTime();
                dt = DateTime.Now.Date;
               
                if (row["Vencimento"].ToString() == dt.ToShortDateString() && row["Status"].ToString() =="Pendente")
                {

                    richTextBox1.Text = richTextBox1.Text + "\n" + row["Conta"].ToString();
                }
                
                
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            string status = "";
            if(pago.Checked)
            {
                status = "Pago";
            }
            if(pendente.Checked)
            {
                status = "Pendente";
            }


            try
            {
                Connection con = new Connection();
                con.conectar();
                string sqInsert = "INSERT INTO Contas(Conta,Valor,Vencimento,Status) VALUES('" + textBox1.Text + "','" + textBox2.Text + "','" + dateTimePicker1.Text + "','" + status + "')";
                SQLiteCommand command = new SQLiteCommand(sqInsert,con.sq);
                command.ExecuteNonQuery();
                string[] valores =
                {
                    textBox1.Text,textBox2.Text,
                    dateTimePicker1.Text,status
                };
                contas.Rows.Add(valores);
                if (status == "Pendente")
                {
                    Properties.Settings.Default.Pagpen = Properties.Settings.Default.Pagpen + float.Parse(textBox2.Text);
                    Properties.Settings.Default.Save();
                    label7.Text = "R$ "+Properties.Settings.Default.Pagpen.ToString();
                }

                pago.Checked = false;
                pendente.Checked = false;
                textBox1.Clear();
                textBox2.Clear();
                
            }
            catch(Exception E)
            {
                MessageBox.Show(E.Message.ToString());
            }

        }

        private void pago_CheckedChanged(object sender, EventArgs e)
        {
            if(pendente.Checked)
            {
                pendente.Checked = false;
            }
        }

        private void pendente_CheckedChanged(object sender, EventArgs e)
        {
            if(pago.Checked)
            {
                pago.Checked = false;
            }
        }
       
        private void button3_Click(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count > 0)
            {
                int b = dataGridView1.SelectedRows[0].Index;
                string celula = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                for(int i = 0; i<contas.Rows.Count; i++ )
                {
                    if(i==b)
                    {
                        DataRow row = contas.Rows[i];
                        row["Status"] = "Pago";
                        float a = Properties.Settings.Default.Pagpen;
                        float c = float.Parse(row["Valor"].ToString());
                        float conta = c - a;
                        Properties.Settings.Default.Pagpen = conta;
                        Properties.Settings.Default.Save();
                        label7.Text = "R$ " + Properties.Settings.Default.Pagpen.ToString();
                    }
                }
                try
                {
                    Connection con = new Connection();
                    con.conectar();
                    string sql = "UPDATE Contas SET Status = 'Pago' WHERE Conta = '" + celula + "'";
                    SQLiteCommand command = new SQLiteCommand(sql, con.sq);
                    command.ExecuteNonQuery();
                    con.desconectar();
                    

                }
                catch(Exception err)
                {
                    MessageBox.Show(err.Message, "Alerta do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            if (dataGridView1.SelectedCells.Count > 0)
            {
                int b = dataGridView1.SelectedCells[0].RowIndex;
                string celula = dataGridView1.SelectedCells[0].Value.ToString();
                for (int i = 0; i < contas.Rows.Count; i++)
                {
                    if (i == b)
                    {
                        DataRow row = contas.Rows[i];
                        row["Status"] = "Pago";
                        float a = Properties.Settings.Default.Pagpen;
                        float c = float.Parse(row["Valor"].ToString());
                        float conta = c - a;
                        Properties.Settings.Default.Pagpen = conta;
                        Properties.Settings.Default.Save();
                        label7.Text = "R$ " + Properties.Settings.Default.Pagpen.ToString();

                    }
                }
                try
                {
                    Connection con = new Connection();
                    con.conectar();
                    string sql = "UPDATE Contas SET Status = 'Pago' WHERE Conta = '" + celula + "'";
                    SQLiteCommand command = new SQLiteCommand(sql, con.sq);
                    command.ExecuteNonQuery();
                    con.desconectar();

                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message, "Alerta do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

       

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows.Count>0)
            {
                textBox4.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            }
            if(dataGridView1.SelectedCells.Count>0)
            {
                int b = dataGridView1.SelectedCells[0].RowIndex;
                for(int i = 0; i<dataGridView1.Rows.Count; i++)
                {
                    if(i==b)
                    {
                        textBox4.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
                    }
                }
            }
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
