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
    public partial class Funcionario : Form
    {
        public Funcionario()
        {
            InitializeComponent();
        }

        private void Funcionario_Load(object sender, EventArgs e)
        {
            inicializar();
        }

        private void inicializar()
        {
            listView1.View = View.Details;

            

            listView1.GridLines = true;


        }
    }
}
