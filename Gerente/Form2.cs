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
            

            DateTime dt1 = DateTime.ParseExact(maskedTextBox1.Text,"dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime dt3 = DateTime.ParseExact(maskedTextBox2.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime dt2 = DateTime.Today;
            if(dt1<dt2||dt3<dt2)
            {
                MessageBox.Show("Data Inválida!","Alerta do Sistema",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    Connection con = new Connection();
                    con.conectar();
                    string Sql = "INSERT INTO Calendario VALUES('" + textBox1.Text + "','" + maskedTextBox1.Text+ "','"+maskedTextBox2.Text+"')";
                    SQLiteCommand command = new SQLiteCommand(Sql, con.sq);
                    command.ExecuteNonQuery();
                    string[] valores =
                    {
                        textBox1.Text,
                        maskedTextBox1.Text,
                        maskedTextBox2.Text
                    };
                    calendario.Rows.Add(valores);
                    monthCalendar2.AddBoldedDate(dt1);
                    monthCalendar2.AddBoldedDate(dt3);
                    maskedTextBox1.Clear();
                    textBox1.Clear();
                    maskedTextBox2.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(),"Alerta do Sistema",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
            
        }

        private void Calendario_Load(object sender, EventArgs e)
        {
            try
            {
                Connection con = new Connection();
                con.conectar();
                string sql = "SELECT * FROM Calendario";
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, con.sq);
                adapter.Fill(calendario);
                con.desconectar();
                dataGridView1.DataSource = calendario;

            }
            catch(Exception Err)
            {
                MessageBox.Show(Err.Message, "Alerta do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }
       
        
       

        private void button2_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter ="docx files (*.docx)|*.docx" ;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string nomearquivo = saveFileDialog1.FileName;
                string texto = "";
                foreach (DataRow row in  calendario.Rows)
                {
                    
                    texto = texto  + row["Evento"] + "  " + row["DataInicio"] + "até "+ row["DataTermino"]+"\n";


                }

                var doc = DocX.Create(nomearquivo, Xceed.Document.NET.DocumentTypes.Document);
                doc.InsertParagraph(texto);
                doc.Save();

            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            foreach (DataRow row in calendario.Rows)
            {
                Properties.Settings.Default.Eventos = Properties.Settings.Default.Eventos  + row["Evento"] + "  " + row["Data"] + "\n";
                Properties.Settings.Default.Save();
                

            }
            printDocument1.Print();

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString(Properties.Settings.Default.Eventos,new Font("Arial",12), Brushes.Black,new PointF(100,100));
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
               
             
                DateTime dtinicio = DateTime.ParseExact(row["DataInicio"].ToString(),"dd/MM/yyyy",CultureInfo.InvariantCulture);
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
    }
}

