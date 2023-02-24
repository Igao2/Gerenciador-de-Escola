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
        private DataSet _DataSet;
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
            inicializar();
            carregardados();
            carregaLista();
            int valor = 0;
            for(int i = 0;i<listView1.Items.Count;i++)
            {
                if(listView1.Items[i].SubItems[3].Text == "Pendente")
                {
                    valor = valor + int.Parse(listView1.Items[i].SubItems[1].Text);
                    Properties.Settings.Default.Pagpen = valor;
                    Properties.Settings.Default.Save();
                    label3.Text = "R$ " + Properties.Settings.Default.Caixa.ToString();
                }
                    
            DateTime dt = new DateTime();
                dt = DateTime.Now.Date;
               
                if (listView1.Items[i].SubItems[2].Text == dt.ToShortDateString() && listView1.Items[i].SubItems[3].Text=="Pendente")
                {
                    
                        richTextBox1.Text = richTextBox1.Text + "\n" + listView1.Items[i].Text;
                }
                string[] sla = listView1.Items[i].SubItems[2].Text.Split('/');
                int dia = int.Parse(sla[0]);
                int mes = int.Parse(sla[1]);
                int ano = int.Parse(sla[2]);
                DateTime dateTime = new DateTime(ano, mes, dia);
                if (dt>=dateTime && listView1.Items[i].SubItems[3].Text == "Pago")
                {
                    try
                    {
                        string pago = "Pago";
                        Connection con  = new Connection();
                        con.conectar();
                        string sql = "DELETE FROM Contas WHERE Vencimento = '" + listView1.Items[i].SubItems[2].Text +"'AND Status = '"+pago+"'";
                        SQLiteCommand command = new SQLiteCommand(sql, con.sq);
                        command.ExecuteNonQuery();
                        listView1.Items.RemoveAt(i);
                    }
                    catch(Exception E)
                    {
                        MessageBox.Show(E.Message);
                    }
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
                ListViewItem list = new ListViewItem(textBox1.Text);
                list.SubItems.Add(textBox2.Text);
                list.SubItems.Add(dateTimePicker1.Text);
                list.SubItems.Add(status);
                listView1.Items.Add(list);
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
        private void inicializar()
        {
            listView1.View = View.Details;



            listView1.GridLines = true;


        }
        private void carregaLista()
        {

            DataTable dtable = _DataSet.Tables["Contas"];


            listView1.Items.Clear();


            for (int i = 0; i < dtable.Rows.Count; i++)
            {
                DataRow drow = dtable.Rows[i];


                if (drow.RowState != DataRowState.Deleted)
                {

                    ListViewItem lvi = new ListViewItem(drow["Conta"].ToString());
                    lvi.SubItems.Add(drow["Valor"].ToString());
                    lvi.SubItems.Add(drow["Vencimento"].ToString());
                    lvi.SubItems.Add(drow["Status"].ToString());



                    listView1.Items.Add(lvi);
                }
            }
        }
        private void carregardados()
        {
            Connection connectDB = new Connection();
            connectDB.conectar();
            string Sqlite = "SELECT * FROM Contas";
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(Sqlite, connectDB.sq);
            _DataSet = new DataSet();
            adapter.Fill(_DataSet, "Contas");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ListViewItem list = new ListViewItem();
            list = listView1.SelectedItems[0];
            list.SubItems.RemoveAt(2);
            list.SubItems.Add("Pago");
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listView1.SelectedItems.Count>0)
            {
                ListViewItem list = new ListViewItem();
                list = listView1.SelectedItems[0];
                if (list.SubItems[3].Text == "Pendente")
                {
                    textBox4.Text = list.Text;
                }
            }
        }
    }
}
