using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using MobileBookStore.DataAccess;
using MobileBookStore.Droid;

[assembly:Dependency(typeof(DataConnection))]
namespace MobileBookStore.Droid
{
    public class DataConnection: IDataConnection
    {
        public string DatabasePath
        {
            get
            {
                return System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "books.db3");
            }
        }
    }
}