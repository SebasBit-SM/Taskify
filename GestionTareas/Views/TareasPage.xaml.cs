using GestionTareas.ViewModels;

namespace GestionTareas.Views;

public partial class TareasPage : ContentPage
{
    private readonly TareasViewModel _viewModel;

    private bool _pageAnimated = false;


    public TareasPage(TareasViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel;

        BindingContext = viewModel;
    }

    // APARICIÓN DE LA PÁGINA

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        ActualizarFiltroVisual();


        if (_pageAnimated)
            return;


        _pageAnimated = true;


        HeaderSection.TranslationY = 12;

        FilterSection.TranslationY = 15;

        SummarySection.TranslationY = 18;

        TaskSection.TranslationY = 20;

        NewTaskButton.TranslationY = 20;


        await Task.WhenAll(
            HeaderSection.FadeTo(1, 220),
            HeaderSection.TranslateTo(0, 0, 220)
        );


        await Task.WhenAll(
            FilterSection.FadeTo(1, 250),
            FilterSection.TranslateTo(0, 0, 250)
        );


        await Task.WhenAll(
            SummarySection.FadeTo(1, 280),
            SummarySection.TranslateTo(0, 0, 280)
        );


        await Task.WhenAll(
            TaskSection.FadeTo(1, 300),
            TaskSection.TranslateTo(0, 0, 300)
        );


        await Task.WhenAll(
            NewTaskButton.FadeTo(1, 300),
            NewTaskButton.TranslateTo(0, 0, 300)
        );
    }

    // FILTROS

    private void OnTodasTapped(
        object sender,
        TappedEventArgs e)
    {
        _viewModel.MostrarTodasCommand.Execute(null);

        ActualizarFiltroVisual();
    }


    private void OnPendientesTapped(
        object sender,
        TappedEventArgs e)
    {
        _viewModel.MostrarPendientesCommand.Execute(null);

        ActualizarFiltroVisual();
    }


    private void OnCompletadasTapped(
        object sender,
        TappedEventArgs e)
    {
        _viewModel.MostrarCompletadasCommand.Execute(null);

        ActualizarFiltroVisual();
    }


    private async void ActualizarFiltroVisual()
    {
        TodasFilter.Background =
            _viewModel.FiltroActual == "Todas"
                ? (Color)Application.Current.Resources["SelectedBackground"]
                : (Color)Application.Current.Resources["Surface"];


        PendientesFilter.Background =
            _viewModel.FiltroActual == "Pendientes"
                ? (Color)Application.Current.Resources["SelectedBackground"]
                : (Color)Application.Current.Resources["Surface"];


        CompletadasFilter.Background =
            _viewModel.FiltroActual == "Completadas"
                ? (Color)Application.Current.Resources["SelectedBackground"]
                : (Color)Application.Current.Resources["Surface"];


        await Task.WhenAll(

            TodasFilter.ScaleTo(
                _viewModel.FiltroActual == "Todas"
                    ? 1.02
                    : 1,
                100),

            PendientesFilter.ScaleTo(
                _viewModel.FiltroActual == "Pendientes"
                    ? 1.02
                    : 1,
                100),

            CompletadasFilter.ScaleTo(
                _viewModel.FiltroActual == "Completadas"
                    ? 1.02
                    : 1,
                100)
        );
    }

    // EDITAR TAREA

    private async void OnEditarTareaClicked(
        object sender,
        EventArgs e)
    {
        if (sender is not Button button)
            return;


        if (button.CommandParameter
            is not Models.Tarea tarea)
            return;

        await button.ScaleTo(
            0.90,
            60);

        await button.ScaleTo(
            1,
            60);

        _viewModel.EditarTareaCommand
            .Execute(tarea);

        await Navigation.PushAsync(
            new NuevaTareaPage(
                _viewModel));
    }

    // ELIMINAR TAREA

    private async void OnEliminarTareaClicked(
        object sender,
        EventArgs e)
    {
        if (sender is not Button button)
            return;


        if (button.CommandParameter
            is not Models.Tarea tarea)
            return;

        // ANIMACIÓN DEL BOTÓN

        await button.ScaleTo(
            0.90,
            60);

        await button.ScaleTo(
            1,
            60);

        // CONFIRMACIÓN

        bool confirmar =
            await DisplayAlert(
                "Eliminar tarea",
                $"¿Quieres eliminar \"{tarea.Titulo}\"?",
                "Eliminar",
                "Cancelar");

        if (!confirmar)
            return;

        // BUSCAR TARJETA

        if (button.Parent
            is VerticalStackLayout acciones
            && acciones.Parent
                is Grid grid
            && grid.Parent
                is Border tarjeta)
        {

            await Task.WhenAll(

                tarjeta.ScaleTo(
                    0.92,
                    140),

                tarjeta.FadeTo(
                    0,
                    160),

                tarjeta.TranslateTo(
                    25,
                    0,
                    160)
            );
        }

        // ELIMINAR

        _viewModel.EliminarTareaCommand
            .Execute(tarea);

        // ACTUALIZAR FILTRO

        ActualizarFiltroVisual();
    }

    // COMPLETAR TAREA

    private async void OnTareaCompletadaChanged(
        object sender,
        CheckedChangedEventArgs e)
    {
        if (sender is not CheckBox checkBox)
            return;


        if (checkBox.BindingContext
            is not Models.Tarea tarea)
            return;

        if (e.Value)
        {
            await checkBox.ScaleTo(
                1.25,
                100);

            await checkBox.ScaleTo(
                1,
                120);
        }
    }

    // NUEVA TAREA

    private async void OnNuevaTareaClicked(
        object sender,
        EventArgs e)
    {
        await NewTaskButton.ScaleTo(
            0.96,
            70);

        await NewTaskButton.ScaleTo(
            1,
            70);


        await Navigation.PushAsync(
            new NuevaTareaPage(
                _viewModel));
    }

    // NAVEGACIÓN

    private async void OnInicioTapped(
        object sender,
        TappedEventArgs e)
    {
        await Navigation.PushAsync(
            new InicioPage(
                _viewModel));
    }


    private async void OnAjustesTapped(
        object sender,
        TappedEventArgs e)
    {
        await Navigation.PushAsync(
            new AjustesPage(
                _viewModel));
    }
}