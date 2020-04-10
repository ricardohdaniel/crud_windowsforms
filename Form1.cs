using System;
using System.Windows.Forms;

namespace CRUD_WindowsForms
{
    public partial class frmInicial : Form
    {
        public frmInicial()
        {
            InitializeComponent();
        }

        private void frmInicial_Load(object sender, EventArgs e)
        {

        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            try
            {
                string strSql = "INSERT INTO clientes (nome, datanasc, email, telefone) VALUES (@nome, @datanasc, @email, @telefone)";
                ConexaoBd con = new ConexaoBd(strSql);
                con.Comando.Parameters.AddWithValue("@nome", txtNome.Text);
                con.Comando.Parameters.AddWithValue("@datanasc", txtDataNasc.Text);
                con.Comando.Parameters.AddWithValue("@email", txtEmail.Text);
                con.Comando.Parameters.AddWithValue("@telefone", txtTelefone.Text);
                con.Comando.ExecuteNonQuery();

                MessageBox.Show("Cliente cadastrado com sucesso!");
                LimparForm();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro no cadastro: " + erro.Message);
                txtNome.Focus();
            }
            
        }

        private void LimparForm()
        {
            txtNome.Clear();
            txtDataNasc.Clear();
            txtEmail.Clear();
            txtTelefone.Clear();
            txtNome.Focus();
        }
    }
}
