using AppBibliotecaUnopar.Infraestructure;
using AppBibliotecaUnopar.Models;
using AppBibliotecaUnopar.Views;
using PCLStorage;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppBibliotecaUnopar.ViewModels
{
    public class NewItemPageViewModel : BaseViewModel
    {
        public NewItemPageViewModel()
        {

        }
        public Livro Livro = new Livro();
        //public ImageSource _SourceFoto;
        public ImageSource Sourcefoto; //{ get => _SourceFoto; set { _SourceFoto = value; OnPropertyChanged(); } }

        public void Save(int livroid, string livrotitulo, string livrocurso, string livrosemsetre)//byte[] livrofoto)
        {
            Livro.Id = livroid;
            Livro.Titulo = livrotitulo;
            Livro.Curso = livrocurso;
            Livro.Semestre = livrosemsetre;
            //Livro.Foto = livrofoto;

            Database.Current.SaveItem<Livro>(Livro);

            MessagingCenter.Send(this, "LivroAdicionado");
        }

        #region Command_Camera
        private Command _Command_Camera;
        public Command Command_Camera => _Command_Camera ?? (_Command_Camera = new Command(async () => await ExecuteCommand_Camera()));

        //criação do metodo anonimo para a captura do evento click do botão
        async Task ExecuteCommand_Camera()
        {
            try
            {
                await CrossMedia.Current.Initialize();

                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    await App.Current.MainPage.DisplayAlert("Não Existe Camêra", "A camêra não está disponivel", "Ok");
                    return;
                }

                var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    Directory = FileSystem.Current.LocalStorage.Name,
                });

                if (file == null)
                {
                    return;
                }

                var stream = file.GetStream();
                var memoryStream = new MemoryStream();
                stream.CopyTo(memoryStream);

                Livro.Foto = File.ReadAllBytes(file.Path);

                Sourcefoto = ImageSource.FromStream(() =>
                {
                    var s = file.GetStream();
                    file.Dispose();
                    return s;
                });

                MessagingCenter.Send(this, "CapaAdd");

            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Erro", ex.Message, "OK");
            }
        }
        #endregion

        #region Command_Album
        private Command _Command_Album;
        public Command Command_Album => _Command_Album ?? (_Command_Album = new Command(async () => await ExecuteCommand_Album()));

        //criação do metodo anonimo para a captura do evento click do botão
        async Task ExecuteCommand_Album()
        {
            try
            {
                await CrossMedia.Current.Initialize();

                if (!CrossMedia.Current.IsTakePhotoSupported)
                {
                    await App.Current.MainPage.DisplayAlert("Album não suportado", "Não existe permissão para acessar o álbum de fotos", "Ok");
                    return;
                }

                var file = await CrossMedia.Current.PickPhotoAsync();

                if (file == null)
                {
                    return;
                }

                var stream = file.GetStream();
                var memoryStream = new MemoryStream();
                stream.CopyTo(memoryStream);

                Livro.Foto = File.ReadAllBytes(file.Path);

                Sourcefoto = ImageSource.FromStream(() =>
                {
                    var s = file.GetStream();
                    file.Dispose();
                    return s;
                });

                MessagingCenter.Send(this, "CapaAdd");

            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Erro", ex.Message, "OK");
            }
        }
        #endregion
    }
}
