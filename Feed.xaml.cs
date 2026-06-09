using System.Windows;
using CRUD.Modelos;
using MySql.Data.MySqlClient;

namespace CRUD;

public partial class Feed : Window
{
    public Feed()
    {
        InitializeComponent();
    }

    private void CarregarPosts_QuandoIniciar()
    {
        List<Postagem> listaPostagens = [];

        const string query = "SELECT p.id, p.conteudo, p.curtidas, p.postado_em, u.nome, u.username FROM postagens p INNER JOIN usuarios u ON  p.usuario_id = u.id";

        using var conexao = new MySqlConnection(App.StringConexao);

        using var comando = new MySqlCommand(query, conexao);
    }
    
}