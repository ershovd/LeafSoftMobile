using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExLeafSoftApplication.SqlLiteEntities
{
    public class FieldTable
    {
        protected static SQLiteAsyncConnection database;

        public FieldTable(string dbPath)
        {
            if (database == null)
                database = new SQLiteAsyncConnection(dbPath);

            database.CreateTableAsync<FieldModel>().Wait();
        }



        public Task<List<LocalFieldModel>> GetNewlyInsertedAsync()
        {
            return database.QueryAsync<LocalFieldModel>(@"SELECT F.FieldName as Fiel_Name,
                    F.FieldFarmerGuid as Fiel_FarmerUniqueId,F.FieldGuid as Fiel_UniqueId,
                    F.FieldGps as Fiel_Gps,F.FieldAreaSize as Fiel_AreaSize,F.FieldUnit as Fiel_Unit
                                                        FROM [FieldModel] as F WHERE F.RowHash = 0 and F.FieldGuid is not null and F.FieldFarmerGuid is not null");
        }


        public Task<List<LocalFieldModel>> GetUpdatedAsync()
        {

            return database.QueryAsync<LocalFieldModel>(@"SELECT F.FieldName as Fiel_Name,
                    F.FieldFarmerGuid as Fiel_FarmerUniqueId,F.FieldGuid as Fiel_UniqueId,
                    F.FieldGps as Fiel_Gps,F.FieldAreaSize as Fiel_AreaSize,F.FieldUnit as Fiel_Unit
                                                        FROM [FieldModel] as F WHERE F.RowHash <> 0 and F.IsUpdated=1");
        }

        public Task<List<HashModel>> GetFieldsRowHashAsync()
        {
            return database.QueryAsync<HashModel>("SELECT FieldGuid as UniqueIdentifier,RowHash FROM [FieldModel] where RowHash <> 0");
        }

        public Task<FieldModel> GetFieldByFarmerIDAsync(int farmerid)
        {
            return database.Table<FieldModel>().Where(i => i.FieldFarmerId == farmerid).FirstOrDefaultAsync();
        }

        public Task<int> GetLastInserted()
        {
            return database.ExecuteScalarAsync<int>("select seq from sqlite_sequence where name='FieldModel'");
        }

        public Task<int> SaveItemAsync(FieldModel item)
        {
            return database.InsertAsync(item);
        }

        public Task<int> UpdateItemAsync(FieldModel item)
        {

            return database.UpdateAsync(item);

        }

        public Task<int> ChekFieldName(string FieldName, string FarmerGuid)
        {
            string sqlStr = string.Format("select count(FieldId) as count from FieldModel where FieldName='{0}' and FieldFarmerGuid='{1}'", FieldName, FarmerGuid);
            return database.ExecuteScalarAsync<int>(sqlStr);
        }

        public void BatchUpdateInsertedAsync(List<CompactFieldModel> items, bool OperationType = true)
        {
            string strSql = string.Empty;
            if (OperationType == true)
            {


                foreach (var item in items)
                {

                    strSql = string.Format("Update [FieldModel] set RowHash = {1},IsUpdated = 0  " +
                        " where FieldName = '{2}' and FieldFarmerGuid= '{3}';",
                        item.Fiel_UniqueId, item.Fiel_RowHash, item.Fiel_Name, item.Fiel_FarmerUniqueId);

                    database.ExecuteAsync(strSql);
                }


            }
            else
            {
                foreach (var item in items)
                {

                    strSql = string.Format("Update [FieldModel] set RowHash = {0}," +
                        " IsUpdated = 0  where FieldGuid = '{1}'; ", item.Fiel_RowHash, item.Fiel_UniqueId);

                    database.ExecuteAsync(strSql);

                }

            }


        }

        public Task<int> DeleteItemAsync(FieldModel item)
        {
            return database.DeleteAsync(item);
        }


        public Task<List<FieldModel>> GetItemsAsync(FarmerModel farmer)
        {
            return database.Table<FieldModel>().Where(k => k.FieldFarmerGuid == farmer.GuidId || k.FieldFarmerId == farmer.FarmerId)
                .OrderByDescending(k => k.FieldId).ToListAsync();
        }


        public async void BatchInsert(List<ServerFieldModel> fields)
        {
            //List<FarmerModel> farmers = new List<FarmerModel>();
            //farmerAddresslist = new List<AddressFarmerModel>();

            string strFieldUpdate = string.Empty;
            string strFieldInsert = string.Empty;


            foreach (ServerFieldModel item in fields)
            {
                if (item.OperationType == "Create")
                {
                    strFieldInsert += string.Format("Insert into [FieldModel] " +
                        "(FieldName,FieldAreaSize,FieldFarmerGuid,FieldGps,FieldUnit,IsUpdated,FieldGuid,RowHash" +
                        ") " +
                        " values('{0}',{1},'{2}','{3}','{4}','{5}','{6}',{7});",
                        item.Fiel_Name.ToString(), item.Fiel_AreaSize.ToString(), item.Fiel_FarmerUniqueId.ToString(),
                        item.Fiel_Gps.ToString(), item.Fiel_Unit.ToString(),
                        0, item.Fiel_UniqueId.ToString(), item.RowHash.ToString());

                }
                else
                {
                    strFieldUpdate += string.Format("update FieldModel set FieldName='{1}',RowHash={2},FieldAreaSize='{3}'," +
                        "FieldFarmerGuid='{4}',FieldGps='{5}',FieldUnit='{6}' where FieldGuid ='{0}';",
                        item.Fiel_UniqueId.ToString(), item.Fiel_Name.ToString(), item.RowHash.ToString(),
                        item.Fiel_AreaSize.ToString(), item.Fiel_FarmerUniqueId.ToString(),
                        item.Fiel_Gps.ToString(), item.Fiel_Unit.ToString()
                       );

                }



            }//end of  foreach (ServerFieldModel item in fields)

            if (!string.IsNullOrEmpty(strFieldInsert))
            {
                await database.ExecuteAsync(strFieldInsert);

            }

            if (!string.IsNullOrEmpty(strFieldUpdate))
            {
                await database.ExecuteAsync(strFieldUpdate);
            }



            //}



        }//end of  public async void BatchInsert(List<ServerFieldModel> fields)



    }
}
