using MySql.Data.MySqlClient;

namespace CRUD_WindowsForms
{
    class ConexaoBd
    {
        public MySqlConnection Conexao { get; set; }
        public MySqlCommand Comando { get; set; }

        public ConexaoBd(string sqlStr)
        {
            Conexao = new MySqlConnection("Server=localhost;Database=dbcrud;Uid=root;Pwd=root;");
            Comando = new MySqlCommand(sqlStr, Conexao);
            Conexao.Open();
        }

        public void FecharConexao()
        {
            Conexao.Close();
        }
    }
}
