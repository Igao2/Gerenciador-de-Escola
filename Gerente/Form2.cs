using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xceed.Words.NET;
using Markdig;
using Xceed.Document.NET;

namespace Gerente
{
    public partial class Calendario : Form
    {
        public Calendario()
        {
            InitializeComponent();
        }

        private DataTable calendario = new DataTable();

        private void button1_Click(object sender, EventArgs e)
        {

            if (textBox1.Text != "" && maskedTextBox1.Text != "" && maskedTextBox2.Text != "")
            {
                DateTime dt1 = DateTime.ParseExact(maskedTextBox1.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime dt3 = DateTime.ParseExact(maskedTextBox2.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime dt2 = DateTime.Today;
                if (dt1 < dt2 || dt3 < dt2)
                {
                    MessageBox.Show("Data Inválida!", "Alerta do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    try
                    {
                        if (Mensal.Checked)
                        {
                            int mesatual = dt1.Month;
                            int z = 1;
                            for (int i = mesatual; i < 13; i++)
                            {
                                if (i > mesatual)
                                {
                                    dt1 = dt1.AddMonths(z);
                                    dt3 = dt3.AddMonths(z);
                                }

                                Connection conn = new Connection();
                                conn.conectar();
                                string Sqlç = "INSERT INTO Calendario VALUES('" + textBox1.Text + "','" + dt1.ToShortDateString() + "','" + dt3.ToShortDateString() + "')";
                                SQLiteCommand commandd = new SQLiteCommand(Sqlç, conn.sq);
                                commandd.ExecuteNonQuery();

                                string[] valoress =
                                {
                                textBox1.Text,
                                dt1.ToShortDateString(),
                                dt3.ToShortDateString()
                            };
                                calendario.Rows.Add(valoress);
                                monthCalendar2.AddBoldedDate(dt1);
                                monthCalendar2.AddBoldedDate(dt3);


                            }


                            monthCalendar2.UpdateBoldedDates();
                            maskedTextBox1.Clear();
                            textBox1.Clear();
                            maskedTextBox2.Clear();


                        }
                        if (Anual.Checked)
                        {
                            int Anoatual = dt1.Year;
                            int ano = DateTime.Now.Year;
                            int anoMaximo = ano + 3;
                            int z = 1;
                            for (int i = Anoatual; i < anoMaximo; i++)
                            {
                                if (i > Anoatual)
                                {
                                    dt1 = dt1.AddYears(z);
                                    dt3 = dt3.AddYears(z);
                                }

                                Connection conn = new Connection();
                                conn.conectar();
                                string Sqlç = "INSERT INTO Calendario VALUES('" + textBox1.Text + "','" + dt1.ToShortDateString() + "','" + dt3.ToShortDateString() + "')";
                                SQLiteCommand commandd = new SQLiteCommand(Sqlç, conn.sq);
                                commandd.ExecuteNonQuery();

                                string[] valoress =
                                {
                                textBox1.Text,
                                dt1.ToShortDateString(),
                                dt3.ToShortDateString()
                            };
                                calendario.Rows.Add(valoress);
                                monthCalendar2.AddBoldedDate(dt1);
                                monthCalendar2.AddBoldedDate(dt3);


                            }


                            monthCalendar2.UpdateBoldedDates();
                            maskedTextBox1.Clear();
                            textBox1.Clear();
                            maskedTextBox2.Clear();

                        }
                        if (!Mensal.Checked && !Anual.Checked)
                        {
                            Connection con = new Connection();
                            con.conectar();
                            string Sql = "INSERT INTO Calendario VALUES('" + textBox1.Text + "','" + maskedTextBox1.Text + "','" + maskedTextBox2.Text + "')";
                            SQLiteCommand command = new SQLiteCommand(Sql, con.sq);
                            command.ExecuteNonQuery();
                            string[] valores =
                            {
                        textBox1.Text,
                        dt1.ToShortDateString(),
                        dt2.ToShortDateString()
                    };
                            calendario.Rows.Add(valores);
                            monthCalendar2.AddBoldedDate(dt1);
                            monthCalendar2.AddBoldedDate(dt3);
                            monthCalendar2.UpdateBoldedDates();
                            maskedTextBox1.Clear();
                            textBox1.Clear();
                            maskedTextBox2.Clear();
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString(), "Alerta do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Preencha todos os campos", "Alerta do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            Anual.Checked = false;
            Mensal.Checked = false;

        }

        private void Calendario_Load(object sender, EventArgs e)
        {
            try
            {
                Connection con = new Connection();
                con.conectar();
                string sql = "SELECT * from Calendario order by julianday(substr(DataInicio, 7, 4) || '-' || substr(DataInicio, 4, 2) || '-' || substr(DataInicio, 1, 2)) ASC";
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, con.sq);
                adapter.Fill(calendario);
                con.desconectar();
                dataGridView1.DataSource = calendario;
                foreach (DataRow row in calendario.Rows)
                {
                    DateTime dt1 = DateTime.ParseExact(row["DataInicio"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    DateTime dt2 = DateTime.ParseExact(row["DataTermino"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    monthCalendar2.AddBoldedDate(dt2);
                    monthCalendar2.AddBoldedDate(dt1);
                    monthCalendar2.UpdateBoldedDates();
                }
            }
            catch (Exception Err)
            {
                MessageBox.Show(Err.Message, "Alerta do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private string negritar(string b)
        {
            string c = Markdown.ToHtml("**" + b + "**");
            return c;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "docx files (*.docx)|*.docx";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string nomearquivo = saveFileDialog1.FileName;
                string texto = "\n" + "Calendário " + DateTime.Now.Year.ToString() + "\n";
                string data = "Data de início: ";
                string data2 = "Data de término: ";
                var doc = DocX.Create(nomearquivo, Xceed.Document.NET.DocumentTypes.Document);
                doc.InsertParagraph(texto).FontSize(25).Font("Arial");
                foreach (DataRow row in calendario.Rows)
                {
                    texto = "";
                    texto = texto + " \n" + row["Evento"] + "\n";
                    Paragraph paragraph = doc.InsertParagraph(texto).FontSize(14).Font("Arial");
                    paragraph.Append(data).Bold().FontSize(12).Font("Arial").Append(row["DataInicio"].ToString()).FontSize(14).Append("\n");
                    paragraph.Append(data2).Bold().FontSize(12).Font("Arial").Append(row["DataTermino"].ToString()).FontSize(14).Append("\n");
                }



                doc.Save();

            }


        }

        private void button3_Click(object sender, EventArgs e)
        {

            foreach (DataRow row in calendario.Rows)
            {
                Properties.Settings.Default.Eventos = Properties.Settings.Default.Eventos + row["Evento"] + "  " + row["Data"] + "\n";
                Properties.Settings.Default.Save();


            }
            printDocument1.Print();

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString(Properties.Settings.Default.Eventos, new System.Drawing.Font("Arial", 12), Brushes.Black, new PointF(100, 100));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "html(*.html)|*.html";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string a = saveFileDialog1.FileName;
                File.WriteAllText(a, gerarHtml());
            }
        }
        private string gerarHtml()
        {



            StringBuilder html = new StringBuilder();

            html.Append(@"<!DOCTYPE html>
        <html>
        <head>
            <meta charset='utf-8' />
             <script src='https://cdn.jsdelivr.net/npm/fullcalendar@6.1.8/index.global.min.js'></script>
             <link href='https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css' rel='stylesheet'>
            <link href='https://cdn.jsdelivr.net/npm/bootstrap-icons@1.8.1/font/bootstrap-icons.css' rel='stylesheet'>
        </head>
        <body>
            <div id='calendarcontainer'>
            <div id='calendar'></div>
            </div>
            <style>
                html, body {
                margin: 0;
                padding: 0;
                font-family: Arial, Helvetica Neue, Helvetica, sans-serif;
                font-size: 14px;
                }

                #calendar {
                max-width: 1100px;
                margin: 40px auto;
                }
            </style>
            <script src='fullcalendar/dist/index.global.js'></script>
            <script>
                document.addEventListener('DOMContentLoaded', function() {
                 var calendarEl = document.getElementById('calendar');
                 var calendar = new FullCalendar.Calendar(calendarEl, {
                 themeSystem: 'bootstrap5',
                 headerToolbar: {
                left: 'prev,next today',
                center: 'title',
                 right: 'dayGridMonth,timeGridWeek,timeGridDay'
      
                },
                 buttonText: { 
                month: ""Mês"",
                week: ""Semana"",
                day: ""Dia"",
                today: ""Hoje""
                },
   
     monthNames: ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
    locale:'PT-BR',
                 events: [");

            foreach (DataRow row in calendario.Rows)
            {

                string evento = row["Evento"].ToString();


                DateTime dtinicio = DateTime.ParseExact(row["DataInicio"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime dtfinal = DateTime.ParseExact(row["DataTermino"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                string eventJson = string.Format("{{ title: '{0}', start: '{1}', end: '{2}' }},",
                        evento, dtinicio.ToString("yyyy-MM-dd"), dtfinal.ToString("yyyy-MM-dd"));

                html.Append(eventJson);
            }


            if (calendario.Rows.Count > 0)
            {
                html.Remove(html.Length - 1, 1);
            }

            html.Append(@"]
                    });
 calendar.render();
                });
            </script>
        </body>
        </html>");

            return html.ToString();




        }

        private void monthCalendar2_DateSelected(object sender, DateRangeEventArgs e)
        {
            label4.Text = "";
            foreach (DataRow row in calendario.Rows)
            {

                DateTime inicio = DateTime.ParseExact(row["DataInicio"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime final = DateTime.ParseExact(row["DataTermino"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                if (monthCalendar2.SelectionStart == inicio && inicio != final)
                {
                    label4.Text = label4.Text + "\n" + row["DataInicio"].ToString() + " " + row["Evento"].ToString();
                }
                if (monthCalendar2.SelectionStart == final)
                {
                    label4.Text = label4.Text + "\n" + row["DataTermino"].ToString() + " " + row["Evento"].ToString();
                }
                if (monthCalendar2.SelectionStart > inicio && monthCalendar2.SelectionStart < final)
                {
                    label4.Text = label4.Text + "\n" + monthCalendar2.SelectionStart.ToShortDateString() + " " + row["Evento"].ToString();
                }

            }
        }

        private void Mensal_CheckedChanged(object sender, EventArgs e)
        {
            if (Mensal.Checked)
            {
                Anual.Checked = false;
            }
        }

        private void Anual_CheckedChanged(object sender, EventArgs e)
        {
            if (Anual.Checked)
            {
                Mensal.Checked = false;
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            Dictionary<int, string> meses = new Dictionary<int, string>();
            meses[1] = "Janeiro";
            meses[2] = "Fevereiro";
            meses[3] = "Março";
            meses[4] = "Abril";
            meses[5] = "Maio";
            meses[6] = "Junho";
            meses[7] = "Julho";
            meses[8] = "Agosto";
            meses[9] = "Setembro";
            meses[10] = "Outubro";
            meses[11] = "Novembro";
            meses[12] = "Dezembro";
            table.Columns.Add("Dia");
            table.Columns.Add("Evento");
            saveFileDialog1.Filter = "docx files (*.docx)|*.docx";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string output = saveFileDialog1.FileName;

                var doc = DocX.Create(output, Xceed.Document.NET.DocumentTypes.Document);
                int ano = DateTime.Now.Year;
                int mess = 1;
                int dia = 1;
                foreach (var mes in meses)
                {

                    int mesNumero = mes.Key;
                    string nomeMes = mes.Value;
                    doc.InsertParagraph(nomeMes).Font("Arial").FontSize(16);
                    DateTime dataatual = new DateTime(ano, mess, 1);
                    DateTime datafinal = dataatual.AddMonths(1).AddDays(-1);

                    Table calendarTable = doc.AddTable(6, 7);
                    calendarTable.Alignment = Alignment.center;
                    calendarTable.Design = TableDesign.LightGrid;
                   

                    int row = 1;
                    int col = (int)dataatual.DayOfWeek;
                    while (dataatual <= datafinal)
                    {
                        if (col == 0)
                        {
                            calendarTable.InsertRow();
                            row++;
                        }
                             calendarTable.Rows[row].Cells[col].Paragraphs.First().Append(dataatual.Day.ToString());

                            foreach (DataRow rown in calendario.Rows)
                            {
                                
                                if (rown["Datainicio"].ToString() == dataatual.ToShortDateString())
                                {
                                    calendarTable.Rows[row].Cells[col].Paragraphs.First().Append("\n" + rown["Evento"].ToString());
                                }
                            }

                          
                        
                       
                            dataatual = dataatual.AddDays(1);
                            col = (col + 1) % 7;
                        

                        
                    }
                   
                    doc.InsertTable(calendarTable);
                    doc.InsertParagraph("\n \n");
                    mess++;

                }
                doc.Save();

            }
        }
    }
}
