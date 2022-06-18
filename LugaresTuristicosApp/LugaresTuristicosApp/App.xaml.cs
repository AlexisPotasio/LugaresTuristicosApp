using LugaresTuristicosApp.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LugaresTuristicosApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LugarView());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
