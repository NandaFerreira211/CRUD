using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CRUD.Modelos;

public class Postagem : INotifyPropertyChanged
{
    public int Id { get; set; }
    public string Conteudo  { get; set; }
    public int Curtidas  { get; set; }
    public DateTime Postado_em  { get; set; }
    public Usuario Usuario { get; set; }
    public event PropertyChangedEventHandler? PropertyChanged;
    private bool _FoiCurtido;
    
    public bool FoiCurtido
    {
        get => _FoiCurtido;
        set
        {
            if (_FoiCurtido != value)
            {
                _FoiCurtido = value;
                NotificarPropriedadeAlterada();
            }
        }
    }

    private void NotificarPropriedadeAlterada([CallerMemberName] string nomePropriedade = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nomePropriedade));
    }
}