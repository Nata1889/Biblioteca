using EstanteDigital.Modelos;
using EstanteDigital.Repositories;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace EstanteDigital.Views;

public partial class InicioApp : ContentPage
{
    public ObservableCollection<Libros> libros { get; set; }
    public Libros LibroSeleccionado { get; set; }
    RepositoryLibros repositoryLibros = new RepositoryLibros();
    public InicioApp()
	{
		InitializeComponent();
        libros = new ObservableCollection<Libros>();
        LibrosCollectionView.SelectionChanged += SeleccionarLibro;
    }

    private void SeleccionarLibro(object sender, SelectionChangedEventArgs e)
    {
        if (LibrosCollectionView.SelectedItems != null)
        {
            LibroSeleccionado = (Libros)LibrosCollectionView.SelectedItem;
        }
    }

    public async void GetAllLibros(object sender, EventArgs e)
    {
        libros = await repositoryLibros.GetAllAsync();
        LibrosCollectionView.ItemsSource = libros;
    }

    private void SeleccionarLibroEnCollectionView()
    {
        foreach (var libros in libros)
        {
            if (libros._id == LibroSeleccionado._id)
            {
                LibrosCollectionView.SelectedItem = libros;
                break;
            }
        }
    }

    protected override void OnAppearing()
    {
        NetworkAccess conexionInternet = Connectivity.Current.NetworkAccess;

        if (conexionInternet == NetworkAccess.Internet)
        {
            GetAllLibros(this, EventArgs.Empty);
        }

    }

    protected override bool OnBackButtonPressed()
    {
        Debug.Print(">>>>>>>>>>>>>>>>>>>>>>> se ha pulsado el botón de volver hacia atrás");
        return false;
    }
    protected override void OnDisappearing()
    {
        Debug.Print(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> se ha cerrado la ventana de la lista de Libros");
    }

    private async void EditarLibro_Clicked(Object sender, EventArgs e)
    {
        if (LibroSeleccionado != null)
        {
            await Navigation.PushAsync(new EditarLibro(LibroSeleccionado));
        }
        else
        {
            await Application.Current.MainPage.DisplayAlert("Editar", "Error: Debe seleccionar el libro que desea editar", "ok");
        }
    }

    private async void EliminarLibro_Clicked(object sender, EventArgs e)
    {
        if (LibroSeleccionado != null)
        {
            LibroSeleccionado = (Libros)LibrosCollectionView.SelectedItem;
            bool respuesta = await Application.Current.MainPage.DisplayAlert("Eliminar",
                $"¿Está seguro que desea Eliminar el libro: {LibroSeleccionado.titulo}?", "Si", "No");
            if (respuesta)
            {
                var eliminado = await repositoryLibros.RemoveAsync(LibroSeleccionado._id);
                if (eliminado)
                {
                    await Application.Current.MainPage.DisplayAlert("Eliminar",
                        $"Se ha eliminado el libro: {LibroSeleccionado.titulo} correctamente", "Ok");
                    GetAllLibros(this, EventArgs.Empty);
                }
            }
        }
        else
        {
            await Application.Current.MainPage.DisplayAlert("Eliminar",
                "Error: Debe seleccionar el libro a eliminar", "Ok");
        }
    }

    private async void AgregarLibro_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AgregarLibro());
    }
}