using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sistema_Estoque.Resources.NewFolder1;

namespace Sistema_Estoque
{
    public partial class F_Editaritens : Form
    {
        int id_Equipamentos;
        public F_Editaritens()
        {
            InitializeComponent();
        }

        private void F_Editaritens_Load(object sender, EventArgs e)
        {
            dgvListaCadastros.Font = new Font(dgvListaCadastros.Font.FontFamily, 12);
            dgvListaCadastros.RowsDefaultCellStyle.ForeColor = Color.FromArgb(123, 117, 201); //Fundo do Datagrid

            //1
            dgvListaCadastros.BackgroundColor = Color.FromArgb(224, 250, 255);//Fundo do Datagrid

            //2
            dgvListaCadastros.RowsDefaultCellStyle.BackColor = Color.FromArgb(230, 251, 218); //fundo da célula 
            dgvListaCadastros.RowsDefaultCellStyle.ForeColor = Color.FromArgb(62, 91, 163); //Frotesd Blue

            //3
            dgvListaCadastros.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(230, 251, 218); //fundo da célula
            dgvListaCadastros.AlternatingRowsDefaultCellStyle.ForeColor = Color.FromArgb(123, 117, 201);//Frotesd Blue

            //5
            dgvListaCadastros.EnableHeadersVisualStyles = false;
            dgvListaCadastros.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(228, 237, 252); //Iceberg

            dgvListaCadastros.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(159, 198, 252); //Frotesd Blue

            dgvListaCadastros.GridColor = Color.FromArgb(63, 63, 70);


            dgvListaCadastros.EnableHeadersVisualStyles = false;


            dgvListaCadastros.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvListaCadastros.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;

            try
            {

                string strconn = "Data Source=SOB041995L4B1PC\\SQLEXPRESS; " + "Initial Catalog = bd_Estoque; Integrated Security = true";

                SqlConnection conn = new SqlConnection(strconn);
                conn.Open();
                string sql = "SELECT * FROM  [dbo].[tb_Estoquee]";

                SqlDataAdapter da = new SqlDataAdapter(sql, conn);


                DataTable dt = new DataTable();


                da.Fill(dt);


                dgvListaCadastros.DataSource = dt;
            }

            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Listar Equipamentos");
            }
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            string tipo = cbxTipo.Text;
            string serial = tbxSerial.Text;
            string mac = tbxMac.Text;
            string modelo = cbxModelo.Text;


            string strconn = "Data Source=SOB041995L4B1PC\\SQLEXPRESS; " + "Initial Catalog = bd_Estoque; Integrated Security = true";
            SqlConnection conn = new SqlConnection(strconn);
            conn.Open();

            string sql = "UPDATE  [dbo].[tb_Estoquee] SET " +
                " Serial = @Serial, Modelo = @Modelo, Mac = @Mac, Tipo = @Tipo WHERE id_Equipamento = @Id_Equipamento ";

            SqlCommand comando = new SqlCommand(sql, conn);

            comando.Parameters.Add(new SqlParameter("@id_Equipamento", id_Equipamentos));
            comando.Parameters.Add(new SqlParameter("@Serial", serial));
            comando.Parameters.Add(new SqlParameter("@Mac", mac));
            comando.Parameters.Add(new SqlParameter("@Modelo",modelo));
            comando.Parameters.Add(new SqlParameter("@Tipo", tipo));




            try
            {

                comando.ExecuteNonQuery();
                MessageBox.Show($"Equipamento modificado com sucesso!");
                this.F_Editaritens_Load(sender, e);

                conn.Close();

            }

            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }



        }

        private void btneditar_Click(object sender, EventArgs e)
        {
            id_Equipamentos = Convert.ToInt32(dgvListaCadastros.CurrentRow.Cells[0].Value.ToString());
            tbxSerial.Text = dgvListaCadastros.CurrentRow.Cells[1].Value.ToString();
            cbxModelo.Text = dgvListaCadastros.CurrentRow.Cells[2].Value.ToString();
            tbxMac.Text = dgvListaCadastros.CurrentRow.Cells[3].Value.ToString();
            cbxTipo.Text = dgvListaCadastros.CurrentRow.Cells[4].Value.ToString();


        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("Você tem certeza que deseja sair?", "Confirmar Saida", MessageBoxButtons.YesNo);

            if (resultado == DialogResult.Yes)
            {
                DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            id_Equipamentos = Convert.ToInt32(dgvListaCadastros.CurrentRow.Cells[0].Value.ToString());
            string Tipo = dgvListaCadastros.CurrentRow.Cells[4].Value.ToString();
            string Modelo= dgvListaCadastros.CurrentRow.Cells[2].Value.ToString();

            DialogResult resultado = MessageBox.Show("Tem certeza que deseja excluir este equipamento ?", "Confirmação de exclusão", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                string strconn = "Data Source=SOB041995L4B1PC\\SQLEXPRESS; " + "Initial Catalog = bd_Estoque; Integrated Security = true";
                SqlConnection conn = new SqlConnection(strconn);
                conn.Open();

                string sql = "DELETE FROM [dbo].[tb_Estoquee] WHERE id_Equipamento = @Id_Equipamento";
                SqlCommand comando = new SqlCommand(sql, conn);
                comando.Parameters.Add(new SqlParameter("@Id_Equipamento", id_Equipamentos));
                comando.Parameters.Add(new SqlParameter("@modelo", Modelo.Trim()));
                comando.Parameters.Add(new SqlParameter("@Tipo", Tipo.Trim()));


                comando.ExecuteNonQuery();
                MessageBox.Show($"{Tipo.Trim()}: {Modelo.Trim()} deletado com sucesso.");
                this.F_Editaritens_Load(sender, e);
                conn.Close();


            }
        }

    }
    
}
