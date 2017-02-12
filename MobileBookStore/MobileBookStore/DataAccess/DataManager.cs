using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using MobileBookStore.Model;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobileBookStore.DataAccess
{
    public class DataManager
    {

        public MobileServiceClient Client { get; set; }

        public MobileServiceSQLiteStore Storage { get; set; }

        public DataManager(string serviceUrl)
        {
            Client = new MobileServiceClient(serviceUrl);

            var dep = DependencyService.Get<IDataConnection>();
            this.Storage = new MobileServiceSQLiteStore(dep.DatabasePath);
            this.Storage.DefineTable<Book>();
            this.Client.SyncContext.InitializeAsync(this.Storage, null);
        }

        public async Task SaveAsync<T>(T item) where T: TableBase
        {
            var table = this.Client.GetSyncTable<T>();

            if (item.Id == null)
            {
                item.Id = Guid.NewGuid().ToString("N");
                await table.InsertAsync(item);
            }
            else
            {
                await table.UpdateAsync(item);
            }
            var canSync = await CanConnectAsync();
            try
            {
                if (canSync == true) await this.Client.SyncContext.PushAsync();
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine(@"Invalid sync operation: {0}", msioe.Message);
            }
            catch (MobileServicePushFailedException ex)
            {
                foreach (var err in ex.PushResult.Errors)
                {
                    Debug.WriteLine(err.TableName);
                    Debug.WriteLine(err.Item);
                    Debug.WriteLine(err.RawResult);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"Sync error: {0}", e.Message);
            }
        }

        public async Task SaveAllAsync<T>(IEnumerable<T> items) where T: TableBase
        {
            var table = this.Client.GetSyncTable<T>();

            foreach(T item in items)
            {
                if (item.Id == null)
                {
                    item.Id = Guid.NewGuid().ToString("N");
                    await table.InsertAsync(item);
                }
                else
                {
                    await table.UpdateAsync(item);
                }
            }
            var canSync = await CanConnectAsync();
            try
            {
                if (canSync == true) await this.Client.SyncContext.PushAsync();
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine(@"Invalid sync operation: {0}", msioe.Message);
            }
            catch (MobileServicePushFailedException ex)
            {
                foreach (var err in ex.PushResult.Errors)
                {
                    Debug.WriteLine(err.TableName);
                    Debug.WriteLine(err.Item);
                    Debug.WriteLine(err.RawResult);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"Sync error: {0}", e.Message);
            }
        }

        public async Task<IEnumerable<T>> LoadAsync<T>(string userId) where T: TableBase
        {
            var table = this.Client.GetSyncTable<T>();

            var canSync = await CanConnectAsync();
            try
            {
                if (canSync == true) await table.PullAsync("items", table.CreateQuery());
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine(@"Invalid sync operation: {0}", msioe.Message);
            }
            catch (MobileServicePushFailedException ex)
            {
                foreach (var err in ex.PushResult.Errors)
                {
                    Debug.WriteLine(err.TableName);
                    Debug.WriteLine(err.Item);
                    Debug.WriteLine(err.RawResult);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"Sync error: {0}", e.Message);
            }
            var query = await table.Where(t=>t.UserId==userId).ToEnumerableAsync();

            return query;
        }

        private async Task<bool> CanConnectAsync()
        {
            return (CrossConnectivity.Current.IsConnected && await CrossConnectivity.Current.IsRemoteReachable(Constants.BaseUrl));
        }
    }
}
