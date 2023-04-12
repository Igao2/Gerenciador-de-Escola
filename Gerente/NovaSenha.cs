using Microsoft.VisualBasic;
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
    public partial class NovaSenha : Form
    {
        public NovaSenha()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetSet get = new GetSet();
            if(textBox1.Text!="")
            {
                try
                {
                    Connection con = new Connection();
                    con.conectar();
                    Criptografia cri = new Criptografia();
                    string[] acess = cri.Criptografar(get.geta(), textBox1.Text);
                    string sqlupdate = "UPDATE Login SET Senha = '" + acess[1] + "'WHERE Email = '" + acess[0] + "'";

                    SQLiteCommand com = new SQLiteCommand(sqlupdate, con.sq);
                    com.ExecuteNonQuery();
                    con.desconectar();
                    MessageBox.Show("Senha atualizada com sucesso, por favor faça o login!!", "Alerta do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                catch (Exception E)
                {
                    MessageBox.Show(E.Message,"Alerta do Sistema",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Não deixe o campo de senha vazio!!", "Alerta do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
           
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                textBox1.UseSystemPasswordChar = false;
                checkBox1.Text = "Esconder";
            }
            if(checkBox1.Checked == false)
            {
                textBox1.UseSystemPasswordChar = true;
                checkBox1.Text = "Revelar";
            }
        }
    }
}
