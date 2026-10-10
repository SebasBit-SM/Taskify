using GestionTareas.Models;
using GestionTareas.ViewModels;

namespace GestionTareas.Views;

public partial class InicioPage : ContentPage
{
    private readonly TareasViewModel _viewModel;

    public InicioPage(TareasViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel;

        BindingContext = viewModel;
    }

    // ANIMACIÓN DE ENTRADA

    private async void OnPageLoaded(
        object sender,
        EventArgs e)
    {
        HeaderSection.TranslationY = 15;
        SummaryCard.TranslationY = 20;
        RecentHeader.TranslationY = 20;
        NewTaskButton.TranslationY = 20;

        await HeaderSection.FadeTo(1, 250);
        await HeaderSection.TranslateTo(0, 0, 250);

        await Task.WhenAll(
            SummaryCard.FadeTo(1, 300),
            SummaryCard.TranslateTo(0, 0, 300)
        );

        await Task.WhenAll(
            RecentHeader.FadeTo(1, 300),
            RecentHeader.TranslateTo(0, 0, 300)
        );

        await Task.WhenAll(
            NewTaskButton.FadeTo(1, 300),
            NewTaskButton.TranslateTo(0, 0, 300)
        );
    }

    // NAVEGACIÓN

    private async void OnVerTodasClicked(
        object sender,
        EventArgs e)
    {
        await Navigation.PushAsync(
            new TareasPage(_viewModel));
    }


    private async void OnNuevaTareaClicked(
        object sender,
        EventArgs e)
    {
        await NewTaskButton.ScaleTo(0.96, 80);
        await NewTaskButton.ScaleTo(1, 80);

        await Navigation.PushAsync(
            new NuevaTareaPage(_viewModel));
    }


    private async void OnTareasTapped(
        object sender,
        TappedEventArgs e)
    {
        await Navigation.PushAsync(
            new TareasPage(_viewModel));
    }


    private async void OnAjustesTapped(
        object sender,
        TappedEventArgs e)
    {
        await Navigation.PushAsync(
            new AjustesPage(_viewModel));
    }
}