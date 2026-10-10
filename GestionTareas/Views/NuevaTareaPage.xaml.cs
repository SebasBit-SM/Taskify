using GestionTareas.ViewModels;

namespace GestionTareas.Views;

public partial class NuevaTareaPage : ContentPage
{
    private readonly TareasViewModel _viewModel;
    private bool _pageAnimated = false;

    public NuevaTareaPage(TareasViewModel viewModel)
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
        if (_pageAnimated)
            return;

        _pageAnimated = true;

        HeaderSection.TranslationY = 15;
        TitleSection.TranslationY = 18;
        PrioritySection.TranslationY = 21;
        PreviewSection.TranslationY = 24;
        CreateButton.TranslationY = 20;

        await Task.WhenAll(
            HeaderSection.FadeTo(1, 220),
            HeaderSection.TranslateTo(0, 0, 220)
        );

        await Task.WhenAll(
            TitleSection.FadeTo(1, 250),
            TitleSection.TranslateTo(0, 0, 250)
        );

        await Task.WhenAll(
            PrioritySection.FadeTo(1, 280),
            PrioritySection.TranslateTo(0, 0, 280)
        );

        await Task.WhenAll(
            PreviewSection.FadeTo(1, 300),
            PreviewSection.TranslateTo(0, 0, 300)
        );

        await Task.WhenAll(
            CreateButton.FadeTo(1, 300),
            CreateButton.TranslateTo(0, 0, 300)
        );
    }

    // CREAR / EDITAR

    private async void OnCrearTareaClicked(
        object sender,
        EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(
            _viewModel.NuevoTitulo))
        {
            await CreateButton.TranslateTo(
                -5, 0, 50);

            await CreateButton.TranslateTo(
                5, 0, 50);

            await CreateButton.TranslateTo(
                -3, 0, 50);

            await CreateButton.TranslateTo(
                0, 0, 50);

            return;
        }


        await CreateButton.ScaleTo(
            0.96, 70);

        await CreateButton.ScaleTo(
            1, 70);


        if (_viewModel.EstaEditando)
        {
            _viewModel.GuardarEdicionCommand
                .Execute(null);
        }
        else
        {
            _viewModel.AgregarTareaCommand
                .Execute(null);
        }


        await Navigation.PopAsync();
    }

    // PRIORIDAD BAJA

    private async void OnBajaTapped(
        object sender,
        TappedEventArgs e)
    {
        _viewModel.NuevaPrioridad = "Baja";

        await AnimarPrioridad(BajaCard);
    }

    // PRIORIDAD MEDIA

    private async void OnMediaTapped(
        object sender,
        TappedEventArgs e)
    {
        _viewModel.NuevaPrioridad = "Media";

        await AnimarPrioridad(MediaCard);
    }

    // PRIORIDAD ALTA

    private async void OnAltaTapped(
        object sender,
        TappedEventArgs e)
    {
        _viewModel.NuevaPrioridad = "Alta";

        await AnimarPrioridad(AltaCard);
    }

    // ANIMACIÓN PRIORIDAD

    private async Task AnimarPrioridad(
        Border card)
    {
        await card.ScaleTo(
            1.04, 80);

        await card.ScaleTo(
            1, 100);

        await PreviewCard.ScaleTo(
            1.015, 80);

        await PreviewCard.ScaleTo(
            1, 100);
    }
}