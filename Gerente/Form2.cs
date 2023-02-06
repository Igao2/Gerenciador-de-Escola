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
using Xceed.Words.NET;

namespace Gerente
{
    public partial class Calendario : Form
    {
        public Calendario()
        {
            InitializeComponent();
        }
        private DataSet _DataSet;


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Connection con = new Connection();
                con.conectar();
                string Sql = "INSERT INTO Calendario(Evento,Data) VALUES('" + textBox1.Text + "','" + maskedTextBox1.Text + "')";
                SQLiteCommand command = new SQLiteCommand(Sql, con.sq);
                command.ExecuteNonQuery();
                ListViewItem list = new ListViewItem(textBox1.Text);
                list.SubItems.Add(maskedTextBox1.Text);
                listView1.Items.Add(list);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void Calendario_Load(object sender, EventArgs e)
        {
            inicializar();
            carregardados();
            carregaLista();
            if (listView1.Items.Count > 0)
            {
                for (int i = 0; i < listView1.Items.Count; i++)
                {

                    string a = listView1.Items[i].SubItems[1].Text;
                    string[] d = a.Split('/');
                    int dia = int.Parse(d[0]);
                    int mes = int.Parse(d[1]);
                    int ano = int.Parse(d[2]);
                    DateTime dateTime = new DateTime(ano, mes, dia);
                    monthCalendar2.AddBoldedDate(dateTime);
                }
            }
        }
        private void inicializar()
        {
            listView1.View = View.Details;



            listView1.GridLines = true;


        }
        private void carregaLista()
        {

            DataTable dtable = _DataSet.Tables["Calendario"];


            listView1.Items.Clear();


            for (int i = 0; i < dtable.Rows.Count; i++)
            {
                DataRow drow = dtable.Rows[i];


                if (drow.RowState != DataRowState.Deleted)
                {

                    ListViewItem lvi = new ListViewItem(drow["Evento"].ToString());
                    lvi.SubItems.Add(drow["Data"].ToString());




                    listView1.Items.Add(lvi);
                }
            }
        }
        private void carregardados()
        {
            Connection connectDB = new Connection();
            connectDB.conectar();
            string Sqlite = "SELECT * FROM Calendario";
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(Sqlite, connectDB.sq);
            _DataSet = new DataSet();
            adapter.Fill(_DataSet, "Calendario");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "docx files (*.docx)|*.docx";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string nomearquivo = saveFileDialog1.FileName;
                string texto = "";
                for (int i = 0; i < listView1.Items.Count; i++)
                {
                    texto = texto  + listView1.Items[i].Text + "  " + listView1.Items[i].SubItems[1].Text + "\n";


                }

                var doc = DocX.Create(nomearquivo, Xceed.Document.NET.DocumentTypes.Document);
                doc.InsertParagraph(texto);
                doc.Save();

            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                Properties.Settings.Default.Eventos = Properties.Settings.Default.Eventos  + listView1.Items[i].Text + "  " + listView1.Items[i].SubItems[1].Text + "\n";
                Properties.Settings.Default.Save();
                

            }
            printDocument1.Print();

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString(Properties.Settings.Default.Eventos,new Font("Arial",12), Brushes.Black,new PointF(100,100));
        }
    }
}

