using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sistema_Estoque.Resources.NewFolder1;

namespace Sistema_Estoque
{
    public partial class F_Login : Form
    {
        public F_Login()
        {
            InitializeComponent();
        }

        

        private void button1_Click(object sender, EventArgs e)
        {


            string usuario = tbxUsuario.Text;
            string senha = tbxSenha.Text;
            string Tipo_Usuario = cbxTipoUsuario.Text;

            string strconn = "Data Source=SOB041995L4B1PC\\SQLEXPRESS; " + "Initial Catalog = bd_Estoque; Integrated Security = true";
            SqlConnection conn = new SqlConnection(strconn);
            conn.Open();



            if (Tipo_Usuario == "Estoque")
            {
                try
                {
                    string sql = "SELECT id_Usuario, usuario, senha FROM [dbo].[tb_UsuariosEstoque]" +
                        "WHERE usuario = @usuario and senha = @senha";
                    SqlCommand comando = new SqlCommand(sql, conn);
                    comando.Parameters.AddWithValue("@usuario", usuario);
                    comando.Parameters.AddWithValue("@senha", senha);

                    SqlDataReader reader = comando.ExecuteReader();
                    if (reader.Read())
                    {
                        C_Logado.usuarioId = Convert.ToInt32(reader["id_Usuario"]);
                        C_Logado.nomeUsuario = reader["usuario"].ToString();
                        MessageBox.Show($"Usuario:{C_Logado.usuarioId}");
                        F_Menu janelaMenu = new F_Menu();
                        this.Visible = false;
                        if (janelaMenu.ShowDialog() != DialogResult.OK)
                        {
                            Close();
                        }
                    }

                    else
                    {
                        MessageBox.Show("Usuário ou senha incorretos!", "Login Aluno", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }


                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Login");

                }



            }

            if (Tipo_Usuario == "Tecnico")
            {




                try
                {
                    string sql = "SELECT id_Tecnico, usuario, senha FROM [dbo].[tb_Tecnicos]" +
                        "WHERE usuario = @usuario and senha = @senha";
                    SqlCommand comando = new SqlCommand(sql, conn);
                    comando.Parameters.AddWithValue("@usuario", usuario);
                    comando.Parameters.AddWithValue("@senha", senha);

                    SqlDataReader reader = comando.ExecuteReader();
                    if (reader.Read())
                    {
                        C_Logado.usuarioId = Convert.ToInt32(reader["id_Tecnico"]);
                        C_Logado.nomeUsuario = reader["usuario"].ToString();
                        MessageBox.Show($"Usuario:{C_Logado.usuarioId}");
                        F_OutroUsuario janelaOutroUsuario = new F_OutroUsuario();
                        this.Visible = false;
                        if (janelaOutroUsuario.ShowDialog() != DialogResult.OK)
                        {
                            Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Usuário ou senha incorretos!", "Login Aluno", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Login");

                }

            }
            else
            {
                MessageBox.Show("Escolha o tipo de usuário", "Login");
            }
        }


        

            private void panel1_Paint(object sender, PaintEventArgs e) 
            {
            int raio = 20;

            GraphicsPath caminho = new GraphicsPath();
            caminho.StartFigure();
            caminho.AddArc(new Rectangle(0, 0, raio, raio), 180, 90);
            caminho.AddArc(new Rectangle(panel1.Width - raio - 1, 0, raio, raio), 270, 90);
            caminho.AddArc(new Rectangle(panel1.Width - raio - 1, panel1.Height - raio - 1, raio, raio), 0, 90);
            caminho.AddArc(new Rectangle(0, panel1.Width - raio - 1, raio, raio), 90, 90);
            caminho.CloseFigure();

            panel1.Region = new Region(caminho);

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using (Pen caneta = new Pen(Color.Gray, 1))
            {
                e.Graphics.DrawPath(caneta, caminho);
            }

            caminho.CloseFigure();

            }
        private void F_Login_Load(object sender, EventArgs e){ }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e){ }

        private void pictureBox1_Click(object sender, EventArgs e){ }

        private void label3_Click(object sender, EventArgs e){ }

        private void label2_Click(object sender, EventArgs e) { }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }   
}

