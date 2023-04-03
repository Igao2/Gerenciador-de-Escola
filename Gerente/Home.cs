using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gerente
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.panel1.Controls.Clear();
            Funcionario func = new Funcionario() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true }; ;
            this.panel1.Controls.Add(func);
            func.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.panel1.Controls.Clear();
            Contas contas = new Contas() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true }; ;
            this.panel1.Controls.Add(contas);
            contas.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.panel1.Controls.Clear();
            Calendario cal = new Calendario() { Dock = DockStyle.Fill,TopLevel = false, TopMost = true }; ;
            this.panel1.Controls.Add(cal);
            cal.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.panel1.Controls.Clear();
            Horario hor = new Horario() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true }; ;
            this.panel1.Controls.Add(hor);
            hor.Show();
        }

        private void Home_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime dt = new DateTime();
            dt = DateTime.Now;
            label1.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.panel1.Controls.Clear();
            Aluno aluno = new Aluno() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true }; ;
            this.panel1.Controls.Add(aluno);
            aluno.Show();
        }
    }
}
