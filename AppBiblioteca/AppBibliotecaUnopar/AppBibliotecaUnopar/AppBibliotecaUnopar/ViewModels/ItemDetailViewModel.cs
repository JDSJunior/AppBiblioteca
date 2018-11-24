using System;

using AppBibliotecaUnopar.Models;

namespace AppBibliotecaUnopar.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Livro Livro { get; set; }
        public ItemDetailViewModel(Livro livro = null)
        {
            Title = livro?.Titulo;
            Livro = livro;
        }
    }
}
