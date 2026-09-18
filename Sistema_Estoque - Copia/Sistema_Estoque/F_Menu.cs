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
            
        }
    }
}
