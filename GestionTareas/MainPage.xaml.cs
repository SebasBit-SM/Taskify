using GestionTareas.ViewModels;
using GestionTareas.Views;

namespace GestionTareas;

public partial class MainPage : ContentPage
{
    private readonly TareasViewModel _viewModel;

    private bool _pageAnimated = false;


    public MainPage(TareasViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel;
    }

    // APARICIÓN

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (_pageAnimated)
            return;

        _pageAnimated = true;

        HeaderSection.TranslationY = -15;

        FormSection.TranslationY = 20;

        LoginButton.TranslationY = 20;


        await Task.WhenAll(
            HeaderSection.FadeTo(1, 250),
            HeaderSection.TranslateTo(0, 0, 250)
        );


        await Task.WhenAll(
            FormSection.FadeTo(1, 300),
            FormSection.TranslateTo(0, 0, 300)
        );


        await Task.WhenAll(
            LoginButton.FadeTo(1, 300),
            LoginButton.TranslateTo(0, 0, 300)
        );
    }

    // INICIAR SESIÓN

    private async void OnIniciarSesionClicked(
        object sender,
        EventArgs e)
    {
        string usuario = UsuarioEntry.Text?.Trim()
            ?? string.Empty;

        string clave = ClaveEntry.Text
            ?? string.Empty;

        // VALIDACIÓN

        if (usuario == "admin" &&
            clave == "admin")
        {
            ErrorContainer.IsVisible = false;

            await LoginButton.ScaleTo(
                0.96,
                70);

            await LoginButton.ScaleTo(
                1,
                70);

            UsuarioEntry.Text = string.Empty;

            ClaveEntry.Text = string.Empty;

            await Navigation.PushAsync(
                new InicioPage(
                    _viewModel));

            return;
        }

        // CREDENCIALES INCORRECTAS

        ErrorContainer.IsVisible = true;


        // Pequeño efecto de error

        await Task.WhenAll(
            UsuarioEntry.TranslateTo(-5, 0, 50),
            ClaveEntry.TranslateTo(-5, 0, 50)
        );

        await Task.WhenAll(
            UsuarioEntry.TranslateTo(5, 0, 50),
            ClaveEntry.TranslateTo(5, 0, 50)
        );

        await Task.WhenAll(
            UsuarioEntry.TranslateTo(-3, 0, 50),
            ClaveEntry.TranslateTo(-3, 0, 50)
        );

        await Task.WhenAll(
            UsuarioEntry.TranslateTo(0, 0, 50),
            ClaveEntry.TranslateTo(0, 0, 50)
        );


        ClaveEntry.Text = string.Empty;

        ClaveEntry.Focus();
    }

    // MOSTRAR / OCULTAR

    private void OnMostrarClaveClicked(
        object sender,
        EventArgs e)
    {
        ClaveEntry.IsPassword = !ClaveEntry.IsPassword;


        MostrarClaveButton.Text = ClaveEntry.IsPassword
                ? "Mostrar"
                : "Ocultar";
    }
}