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
    public partial class F_OutroUsuario : Form
    {
        int id_Equipamento;
        public F_OutroUsuario()
        {
            InitializeComponent();
        }

        private void F_OutroUsuario_Load(object sender, EventArgs e)
        {
            dgvDemandas.Font = new Font(dgvDemandas.Font.FontFamily, 12);
            dgvDemandas.RowsDefaultCellStyle.ForeColor = Color.FromArgb(123, 117, 201); //Fundo do Datagrid

            //1
            dgvDemandas.BackgroundColor = Color.FromArgb(242, 255, 246);//Fundo do Datagrid

            //2
            dgvDemandas.RowsDefaultCellStyle.BackColor = Color.FromArgb(230, 251, 218); //fundo da célula 
            dgvDemandas.RowsDefaultCellStyle.ForeColor = Color.FromArgb(62, 91, 163); //Frotesd Blue

            //3
            dgvDemandas.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(230, 251, 218); //fundo da célula
            dgvDemandas.AlternatingRowsDefaultCellStyle.ForeColor = Color.FromArgb(123, 117, 201);//Frotesd Blue

            //5
            dgvDemandas.EnableHeadersVisualStyles = false;
            dgvDemandas.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(228, 237, 252); //Iceberg

            dgvDemandas.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(159, 198, 252); //Frotesd Blue

            dgvDemandas.GridColor = Color.FromArgb(63, 63, 70);


            dgvDemandas.EnableHeadersVisualStyles = false;


            dgvDemandas.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvDemandas.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;

            try
            {
                string strconn = "Data Source=SOB041995L4B1PC\\SQLEXPRESS; Initial Catalog = bd_Estoque; Integrated Security = true";
                SqlConnection conn = new SqlConnection(strconn);
                conn.Open();

                string sql = $"SELECT tb_Estoquee.Serial,tb_Estoquee.Mac,tb_Estoquee.Modelo,tb_Estoquee.Tipo " +
                    $"FROM tb_demanda INNER JOIN tb_Estoquee ON tb_demanda.id_Equipamento = tb_Estoquee.id_Equipamento " +
                    $"WHERE tb_demanda.id_Tecnico = {C_Logado.usuarioId} ";

                
                //executa o comando SQL e recebe os dados
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);

                //intancia/cria um DtaTAble wue irá servir de intermédiario
                DataTable dt = new DataTable();

                //preencher o datagridview com os dedos do dt
                da.Fill(dt);

                dgvDemandas.DataSource = dt;

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Listar Cadastro.");
            }
        }


        private void dgvDemandas_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
    }
}
