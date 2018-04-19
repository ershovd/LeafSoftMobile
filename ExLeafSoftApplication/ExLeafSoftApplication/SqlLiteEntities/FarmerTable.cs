using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExLeafSoftApplication.SqlLiteEntities
{
    public class FarmerTable 
    {
        protected static SQLiteAsyncConnection database;

        public FarmerTable(string dbPath)
        {
            if(database == null)
                database = new SQLiteAsyncConnection(dbPath);

            database.CreateTableAsync<FarmerModel>().Wait();
        }

        //public List<AddressFarmerModel> farmerAddresslist { get; set; }

        public async void BatchInsert(List<ServerCustomerModel> customers)
        {
            //List<FarmerModel> farmers = new List<FarmerModel>();
            //farmerAddresslist = new List<AddressFarmerModel>();

            string strFarmerUpdate = string.Empty;
            string strAddressUpdate = string.Empty;
            string strFarmerInsert = string.Empty;
            string strAddressInsert = string.Empty;

            foreach (ServerCustomerModel item in customers)
            {
                if (item.OperationType == "Create")
                {
                    strFarmerInsert = string.Format("Insert into [FarmerModel] (GuidId,RowHash,Email,CreateDate,BirthDate," +
                        "FirstName,LastName,IsUpdated,Phone) " +
                        " values('{0}',{1},'{2}','{3}','{4}','{5}','{6}',{7},'{8}');", 
                        item.User_UniqueId.ToString(), item.RowHash, item.User_Email,item.CreateDate,item.User_BirthDate,
                        item.User_FirstName,item.User_LastName,0,item.User_PhoneNumber);

                    await database.ExecuteAsync(strFarmerInsert);

                    strAddressInsert = string.Format("insert into [AddressFarmerModel] (Address,CityID,CountryID,FarmerGuid) " +
                        " values('{0}',{1},{2},'{3}');",item.Address_Text,item.City_ID,item.Country_ID,item.User_UniqueId);

                   
                    await database.ExecuteAsync(strAddressInsert);
                }
                else
                {
                    strFarmerUpdate = string.Format("update Farmermodel set FirstName='{1}',RowHash={2},LastName='{3}'," +
                        "Email='{4}',CreateDate='{5}',BirthDate='{6}',Phone={7} where GuidId ='{0}';",
                        item.User_UniqueId, item.User_FirstName, item.RowHash, item.User_LastName,
                        item.User_Email, item.CreateDate, item.User_BirthDate, item.User_PhoneNumber);
                    await database.ExecuteAsync(strFarmerUpdate);
                  
                    strAddressUpdate = string.Format("update AddressFarmermodel set CountryId={1},CityId={2},Address='{3}' where FarmerGuid = '{0}';",
                        item.User_UniqueId,item.Country_ID,item.City_ID,item.Address_Text);
                    await database.ExecuteAsync(strAddressUpdate);
                }


               
            }

            
           


        }

        public Task<List<CustomerModel>> GetNewlyInsertedAsync()
        {
            return database.QueryAsync<CustomerModel>(@"SELECT A.CountryID as Country_ID ,F.FirstName as User_FirstName,F.BirthDate as User_BirthDate,
                                                        A.Address as Address_Text, A.CityID as City_ID,
                                                        """" as User_Password,"""" as User_LastName, """" as User_LoginName, F.FarmerId as User_ID,
                                                        F.GuidId as User_UniqueId, F.Email as User_Email,F.Phone as User_PhoneNumber,F.LastName as User_LastName
                                                        FROM [FarmerModel] as F inner join 
                                                        [AddressFarmerModel] as A on F.FarmerId = A.AddressFarmerID WHERE F.GuidId is not null and F.RowHash=0");
        }


        public Task<List<CustomerModel>> GetUpdatedAsync()
        {
            return database.QueryAsync<CustomerModel>(@"SELECT A.CountryID as Country_ID ,F.FirstName as User_FirstName,F.BirthDate as User_BirthDate,
                                                        A.Address as Address_Text, A.CityID as City_ID,
                                                        """" as User_Password, F.FirstName as User_LoginName, F.FarmerId as User_ID,
                                                        F.GuidId as User_UniqueId, F.Email as User_Email,F.Phone as User_PhoneNumber,F.LastName as User_LastName
                                                        FROM [FarmerModel] as F inner join 
                                                        [AddressFarmerModel] as A on F.GuidId = A.FarmerGuid WHERE F.IsUpdated = 1");
        }

        public Task<List<HashModel>> GetFarmersRowHashAsync()
        {
            return database.QueryAsync<HashModel>("SELECT GuidId as UniqueIdentifier ,RowHash FROM [FarmerModel] where GuidId is not null and RowHash is not null");
        }

        public Task<List<ValidateEmail>> CheckEmail(string email,string guid,int farmerid)
        {
            string strSQL = string.Empty;
            if(farmerid > 0 || (guid != null && guid != "00000000-0000-0000-0000-000000000000"))
            strSQL = string.Format("Select count(FarmerId) as Count,'{3}' as OpType, 0 as AnotherRecordHas from [FarmerModel] " +
                "where Email = '{0}' and (GuidId='{1}' or FarmerId={2}) " +
                "Union select count(FarmerId) as Count,'{3}' as OpType, 1 as AnotherRecordHas from [FarmerModel] " +
                "where Email = '{0}' and(GuidId != '{1}' or FarmerId != {2})", email, guid, farmerid,"update");
            else
                strSQL = string.Format("Select count(FarmerId) as Count,'{1}' as OpType, -1 as AnotherRecordHas  from [FarmerModel] where Email = '{0}'", email,"insert");

            return database.QueryAsync<ValidateEmail>(strSQL);
        }


        public Task<FarmerModel> GetItemAsync(int id)
        {
            return database.Table<FarmerModel>().Where(i=> i.FarmerId == id).FirstOrDefaultAsync();
        }

        public Task<int> GetLastInserted()
        {
            return database.ExecuteScalarAsync<int>("select seq from sqlite_sequence where name='FarmerModel'");
        }

        public Task<int> SaveItemAsync(FarmerModel item)
        {
            return database.InsertAsync(item);
        }

        public Task<int> UpdateItemAsync(FarmerModel item)
        {

            return database.UpdateAsync(item);

        }

        public async void BatchUpdateInsertedAsync(List<CompactCustomerModel> items,bool OperationType = true)
        {
            string strSql = string.Empty;
            if (OperationType == true)
            {
                string updateAddress = string.Empty;
                string updateField = string.Empty;

                foreach (var item in items)
                {

                    strSql = string.Format("Update [FarmerModel] set RowHash = {1},IsUpdated = 0  " +
                        " where Email = '{2}'; ", item.User_UniqueId, item.Frmr_RowHash, item.User_Email);
                    int a = await database.ExecuteAsync(strSql);

                    //updateAddress = string.Format("Update [AddressFarmerModel] set FarmerGuid = '{0}'" + 
                    //            " where AddressFarmerId = (select FarmerId from[FarmerModel] " +
                    //            " where Email = '{1}');", item.User_UniqueId, item.User_Email);
                    //int b = await database.ExecuteAsync(updateAddress);


                }
                
               // updateField = string.Format(@"update FieldModel 
               //     set FieldFarmerGuid = (select FarmerModel.GuidId
               //     from FarmerModel
               //     where FieldModel.FieldFarmerId = FarmerModel.FarmerId
               //     )
               //     where FieldFarmerGuid is null");

              
              
               //int c = await database.ExecuteAsync(updateField);
            }
            else
            {
                foreach (var item in items)
                {

                    strSql = string.Format("Update [FarmerModel] set RowHash = {0}," +
                        " IsUpdated = 0  where GuidId = '{1}'; ",item.Frmr_RowHash, item.User_UniqueId);
                    int c = await database.ExecuteAsync(strSql);

                }
                
            }

           
        }

        public Task<int> DeleteItemAsync(FarmerModel item)
        {
            return database.DeleteAsync(item);
        }

        public void TestTransaction()
        {
            //https://medium.com/@JasonWyatt/squeezing-performance-from-sqlite-insertions-971aff98eef2

             database.RunInTransactionAsync(async (arg) => await A(arg));
        }

        public Task<List<FarmerModel>> GetFarmersHasFieldsAsync()
        {
            string strSql = "select Fa.* from FarmerModel as Fa" +
                            " inner join FieldModel as Fi " +
                            "on Fa.FarmerID = Fi.FieldFarmerID ";

            return database.QueryAsync<FarmerModel>(strSql);
        }


        public Task<List<FarmerModel>> GetAllFarmersAsync()
        {

            return database.Table<FarmerModel>().OrderByDescending(k => k.FarmerId).ToListAsync();
        }

        async Task A(SQLiteConnection arg)
        {
           int a =  arg.Insert("select * from FarmerModel");
           await Task.Delay(1000);
        }

        public Task<List<FarmerModel>> GetFarmersAsync(string search)
        {
            return database.Table<FarmerModel>().Where(k => k.FirstName.Contains(search) || k.LastName.Contains(search)).ToListAsync();
        }

    }



}
