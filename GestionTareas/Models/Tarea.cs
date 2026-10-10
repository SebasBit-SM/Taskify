using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Graphics;

namespace GestionTareas.Models;

public class Tarea : INotifyPropertyChanged
{
    public int Id { get; set; }

    // TÍTULO

    private string _titulo = string.Empty;

    public string Titulo
    {
        get => _titulo;
        set
        {
            if (_titulo != value)
            {
                _titulo = value;
                OnPropertyChanged();
            }
        }
    }

    // DESCRIPCIÓN

    private string _descripcion = string.Empty;

    public string Descripcion
    {
        get => _descripcion;
        set
        {
            if (_descripcion != value)
            {
                _descripcion = value;
                OnPropertyChanged();
            }
        }
    }

    // PRIORIDAD

    private string _prioridad = "Media";

    public string Prioridad
    {
        get => _prioridad;
        set
        {
            if (_prioridad != value)
            {
                _prioridad = value;

                OnPropertyChanged();
                OnPropertyChanged(nameof(PrioridadTexto));
                OnPropertyChanged(nameof(PrioridadIcono));
                OnPropertyChanged(nameof(PrioridadColor));
            }
        }
    }

    // ESTADO

    private bool _esCompletada;

    public bool EsCompletada
    {
        get => _esCompletada;
        set
        {
            if (_esCompletada != value)
            {
                _esCompletada = value;

                OnPropertyChanged();
                OnPropertyChanged(nameof(EstadoTexto));
            }
        }
    }

    // TEXTOS

    public string EstadoTexto =>
        EsCompletada
            ? "Completada"
            : "Pendiente";


    public string PrioridadTexto =>
        $"Prioridad: {Prioridad}";


    public string PrioridadIcono =>
        "●";

    // COLOR DE PRIORIDAD

    public Color PrioridadColor =>
        Prioridad switch
        {
            "Baja" => Colors.Green,
            "Media" => Colors.Orange,
            "Alta" => Colors.Red,
            _ => Colors.Gray
        };

    // PROPERTY CHANGED

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