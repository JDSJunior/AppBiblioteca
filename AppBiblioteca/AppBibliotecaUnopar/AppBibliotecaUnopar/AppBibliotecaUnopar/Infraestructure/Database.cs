using AppBibliotecaUnopar.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;

namespace AppBibliotecaUnopar.Infraestructure
{
    public class Database
    {
        private static Lazy<Database> _Lazy = new Lazy<Database>(() => new Database());
        public static Database Current { get => _Lazy.Value; }
        private readonly SQLiteConnection Connection;
        public static string Root;

        public Database()
        {
            var local = "AppBibliotecaUnoparDB.db3";
            local = Path.Combine(Root, local);
            Connection = new SQLiteConnection(local);
            Connection.CreateTable<Livro>();
        }

        public T GetItemById<T>(long id) where T : IRule, new()
        {
            var query = Connection.Table<T>().FirstOrDefault(c => c.Id == id);
            return query;
        }

        public IEnumerable<T> GetAll<T>() where T : IRule, new()
        {
            return (from i in Connection.Table<T>()
                    select i);
        }

        public int SaveItem<T>(T item) where T : IRule
        {
            if(item.Id != 0)
            {
                Connection.Update(item);
                return item.Id;
            }
            return Connection.Insert(item);
        }

        public int DeleteById<T>(int id) where T : IRule, new()
        {
            return Connection.Delete<T>(id);
        }

        public void Update<T>(T item) where T : IRule
        {
            Connection.Update(item);
        }
    }
}
