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
    }
}
