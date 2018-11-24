using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Plugin.Media;

namespace AppBibliotecaUnopar.Droid
{
    [Activity(Label = "Biblioteca Fagep/Unopar", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override async void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            await CrossMedia.Current.Initialize();

            global::Xamarin.Forms.Forms.Init(this, bundle);

            AppBibliotecaUnopar.Infraestructure.Database.Root = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

            LoadApplication(new App());
        }
    }
}

