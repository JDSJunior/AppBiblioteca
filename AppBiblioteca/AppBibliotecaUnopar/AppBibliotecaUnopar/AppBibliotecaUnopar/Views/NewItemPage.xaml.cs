using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using AppBibliotecaUnopar.Models;
using AppBibliotecaUnopar.Infraestructure;
using AppBibliotecaUnopar.ViewModels;
using System.Collections.ObjectModel;
using Plugin.Media;
using System.IO;
using PCLStorage;

namespace AppBibliotecaUnopar.Views
{
    public partial class NewItemPage : ContentPage
    {
        private NewItemPageViewModel viewModel;
        private Livro Livro = new Livro();

        public NewItemPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new NewItemPageViewModel();

            Clean_Fields();

            Att_Foto();

        }

        public NewItemPage(Livro livro)
        {
            InitializeComponent();

            BindingContext = viewModel = new NewItemPageViewModel();

            Livro = livro;

            Complete_Fields(Livro.Titulo, Livro.Curso, Livro.Semestre, Livro.Foto);

            Att_Foto();
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (TituloLivro.Text.Trim() == string.Empty || CursoLivro.Text.Trim() == string.Empty || SemestreLivro.Text.Trim() == string.Empty)
                {
                    await this.DisplayAlert("Erro", "Você precisa preencher todos os Campos", "OK");
                }
                else
                {
                    viewModel.Save(Livro.Id, TituloLivro.Text, CursoLivro.Text, SemestreLivro.Text);

                    await this.DisplayAlert("Confirmação.", "O livro " + TituloLivro.Text.ToUpper() + " foi salvo!", "OK");

                    Clean_Fields();

                    await Navigation.PopModalAsync();
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "Ok");
            }
        }

        async void Cancelar_Clicked(object sender, EventArgs e)
        {
            Clean_Fields();

            await Navigation.PopModalAsync();
        }
        
        private void Att_Foto()
        {
            MessagingCenter.Subscribe<NewItemPageViewModel>(this, "CapaAdd", (sender) =>
            {
                CapaLivro.Source = sender.Sourcefoto;

                MessagingCenter.Unsubscribe<NewItemPage>(this, "CapaAdd");
            });
        }

        private void Complete_Fields(string titulo, string curso, string semestre, byte[] foto)
        {
            TituloLivro.Text = titulo;
            CursoLivro.Text = curso;
            SemestreLivro.Text = semestre;
            CapaLivro.Source = ImageSource.FromStream(() => new MemoryStream(Livro.Foto));
        }

        private void Clean_Fields()
        {
            TituloLivro.Text = string.Empty;
            CursoLivro.Text = string.Empty;
            SemestreLivro.Text = string.Empty;
            CapaLivro.Source = null;
        }
    }
}