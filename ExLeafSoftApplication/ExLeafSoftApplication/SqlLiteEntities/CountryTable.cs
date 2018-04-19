using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExLeafSoftApplication.SqlLiteEntities
{
    public class CountryTable 
    {
        public static List<CountryModel> CountryList = new List<CountryModel>();
        protected static SQLiteAsyncConnection database;

        public CountryTable(string dbPath)
        {
            if (database == null)
                database = new SQLiteAsyncConnection(dbPath);

            database.CreateTableAsync<CountryModel>().Wait();
        }

      
        public Task<List<CountryModel>> GetCompactCountrylist()
        {
            return database.QueryAsync<CountryModel>("select CountryID,Coun_Name from CountryModel where Coun_IsActive = 1");
        }


        
       

    }



}
