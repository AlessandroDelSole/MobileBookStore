using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MobileBookStore.Model
{
    public abstract class TableBase: INotifyPropertyChanged
    {
        private string id;
        [JsonProperty("id")]
        public string Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value; OnPropertyChanged();
            }
        }

        private string userId;
        [JsonProperty("userId")]
        public string UserId
        {
            get
            {
                return userId;
            }

            set
            {
                userId = value; OnPropertyChanged();
            }
        }

        private DateTimeOffset createdAt;
        [JsonProperty("createdAt")]
        public DateTimeOffset CreatedAt
        {
            get
            {
                return createdAt;
            }

            set
            {
                createdAt = value; OnPropertyChanged();
            }
        }

        private DateTimeOffset updatedAt;
        [JsonProperty("updatedAt")]
        public DateTimeOffset UpdatedAt
        {
            get
            {
                return updatedAt;
            }

            set
            {
                updatedAt = value; OnPropertyChanged();
            }
        }

        private byte[] version;
        [JsonProperty("version")]
        public byte[] Version
        {
            get
            {
                return version;
            }

            set
            {
                version = value; OnPropertyChanged();
            }
        }

        private bool deleted;
        [JsonProperty("deleted")]
        public bool Deleted
        {
            get
            {
                return deleted;
            }

            set
            {
                deleted = value; OnPropertyChanged();
            }
        }

        public TableBase()
        {
            this.CreatedAt = DateTimeOffset.Now;
            this.UpdatedAt = DateTimeOffset.Now;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
