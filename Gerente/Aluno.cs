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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Gerente
{
    public partial class Aluno : Form
    {
        public Aluno()
        {
            InitializeComponent();
        }
        private DataSet dataSet = new DataSet();
        private void Aluno_Load(object sender, EventArgs e)
        {
           
            carregar();
            inicializar();
        }

        private void carregar()
        {
         
            try
            {
                Connection con = new Connection();
                con.conectar();
                string sql = "SELECT nomeAluno,CPF,nomeResponsavel,telefoneCasa,Turma.CodTurma From Aluno " +
                    "Inner join Turma on Turma.CodTurma = Aluno.CodTurma ";
                string Sql = "Select * From Turma";
                SQLiteDataAdapter sQLiteData = new SQLiteDataAdapter(sql, con.sq);
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(Sql, con.sq);
                sQLiteData.Fill(dataSet, "Alunos");
                adapter.Fill(dataSet, "Turmas");
                con.desconectar();
            }
             catch(Exception E )
            {
                MessageBox.Show(E.Message,"Alerta do Sistema",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        private void inicializar()
        {
            DataTable aluno = dataSet.Tables["Alunos"];
            for(int i = 0; i<aluno.Rows.Count;i++)
            {
                DataRow linhaAluno = aluno.Rows[i];
                if (linhaAluno.RowState != DataRowState.Deleted)
                {

                    ListViewItem lvi = new ListViewItem(linhaAluno["nomeAluno"].ToString());
                    lvi.SubItems.Add(linhaAluno["CPF"].ToString());
                    lvi.SubItems.Add(linhaAluno["nomeResponsavel"].ToString());
                    lvi.SubItems.Add(linhaAluno["telefoneCasa"].ToString());
                    lvi.SubItems.Add(linhaAluno["CodTurma"].ToString());

                    listView1.Items.Add(lvi);
                }
            }
            DataTable turmas = dataSet.Tables["Turmas"];
            for(int i = 0; i<turmas.Rows.Count;i++)
            {
                DataRow linhaTurmas = turmas.Rows[i];
                comboBox1.Items.Add(linhaTurmas.ItemArray[2].ToString());
                comboBox2.Items.Add(linhaTurmas.ItemArray[2].ToString());
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dataSet.Reset();
                Connection con = new Connection();
                con.conectar();
                string SQl = "Select nomeAluno,CPF,nomeResponsavel,telefoneCasa,Turma.descTurma from Aluno  inner join Turma on Turma.CodTurma = Aluno.CodTurma Where descTurma = '" + comboBox1.Text + "'";
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(SQl,con.sq);
                adapter.Fill(dataSet, "Alunos");
                DataTable alunos = dataSet.Tables["Alunos"];
                for(int i = 0;i<alunos.Rows.Count;i++)
                {
                    DataRow linhaAluno = alunos.Rows[i];
                    ListViewItem list = new ListViewItem(linhaAluno["nomeAluno"].ToString());
                    list.SubItems.Add(linhaAluno["CPF"].ToString());
                    list.SubItems.Add(linhaAluno["nomeResponsavel"].ToString());
                    list.SubItems.Add(linhaAluno["telefoneCasa"].ToString());
                    listView1.Items.Clear();
                    listView1.Items.Add(list);

                }

            }
            catch(Exception E)
            {
                MessageBox.Show(E.Message, "Alerta do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Hide();
            groupBox1.Show();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
