using System.Windows;
using MySql.Data.MySqlClient;

namespace CRUD;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class Cadastro : Window
{
    public string stringConexao = Environment.GetEnvironmentVariable("MYSQL_STRING");


    public Cadastro()
    {
        InitializeComponent();
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrEmpty(TxtNome.Text) ||
            string.IsNullOrWhiteSpace(TxtUsername.Text) ||
            string.IsNullOrWhiteSpace(TxtEmail.Text) ||
            string.IsNullOrWhiteSpace(TxtSenha.Password))
        {
            MessageBox.Show("Todos os campos são obrigatórios.", "Error!");
            return;
        }

        using (var conexao = new MySqlConnection(stringConexao))
        {
            var query = "INSERT INTO usuarios(nome,username,email,senha)\nVALUES(@nome,@username,@email,@senha)";

            using (var comando = new MySqlCommand(query, conexao))
            {
                comando.Parameters.AddWithValue("@nome", TxtNome.Text);
                comando.Parameters.AddWithValue("@username", TxtUsername.Text);
                comando.Parameters.AddWithValue("@email", TxtEmail.Text);
                comando.Parameters.AddWithValue("@senha", TxtSenha.Password);

                try
                {
                    conexao.Open();

                    var linhasAfetadas = comando.ExecuteNonQuery();
                    if (linhasAfetadas > 0) MessageBox.Show("Cadastro realizado com sucesso!");
                }
                catch (Exception exception)
                {
                    if (exception is MySqlException erroSql)
                    {
                        if (erroSql.Number == 1062)
                        {
                            MessageBox.Show("O email ou username já foram utilizados");
                        }
                    }

                    Console.WriteLine(exception);
                }
            }
        }
    }
}