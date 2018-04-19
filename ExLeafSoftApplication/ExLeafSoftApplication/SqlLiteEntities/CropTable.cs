using ExLeafSoftApplication.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExLeafSoftApplication.SqlLiteEntities
{
  
    public class CropTable
    {
        protected static SQLiteAsyncConnection database;

        public CropTable(string dbPath)
        {
            if (database == null)
                database = new SQLiteAsyncConnection(dbPath);

            database.CreateTableAsync<CropModel>().Wait();
        }

        public Task<List<CropModel>> GetItemsNotDoneAsync()
        {
            return database.QueryAsync<CropModel>("SELECT * FROM [CropModel] WHERE [Done] = 0");
        }

        public Task<CropModel> GetCropAsync(int id)
        {
            return database.Table<CropModel>().Where(i => i.Crop_ID == id).FirstOrDefaultAsync();
        }

        public Task<List<CropModel>> GetCrops()
        {
            return database.QueryAsync<CropModel>("Select * from [CropModel]");
        }


    
    }

   
}
