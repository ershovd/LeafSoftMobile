using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExLeafSoftApplication.SqlLiteEntities
{
    public class CountryCityTable
    {
        protected static SQLiteAsyncConnection database;

        public CountryCityTable(string dbPath)
        {
            if (database == null)
                database = new SQLiteAsyncConnection(dbPath);

            database.CreateTableAsync<CountryCityModel>().Wait();
        }

        public Task<List<CountryCityModel>> GetCityList(int countryID)
        {
            Task<List<CountryCityModel>> citylist =  database.Table<CountryCityModel>().Where(k => k.CountryID == countryID).ToListAsync();

            return citylist;
        }



    }



}
