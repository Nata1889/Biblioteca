using EstanteDigital.Views;


namespace EstanteDigital;


public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new NavigationPage(new PaginaInicio());
    }
}
