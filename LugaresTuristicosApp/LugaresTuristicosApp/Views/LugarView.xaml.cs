using LugaresTuristicosApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LugaresTuristicosApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LugarView : ContentPage
    {
        public LugarView()
        {
            InitializeComponent();
        }
        private async void SwipeItem_Clicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("Por favor, Confirmar eliminación", "¿Está usted seguro de eliminar?", "Sí, Eliminar", "No, Cancelar") == true)
            {
                ((LugarViewMode)BindingContext).EliminarCommand.Execute(((SwipeItem)sender).CommandParameter);
            }
        }

        private void OrdA_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (OrdA.IsChecked == true)
            {
                ((LugarViewMode)BindingContext).OrdenarAZCommand.Execute(null);
            }
        }

        private void OrdB_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (OrdB.IsChecked == true)
            {
                ((LugarViewMode)BindingContext).OrdenarZACommand.Execute(null);
            }
        }
    }
}