using EstanteDigital.Modelos;
using EstanteDigital.Repositories;

namespace EstanteDigital.Views;

public partial class AgregarLibro : ContentPage
{
    RepositoryLibros repositoryLibros = new RepositoryLibros();

    public AgregarLibro()
    {
        InitializeComponent();
    }

    private async void GuardarBtn_Clicked(object sender, EventArgs e)
    {
        Libros nuevoLibro = new Libros()
        {
            titulo = txtTitulo.Text,
            autor = txtAutor.Text,
            genero = txtGenero.Text,
            editorial = txtGenero.Text,
            portada = txtPortadaUrl.Text,
        };


        //enviamos por POST el objeto que creamos a la URL de la API
        var biblioteca = await repositoryLibros.AddAsync(nuevoLibro);


        if (biblioteca)
        {
            await DisplayAlert("Notificación", "Libro Cargado", "OK");
            await Navigation.PopAsync();
        }
    }

    private async void CancelarBtn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}