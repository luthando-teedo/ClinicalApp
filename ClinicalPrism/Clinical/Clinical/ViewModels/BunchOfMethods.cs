﻿using Clinical.Services.Interface;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Clinical.ViewModels
{
    public class BunchOfMethods : IDatabase
    {
        readonly SQLiteAsyncConnection database;

        public BunchOfMethods()
        {
            var dbPath = GetDbPath();

            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<ClientDetails>().Wait();
        }

        private string GetDbPath()
        {
            return Path.Combine
                        (Environment.GetFolderPath
                        (Environment.SpecialFolder.LocalApplicationData), "PatientInforSQLite.db3");

        }



        public Task<int> SaveItemAsync(ClientDetails clientDetails)
        {
            if (clientDetails.ID != 0)
            {
                return database.UpdateAsync(clientDetails);
            }
            else
            {
                return database.InsertAsync(clientDetails);
            }
        }

        public async Task<ClientDetails> GetClientDetailsByUserName(string userName)
        {
            return await database.Table<ClientDetails>().Where(x => x.UserName == userName).FirstOrDefaultAsync();
        }

        public Task<List<ClientDetails>> GetItemsAsync()
        {
            return database.Table<ClientDetails>().ToListAsync();
        }

        public Task<int> DeleteItemAsync(ClientDetails item)
        {
            return database.DeleteAsync(item);
        }

        internal IList<ClientDetails> DeleteItemAsync(int stuffs)
        {
            throw new NotImplementedException();
        }
    }
}