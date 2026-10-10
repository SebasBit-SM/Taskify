using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using GestionTareas.Models;

namespace GestionTareas.ViewModels;

public class TareasViewModel : INotifyPropertyChanged
{
    public ObservableCollection<Tarea> Tareas { get; set; }

    public ObservableCollection<Tarea> TareasFiltradas { get; set; }

    // DATOS DEL FORMULARIO

    private string _nuevoTitulo = string.Empty;

    public string NuevoTitulo
    {
        get => _nuevoTitulo;
        set
        {
            if (_nuevoTitulo != value)
            {
                _nuevoTitulo = value;
                OnPropertyChanged();
            }
        }
    }


    private string _nuevaDescripcion = string.Empty;

    public string NuevaDescripcion
    {
        get => _nuevaDescripcion;
        set
        {
            if (_nuevaDescripcion != value)
            {
                _nuevaDescripcion = value;
                OnPropertyChanged();
            }
        }
    }

    private string _nuevaPrioridad = "Media";

    public string NuevaPrioridad
    {
        get => _nuevaPrioridad;
        set
        {
            if (_nuevaPrioridad != value)
            {
                _nuevaPrioridad = value;

                OnPropertyChanged();

                OnPropertyChanged(nameof(EsPrioridadBaja));

                OnPropertyChanged(nameof(EsPrioridadMedia));

                OnPropertyChanged(nameof(EsPrioridadAlta));

                OnPropertyChanged(nameof(TextoPrioridadBaja));

                OnPropertyChanged(nameof(TextoPrioridadMedia));

                OnPropertyChanged(nameof(TextoPrioridadAlta));
            }
        }
    }

    // PRIORIDADES

    public bool EsPrioridadBaja =>
        NuevaPrioridad == "Baja";

    public bool EsPrioridadMedia =>
        NuevaPrioridad == "Media";

    public bool EsPrioridadAlta =>
        NuevaPrioridad == "Alta";


    public string TextoPrioridadBaja =>
        EsPrioridadBaja
            ? "Baja ✓"
            : "Baja";


    public string TextoPrioridadMedia =>
        EsPrioridadMedia
            ? "Media ✓"
            : "Media";


    public string TextoPrioridadAlta =>
        EsPrioridadAlta
            ? "Alta ✓"
            : "Alta";

    // CONTADORES

    public int TareasPendientes => Tareas.Count(t => !t.EsCompletada);


    public int TareasCompletadas => Tareas.Count(t => t.EsCompletada);

    // FILTROS

    private string _filtroActual = "Todas";

    public string FiltroActual
    {
        get => _filtroActual;
        set
        {
            if (_filtroActual != value)
            {
                _filtroActual = value;

                OnPropertyChanged();

                OnPropertyChanged(nameof(EsFiltroTodas));

                OnPropertyChanged(nameof(EsFiltroPendientes));

                OnPropertyChanged(nameof(EsFiltroCompletadas));

                AplicarFiltro();
            }
        }
    }


    public bool EsFiltroTodas => FiltroActual == "Todas";


    public bool EsFiltroPendientes => FiltroActual == "Pendientes";


    public bool EsFiltroCompletadas => FiltroActual == "Completadas";


    private void AplicarFiltro()
    {
        TareasFiltradas.Clear();

        IEnumerable<Tarea> tareas;

        switch (FiltroActual)
        {
            case "Pendientes":

                tareas =
                    Tareas.Where(t => !t.EsCompletada);

                break;


            case "Completadas":

                tareas = Tareas.Where(t => t.EsCompletada);

                break;


            default:

                tareas = Tareas;

                break;
        }


        foreach (var tarea in tareas)
        {
            TareasFiltradas.Add(tarea);
        }
    }


    // =========================
    // TAREAS RECIENTES
    // =========================

    public IEnumerable<Tarea> TareasRecientes =>
        Tareas.Reverse().Take(3);


    // =========================
    // EDICIÓN
    // =========================

    private Tarea? _tareaEditando;

    public Tarea? TareaEditando
    {
        get => _tareaEditando;

        private set
        {
            if (_tareaEditando != value)
            {
                _tareaEditando = value;

                OnPropertyChanged();
                OnPropertyChanged(
                    nameof(EstaEditando));

                OnPropertyChanged(
                    nameof(TituloFormulario));

                OnPropertyChanged(
                    nameof(EtiquetaFormulario));

                OnPropertyChanged(
                    nameof(TextoBotonFormulario));
            }
        }
    }


    public bool EstaEditando =>
        TareaEditando != null;


    public string TituloFormulario =>
        EstaEditando
            ? "Editar tarea"
            : "Crear tarea";


    public string EtiquetaFormulario =>
        EstaEditando
            ? "EDITAR TAREA"
            : "NUEVA TAREA";


