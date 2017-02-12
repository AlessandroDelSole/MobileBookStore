using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.ComponentModel;

namespace MobileBookStore.Model
{
    public class Book: TableBase
    {
        private string title;
        [JsonProperty("title")]
        public string Title
        {
            get
            {
                return title;
            }

            set
            {
                title = value; OnPropertyChanged();
            }
        }

        private string author;
        [JsonProperty("author")]
        public string Author
        {
            get
            {
                return author;
            }

            set
            {
                author = value; OnPropertyChanged();
            }
        }

        private string iSBN;
        [JsonProperty("isbn")]
        public string ISBN
        {
            get
            {
                return iSBN;
            }

            set
            {
                iSBN = value; OnPropertyChanged();
            }
        }

        private DateTimeOffset publicationDate;
        [JsonProperty("publicationDate")]
        public DateTimeOffset PublicationDate
        {
            get
            {
                return publicationDate;
            }

            set
            {
                publicationDate = value; OnPropertyChanged();
            }
        }

        public Book()
        {
            this.PublicationDate = DateTimeOffset.Now;
        }
    }
}
