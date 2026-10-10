using GestionTareas.ViewModels;

namespace GestionTareas.Views;

public partial class AjustesPage : ContentPage
{
    private readonly TareasViewModel _viewModel;
    private bool _pageAnimated = false;

    public AjustesPage(TareasViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel;
    }

    // ANIMACIÓN DE ENTRADA

    private async void OnPageLoaded(
        object sender,
        EventArgs e)
    {
        if (_pageAnimated)
            return;

        _pageAnimated = true;

        HeaderSection.TranslationY = 15;
        GeneralSection.TranslationY = 18;
        PreferencesSection.TranslationY = 21;
        AboutSection.TranslationY = 24;

        await Task.WhenAll(
            HeaderSection.FadeTo(1, 220),
            HeaderSection.TranslateTo(0, 0, 220)
        );

        await Task.WhenAll(
            GeneralSection.FadeTo(1, 250),
            GeneralSection.TranslateTo(0, 0, 250)
        );

        await Task.WhenAll(
            PreferencesSection.FadeTo(1, 280),
            PreferencesSection.TranslateTo(0, 0, 280)
        );

        await Task.WhenAll(
            AboutSection.FadeTo(1, 300),
            AboutSection.TranslateTo(0, 0, 300)
        );
    }

    // NAVEGACIÓN

    private async void OnInicioTapped(
        object sender,
        TappedEventArgs e)
    {
        await Navigation.PushAsync(
            new InicioPage(_viewModel));
    }


    private async void OnTareasTapped(
        object sender,
        TappedEventArgs e)
    {
        await Navigation.PushAsync(
            new TareasPage(_viewModel));
    }
}