    public string TextoBotonFormulario =>
        EstaEditando
            ? "Guardar cambios"
            : "Crear tarea";


    // =========================
    // COMMANDS
    // =========================

    public ICommand AgregarTareaCommand { get; }

    public ICommand EliminarTareaCommand { get; }

    public ICommand EditarTareaCommand { get; }

    public ICommand GuardarEdicionCommand { get; }

    public ICommand MostrarTodasCommand { get; }

    public ICommand MostrarPendientesCommand { get; }

    public ICommand MostrarCompletadasCommand { get; }


    // =========================
    // CONSTRUCTOR
    // =========================

    public TareasViewModel()
    {
        Tareas =
            new ObservableCollection<Tarea>();

        TareasFiltradas =
            new ObservableCollection<Tarea>();


        AgregarTareaCommand =
            new Command(AgregarTarea);


        EliminarTareaCommand =
            new Command<Tarea>(EliminarTarea);


        EditarTareaCommand =
            new Command<Tarea>(PrepararEdicion);


        GuardarEdicionCommand =
            new Command(GuardarEdicion);


        MostrarTodasCommand =
            new Command(() =>
                FiltroActual = "Todas");


        MostrarPendientesCommand =
            new Command(() =>
                FiltroActual = "Pendientes");


        MostrarCompletadasCommand =
            new Command(() =>
                FiltroActual = "Completadas");


        AplicarFiltro();
    }


    // =========================
    // AGREGAR TAREA
    // =========================

    private void AgregarTarea()
    {
        if (string.IsNullOrWhiteSpace(NuevoTitulo))
            return;


        var nuevaTarea = new Tarea
        {
            Id = Tareas.Count + 1,

            Titulo =
                NuevoTitulo.Trim(),

            Descripcion =
                NuevaDescripcion.Trim(),

            EsCompletada = false,

            Prioridad =
                NuevaPrioridad
        };


        nuevaTarea.PropertyChanged +=
            Tarea_PropertyChanged;


        Tareas.Add(nuevaTarea);


        LimpiarFormulario();


        OnPropertyChanged(
            nameof(TareasPendientes));

        OnPropertyChanged(
            nameof(TareasCompletadas));

        OnPropertyChanged(
            nameof(TareasRecientes));


        AplicarFiltro();
    }


    // =========================
    // PREPARAR EDICIÓN
    // =========================

    private void PrepararEdicion(Tarea tarea)
    {
        if (tarea == null)
            return;


        TareaEditando = tarea;


        NuevoTitulo =
            tarea.Titulo;


        NuevaDescripcion =
            tarea.Descripcion;


        NuevaPrioridad =
            tarea.Prioridad;
    }


    // =========================
    // GUARDAR EDICIÓN
    // =========================

    private void GuardarEdicion()
    {
        if (TareaEditando == null)
            return;


        if (string.IsNullOrWhiteSpace(NuevoTitulo))
            return;


        TareaEditando.Titulo =
            NuevoTitulo.Trim();


        TareaEditando.Descripcion =
            NuevaDescripcion.Trim();


        TareaEditando.Prioridad =
            NuevaPrioridad;


        OnPropertyChanged(
            nameof(TareasRecientes));


        AplicarFiltro();


        LimpiarFormulario();
    }


    // =========================
    // LIMPIAR FORMULARIO
    // =========================

    private void LimpiarFormulario()
    {
        NuevoTitulo = string.Empty;

        NuevaDescripcion = string.Empty;

        NuevaPrioridad = "Media";

        TareaEditando = null;
    }


    // =========================
    // CAMBIO DE ESTADO
    // =========================

    private void Tarea_PropertyChanged(
        object? sender,
        PropertyChangedEventArgs e)
    {
        if (e.PropertyName ==
            nameof(Tarea.EsCompletada))
        {
            OnPropertyChanged(
                nameof(TareasPendientes));

            OnPropertyChanged(
                nameof(TareasCompletadas));

            AplicarFiltro();
        }
    }


    // =========================
    // ELIMINAR
    // =========================

    private void EliminarTarea(Tarea tarea)
    {
        if (Tareas.Contains(tarea))
        {
            tarea.PropertyChanged -=
                Tarea_PropertyChanged;


            Tareas.Remove(tarea);


            OnPropertyChanged(
                nameof(TareasPendientes));

            OnPropertyChanged(
                nameof(TareasCompletadas));

            OnPropertyChanged(
                nameof(TareasRecientes));


            AplicarFiltro();
        }
    }


    // =========================
    // PROPERTY CHANGED
    // =========================

    public event PropertyChangedEventHandler?
        PropertyChanged;


    protected void OnPropertyChanged(
        [CallerMemberName]
        string? propertyName = null)
    {
        PropertyChanged?.Invoke(
            this,
            new PropertyChangedEventArgs(
                propertyName));
    }
}