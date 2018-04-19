using ExLeafSoftApplication.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExLeafSoftApplication.SqlLiteEntities
{


    public class OrderDetailTable
    {
        protected static SQLiteAsyncConnection database;

        public OrderDetailTable(string dbPath)
        {
            if (database == null)
                database = new SQLiteAsyncConnection(dbPath);

            database.CreateTableAsync<OrderDetailModel>().Wait();
        }

        public Task<List<OrderDetailModel>> GetItemsNotDoneAsync()
        {
            return database.QueryAsync<OrderDetailModel>("SELECT * FROM [OrderDetailModel] WHERE [Done] = 0");
        }

        public Task<OrderDetailModel> GetItemAsync(int id)
        {
            return database.Table<OrderDetailModel>().Where(i => i.Ocad_CropID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveOrderDetail(OrderDetailModel order)
        {
            return database.InsertAsync(order);
        }



    }



    public class OrderTable
    {
        protected static SQLiteAsyncConnection database;

        public OrderTable(string dbPath)
        {
            if (database == null)
                database = new SQLiteAsyncConnection(dbPath);

            database.CreateTableAsync<OrderModel>().Wait();
        }

        public Task<List<CompactOrderModel>> GetAllOrdersAsync()
        {
            return database.QueryAsync<CompactOrderModel>("select F.FieldName as OrdeFieldName,O.Orde_StatusId as OrdeStatus,O.Orde_CreationDate as OrdeCreationDate " + 
                                            " from OrderModel as O inner join " +
                                " FieldModel as F on O.Orde_FieldGuid = F.FieldGuid");
        }

        public Task<List<CompactOrderModel>> GetOrdersAsync(string searchKey)
        {
            return database.QueryAsync<CompactOrderModel>(string.Format("select F.FieldName as OrdeFieldName,O.Orde_StatusId as OrdeStatus,O.Orde_CreationDate as OrdeCreationDate " +
                                            " from OrderModel as O inner join " +
                                " FieldModel as F on O.Orde_FieldGuid = F.FieldGuid where F.FieldName like '%{0}%'",searchKey));
        }

        public Task<int> SaveOrder(OrderModel order)
        {
            return database.InsertAsync(order);
        }

      

        

    }



}
