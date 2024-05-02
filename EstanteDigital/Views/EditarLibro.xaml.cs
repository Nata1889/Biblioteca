using EstanteDigital.Modelos;
using EstanteDigital.Repositories;

namespace EstanteDigital.Views;

public partial class EditarLibro : ContentPage
{
    RepositoryLibros repositoryLibros = new RepositoryLibros();
    public Libros LibroSeleccionado { get; }

    public EditarLibro()
    {
        InitializeComponent();
    }

    public EditarLibro(Libros libroSeleccionado)
    {
        InitializeComponent();
        LibroSeleccionado = libroSeleccionado;
        CargarDatosEnPantalla();
    }

    private void CargarDatosEnPantalla()
    {
        txtTitulo.Text = LibroSeleccionado.titulo;
        txtAutor.Text = LibroSeleccionado.autor;
        txtGenero.Text = LibroSeleccionado.genero;
        txtEditorial.Text = LibroSeleccionado.editorial;
        txtPortadaUrl.Text = LibroSeleccionado.portada;
    }

    private async void GuardarBtn_Clicked(object sender, EventArgs e)
    {
        Libros libroEditado = new Libros()
        {
            _id = LibroSeleccionado._id,
            titulo = txtTitulo.Text,
            autor = txtAutor.Text,
            genero = txtGenero.Text,
            editorial = txtEditorial.Text,
            portada = txtPortadaUrl.Text,
        };


        //enviamos por POST el objeto que creamos a la URL de la API
        var biblioteca = await repositoryLibros.UpdateAsync(libroEditado);


        if (biblioteca)
        {
            await DisplayAlert("Notificación", "Libro guardado correctamente", "OK");
            await Navigation.PopAsync();
        }
    }

    private async void CancelarBtn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}