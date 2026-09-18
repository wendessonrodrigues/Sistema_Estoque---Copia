using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_Estoque
{
    public partial class F_JanelaEnvio : Form
    {

        int id_Equipamentos;
        int id_Usuario;
        int id_Tecnico;

        public F_JanelaEnvio()
        {
            InitializeComponent();
        }

        private void F_JanelaEnvio_Load(object sender, EventArgs e)
        {
            dgvEnvios.Font = new Font(dgvEnvios.Font.FontFamily, 12);
            dgvEnvios.RowsDefaultCellStyle.ForeColor = Color.FromArgb(123, 117, 201); //Fundo do Datagrid

            //1
            dgvEnvios.BackgroundColor = Color.FromArgb(242, 255, 246);//Fundo do Datagrid

            //2
            dgvEnvios.RowsDefaultCellStyle.BackColor = Color.FromArgb(230, 251, 218); //fundo da célula 
            dgvEnvios.RowsDefaultCellStyle.ForeColor = Color.FromArgb(62, 91, 163); //Frotesd Blue

            //3
            dgvEnvios.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(230, 251, 218); //fundo da célula
            dgvEnvios.AlternatingRowsDefaultCellStyle.ForeColor = Color.FromArgb(123, 117, 201);//Frotesd Blue

            //5
            dgvEnvios.EnableHeadersVisualStyles = false;
            dgvEnvios.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(228, 237, 252); //Iceberg

            dgvEnvios.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(159, 198, 252); //Frotesd Blue
                
            dgvEnvios.GridColor = Color.FromArgb(63, 63, 70);


            dgvEnvios.EnableHeadersVisualStyles = false;


            dgvEnvios.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvEnvios.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;


            try
            {



                string strconn = "Data Source=SOB041995L4B1PC\\SQLEXPRESS; " + "Initial Catalog = bd_Estoque; Integrated Security = true";

                SqlConnection conn = new SqlConnection(strconn);
                conn.Open();
                string tecnicos = "SELECT id_Tecnico, TRIM (nome) AS nome FROM [dbo].[tb_Tecnicos]";
                string sql = "SELECT id_Equipamento AS ID, Serial AS Serial," +
                 " Modelo AS Modelo, Mac AS Mac, Tipo AS Típo " +
                "FROM [dbo].[tb_Estoquee]";

                SqlDataAdapter da = new SqlDataAdapter(tecnicos, conn);
                SqlDataAdapter da_equipamanto = new SqlDataAdapter(sql, conn);


                DataTable dt = new DataTable();
                DataTable dtt = new DataTable();
                da.Fill(dt);
                da_equipamanto.Fill(dtt);
                dgvEnvios.DataSource = dtt;
                conn.Close();



                cbxTecnicos.DataSource = dt;
                //inserindo o titulo no displayMenber 
                //O titulo ficara visivel no combobox
                cbxTecnicos.DisplayMember = "nome";
                //inserindo o id_Livro no ValueMember
                //valor sera utilizado ao selecionar um item
                cbxTecnicos.ValueMember = "id_Tecnico";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Listar Cadastro.");
            }




        }

        private void btnSelecionar_Click(object sender, EventArgs e)
        {
            id_Equipamentos = Convert.ToInt32(dgvEnvios.CurrentRow.Cells[0].Value.ToString());
            tbxSerial.Text = dgvEnvios.CurrentRow.Cells[1].Value.ToString();
            cbxModelo.Text = dgvEnvios.CurrentRow.Cells[2].Value.ToString();
            tbxMac.Text = dgvEnvios.CurrentRow.Cells[3].Value.ToString();
            cbxTipo.Text = dgvEnvios.CurrentRow.Cells[4].Value.ToString();

        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            string tipo = cbxTipo.Text;
            string serial = tbxSerial.Text;
            string mac = tbxMac.Text;
            string modelo = cbxModelo.Text;
            id_Tecnico = Convert.ToInt32(cbxTecnicos.SelectedValue);


            string strconn = "Data Source = SOB041995L4B1PC\\SQLEXPRESS;" + "Initial Catalog = bd_Estoque; Integrated Security = true";

            SqlConnection conn = new SqlConnection(strconn);
            conn.Open();
            string sql = "INSERT INTO [dbo].[tb_demanda](id_Equipamento,id_Usuario,id_Tecnico)" + "VALUES(@id_Equipamento, @id_Usuario, @id_Tecnico )";


            SqlCommand comando = new SqlCommand(sql, conn);
            comando.Parameters.Add(new SqlParameter("@id_Equipamento", id_Equipamentos ));
            comando.Parameters.Add(new SqlParameter("@id_Usuario", C_Logado.usuarioId));
            comando.Parameters.Add(new SqlParameter("@id_Tecnico", id_Tecnico));

            try
            {

                comando.ExecuteNonQuery();
                MessageBox.Show($"{modelo.Trim()} enviado sucesso!!");
                conn.Close();
               
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

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
    }
}
