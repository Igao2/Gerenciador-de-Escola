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
    public partial class Cadastro : Form
    {
        public Cadastro()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Connection con = new Connection();
            con.conectar();
            string arroba = "@";
            string com = ".com";
            if (email.Text.Contains(arroba) && email.Text.Contains(com))
            {
                int x = 0;
                if(radioButton1.Checked)
                {
                    x = 2;
                }
                if(radioButton2.Checked)
                {
                    x = 1;
                }

                try
                {
                    Criptografia criptografia = new Criptografia();
                   string [] asc =  criptografia.Criptografar(email.Text,senha.Text);

                    string sqlinsert = "INSERT INTO Login(Email,Senha) VALUES('" + asc[0] + "','" + asc[1] + "')";
                    SQLiteCommand command = new SQLiteCommand(sqlinsert, con.sq);
                    command.ExecuteNonQuery();
                    string sqlInsert = "INSERT INTO pergunta_usuario(emailUsuario,CodPergunta,resposta) VALUES('"+ asc[0] +"','"+x+"','"+textBox3.Text+"')";
                    SQLiteCommand comand = new SQLiteCommand(sqlInsert, con.sq);
                    comand.ExecuteNonQuery();
                    MessageBox.Show("Cadastro realizado, por favor faça o Login", "Mensagem do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                catch (Exception E)
                {
                    MessageBox.Show(E.Message.ToString());
                }

            }
            else
            {
                MessageBox.Show("Digite um email válido!", "Alerta do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton1.Checked)
            {
                radioButton2.Checked = false;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton2.Checked)
            {
                radioButton1.Checked = false;
            }
        }
    }
}
