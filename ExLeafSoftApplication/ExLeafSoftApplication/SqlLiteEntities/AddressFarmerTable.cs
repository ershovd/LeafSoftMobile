
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExLeafSoftApplication.SqlLiteEntities
{
    public class AddressFarmerTable
    { 
        protected static SQLiteAsyncConnection database;

        public AddressFarmerTable(string dbPath)
        {
            if (database == null)
                database = new SQLiteAsyncConnection(dbPath);

            database.CreateTableAsync<AddressFarmerModel>().Wait();
        }

        public async void BacthInsert(List<AddressFarmerModel> farmerAddresslist)
        {
            int b = await database.InsertAllAsync(farmerAddresslist);
        }

        public Task<List<AddressFarmerModel>> GetItemsNotDoneAsync()
        {
            return database.QueryAsync<AddressFarmerModel>("SELECT * FROM [AddressFarmerTable] WHERE [Done] = 0");
        }

        public Task<AddressFarmerModel> GetItemAsync(int id,Guid farmerGuid)
        {
            return database.Table<AddressFarmerModel>().Where(i=> i.AddressFarmerID == id || i.FarmerGuid == farmerGuid).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(AddressFarmerModel item)
        {
            return database.InsertAsync(item);
        }

        public Task<int> UpdateItemAsync(AddressFarmerModel item)
        {
            string strSql = string.Format("update AddressFarmerModel set Address='{1}',CountryID={3},CityID={4} where FarmerGuid = '{0}' or AddressFarmerID = {2} ",
                item.FarmerGuid, item.Address, item.AddressFarmerID, item.CountryID, item.CityID);

            return database.ExecuteAsync(strSql);

        }

        public Task<int> DeleteItemAsync(AddressFarmerModel item)
        {
            return database.DeleteAsync(item);
        }


    }



}
