using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using AppBibliotecaUnopar.Models;
using AppBibliotecaUnopar.ViewModels;
using System.IO;

namespace AppBibliotecaUnopar.Views
{
	public partial class ItemDetailPage : ContentPage
	{
        ItemDetailViewModel viewModel;

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;

            CapaLivro.Source = ImageSource.FromStream(() => new MemoryStream(viewModel.Livro.Foto));

        }

        public ItemDetailPage()
        {
            InitializeComponent();

            var livro = new Livro
            {
                Titulo = viewModel.Livro.Titulo,
                Curso = viewModel.Livro.Curso,
                Semestre = viewModel.Livro.Semestre,
            };

            viewModel = new ItemDetailViewModel(livro);
            BindingContext = viewModel;
        }
    }
}