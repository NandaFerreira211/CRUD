using System.Windows;
using CRUD.Modelos;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Bcpg;

namespace CRUD;

public partial class MeuPerfil : Window
{
    public Usuario UsuarioAtual;

    public MeuPerfil(Usuario usuario)
    {
        InitializeComponent();
        UsuarioAtual = usuario;

        TxtNome.Text = UsuarioAtual.Nome;
        TxtUsuario.Text = UsuarioAtual.Username;
        TxtEmail.Text = UsuarioAtual.Email;
    }

    private void BtnSalvar_OnClick(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(TxtNome.Text))
        {
            MessageBox.Show("Preencha o campo de Nome");
            TxtNome.Focus();
            return;
        }

        if (string.IsNullOrWhiteSpace(TxtUsuario.Text))
        {
            MessageBox.Show("Preencha o campo de usuário!");
            TxtUsuario.Focus();
            return;
        }
        if (string.IsNullOrWhiteSpace(TxtEmail.Text))
        {
            MessageBox.Show("Preencha o campo de Email!");
            TxtEmail.Focus();
            return;
        }

        var senhaFoiAlterada = !string.IsNullOrWhiteSpace(TxtSenha.Password);
        
        UsuarioAtual.Username = TxtUsuario.Text;
        UsuarioAtual.Nome = TxtNome.Text;
        UsuarioAtual.Email = TxtEmail.Text;
        if (senhaFoiAlterada) UsuarioAtual.Senha = TxtSenha.Password;
        
        using var conexao = new MySqlConnection(App.StringConexao);
       var query = "UPDATE usuarios SET username= @username , nome = @nome, email = @email";
        if (senhaFoiAlterada) query += ", senha = @senha";
        
        query += " WHERE id = @id";
        
        using var comando = new MySqlCommand(query, conexao);
        comando.Parameters.AddWithValue("@username", UsuarioAtual.Username);
        comando.Parameters.AddWithValue("@nome", UsuarioAtual.Nome);
        comando.Parameters.AddWithValue("@email", UsuarioAtual.Email);
        comando.Parameters.AddWithValue("@id", UsuarioAtual.Id);
        
        if (senhaFoiAlterada) comando.Parameters.AddWithValue("senha",UsuarioAtual.Senha);
        
        try
        {
            conexao.Open();
            var linhasAfetadas = comando.ExecuteNonQuery();
            if (linhasAfetadas > 0)
            {
                MessageBox.Show("Cadastro atualizado com sucesso!");
            }
            else
            {
                MessageBox.Show("Erro ao atualizar o cadastro!");
            }
            
        }
        catch (Exception exception)
        {
            MessageBox.Show("Erro de DB:");
            
        }
        
        
    }
}