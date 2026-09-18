using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_Estoque
{
    public partial class F_Menu : Form
    {
        public F_Menu()
        {
            InitializeComponent();
        }

        private void btnUsuario_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
         

        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            string tipo = cbxTipo.Text;
            string serial = tbxSerial.Text;
            string mac = tbxMac.Text;
            string modelo = cbxModelo.Text;


            string strconn = "Data Source = SOB041995L4B1PC\\SQLEXPRESS;" + "Initial Catalog = bd_Estoque; Integrated Security = true";

            SqlConnection conn = new SqlConnection(strconn);
            conn.Open();
            string sql = "INSERT INTO [dbo].[tb_Estoquee](Serial, Modelo, Mac, Tipo)" + "VALUES(@Serial, @Modelo, @Mac, @Tipo)";


            SqlCommand comando = new SqlCommand(sql, conn);
            comando.Parameters.Add(new SqlParameter("@Serial", serial));
            comando.Parameters.Add(new SqlParameter("@Modelo", modelo));
            comando.Parameters.Add(new SqlParameter("@Mac", mac));
            comando.Parameters.Add(new SqlParameter("@Tipo", tipo));


            try 
            {

                comando.ExecuteNonQuery();
                MessageBox.Show($"{tipo} salvo com sucesso!!");
                conn.Close();
                this.F_Menu_Load(sender, e);

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void F_Menu_Load(object sender, EventArgs e)
        {
            dgvListaCadastros.Font = new Font(dgvListaCadastros.Font.FontFamily, 12);
            dgvListaCadastros.RowsDefaultCellStyle.ForeColor = Color.FromArgb(123, 117, 201); //Fundo do Datagrid

            //1
            dgvListaCadastros.BackgroundColor = Color.FromArgb(242, 255, 246);//Fundo do Datagrid

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
                string strconn = "Data Source=SOB041995L4B1PC\\SQLEXPRESS; Initial Catalog = bd_Estoque; Integrated Security = true";
                SqlConnection conn = new SqlConnection(strconn);
                conn.Open();

                string sql = "SELECT id_Equipamento AS ID, Serial AS Serial," +
                    " Modelo AS Modelo, Mac AS Mac, Tipo AS Típo " +
                  "FROM [dbo].[tb_Estoquee]";

                
                //executa o comando SQL e recebe os dados
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);

                //intancia/cria um DtaTAble wue irá servir de intermédiario
                DataTable dt = new DataTable();

                //preencher o datagridview com os dedos do dt
                da.Fill(dt);

                dgvListaCadastros.DataSource = dt;

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Listar Cadastro.");
            }
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                string pesquisarTipo = cbxPesquisar.Text;
                string strconn = "Data Source=SOB041995L4B1PC\\SQLEXPRESS; " + "Initial Catalog = bd_Estoque; Integrated Security = true";

                SqlConnection conn = new SqlConnection(strconn);
                conn.Open();
                string sql = "SELECT id_Equipamento AS ID, Serial AS Serial," + 
                    " Modelo AS Modelo, Mac AS Mac, Tipo AS Típo " +
                  " FROM [dbo].[tb_Estoquee] " +
                  $" WHERE tipo LIKE '%{pesquisarTipo}%' ";


                //executa o comando SQL e recebe os dados
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);

                //instancia/cria um DtaTable que ira servir de intermediario 
                DataTable dt = new DataTable();

                //preenche i dt com os dados presentes no da
                da.Fill(dt);

                //preencher o datagridview com os dados do dt
                dgvListaCadastros.DataSource = dt;
               
            }

            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Listar Cadastro");
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            tbxMac.Text = "";
            tbxSerial.Text = "";
            cbxModelo.Text = "";
            cbxTipo.Text = "";
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("Você tem certeza que deseja sair?", "Confirmar Saida", MessageBoxButtons.YesNo);

            if (resultado == DialogResult.Yes)
            {
                this.Close();
            }

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            F_Editaritens JanelaEditaritens = new F_Editaritens();
            if ( JanelaEditaritens.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string strconn = "Data Source=SOB041995L4B1PC\\SQLEXPRESS; Initial Catalog = bd_Estoque; Integrated Security = true";
                    SqlConnection conn = new SqlConnection(strconn);
                    conn.Open();

                    string sql = "SELECT id_Equipamento AS ID, Serial AS Serial," +
                        " Modelo AS Modelo, Mac AS Mac, Tipo AS Típo " +
                      "FROM [dbo].[tb_Estoquee]";


                   
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);

                    
                    DataTable dt = new DataTable();

                    
                    da.Fill(dt);

                    dgvListaCadastros.DataSource = dt;

                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Listar Cadastro.");
                }
            }
            

        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            F_JanelaEnvio JanelaEnvio = new F_JanelaEnvio();
            if (JanelaEnvio.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string strconn = "Data Source=SOB041995L4B1PC\\SQLEXPRESS; Initial Catalog = bd_Estoque; Integrated Security = true";
                    SqlConnection conn = new SqlConnection(strconn);
                    conn.Open();

                    string sql = "SELECT id_Equipamento AS ID, Serial AS Serial," +
                        " Modelo AS Modelo, Mac AS Mac, Tipo AS Típo " +
                      "FROM [dbo].[tb_Estoquee]";



                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);


                    DataTable dt = new DataTable();


                    da.Fill(dt);

                    dgvListaCadastros.DataSource = dt;

                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Listar Cadastro.");
                }
            }
        }
    }
}
