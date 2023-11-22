using OfficeOpenXml.FormulaParsing.Excel.Functions.Engineering;
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
        private int mon = 0;
        private int vl = 0;
        private DataTable contas = new DataTable();
        private DataTable montante = new DataTable();
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Connection con = new Connection();
            string sql = "UPDATE Caixa SET Montante ='" + textBox3.Text + "'";
            try
            {
                con.conectar();
                SQLiteCommand com = new SQLiteCommand(sql, con.sq);
                com.ExecuteNonQuery();
                label3.Text = "R$ " + textBox3.Text;
                con.desconectar();
            }
            catch(Exception E)
            {
                MessageBox.Show(E.ToString(), "Alerta do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            textBox3.Clear();

        }

        private void Contas_Load(object sender, EventArgs e)
        {
            label3.Text = "R$ ";
            label7.Text = "R$ ";

            try
            {
                Connection con = new Connection();
                con.conectar();
                string sql = "SELECT * FROM Contas";
                string Sql = "Select Montante from Caixa";
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, con.sq);
                SQLiteDataAdapter ad = new SQLiteDataAdapter(Sql, con.sq);
                adapter.Fill(contas);
                ad.Fill(montante);
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
                    valor+=int.Parse(row["Valor"].ToString());
                    vl = valor;

                    label7.Text = "R$ " + valor;
                }
                if (row["Status"].ToString()=="Pago")
                {
                    label7.Text = "R$ " + valor;
                }
                    
            DateTime dt = new DateTime();
                dt = DateTime.Now.Date;
               
                if (row["Vencimento"].ToString() == dt.ToShortDateString() && row["Status"].ToString() =="Pendente")
                {

                    richTextBox1.Text = richTextBox1.Text + "\n" + row["Conta"].ToString();
                }
                
                
            }
            int mont = 0;
            foreach(DataRow dr in montante.Rows)
            {
                mont = int.Parse(dr["Montante"].ToString());
                label3.Text = "R$ " + mont;
                mon = mont;
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
                if(status == "Pendente")
                {
                    vl += int.Parse(textBox2.Text);
                    label7.Text = "R$ " + vl;
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
            int montant = 0;
            foreach(DataRow row in montante.Rows)
            {
                montant = int.Parse(row["Montante"].ToString());
            }
            foreach(DataRow r in contas.Rows)
            {
                if (r["Conta"].ToString()==textBox4.Text)
                {
                    if (r["Status"] == "Pendente")
                    {
                        r["Status"] = "Pago";
                        try
                        {
                            Connection con = new Connection();
                            con.conectar();
                            string sql = "UPDATE Contas SET Status = 'Pago' WHERE Conta = '" + textBox4.Text + "'";
                            SQLiteCommand command = new SQLiteCommand(sql, con.sq);

                            command.ExecuteNonQuery();
                            con.desconectar();
                            vl = vl - int.Parse(r["Valor"].ToString());
                            mon = mon - int.Parse(r["Valor"].ToString());
                            label3.Text = "R$ " + mon;
                            label7.Text = "R$ " + vl;

                        }
                        catch (Exception Err)
                        {
                            MessageBox.Show(Err.Message, "Alerta do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("A conta já está registrada como paga!!", "Alerta do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    
                    

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
