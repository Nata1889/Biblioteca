namespace EstanteDigital.Views;

public partial class PaginaInicio : ContentPage
{
	public PaginaInicio()
	{
		InitializeComponent();
	}

    private async void Libros_Clicked(object sender, EventArgs e)
    {
       await Navigation.PushAsync(new InicioApp());
    }
}