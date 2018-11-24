using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using AppBibliotecaUnopar.Models;
using AppBibliotecaUnopar.Views;
using AppBibliotecaUnopar.ViewModels;
using AppBibliotecaUnopar.Infraestructure;
using System.Collections.ObjectModel;

namespace AppBibliotecaUnopar.Views
{
    public partial class ItemsPage : ContentPage
    {
        public ItemsViewModel viewModel;

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ItemsViewModel();
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            ItemsListView.BeginRefresh();

            ItemsListView.ItemsSource = viewModel.Livros.Where(i => i.Titulo.Contains(e.NewTextValue));

            ItemsListView.EndRefresh();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var livro = args.SelectedItem as Livro;
            if (livro == null)
                return;

            await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(livro)));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
        }

        public async void OnAlterarClick(object sender, EventArgs args)
        {
            var mi = ((MenuItem)sender);
            var item = mi.CommandParameter as Livro;
            await Navigation.PushModalAsync(new NavigationPage(new NewItemPage(item)));
            //await Navigation.PushModalAsync(new NewItemPage(item));
        }

        public async void OnRemoverClick(object sender, EventArgs args)
        {
            var mi = ((MenuItem)sender);
            var item = mi.CommandParameter as Livro;
            var opcao = await DisplayAlert("Confirmação de exclusão", "Deseja exluir " + item.Titulo + " ?", "Sim", "Cancelar");

            if (opcao)
            {
                Database.Current.DeleteById<Livro>(item.Id);
            }

            ItemsListView.ItemsSource = Database.Current.GetAll<Livro>();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Livros.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);

            //ItemsListView.ItemsSource = Database.Current.GetAll<Livro>();
        }
    }
}