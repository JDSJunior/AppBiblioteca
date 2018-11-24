using AppBibliotecaUnopar.Infraestructure;
using System;

namespace AppBibliotecaUnopar.Models
{
    public class Livro : TaskDB
    {
        public string Titulo { get; set; }
        public string Curso { get; set; }
        public string Semestre { get; set; }
        public byte[] Foto { get; set; }

        public override bool Equals(object obj)
        {
            Livro livro = obj as Livro;

            if(livro == null)
            {
                return false;
            }

            return (Id.Equals(livro.Id));
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}