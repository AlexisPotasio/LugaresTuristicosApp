using LugaresTuristicosApp.Models;
using LugaresTuristicosApp.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace LugaresTuristicosApp.ViewModel
{
    public class LugarViewMode : INotifyPropertyChanged
    {
        
        public Lugar Lugar { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<Lugar> Lugares { get; set; } = new ObservableCollection<Lugar>();
        public ICommand OrdenarAZCommand { get; set; }
        public ICommand OrdenarZACommand { get; set; }
        public ICommand AgregarCommand { get; set; }
        public ICommand CambiarVistaCommand { get; set; }
        public ICommand VerDetallesCommand { get; set; }
        public ICommand EliminarCommand { get; set; }
        public ICommand GuardarCommand { get; set; }
        public ICommand ModificarCommand { get; set; }
        public string Error { get; set; } = ""; 


        public LugarViewMode()
        {
            Abrir();
            AgregarCommand = new Command(Agregar);
            CambiarVistaCommand = new Command<string>(CambiarVista);
            EliminarCommand = new Command<Lugar>(Eliminar);
            VerDetallesCommand = new Command<Lugar>(VerDetalles);
            ModificarCommand = new Command<Lugar>(Modificar);
            OrdenarAZCommand = new Command(OrdenarAZ);
            OrdenarZACommand = new Command(OrdenarZA);
            GuardarCommand = new Command(Guardar);
        }

        private void OrdenarZA()
        {
            Lugares = new ObservableCollection<Lugar>(Lugares.OrderByDescending(x => x.Nombre));
            PropertyChanged(this, new PropertyChangedEventArgs(null));
        }

        public void OrdenarAZ()
        {
            Lugares = new ObservableCollection<Lugar>(Lugares.OrderBy(x => x.Nombre));
            PropertyChanged(this, new PropertyChangedEventArgs(null));
        }

        AgregarLugarView AgregarLugar;
        EditarLugarView EditarLugar;
        DetallesLugarView DetalleLugar;

        int posicion;
        private void Modificar(Lugar Lugar)
        {
            posicion = Lugares.IndexOf(Lugar);
            this.Lugar = new Lugar
            {
                Nombre = Lugar.Nombre,
                Descripcion = Lugar.Descripcion,
                Imagen = Lugar.Imagen,
                Dirección = Lugar.Dirección
            };

            EditarLugar = new EditarLugarView() { BindingContext = this };
            Application.Current.MainPage.Navigation.PushAsync(EditarLugar);
        }

        private void Guardar()
        {
            Error = Validar();
            if (string.IsNullOrWhiteSpace(Error))
            {
                Lugares[posicion] = Lugar;
                Serializar();
                Application.Current.MainPage.Navigation.PopToRootAsync();                
            }
        }

        private void VerDetalles(Lugar Lugar)
        {
            this.Lugar = Lugar;
            DetalleLugar = new DetallesLugarView() { BindingContext = this };
            Application.Current.MainPage.Navigation.PushAsync(DetalleLugar);
            PropertyChanged(this, new PropertyChangedEventArgs(null));
        }

        private void Eliminar(Lugar Lugar)
        {
            Lugares.Remove(Lugar);
            Serializar();
            Application.Current.MainPage.Navigation.PopToRootAsync();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }

        private void Agregar()
        {
            Error = Validar();
            if (string.IsNullOrWhiteSpace(Error))
            {
                Lugares.Add(Lugar);
                Serializar();
                CambiarVista("ver");                
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }

        private void CambiarVista(string Vista)
        {
            if (Vista == "agregar")
            {
                Lugar = new Lugar();
                AgregarLugar = new AgregarLugarView() { BindingContext = this };
                Application.Current.MainPage.Navigation.PushAsync(AgregarLugar);
                PropertyChanged(this, new PropertyChangedEventArgs(null));
            }
            if (Vista == "ver")
            {
                Application.Current.MainPage.Navigation.PopToRootAsync();
            }
        }

        private void Serializar()
        {
            var file = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "Lugares.json";
            File.WriteAllText(file, JsonConvert.SerializeObject(Lugares));
        }
        private void Abrir()
        {
            var file = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "Lugares.json";
            if (File.Exists(file))
            {
                Lugares = JsonConvert.DeserializeObject<ObservableCollection<Lugar>>(File.ReadAllText(file));
            }
        }
        private string Validar()
        {
            if (string.IsNullOrWhiteSpace(Lugar.Nombre))
            {
                return "Escriba el nombre del lugar";

            }

            if (string.IsNullOrWhiteSpace(Lugar.Dirección))
            {
                return "Agregue una direccion del lugar";
            }

            if (string.IsNullOrWhiteSpace(Lugar.Descripcion))
            {
                return "Agregue una descripción del lugar";
            }

            if (string.IsNullOrWhiteSpace(Lugar.Imagen))
            {
                return "Agregue el URL del lugar";
            }
            if (!Uri.TryCreate(Lugar.Imagen, UriKind.Absolute, out var uri))
            {
                return "Agregue una URL válida";
            }
            return "";
        }
    }
}