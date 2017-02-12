using System;
using MobileBookStore.DataAccess;
using System.IO;
using MobileBookStore.iOS;

[assembly: Xamarin.Forms.Dependency(typeof(DataConnection))]
namespace MobileBookStore.iOS
{
    public class DataConnection : IDataConnection
    {
        public string DatabasePath
        {
            get
            {
                var dbName = "books.db3";
                string personalFolder =
                  System.Environment.
                  GetFolderPath(Environment.SpecialFolder.Personal);
                string libraryFolder =
                  Path.Combine(personalFolder, "..", "Library");
                var path = Path.Combine(libraryFolder, dbName);

                return path;
            }
        }
    }
}