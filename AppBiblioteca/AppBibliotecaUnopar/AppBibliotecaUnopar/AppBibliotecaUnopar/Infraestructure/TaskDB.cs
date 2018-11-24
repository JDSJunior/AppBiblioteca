using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppBibliotecaUnopar.Infraestructure
{
    public class TaskDB : IRule
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
    }
}
