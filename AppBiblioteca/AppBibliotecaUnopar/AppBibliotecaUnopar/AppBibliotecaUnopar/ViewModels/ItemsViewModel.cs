using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using AppBibliotecaUnopar.Models;
using AppBibliotecaUnopar.Views;
using AppBibliotecaUnopar.Infraestructure;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AppBibliotecaUnopar.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private ObservableCollection<Livro> _Livros = null;
        public ObservableCollection<Livro> Livros
        {
            get => _Livros;
            set
            {
                _Livros = value;
                OnPropertyChanged();
            }
        }

        public ItemsViewModel() : base()
        {
            Livros = new ObservableCollection<Livro>(Database.Current.GetAll<Livro>());

            MessagingCenter.Subscribe<NewItemPageViewModel>(this, "LivroAdicionado", (sender) =>
            {
                this.Livros.Add(sender.Livro); //new ObservableCollection<Livro>(Database.Current.GetAll<Livro>());

                MessagingCenter.Unsubscribe<ItemsViewModel>(this, "LivroAdicionado");
            });
        }

        #region LoadItensCommand 
        private Command _LoadItemsCommand;
        public Command LoadItemsCommand => _LoadItemsCommand ?? (_LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand()));
        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Livros.Clear();
                var items = new ObservableCollection<Livro>(Database.Current.GetAll<Livro>());
                foreach (var item in items)
                {
                    Livros.Add(item);
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Erro", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
        #endregion
    }
}