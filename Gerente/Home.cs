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
            button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatAppearance.MouseDownBackColor = Color.Transparent;
            button1.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button1.BackColor = Color.Transparent;
            button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatAppearance.MouseDownBackColor = Color.Transparent;
            button2.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button2.BackColor = Color.Transparent;
            button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button3.FlatAppearance.BorderSize = 0;
            button3.FlatAppearance.MouseDownBackColor = Color.Transparent;
            button3.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button3.BackColor = Color.Transparent;
            button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button5.FlatAppearance.BorderSize = 0;
            button5.FlatAppearance.MouseDownBackColor = Color.Transparent;
            button5.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button5.BackColor = Color.Transparent;
            button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button4.FlatAppearance.BorderSize = 0;
            button4.FlatAppearance.MouseDownBackColor = Color.Transparent;
            button4.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button4.BackColor = Color.Transparent;
            panel1.BackColor = Color.Transparent;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.BackColor = Color.White;
            this.panel1.Controls.Clear();
            Funcionario func = new Funcionario() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true }; ;
            this.panel1.Controls.Add(func);
            func.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel1.BackColor = Color.White;
            this.panel1.Controls.Clear();
            Contas contas = new Contas() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true }; ;
            this.panel1.Controls.Add(contas);
            contas.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel1.BackColor = Color.White;
            this.panel1.Controls.Clear();
            Calendario cal = new Calendario() { Dock = DockStyle.Fill,TopLevel = false, TopMost = true }; ;
            this.panel1.Controls.Add(cal);
            cal.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel1.BackColor = Color.White;
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
            panel1.BackColor = Color.White;
            this.panel1.Controls.Clear();
            Aluno aluno = new Aluno() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true }; ;
            this.panel1.Controls.Add(aluno);
            aluno.Show();
        }

        private void Home_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
