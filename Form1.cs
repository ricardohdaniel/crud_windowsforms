﻿using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

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
                con.FecharConexao();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro no cadastro: " + erro.Message);
                txtNome.Focus();
            }

        }

        private void LimparForm()
        {
            txtId.Clear();
            txtNome.Clear();
            txtDataNasc.Clear();
            txtEmail.Clear();
            txtTelefone.Clear();
            txtNome.Focus();
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            try
            {
                txtId.Enabled = true;

                string strSql = "UPDATE clientes SET nome=@nome, datanasc=@datanasc, email=@email, telefone=@telefone WHERE id = @id";
                ConexaoBd con = new ConexaoBd(strSql);
                con.Comando.Parameters.AddWithValue("@nome", txtNome.Text);
                con.Comando.Parameters.AddWithValue("@datanasc", txtDataNasc.Text);
                con.Comando.Parameters.AddWithValue("@email", txtEmail.Text);
                con.Comando.Parameters.AddWithValue("@telefone", txtTelefone.Text);
                con.Comando.Parameters.AddWithValue("@id", MySqlDbType.Int32).Value = txtId.Text;
                con.Comando.ExecuteNonQuery();

                MessageBox.Show("Alteração efetuada com sucesso!");
                LimparForm();
                con.FecharConexao();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro: " + erro.Message);
                txtNome.Focus();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            txtId.Visible = true;
            txtId.Focus();
            if (txtId.TextLength == 0)
            {
                MessageBox.Show("Preencha o campo ID para realizar a busca");
            }
            else
            {
                try
                {
                    string strSql = "SELECT * from clientes WHERE id=@id ";
                    ConexaoBd con = new ConexaoBd(strSql);
                    con.Comando.Parameters.AddWithValue("@id", MySqlDbType.Int32).Value = txtId.Text;
                    MySqlDataReader dr = con.Comando.ExecuteReader();

                    if (dr.Read())
                    {
                        txtNome.Text = dr["nome"].ToString();
                        txtDataNasc.Text = dr["datanasc"].ToString();
                        txtEmail.Text = dr["email"].ToString();
                        txtTelefone.Text = dr["telefone"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("ID não localizado no Banco de Dados");
                        LimparForm();
                        txtId.Focus();
                    }
                    con.FecharConexao();
                }
                catch (Exception erro)
                {
                    MessageBox.Show("Erro na consulta: " + erro.Message);
                    LimparForm();
                    txtId.Focus();
                }
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            txtId.Visible = true;
            txtId.Focus();
            if (txtId.TextLength == 0)
            {
                MessageBox.Show("Preencha o campo ID para realizar a exclusão");
            }
            else
            {
                try
                {
                    string strSql = "DELETE from clientes WHERE id=@id ";
                    ConexaoBd con = new ConexaoBd(strSql);
                    con.Comando.Parameters.AddWithValue("@id", MySqlDbType.Int32).Value = txtId.Text;
                    con.Comando.ExecuteNonQuery();
                    con.FecharConexao();
                    MessageBox.Show("Exclusão efetuada com sucesso!");
                    LimparForm();
                }
                catch (Exception erro)
                {
                    MessageBox.Show("Erro na exclusão do cadastro: " + erro.Message);
                    LimparForm();
                    txtId.Focus();
                }
            }
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            try
            {


                string strSql = "SELECT * from clientes";
                ConexaoBd con = new ConexaoBd(strSql);
                MySqlDataReader dr = con.Comando.ExecuteReader();

                int i = 0;

                while (dr.Read())

                {

                    dgvLista.Rows.Add();
                    dgvLista.Rows[i].Cells["id"].Value = Convert.ToString(dr["id"]);
                    dgvLista.Rows[i].Cells["nome"].Value = Convert.ToString(dr["nome"]);
                    dgvLista.Rows[i].Cells["datanasc"].Value = Convert.ToString(dr["datanasc"]);
                    dgvLista.Rows[i].Cells["email"].Value = Convert.ToString(dr["email"]);
                    dgvLista.Rows[i].Cells["telefone"].Value = Convert.ToString(dr["telefone"]);
                    i++;
                }
                con.FecharConexao();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro --> " + erro.Message);
            }
        }
    }
}
